using LibraryManagement.DAL.EF;
using LibraryManagement.Model;
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
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        ApplicationDbContext db = null!;
        Client Client { get; set; } = new Client();
        Library Library { get; set; } = null!;
        public AddClientWindow(ApplicationDbContext context, Library lib)
        {
            this.db = context;
            Library = lib;
            InitializeComponent();
        }
        private bool IsDataValid()
        {
            //if(DatePicker.Text > DateTime.Now)
              //  return false;
            return true;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if(IsDataValid())
            {
                Client.Name = Name_TextBox.Text;
                Client.Surname = Surname_TextBox.Text;
                Client.DateOfBirth = DateTime.Parse(DatePicker.Text);
                Library.Clients.Add(Client);
                db.Library.Update(Library);
                db.SaveChanges();
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
