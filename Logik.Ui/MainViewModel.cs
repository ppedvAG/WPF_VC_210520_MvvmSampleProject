using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using System.ComponentModel;
using Logic.Model;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.ObjectModel;

namespace Logic.Ui
{
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public IView View { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Controls-Props

        public string Wnd_MainView_Title { get; set; } = "MvvmSample";

        private string processString = "Prozess not started";
        public string Lbl_Process_Content
        {
            get => processString;
            set
            {
                processString = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Lbl_Length_Content));
            }
        }
        public int Lbl_Length_Content
        {
            get => processString.Length;
        }

        private int processValue = 0;
        public int Pgb_Process_Value
        {
            get => processValue;
            set
            {
                processValue = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Model-Props

        public string Person_Firstname
        {
            get { return Person.CurrentPerson.Firstname; }
            set { Person.CurrentPerson.Firstname = value; OnPropertyChanged(); }
        }

        public int Person_Age
        {
            get { return Person.CurrentPerson.Age; }
            set { Person.CurrentPerson.Age = value; OnPropertyChanged(); }
        }

        public Person CurrentPerson
        {
            get { return Person.CurrentPerson; }
            set { Person.CurrentPerson = value; OnPropertyChanged(); OnPropertyChanged(nameof(Person_Age)); OnPropertyChanged(nameof(Person_Firstname)); }
        }

        private ObservableCollection<Person> Persons
        {
            get { return Person.Persons; }
            set { Persons = value; OnPropertyChanged(); }
        }

        #endregion

        #region Command-Props

        public ICustomCommand StartCmd { get; set; }
        public ICustomCommand ResetCmd { get; set; }
        public ICustomCommand Open2ndViewCmd { get; set; }
        public ICustomCommand AddCmd { get; set; }
        public ICustomCommand DeleteCmd { get; set; }

        private bool canExe = true;
        public bool CanExeTimer { get => canExe; set { canExe = value; OnPropertyChanged(); } }

        #endregion

        public Timer timer { get; set; }

        #region Konstruktoren

        //Command-Objekte müssen dem Konstruktor übergeben werden, damit die Methoden angepasst werden können.
        //Alternative: DependencyInjection-Systeme o.ä. aus MVVM-Frameworks
        public MainViewModel(IView view, ICustomCommand startCmd, ICustomCommand resetCmd, ICustomCommand open2ndViewCmd, ICustomCommand addCmd, ICustomCommand deleteCmd)
        {
            View = view;
            StartCmd = startCmd;
            ResetCmd = resetCmd;
            Open2ndViewCmd = open2ndViewCmd;
            AddCmd = addCmd;
            DeleteCmd = deleteCmd;


            timer = new Timer()
            {
                Interval = 10
            };
            timer.Elapsed += (s, e) =>
                             {
                                 Pgb_Process_Value += 1;
                                 if (Pgb_Process_Value >= 100)
                                 {
                                     timer.Stop();
                                     Lbl_Process_Content = "Process finished";
                                 }
                             };

            StartCmd.ExecuteMethod = p =>
                   {
                       CanExeTimer = false;
                       Lbl_Process_Content = "Process on the run";
                       timer.Start();
                   };
            //StartCmd.CanExecuteMethod = p => Pgb_Process_Value == 0; <- funktioniert hier aufgrund des Timers nur eingeschränkt
            StartCmd.CanExecuteMethod = p => CanExeTimer;

            ResetCmd.ExecuteMethod = p =>
                   {
                       CanExeTimer = true;
                       timer.Stop();
                       Pgb_Process_Value = 0;
                       Lbl_Process_Content = "Prozess not started";
                   };
            //ResetCmd.CanExecuteMethod = p => Pgb_Process_Value > 0; <- funktioniert hier aufgrund des Timers nur eingeschränkt
            ResetCmd.CanExecuteMethod = p => !CanExeTimer;

            Open2ndViewCmd.ExecuteMethod = p =>
            {
                //Neue Fenster werden durch die ChildView-Liste der Views instanziert (vgl. IView)
                //Alternativ zur Übergabe des Views über den Commandparameter kann auch eine Property vom Typ IView verwendet werden, welche das View beinhaltet
                IView secondView = (IView)Activator.CreateInstance((p as IView).ChildViews[0]);
                //IView secondView = (IView)Activator.CreateInstance(View.ChildViews[0]);

                if (secondView.ShowDialog() == true)
                    Lbl_Process_Content = "YOU PRESSED OK";
                else
                    Lbl_Process_Content = "YOU PRESSED CANCEL";

            };

            AddCmd.ExecuteMethod = p =>
            {
                Persons.Add(
                    new Person()
                    {
                        Firstname = "NewPerson",
                        Age = 50
                    });
            };

            DeleteCmd.ExecuteMethod = p =>
            {
                if (p is Person)
                {
                    if (View.ShowMessageBox($"Should {(p as Person).Firstname} be deleted?", "Warning", 4) == 6)
                        Persons.Remove(p as Person);
                }
            };
            DeleteCmd.CanExecuteMethod = p => p is Person;

        }

        public MainViewModel()
        {

        }

        #endregion

        #region IDataErrorInfo

        public string Error => null;

        //Alternative: Reflection und Attribute an den Properties (vgl. ViewModelBase-Klasse)
        private Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();
        private void CollectErrors()
        {
            Errors.Clear();

            if (string.IsNullOrEmpty(Person_Firstname)) Errors.Add(nameof(Person_Firstname), "Firstname must not be empty!");
            else if (!Person_Firstname.All(c => Char.IsLetter(c))) Errors.Add(nameof(Person_Firstname), "Firstname must only contain letters!");

            if (Person_Age < 0) Errors.Add(nameof(Person_Age), "Age must not be smaller than 0");
            else if (Person_Age > 150) Errors.Add(nameof(Person_Age), "Age must not be greater than 150");

            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(HasNoErrors));
        }

        public string this[string columnName] { get { CollectErrors(); return Errors.ContainsKey(columnName) ? Errors[columnName] : string.Empty; } }

        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }

        public bool HasNoErrors
        {
            get { return Errors.Count == 0; }
        }

        #endregion

    }
}
