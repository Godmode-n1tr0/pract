using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using pract7.Models;

namespace pract7.Pages
{
    public partial class LoginPage :Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string doctorId = DoctorIdBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(doctorId) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните поля");
                return;
            }

            string filePath = $"D_{doctorId}.json";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Врач не найден");
                return;
            }

            string json = File.ReadAllText(filePath);
            Doctor doctor = JsonSerializer.Deserialize<Doctor>(json);

            if (doctor != null && doctor.Password == password)
            {
                NavigationService.Navigate(new MainPage(doctor));
            }
            else
            {
                MessageBox.Show("Неверный пароль");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}