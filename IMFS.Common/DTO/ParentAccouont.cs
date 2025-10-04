using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("ParentAccount")]
    public class ParentAccount
    {
        private int _AccountID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        private int _AccountTypeID = 0;

        [DataType("int")]
        public int AccountTypeID
        {
            get { return _AccountTypeID; }
            set { _AccountTypeID = value; }
        }

        private string _accountsName = string.Empty;

        [DataType("varchar")]
        public string AccountsName
        {
            get { return _accountsName; }
            set { _accountsName = value; }
        }

        private bool _IsIncomeStatementItem = false;

        [DataType("bool")]
        public bool IsIncomeStatementItem
        {
            get { return _IsIncomeStatementItem; }
            set { _IsIncomeStatementItem = value; }
        }

        private bool _IsBalanceSheetItem = false;

        [DataType("bool")]
        public bool IsBalanceSheetItem
        {
            get { return _IsBalanceSheetItem; }
            set { _IsBalanceSheetItem = value; }
        }

        private bool _IsTradingAccountItem = false;

        [DataType("bool")]
        public bool IsTradingAccountItem
        {
            get { return _IsTradingAccountItem; }
            set { _IsTradingAccountItem = value; }
        }

        private string _CreatedBy = string.Empty;

        [DataType("varchar")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private DateTime _CreatedDate = DateTime.Now;

        [DataType("DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private int _Status = 0;

        [DataType("int")]
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }




        public int AccountHeadID { get; set; }



        private bool _IsDefault = false;

        public bool IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }

    }
}
