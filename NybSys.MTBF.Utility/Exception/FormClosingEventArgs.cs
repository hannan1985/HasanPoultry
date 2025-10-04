using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NybSys.MTBF.Utility.Exception
{
    public class CustomFormClosingEventArgs : EventArgs
    {
        private string _formName = string.Empty;

        public CustomFormClosingEventArgs(string formName)
        {
            this._formName = formName;
        }

        public string FormName { get { return this._formName; } }
    }
}
