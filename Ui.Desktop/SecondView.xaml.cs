using Logic.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for SecondView.xaml
    /// </summary>
    public partial class SecondView : Window, IView
    {
        public SecondView()
        {
            InitializeComponent();

            this.DataContext = new Logic.Ui.SecondViewModel(new CustomCommand(), new CustomCommand());
        }

        public List<Type> ChildViews { get; set; } = new List<Type>();

        public int ShowMessageBox(string msg, string title, int buttons) => throw new NotImplementedException();
        
    }
}
