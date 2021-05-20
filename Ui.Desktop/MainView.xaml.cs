using Logic.Ui;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ui.Desktop
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IView
    {
        public MainView()
        {
            InitializeComponent();

            //Ohne DI-System aus MVVM-Framework muss das ViewModel (wenn es Commands beinhaltet), aufgrund der Übergabeparameter im View-Konstruktor zugeordnet werden
            this.DataContext = new Logic.Ui.MainViewModel(this, new CustomCommand(), new CustomCommand(), new CustomCommand(), new CustomCommand(), new CustomCommand());
        }

        public List<Type> ChildViews { get; set; } = new List<Type>() { typeof(SecondView) };

        public int ShowMessageBox(string msg, string title, int buttons) => (int)MessageBox.Show(msg, title, (MessageBoxButton)buttons);
    }

}
