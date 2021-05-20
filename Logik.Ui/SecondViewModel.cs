using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Ui
{
    public class SecondViewModel
    {
        public ICustomCommand OkCmd { get; set; }
        public ICustomCommand CancelCmd { get; set; }

        public SecondViewModel(ICustomCommand okCmd, ICustomCommand cancelCmd)
        {
            OkCmd = okCmd;
            CancelCmd = cancelCmd;

            OkCmd.ExecuteMethod = p =>
            {
                (p as IView).DialogResult = true;
                (p as IView).Close();
            };

            CancelCmd.ExecuteMethod = p =>
            {
                (p as IView).DialogResult = false;
                (p as IView).Close();
            };
        }

        public SecondViewModel()
        {

        }
    }
}
