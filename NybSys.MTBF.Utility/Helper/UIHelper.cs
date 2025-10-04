using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Constants;
using Infragistics.Win.UltraWinGrid;
using System.Data;
using System.Reflection;
using Infragistics.Win.UltraWinEditors;

namespace NybSys.MTBF.Utility.Helper
{
    public class UIHelper
    {
        /// <summary>
        /// Method to get cell value of a grid.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetGridViewCellValue(DataGridViewCell cell)
        {
            string result = String.Empty;

            if (cell.Value != null)
            {
                result = cell.Value.ToString();
            }

            return result;
        }

        /// <summary>
        /// Method to get cell value of a grid.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetGridViewCellValue(UltraGridCell cell)
        {
            string result = String.Empty;

            if (cell.Value != null)
            {
                result = cell.Value.ToString();
            }

            return result;
        }

        /// <summary>
        /// Method to get edited value of a grid view cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetGridViewCellEditedValue(DataGridViewCell cell)
        {
            string result = String.Empty;

            if (cell.EditedFormattedValue != null)
            {
                result = cell.EditedFormattedValue.ToString();
            }

            return result;
        }


        /// <summary>
        /// Method to get edited value of a ultra grid view cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetGridViewCellEditedValue(UltraGridCell cell)
        {
            string result = String.Empty;

            if (cell.Value != null)
            {
                result = cell.Value.ToString();
            }

            return result;
        }

        /// <summary>
        /// Method to mark a grid row as error.
        /// </summary>
        /// <param name="row"></param>
        public static void MarkDataGridRowAsError(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = MTBFConstants.UIConstants.ERROR_ROW_COLOR;
        }

        /// <summary>
        /// Method to mark a grid row as invalid.
        /// </summary>
        /// <param name="row"></param>
        public static void MarkDataGridRowAsInvalid(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = MTBFConstants.UIConstants.INVALID_ROW_COLOR;
        }

        /// <summary>
        /// Method to mark a ultra grid row as invalid.
        /// </summary>
        /// <param name="row"></param>
        public static void MarkDataGridRowAsInvalid(UltraGridRow row)
        {
            row.Appearance.BackColor = MTBFConstants.UIConstants.INVALID_ROW_COLOR;
        }

        /// <summary>
        /// Method to mark a grid row as normal.
        /// </summary>
        /// <param name="row"></param>
        public static void MarkDataGridRowAsNormal(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = MTBFConstants.UIConstants.NORMAL_ROW_COLOR;
        }

        /// <summary>
        /// Method to mark a ultra grid row as normal.
        /// </summary>
        /// <param name="row"></param>
        public static void MarkDataGridRowAsNormal(UltraGridRow row)
        {
            row.Appearance.BackColor = MTBFConstants.UIConstants.NORMAL_ROW_COLOR;
        }

        /// <summary>
        /// Method to mark a grid row as readonly.
        /// </summary>
        /// <param name="row"></param>
        public static void MarkDataGridRowAsReadOnly(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = MTBFConstants.UIConstants.READONLY_ROW_COLOR;
        }


        /// <summary>
        /// Method to mark a ultra grid row as readonly.
        /// </summary>
        /// <param name="row"></param>
        public static void MarkDataGridRowAsReadOnly(UltraGridRow row)
        {
            row.Appearance.BackColor = MTBFConstants.UIConstants.READONLY_ROW_COLOR;
        }
        /// <summary>
        /// Method to populate a dropdown list
        /// </summary>
        /// <param name="cmbBox"></param>
        /// <param name="dataList"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        public static void SetComboBoxData(ComboBox cmbBox, object dataList, string displayMember, string valueMember)
        {
            cmbBox.DisplayMember = displayMember;
            cmbBox.ValueMember = valueMember;
            cmbBox.DataSource = dataList;
            cmbBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Method to populate a dropdown list
        /// </summary>
        /// <param name="cmbBox"></param>
        /// <param name="dataList"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        public static void SetComboBoxData<T>(UltraCombo cmbBox, List<T> list, string displayMember, string valueMember)
        {
            cmbBox.DataSource = null;
            cmbBox.DisplayMember = MTBFConstants.NAME;
            cmbBox.ValueMember = MTBFConstants.ID;
            cmbBox.DataSource = BuildComboTable(list, displayMember, valueMember);
            try
            {
                // if (cmbBox.Value != null)
                cmbBox.Value = MTBFConstants.DataField.COMBO_DEFULT_CODE;
            }
            catch (System.Exception)
            {
                // if (cmbBox.Value != null)
                cmbBox.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            }
           
            cmbBox.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbBox.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        public static void SetComboBoxData<T>(UltraComboEditor cmbBox, List<T> list, string displayMember, string valueMember)
        {
            cmbBox.DataSource = null;
            cmbBox.DisplayMember = MTBFConstants.NAME;
            cmbBox.ValueMember = MTBFConstants.ID;
            cmbBox.DataSource = BuildComboTable(list, displayMember, valueMember);
            try
            {
                // if (cmbBox.Value != null)
                cmbBox.Value = MTBFConstants.DataField.COMBO_DEFULT_CODE;
            }
            catch (System.Exception)
            {
                // if (cmbBox.Value != null)
                cmbBox.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            }

        }

        public static DataTable BuildComboTable<T>(List<T> list, string displayMember, string valueMember)
        {
            DataTable dt = new DataTable();
            string column1Name = "ID";
            string column2Name = "Name";


            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.Name == valueMember)
                {
                    dt.Columns.Add(new DataColumn(column1Name, info.PropertyType));
                }
                else if (info.Name == displayMember)
                {
                    dt.Columns.Add(new DataColumn(column2Name, info.PropertyType));
                }

            }

            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (info.Name == valueMember)
                    {
                        row[column1Name] = info.GetValue(t, null);
                    }
                    else if (info.Name == displayMember)
                    {
                        row[column2Name] = info.GetValue(t, null);
                    }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }




        public static string NumberToWords(int number)
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
