using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Journal")]
    public class Journal
    {

        private int _JournalID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int JournalID
        {
            get { return _JournalID; }
            set { _JournalID = value; }
        }

        private DateTime  _JournalDate = DateTime .Now ;

        [DataType("datetime")]
        public DateTime  JournalDate
        {
            get { return _JournalDate; }
            set { _JournalDate = value; }
        }

        private int _JAccountID = 0;

        [DataType("int")]
        public int JAccountID
        {
            get { return _JAccountID; }
            set { _JAccountID = value; }
        }

        private int _ChildAccountID = 0;

        [DataType("int")]
        public int ChildAccountID
        {
            get { return _ChildAccountID; }
            set { _ChildAccountID = value; }
        }


        private int _AccountID = 0;
        [DataType("int")]
        public int AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        private string _DrCrIndecator = string.Empty;

        [DataType("varchar")]
        public string DrCrIndecator
        {
            get { return _DrCrIndecator; }
            set { _DrCrIndecator = value; }
        }

        private decimal _Amount = 0;

        [DataType("decimal")]
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private int _ReferenceNo = 0;

        [DataType("int")]
        public int ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }

        private int _CashReceiveID = 0;
        [DataType("int")]
        public int CashReceiveID
        {
            get { return _CashReceiveID; }
            set { _CashReceiveID = value; }
        }

        private int _PaymentID = 0;

        [DataType("int")]
        public int PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }

        public int JournalType { get; set; }

        public int PurchaseID { get; set; }

        public int SalesID { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public int ExpenseID { get; set; }

        public int PartyReceiveID { get; set; }

        public int PartyPaymentID { get; set; }

        public int StockSalesID { get; set; }

        public int BankAccountID { get; set; }

        public int BankWithdrawID { get; set; }

        public int BankDepositID { get; set; }

        public int SalesReturnID { get; set; }

        public int MaterialReceiveID { get; set; }

        public int MaterialDistributeID { get; set; }

        public int StockID { get; set; }

        public int ProductionCostID { get; set; }

        public string Description { get; set; }

        public int OtherReceiveID { get; set; }

        public int OtherPaymentID { get; set; }

        public int PurchaseRetrunID { get; set; }

        public int DamageInfoID { get; set; }

        public int ReceiveProductID { get; set; }
    }
}
