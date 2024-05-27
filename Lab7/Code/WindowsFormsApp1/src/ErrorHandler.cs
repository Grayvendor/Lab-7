using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.src
{
    internal class ErrorHandler
    {
        List<string> errors = new List<string>();
        public void addError (string errorMessage)
        {
            errors.Add(errorMessage);
        }
        public void renderErrors(Label label)
        {
            foreach(string error in errors)
            {
                label.Text = label.Text + "\n" + error;
            }
        }
        public void clearErrors()
        {
            errors.Clear();
        }
        public int errorsCount()
        {
            return errors.Count();
        }
    }
}
