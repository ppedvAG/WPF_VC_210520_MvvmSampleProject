using System;
using System.Windows.Input;

namespace SharedMember
{
    public class CustomCommand : ICommand
    {
        //Delegates zum Speichern der Logik
        public Action<object> ExecuteMethode { get; set; }
        public Func<object, bool> CanExecuteMethode { get; set; }

        //Konstruktor
        public CustomCommand(Action<object> exe, Func<object, bool> can = null)
        {
            ExecuteMethode = exe;

            CanExecuteMethode = (can == null) ? (p => true) : can;
        }

        public CustomCommand()
        {
            ExecuteMethode = p => { };
            CanExecuteMethode = p => true;
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
            return CanExecuteMethode(parameter);
        }

        //Aktion bei Ausführung
        public void Execute(object parameter)
        {
            ExecuteMethode(parameter);
        }
    }
}

