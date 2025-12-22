using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using pract7.Models;

namespace pract7.Pages
{
    public partial class AppointmentPage :Page
    {
        private Doctor doctor;
        private Patient patient;

        public AppointmentPage(Doctor doctor, Patient patient)
        {
            InitializeComponent();
            this.doctor = doctor;
            this.patient = patient;

            PatientLabel.Content = $"{patient.LastName} {patient.Name}";
            HistoryList.ItemsSource = patient.Appointments;
        }

        private void SaveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DiagnosisBox.Text))
            {
                MessageBox.Show("Введите диагноз");
                return;
            }

            var appointment = new Appointment
            {
                Date = DateTime.Now.ToString("dd.MM.yyyy"),
                DoctorId = doctor.Id,
                Diagnosis = DiagnosisBox.Text,
                Recommendations = RecommendationsBox.Text
            };

            patient.Appointments.Add(appointment);

            string json = JsonSerializer.Serialize(patient);
            string patientId = FindPatientId();
            File.WriteAllText($"P_{patientId}.json", json);

            MessageBox.Show("Прием сохранен");

            DiagnosisBox.Text = "";
            RecommendationsBox.Text = "";
        }

        private string FindPatientId()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "P_*.json");
            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                Patient patientFromFile = JsonSerializer.Deserialize<Patient>(json);
                if (patientFromFile != null &&
                    patientFromFile.LastName == patient.LastName &&
                    patientFromFile.Name == patient.Name)
                {
                    return Path.GetFileNameWithoutExtension(file).Substring(2);
                }
            }
            return "";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}