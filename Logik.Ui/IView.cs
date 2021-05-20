using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Ui
{
    //IView gewährt dem ViewModel Zugriff auf Window-Funktionen und zwingt die Views zur Implementierung einer Referenzliste für Child-Views
    public interface IView
    {
        //In diesem System müssen alle Views die explizieten Datentypen folgender Views kennen, damit diese über das Interface im Logic.Ui-Part (ViewModel-Part) aufgerufen
        //werden können. Alternativ verwenden viele MVVM-Frameworks DI oder ein Messenger-System (ServiceBus) um das richtige Fenster zu identifizieren.
        public List<Type> ChildViews { get; set; }

        public void Close();
        public void Show();
        public bool? ShowDialog();

        public bool? DialogResult { get; set; }

        //MessageBoxen sind ebenfalls (wie die spezifischen View-Klassen) WPF(Desktop)-spezifisch und müssen UI-seitig definiert werden
        int ShowMessageBox(string msg, string title, int buttons);
    }
}
