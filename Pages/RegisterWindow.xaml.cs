using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using pract7.Models;

namespace pract7.Pages
{
    public partial class RegisterPage :Page
    {
        Random rnd = new Random();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LastNameBox.Text) ||
                string.IsNullOrEmpty(FirstNameBox.Text))
            {
                MessageBox.Show("Заполните фамилию и имя");
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            int doctorId = rnd.Next(10000, 99999);

            var doctor = new Doctor
            {
                Id = doctorId,
                LastName = LastNameBox.Text,
                Name = FirstNameBox.Text,
                MiddleName = MiddleNameBox.Text,
                Specialization = SpecBox.Text,
                Password = PasswordBox.Password
            };

            string json = JsonSerializer.Serialize(doctor);
            File.WriteAllText($"D_{doctorId}.json", json);

            MessageBox.Show($"Врач зарегистрирован! ID: {doctorId}");

            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}