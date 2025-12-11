using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace pract7
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Random rnd = new();

        private ObservableCollection<string> vrachFiles = new ObservableCollection<string>();
        private ObservableCollection<string> pacientFiles = new ObservableCollection<string>();

        private Pacient currentPacient = new Pacient();
        private string selectedVrachFile = "";
        private string vrachPassword = "";
        private string fioText = "";
        private string specText = "";
        private string selectedPacientFile = "";

        private int _vrachCount = 0;
        private int _pacientCount = 0;

        public ObservableCollection<string> VrachFiles
        {
            get => vrachFiles;
            set
            {
                vrachFiles = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> PacientFiles
        {
            get => pacientFiles;
            set
            {
                pacientFiles = value;
                OnPropertyChanged();
            }
        }

        public Pacient CurrentPacient
        {
            get => currentPacient;
            set
            {
                currentPacient = value;
                OnPropertyChanged();
            }
        }

        public string SelectedVrachFile
        {
            get => selectedVrachFile;
            set
            {
                selectedVrachFile = value;
                OnPropertyChanged();
                CheckVrach();
            }
        }

        public string VrachPassword
        {
            get => vrachPassword;
            set
            {
                vrachPassword = value;
                OnPropertyChanged();
                CheckVrach();
            }
        }

        public string FioText
        {
            get => fioText;
            set
            {
                fioText = value;
                OnPropertyChanged();
            }
        }

        public string SpecText
        {
            get => specText;
            set
            {
                specText = value;
                OnPropertyChanged();
            }
        }

        public string SelectedPacientFile
        {
            get => selectedPacientFile;
            set
            {
                selectedPacientFile = value;
                OnPropertyChanged();
                LoadSelectedPacient();
            }
        }

        public int VrachCount
        {
            get => _vrachCount;
            set
            {
                _vrachCount = value;
                OnPropertyChanged();
            }
        }

        public int PacientCount
        {
            get => _pacientCount;
            set
            {
                _pacientCount = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadJsonFileNames();
        }

        private void LoadJsonFileNames()
        {
            string folder = Directory.GetCurrentDirectory();
            string[] vjsonFiles = Directory.GetFiles(folder, "D_*.json");
            string[] pjsonFiles = Directory.GetFiles(folder, "P_*.json");

            VrachFiles.Clear();
            PacientFiles.Clear();

            foreach (string filePath in vjsonFiles)
            {
                VrachFiles.Add(Path.GetFileName(filePath));
            }

            foreach (string filePath in pjsonFiles)
            {
                PacientFiles.Add(Path.GetFileName(filePath));
            }

            VrachCount = vjsonFiles.Length;
            PacientCount = pjsonFiles.Length;
        }

        private void CheckVrach()
        {
            if (!string.IsNullOrEmpty(SelectedVrachFile) && !string.IsNullOrEmpty(VrachPassword))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), SelectedVrachFile);

                if (File.Exists(filePath))
                {
                    try
                    {
                        string json = File.ReadAllText(filePath);
                        Vrach v = JsonSerializer.Deserialize<Vrach>(json);

                        if (v != null && v.Password == VrachPassword)
                        {
                            FioText = $"{v.Lastname} {v.Name} {v.MiddleName}";
                            SpecText = v.Specialisation;
                        }
                        else
                        {
                            FioText = "";
                            SpecText = "";
                        }
                    }
                    catch
                    {
                        FioText = "";
                        SpecText = "";
                    }
                }
            }
            else
            {
                FioText = "";
                SpecText = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(f.Text) && !string.IsNullOrEmpty(i.Text) && !string.IsNullOrEmpty(o.Text) &&
                !string.IsNullOrEmpty(par.Text) && !string.IsNullOrEmpty(ppar.Text) && spec.SelectedIndex != -1)
            {
                bool areEqual = string.Equals(par.Text, ppar.Text, StringComparison.OrdinalIgnoreCase);
                if (areEqual == true)
                {
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
                    File.WriteAllText(filepath, jsonstring);

                    f.Text = "";
                    i.Text = "";
                    o.Text = "";
                    par.Text = "";
                    ppar.Text = "";
                    spec.SelectedIndex = -1;

                    LoadJsonFileNames();
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(f_pac.Text) && !string.IsNullOrEmpty(i_pac.Text) &&
                !string.IsNullOrEmpty(o_pac.Text) && !string.IsNullOrEmpty(date_pac.Text))
            {
                var pacient = new Pacient()
                {
                    Name = f_pac.Text,
                    LastName = i_pac.Text,
                    MiddleName = o_pac.Text,
                    Birthday = date_pac.Text,
                    LastAppointment = null,
                    LastDoctor = "",
                    Diagnosis = "",
                    Recomendations = "",
                };

                string jsonstring = JsonSerializer.Serialize(pacient, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                });

                string a = "P_" + Convert.ToString(rnd.Next(1000000, 9999999));
                string filepath = a + ".json";
                File.WriteAllText(filepath, jsonstring);

                f_pac.Text = "";
                i_pac.Text = "";
                o_pac.Text = "";
                date_pac.Text = "";

                LoadJsonFileNames();
            }
            else
            {
                MessageBox.Show("Поля не заполненны полностью, пожалуйста заполните до конца!!!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SavePacient();
        }

        private void SavePacient()
        {
            if (!string.IsNullOrEmpty(SelectedPacientFile))
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        WriteIndented = true
                    };

                    string json = JsonSerializer.Serialize(CurrentPacient, options);
                    File.WriteAllText(SelectedPacientFile, json);
                    MessageBox.Show("Сохранено");
                }
                catch
                {
                    MessageBox.Show("Ошибка сохранения");
                }
            }
        }

        private void LoadSelectedPacient()
        {
            if (!string.IsNullOrEmpty(SelectedPacientFile))
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), SelectedPacientFile);

                if (File.Exists(filepath))
                {
                    try
                    {
                        string json = File.ReadAllText(filepath);
                        Pacient p = JsonSerializer.Deserialize<Pacient>(json);
                        if (p != null)
                        {
                            CurrentPacient = p;
                        }
                    }
                    catch { }
                }
            }
        }

        private void ChangePatient_Click(object sender, RoutedEventArgs e)
        {
            SavePacient();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            CurrentPacient = new Pacient()
            {
                Name = "",
                LastName = "",
                MiddleName = "",
                Birthday = "",
                LastAppointment = null,
                LastDoctor = "",
                Diagnosis = "",
                Recomendations = ""
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}