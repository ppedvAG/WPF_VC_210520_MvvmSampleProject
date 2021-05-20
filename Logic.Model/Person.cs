using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Logic.Model
{
    public class Person : INotifyPropertyChanged
    {
        public static Person CurrentPerson { get; set; } = new Person();
        public static ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();

        static Person()
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                Persons.Add(
                    new Person()
                    {
                        Firstname = Guid.NewGuid().ToString("N").Substring(0, 10),
                        Age = random.Next(0, 100)
                    });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; OnPropertyChanged(); }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(); }
        }


    }
}
