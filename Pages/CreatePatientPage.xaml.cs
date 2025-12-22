using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using pract7.Models;

namespace pract7.Pages
{
    public partial class CreatePatientPage :Page
    {
        Random rnd = new Random();

        public event Action PatientCreated;

        public CreatePatientPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LastNameBox.Text) ||
                string.IsNullOrEmpty(FirstNameBox.Text))
            {
                MessageBox.Show("Заполните фамилию и имя");
                return;
            }

            int patientId = rnd.Next(1000000, 9999999);

            var patient = new Patient
            {
                LastName = LastNameBox.Text,
                Name = FirstNameBox.Text,
                MiddleName = MiddleNameBox.Text,
                Birthday = BirthdayPicker.SelectedDate?.ToString("dd.MM.yyyy") ?? "",
                PhoneNumber = PhoneBox.Text
            };

            string json = JsonSerializer.Serialize(patient);
            File.WriteAllText($"P_{patientId}.json", json);

            MessageBox.Show($"Пациент создан. ID: {patientId}");

            PatientCreated?.Invoke();

            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}