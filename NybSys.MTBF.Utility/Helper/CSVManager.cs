using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

namespace NybSys.MTBF.Utility.Helper
{
    public class CSVManager
    {

        #region "Member variables"

        private string schemaLocation = string.Empty;

        private string fileLocation = string.Empty;

        private string mvarFilename = string.Empty;

        private bool skipFirstRow = false;

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="schemaPath"></param>
        public CSVManager(string filePath, string schemaPath)
        {
            if (ValidateFileLocations(filePath, schemaPath))
            {
                this.schemaLocation = schemaPath;
                this.fileLocation = filePath;
                this.skipFirstRow = false;
            }
        }

        public CSVManager(string filePath, string schemaPath, bool skipHeaderRow)
        {
            if (ValidateFileLocations(filePath, schemaPath))
            {
                this.schemaLocation = schemaPath;
                this.fileLocation = filePath;
                this.skipFirstRow = skipHeaderRow;
            }
        }

        #region "Public Methods"

        public bool IsValidFile(ref string validationMessage)
        {
            validationMessage = string.Empty;
            List<SchemaColumn> schemaColumns = ReadSchemaFile();
            List<string> lineEntries = ReadCSVFile();

            if (schemaColumns.Count == 0)
            {
                validationMessage = "No column found in schema file.";
                return false;
            }

            if (lineEntries.Count == 0)
            {
                validationMessage = "CSV file contains no data.";
                return false;
            }

            if (!HasValidNumberOfColumn(schemaColumns.Count, lineEntries))
            {
                validationMessage = "CSV file has invalid number of columns.";
                return false;
            }

            foreach (string lineEntry in lineEntries)
            {
                int columnIndex = -1;
                bool result = CheckLineEntry(lineEntry, schemaColumns, ref columnIndex);
                if (!result)
                {
                    int lineIndex = lineEntries.IndexOf(lineEntry) + 1;
                    validationMessage = string.Format("Invalid data at line : ({0}), column : ({1})", lineIndex.ToString(), columnIndex.ToString());
                    return false;
                }
            }

            return true;
        }

        public List<T> GetCSVData<T>()
        {

            List<T> returnList = new List<T>();
            List<string> lineEntries = ReadCSVFile();

            foreach (string lineEntry in lineEntries)
            {
                returnList.Add(FillObject<T>(lineEntry));
            }

            return returnList;

        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Method to validate file paths.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="schemaPath"></param>
        /// <returns></returns>
        private bool ValidateFileLocations(string filePath, string schemaPath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new CSVException("CSV file path is empty.");
            }

            if (string.IsNullOrEmpty(schemaPath))
            {
                throw new CSVException("Schema path is empty.");
            }

            if (!System.IO.File.Exists(filePath))
            {
                throw new CSVException("CSV file not found.");
            }

            if (!System.IO.File.Exists(schemaPath))
            {
                throw new CSVException("Schema file not found.");
            }

            return true;
        }

        /// <summary>
        /// Method to read schema file.
        /// </summary>
        /// <returns></returns>
        private List<SchemaColumn> ReadSchemaFile()
        {
            List<SchemaColumn> columnList = new List<SchemaColumn>();
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(this.schemaLocation);
                XmlNodeList chileNodes = xDoc.SelectSingleNode("columns").ChildNodes;
                foreach (XmlNode chileNode in chileNodes)
                {
                    SchemaColumn schemaColumn = new SchemaColumn();
                    if (chileNode.Attributes["name"] != null)
                    {
                        schemaColumn.ColumnName = chileNode.Attributes["name"].Value;
                    }
                    if (chileNode.Attributes["type"] != null)
                    {
                        schemaColumn.ColumnType = chileNode.Attributes["type"].Value;
                    }
                    if (chileNode.Attributes["format"] != null)
                    {
                        schemaColumn.ColumnFormat = chileNode.Attributes["format"].Value;
                    }
                    //code by yasir
                    if (chileNode.Attributes["allowEmpty"] != null)
                    {
                        schemaColumn.AllowEmpty = Convert.ToBoolean(chileNode.Attributes["allowEmpty"].Value);
                    }
                    columnList.Add(schemaColumn);
                }
            }
            catch (System.Exception ex)
            {
                throw new CSVException("Error while reading XML schema.", ex);
            }
            finally
            {
                xDoc = null;
            }


            return columnList;
        }

        /// <summary>
        /// Method to read CSV file.
        /// </summary>
        /// <returns></returns>
        private List<string> ReadCSVFile()
        {
            //Return System.IO.File.ReadAllLines(fileLocation).ToList()
            List<string> csvData = System.IO.File.ReadAllLines(fileLocation).Where(s => s.Trim() != string.Empty).ToList();
            if (this.skipFirstRow && csvData.Count > 0)
            {
                csvData.RemoveAt(0);
            }
            return csvData;
        }

        /// <summary>
        /// Method to check if csv file has valid number of columns.
        /// </summary>
        /// <returns></returns>
        private bool HasValidNumberOfColumn(int columnCount, List<string> lineEntries)
        {
            foreach (string lineEntry in lineEntries)
            {
                string[] arTemp = lineEntry.Split(',');
                if (arTemp.Length != columnCount)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckLineEntry(string lineEntry, List<SchemaColumn> columns, ref int columnIndex)
        {
            bool result = true;
            columnIndex = -1;
            string[] lineValues = lineEntry.Split(',');
            for (int i = 0; i <= lineValues.Length - 1; i++)
            {
                columnIndex = i + 1;
                string value = lineValues[i];
                result = CheckDataType(value, columns[i].ColumnType.ToLower(), columns[i].AllowEmpty);
                if (!result)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            return result;
        }

        /// <summary>
        /// Method to check datatype of a value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private bool CheckDataType(string value, string dataType, bool allowEmpty)
        {
            bool result = true;
            switch (dataType)
            {
                case "int":
                    int intValue = 0;
                    result = int.TryParse(value, out intValue);
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case "double":
                    if (allowEmpty == false)
                    {
                        double doubleValue = 0;
                        result = double.TryParse(value, out doubleValue);
                        break; // TODO: might not be correct. Was : Exit Select
                    }

                    break;
                default:
                    //for string no need to check data type.
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
            }
            return result;
        }

        private T FillObject<T>(string lineEntry)
        {

            string[] lineValues = lineEntry.Split(',');
            dynamic entityObject = Activator.CreateInstance(typeof(T));
            PropertyInfo[] properties = typeof(T).GetProperties();
            int propertyCount = properties.Length;
            List<SchemaColumn> schemaColumns = ReadSchemaFile();


            for (int i = 0; i <= propertyCount - 1; i++)
            {
                PropertyInfo propertyInfo = properties[i];
                object propertyValue = null;
                if ((propertyInfo.PropertyType == typeof(string)))
                {
                    // No need to check for valid input as it takes anything.
                    propertyValue = Convert.ChangeType(lineValues[i], propertyInfo.PropertyType, System.Globalization.CultureInfo.InvariantCulture);
                    propertyInfo.SetValue(entityObject, propertyValue, null);


                }
                else if ((propertyInfo.PropertyType == typeof(System.DateTime)))
                {
                    propertyValue = ConvertDateFormate(schemaColumns[i].ColumnFormat, lineValues[i]);
                    propertyInfo.SetValue(entityObject, propertyValue, null);
                }
                else
                {
                    // Not every datatype other than string takes empty string as a valid input
                    // So we will skip assigning any value to the fileds which is not of type string and for which empty string was received.
                    // TODO: Need to checkl for valid input based on data types.
                    if (lineValues[i].Trim() != string.Empty)
                    {
                        propertyValue = Convert.ChangeType(lineValues[i], propertyInfo.PropertyType, System.Globalization.CultureInfo.InvariantCulture);
                        propertyInfo.SetValue(entityObject, propertyValue, null);
                    }

                }
            }
            return entityObject;

        }

        private System.DateTime ConvertDateFormate(string schemaDateFormat, string lineValues)
        {
            string dateFormat = null;
            System.DateTime convertedDate = default(System.DateTime);

            if (lineValues == string.Empty)
            {
                convertedDate = Convert.ToDateTime("01-01-1990");//C_DATE_01_01_1990;
            }
            else
            {
                if (schemaDateFormat == "yyyyMMdd")
                {
                    dateFormat = lineValues.Substring(0, 4) + "-" + lineValues.Substring(4, 2) + "-" + lineValues.Substring(6, 2);
                    convertedDate = Convert.ToDateTime(dateFormat);
                }
                else if (schemaDateFormat == "ddMMyyyy")
                {
                    dateFormat = lineValues.Substring(3, 2) + "-" + lineValues.Substring(0, 2) + "-" + lineValues.Substring(6, 4);
                    convertedDate = Convert.ToDateTime(dateFormat);
                }
                else
                {
                    convertedDate = Convert.ToDateTime(lineValues);
                }
            }

            return convertedDate;
        }

        #endregion

        #region "Internal Class"

        /// <summary>
        /// Class to denote each column.
        /// </summary>
        private class SchemaColumn
        {

            private string _columnName;
            public string ColumnName
            {
                get { return _columnName; }
                set { _columnName = value; }
            }

            private string _columnType;
            public string ColumnType
            {
                get { return _columnType; }
                set { _columnType = value; }
            }

            private string _columnFormat;
            public string ColumnFormat
            {
                get { return _columnFormat; }
                set { _columnFormat = value; }
            }

            private bool _allowEmpty;
            public bool AllowEmpty
            {
                get { return _allowEmpty; }
                set { _allowEmpty = value; }
            }


        }

        #endregion



    }

    public class CSVException :System.Exception
    {

        public CSVException(string message): base(message)
        {
        }

        public CSVException(string message, System.Exception ex)
            : base(message, ex)
        {
        }

    }
}
