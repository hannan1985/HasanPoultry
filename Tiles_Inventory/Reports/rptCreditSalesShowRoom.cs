using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptCreditSales.
    /// </summary>
    public partial class rptCreditSalesShowRoom : DataDynamics.ActiveReports.ActiveReport
    {

        public rptCreditSalesShowRoom()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            // txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtTime.Text = DateTime.Now.ToString("h:mm tt");
        }

        private void detail_Format(object sender, EventArgs e)
        {

        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            decimal amount = 0;
        
            decimal totalDue = 0;
            decimal currentDue = 0;

            decimal.TryParse(txtTotal.Text, out amount);
            string strAmount = amount.ToString("F2");

            string numberInWord = NumberToWords(Convert.ToInt32(amount));
           // txtInWords.Text = char.ToUpper(amountWord[0]) + amountWord.Substring(1);
            if (strAmount.Contains("."))
            {
                int sAmount = Convert.ToInt32(strAmount.Substring(strAmount.LastIndexOf(".") + 1));
            }
            txtAmountInWord.Text = char.ToUpper(numberInWord[0]) + numberInWord.Substring(1)+" only.";


            decimal.TryParse(txtCurrentDue.Text, out currentDue);
            decimal.TryParse(txtTotalDue.Text, out totalDue);
          

        }


        public string NumberToWords(int number)
        {

            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                //if (words != "")
                //    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

    }
}
