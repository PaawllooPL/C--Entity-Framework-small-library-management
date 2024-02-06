using LibraryManagement.DAL.EF;
using LibraryManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryManagement.WpfApp
{
    /// <summary>
    /// Interaction logic for AddOrEditLibraryWindow.xaml
    /// </summary>
    public partial class AddOrEditLibraryWindow : Window
    {
        ApplicationDbContext db = null!;
        DataGrid dg = null!;
        ComboBox cb = null!;
        private Library Library = new();
        bool isLibraryNull;
        public AddOrEditLibraryWindow(ApplicationDbContext dbContext, DataGrid dataGrid, ComboBox cb,Library? lib = null)
        {
            InitializeComponent();
            this.db = dbContext;
            this.dg = dataGrid;
            this.cb = cb;
            if(lib != null)
            {
                Library = lib;
                Name_TextBox.Text = Library.Name;
                Address_TextBox.Text = Library.Address;
                isLibraryNull = false;
            }
            else
                isLibraryNull = true;
        }
        private bool IsDataValid()
        {   //future updates
            Library.Name = Name_TextBox.Text;
            Library.Address = Address_TextBox.Text;
                return true;
        }
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataValid()) 
            {
                if (isLibraryNull == true)
                {
                    Library = new Library()
                    {
                        Name = Name_TextBox.Text,
                        Address = Address_TextBox.Text
                    };
                    db.Library.Add(Library);
                    db.SaveChanges();
                    cb.Items.Add(Library.Id);

                }
                else
                {
                    db.Library.Update(Library);
                    db.SaveChanges();
                }
                DialogResult = true;
                return;
            }
            DialogResult = false;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
