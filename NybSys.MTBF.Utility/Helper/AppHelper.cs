using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NybSys.MTBF.Utility.Constants;

namespace NybSys.MTBF.Utility.Helper
{
    public class AppHelper
    {
        private static string _helpFileUrl = string.Empty;
        private static string _rssUrl = string.Empty;
        private static string _supportPhone = string.Empty;

        private static string _currentFiscalYear = string.Empty;

        /// <summary>
        /// Method to get the institutional level where user wants to set celling.
        /// </summary>
        /// <returns></returns>
        public static int GetInstitutionalCellingLevel()
        {
            return (int)NybSys.MTBF.Utility.Enums.MTBFEnums.InstitutionalCellingLevel.ACTIVITY;
        }

        /// <summary>
        /// Method to get the const center level where user wants to set celling.
        /// </summary>
        /// <returns></returns>
        public static int GetCostCenterCellingLevel()
        {
            return (int)NybSys.MTBF.Utility.Enums.MTBFEnums.CostCenterCellingLevel.REPORTING_ITEM;
        }

        /// <summary>
        /// Method to get help file url from config.
        /// </summary>
        /// <returns></returns>
        public static string GetHelpFileUrl()
        {
            if (_helpFileUrl == string.Empty)
            {
                _helpFileUrl = System.Configuration.ConfigurationManager.AppSettings["helpfile"];
            }

            return _helpFileUrl;
        }

        public static decimal GetDefaultCreditLimit()
        {
            return Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["DefaultCreditLimit"]);
        }

        /// <summary>
        /// Method to get root directory path.
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string directoryPath = string.Empty;
            directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            return directoryPath;
        }

        /// <summary>
        /// Method to get RSS link url from config.
        /// </summary>
        /// <returns></returns>
        public static string GetRSSUrl()
        {
            if (_rssUrl == string.Empty)
            {
                _rssUrl = System.Configuration.ConfigurationManager.AppSettings["rssUrl"];
            }

            return _rssUrl;
        }

        /// <summary>
        /// Method to get support phone number from config
        /// </summary>
        /// <returns></returns>
        public static string GetSupportPhoneNumber()
        {
            if (_supportPhone == string.Empty)
            {
                _supportPhone = System.Configuration.ConfigurationManager.AppSettings["supportPhone"];
            }

            return _supportPhone;
        }

        /// <summary>
        /// Method to get lock offset for budget screen.
        /// </summary>
        /// <returns></returns>
        public static int GetBudgetLockOffset()
        {
            int lockOffset = 0;
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["lockOffset"], out lockOffset);
            return lockOffset;
        }

        /// <summary>
        /// Method to get current fiscal year.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFiscalYear()
        {
            return _currentFiscalYear;
        }

        public static void SetCurrentFiscalYear(string fiscalYear)
        {
            _currentFiscalYear = fiscalYear;
        }

        /// <summary>
        /// Method to check email validation.
        /// </summary>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static bool IsValidEmailFormat(string formatString)
        {
            return Regex.IsMatch(formatString, MTBFConstants.QueryConstants.EmailValidation);
        }
    }
}
