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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLogIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(TextBoxIme.Text))
            {
                MessageBox.Show("Ime");
                TextBoxIme.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextBoxLozinka.Text) || TextBoxLozinka.Text.Length<3)
            {
                MessageBox.Show("Lozinka");
                TextBoxLozinka.Focus();
                return false;
            }

            return true;
        }

        private void Check()
        {
            Korisnik k = new Korisnik
            {
                KorisnickoIme = TextBoxIme.Text,
                Lozinka = TextBoxLozinka.Text
            };

            int rez = KorisnikDal.Proveri(k);

            if (rez == 1)
            {
                Window1 w1 = new Window1();
                w1.TextBlock1.Text = "Uspesno logovanje";
                w1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pogresni podaci");
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (!Validacija())
            {
                return;
            }

            Check();
        }
    }
}
