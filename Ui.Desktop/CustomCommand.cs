using Logic.Ui;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Ui.Desktop
{
    //CustomCommand muss in WPF-Projekt sein, um Zugriff auf den CommandManager nehmen zu können
    public class CustomCommand : ICustomCommand // <- Interface in Logic.Ui gewährt Zugriff auf Methoden
    {
        //Delegates zum Speichern der Logik
        public Action<object> ExecuteMethod { get; set; }
        public Func<object, bool> CanExecuteMethod { get; set; }

        //Konstruktor
        public CustomCommand(Action<object> exe, Func<object, bool> can = null)
        {
            ExecuteMethod = exe;

            CanExecuteMethod = (can == null) ? (p => true) : can;
        }

        public CustomCommand()
        {
            ExecuteMethod = p => { };
            CanExecuteMethod = p => true;
        }

        //Anmeldung des Commands im CommandManager
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Bedingung für die Ausführung
        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod(parameter);
        }

        //Aktion bei Ausführung
        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}

