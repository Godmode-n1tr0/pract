using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pract7
{
    public partial class MainWindow : Window
    {
        Random rnd = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(f.Text) && !string.IsNullOrEmpty(i.Text) && !string.IsNullOrEmpty(o.Text) && !string.IsNullOrEmpty(par.Text) && !string.IsNullOrEmpty(ppar.Text) && spec.SelectedIndex != -1)
            {
                bool areEqual = string.Equals(par.Text, ppar.Text, StringComparison.OrdinalIgnoreCase);
                if (areEqual == true)
                {
                    string special = Convert.ToString(spec.Text);
                    var vrach = new Vrach()
                    {
                        Name = f.Text,
                        Lastname = i.Text,
                        MiddleName = o.Text,
                        Specialisation = spec.Text,
                        Password = par.Text,
                    };
                    string jsonstring = JsonSerializer.Serialize(vrach);
                    string a = "D_" + Convert.ToString(rnd.Next(10000, 99999));
                    string filepath = a + ".json";
                    using (StreamWriter writer = new StreamWriter(filepath))
                    {
                        writer.WriteAsync(jsonstring);
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            }    
            else
            {
                MessageBox.Show("Поля не заполненны полностью, пожалуйста заполните до конца!!!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(id_vrach.Text) && !string.IsNullOrEmpty(Pas_vhod.Text))
            {
                MessageBox.Show("Все заполненно");
            }
            else
            {
                MessageBox.Show("Вы не заполнили поле");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(f_pac.Text) && !string.IsNullOrEmpty(i_pac.Text) && !string.IsNullOrEmpty(o_pac.Text) && !string.IsNullOrEmpty(date_pac.Text))
            {
                MessageBox.Show("Вы все заполнили молодцы");
            }
            else
            {
                MessageBox.Show("Поля не заполненны полностью, пожалуйста заполните до конца!!!");
            }
        }
    }
}