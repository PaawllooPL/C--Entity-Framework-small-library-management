using LibraryManagement.DAL.EF;
using LibraryManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagement.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _dbContext;

        public MainWindow(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
            InitializeComponent();

        }
        private void SetGrid<T>(DataGrid dg, IEnumerable<T> list) where T : new()
        {
            dg.Columns.Clear();
            var properties = typeof(T);
            foreach (var property in properties.GetProperties())
            {
                if(property.GetCustomAttribute<HideAttribute>() == null)
                {
                    dg.Columns.Add(new DataGridTextColumn
                    {
                        Binding = new Binding(property.Name),
                        Header = property.Name
                    });
                }
            }
            dg.AutoGenerateColumns = false;
            dg.ItemsSource = list;
            dg.Items.Refresh();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetGrid(Libraries_DataGrid, _dbContext.Library.Include(x => x.Clients));
            SelectLibrary_ComboBox.Items.Clear();
            foreach(var item in _dbContext.Library)
            {
                SelectLibrary_ComboBox.Items.Add(item.Id.ToString());
            }
        }

        private void AddOrEditLibrary_Button_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditLibraryWindow window;
            if (Libraries_DataGrid.SelectedItem is Library lib)
                window = new AddOrEditLibraryWindow(_dbContext, Libraries_DataGrid, SelectLibrary_ComboBox, lib);
            else
                window = new AddOrEditLibraryWindow(_dbContext, Libraries_DataGrid, SelectLibrary_ComboBox);
            window.ShowDialog();

            if(window.DialogResult == true)
            {
                SetGrid(Libraries_DataGrid, _dbContext.Library
                    .Include(x => x.Clients));
            }
        }

        private void RemoveLibrary_Button_Click(object sender, RoutedEventArgs e)
        {
            if(Libraries_DataGrid.SelectedItem is Library lib)
            {
                _dbContext.Library.Remove(lib);
                _dbContext.SaveChanges();
                SetGrid(Libraries_DataGrid, _dbContext.Library.Include(x => x.Clients));
                SelectLibrary_ComboBox.Items.Remove(lib.Id);
            }

        }

        private void AddClientToLibrary_Button_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow window;
            if (int.TryParse(SelectLibrary_ComboBox.SelectedItem.ToString(), out int id))
            {
                window = new AddClientWindow(_dbContext, _dbContext.Library
                                                            .First(x => x.Id == id));
            }
            else
                return;
                
            window.ShowDialog();
            if(window.DialogResult == true)
            {
                SetGrid(Libraries_DataGrid, _dbContext.Library.Include(_x => _x.Clients));
            }
        }

        private void DatabaseReset_Button_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.RemoveRange(_dbContext.Library);
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Library', RESEED, 0)");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Clients', RESEED, 0)");
            _dbContext.SaveChanges();
            //int test = _dbContext.Clients.Count();
            SetGrid(Libraries_DataGrid, _dbContext.Library.Include(x => x.Clients));

        }
    }
}
