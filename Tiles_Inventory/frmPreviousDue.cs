using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IFMS.BLL;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmPreviousDue : BaseForm
    {
        private int _cashReceiveID = 0;
        private decimal PreviousAmount = 0;
        private decimal updateAmount = 0;
        private bool _isUpdate = false;

        public event OnDueInformationLoadEventHandler OnDueInformationLoad;
        public delegate void OnDueInformationLoadEventHandler();

        public frmPreviousDue(bool isEdit, int sl)
        {
            if (isEdit)
            {
                _isUpdate = isEdit;
                _cashReceiveID = sl;
            }
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmCashReceiveInformation_Load(System.Object sender, System.EventArgs e)
        {
            LoadDueType();
            this.LoadCustomerCombo();
            this.LoadSupplierInformation();
            cmbCustomerName.Enabled = true;
            if (_isUpdate)
            {
                LoadExistingCashReceiveInformation();
            }
        }

        private void LoadDueType()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            list.Add(MTBFConstants.DataField.COMBO_DEFULT_TASK_TYPE, MTBFConstants.DataField.COMBO_DEFAULT_ID);

            foreach (int enumValue in Enum.GetValues(typeof(MTBFEnums.DueType)))
            {
                list.Add(Enum.GetName(typeof(MTBFEnums.DueType), enumValue), enumValue);
            }

            cmbDueType.DisplayMember = "Key";
            cmbDueType.ValueMember = "Value";
            cmbDueType.DataSource = list.ToList();
            cmbDueType.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }



        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {
                if (_isUpdate)
                {

                    if (UpdateCashReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBox.Show("Successfully saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();
                        _isUpdate = false;
                        if (OnDueInformationLoad != null)
                        {
                            OnDueInformationLoad();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {

                    if (InsertCashReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBox.Show("Successfully saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();
                        if (OnDueInformationLoad != null)
                        {
                            OnDueInformationLoad();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        private void btnWholeSales_Click(System.Object sender, System.EventArgs e)
        {
            ResetAllControls();
        }

        private void txtReceiveAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("-1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }




        /// <summary>
        /// Method to update journal information.
        /// </summary>
        /// <param name="cashReceiveID"></param>
        /// <returns></returns>
        public int UpdateJournalInformationForCashReceive(int cashReceiveID)
        {
            int result = 0;
            List<IMFS.Common.DTO.Journal> lstJournalInformation = new List<IMFS.Common.DTO.Journal>();

            lstJournalInformation = DataAccessProxy.GetJournalByCashReceiveID(_cashReceiveID).Cast<IMFS.Common.DTO.Journal>().ToList();

            foreach (IMFS.Common.DTO.Journal journal in lstJournalInformation)
            {
                journal.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
                result = DataAccessProxy.UpdateJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to load existing cash receive information.
        /// </summary>
        private void LoadExistingCashReceiveInformation()
        {
            CustomerPreviousDue cashReceive = new PreviousDueManager().GetCustomerPreviousDueByID(_cashReceiveID);
            cmbDueType.Value = cashReceive.DueType;
            txtReceiveAmount.Text = cashReceive.Amount.ToString();
            dtpReceiveDate.Value = cashReceive.DueDate;

            cmbCustomerName.Value = cashReceive.CustomerID;
            cmbSupplierName.Value = cashReceive.SupplierID;

        }

        private DataTable BuildCustomerTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CustomerID");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            return table;
        }

        private void LoadCustomerCombo()
        {
            List<Customer> lstCustomer = new List<Customer>();
            lstCustomer = DataAccessProxy.GetAllCustomer().Cast<Customer>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            DataTable table = BuildCustomerTable();
            List<Zone> lstZone = new List<Zone>();
            lstZone = new CustomerManager().GetAllZone();

            Customer emptyCustomer = new Customer();
            emptyCustomer.CustomerID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyCustomer.CustomerName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstCustomer.Insert(0, emptyCustomer);


            foreach (Customer customer in lstCustomer)
            {
                Zone zone = lstZone.Where(z => z.ZoneID == customer.ZoneID).FirstOrDefault();
                DataRow row = table.NewRow();
                row["CustomerID"] = customer.CustomerID;
                row["Customer Name"] = customer.CustomerName;
                row["Address"] = customer.Address;
                row["Phone"] = customer.Phone;
                table.Rows.Add(row);

            }

            cmbCustomerName.DisplayMember = "Customer Name";
            cmbCustomerName.ValueMember = "CustomerID";
            cmbCustomerName.DataSource = table;
            cmbCustomerName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;

            cmbCustomerName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCustomerName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            cmbCustomerName.DisplayLayout.Bands[0].Columns["CustomerID"].Hidden = true;
        }






        private void LoadSupplierInformation()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            List<Supplier> lstSupplier = new List<Supplier>();
            lstSupplier = DataAccessProxy.GetAllSupplier().Cast<Supplier>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            Supplier emptySupplier = new Supplier();
            emptySupplier.SupplierID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptySupplier.SupplierName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;


            lstSupplier.Insert(0, emptySupplier);

            foreach (Supplier supplier in lstSupplier)
            {
                DataRow row = table.NewRow();
                row[0] = supplier.SupplierID;
                row[1] = supplier.SupplierName;
                table.Rows.Add(row);
            }

            cmbSupplierName.DisplayMember = "Name";
            cmbSupplierName.ValueMember = "ID";
            cmbSupplierName.DataSource = table;
            cmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        /// <summary>
        /// Method to insert cash receive information.
        /// </summary>
        /// <returns></returns>
        private int InsertCashReceiveInformation()
        {
            int dueType = 0;
            int.TryParse(cmbDueType.Value.ToString(), out dueType);
            int result = 0;
            CustomerPreviousDue previousDue = new CustomerPreviousDue();
            previousDue.DueDate = dtpReceiveDate.Value;
            previousDue.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
            if (dueType == (int)MTBFEnums.DueType.Customer)
            {
                previousDue.CustomerID = Convert.ToInt32(cmbCustomerName.Value);
                previousDue.CustomerName = cmbCustomerName.Text;
                previousDue.SupplierID = 0;
                previousDue.SupplierName = string.Empty;
            }
            else
            {
                previousDue.CustomerID = 0;
                previousDue.CustomerName = string.Empty;
                previousDue.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
                previousDue.SupplierName = cmbSupplierName.Text;
            }
            previousDue.DueType = dueType;
            previousDue.BranchID = MTBFConstants.AppConstants.BranchID;
            previousDue.OrganizationID = MTBFConstants.AppConstants.OrganizationID;

            result = new PreviousDueManager().InsertCustomerPreviousDue(previousDue);
            return result;
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ValidFormData()
        {   
            int dueType = 0;
            int.TryParse(cmbDueType.Value.ToString(), out dueType);
            int customerID = 0;
            int supplierID = 0;

            if (dueType == (int)MTBFEnums.DueType.Customer)
            {
                int.TryParse(cmbCustomerName.Value.ToString(), out customerID);
                if (customerID == 0)
                {
                    MessageBoxHelper.ShowInformation("Please select customer name");
                    cmbCustomerName.Focus();
                    return false;
                }
            }
            else
            {
                int.TryParse(cmbSupplierName.Value.ToString(), out supplierID);
                if (supplierID == 0)
                {
                    MessageBoxHelper.ShowInformation("Please select supplier name");
                    cmbSupplierName.Focus();
                    return false;
                }
            }


            if (txtReceiveAmount.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please check receive amount.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReceiveAmount.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to get actual receive amount.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private decimal GetActualReceiveAmount()
        {
            decimal presentAmount = 0;

            decimal.TryParse(txtReceiveAmount.Text.Trim(), out presentAmount);
            decimal actualAmount = 0;

            if (_isUpdate)
            {
                if (updateAmount > presentAmount)
                {
                    actualAmount = 0;
                }
                else if (presentAmount > updateAmount)
                {
                    actualAmount = presentAmount - updateAmount;
                }
                else if (presentAmount == updateAmount)
                {
                    actualAmount = 0;
                }
            }
            else
            {
                actualAmount = presentAmount;
            }

            return actualAmount;
        }

        /// <summary>
        /// Method to update cash receive information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int UpdateCashReceiveInformation()
        {
            int dueType = 0;
            int.TryParse(cmbDueType.Value.ToString(), out dueType);
            int result = 0;
            CustomerPreviousDue previousDue = new PreviousDueManager().GetCustomerPreviousDueByID(_cashReceiveID);
            previousDue.DueDate = dtpReceiveDate.Value;
            previousDue.Amount = Convert.ToDecimal(txtReceiveAmount.Text);

            if (dueType == (int)MTBFEnums.DueType.Customer)
            {
                previousDue.CustomerID = Convert.ToInt32(cmbCustomerName.Value);
                previousDue.CustomerName = cmbCustomerName.Text;
                previousDue.SupplierID = 0;
                previousDue.SupplierName = string.Empty;
            }
            else
            {
                previousDue.CustomerID = 0;
                previousDue.CustomerName = string.Empty;
                previousDue.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
                previousDue.SupplierName = cmbSupplierName.Text;
            }
            previousDue.DueType = dueType;
            previousDue.BranchID = MTBFConstants.AppConstants.BranchID;
            previousDue.OrganizationID = MTBFConstants.AppConstants.OrganizationID;

            result = new PreviousDueManager().UpdateCustomerPreviousDue(previousDue);
            return result;
        }

        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            cmbCustomerName.Enabled = true;
            txtReceiveAmount.Clear();
            btnSave.Enabled = true;
            _isUpdate = false;
        }

        /// <summary>
        /// Method to get due amount by customer id.
        /// </summary>
        /// <remarks></remarks>
        private decimal GetDueAmountByCustomerID(int customerId)
        {
            decimal dueAmount = default(decimal);
            dueAmount = DataAccessProxy.GetCustomerDueAmountByCustomerID(customerId, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            return dueAmount;
        }

        private void cmbDueType_ValueChanged(object sender, EventArgs e)
        {
            int dueType = 0;
            if (cmbDueType.Value != null)
            {
                int.TryParse(cmbDueType.Value.ToString(), out dueType);

                if (dueType == (int)MTBFEnums.DueType.Customer)
                {
                    cmbSupplierName.Enabled = false;
                    cmbCustomerName.Enabled = true;
                }
                else
                {
                    cmbCustomerName.Enabled = false;
                    cmbSupplierName.Enabled = true;
                }

            }
        }


    }
}
