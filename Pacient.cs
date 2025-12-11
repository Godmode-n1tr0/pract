using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pract7
{
    public class Pacient : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _birthday;
        public string Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _lastAppointment;
        public DateTime? LastAppointment
        {
            get => _lastAppointment;
            set
            {
                if (_lastAppointment != value)
                {
                    _lastAppointment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastDoctor;
        public string LastDoctor
        {
            get => _lastDoctor;
            set
            {
                if (_lastDoctor != value)
                {
                    _lastDoctor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _diagnosis;
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                if (_diagnosis != value)
                {
                    _diagnosis = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _recomendations;
        public string Recomendations
        {
            get => _recomendations;
            set
            {
                if (_recomendations != value)
                {
                    _recomendations = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}