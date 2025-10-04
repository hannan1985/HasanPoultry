using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NybSys.MTBF.Utility.Helper
{
    public class MessageBoxHelper
    {
        /// <summary>
        /// Method to display information.
        /// </summary>
        /// <param name="message"></param>
        public static void ShowInformation(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Method to show error message.
        /// </summary>
        /// <param name="message"></param>
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Method to show warning
        /// </summary>
        /// <param name="message"></param>
        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Method to show confirmation
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult ShowConfirmation(string message)
        {
            return MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

    }
}
