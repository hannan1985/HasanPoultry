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
using NybSys.MTBF.Utility.Helper;
using System.IO;
using System.Drawing.Drawing2D;

namespace Tiles_Inventory
{
    public partial class frmPharmacy : BaseForm
    {
        private int _pharmacyID = 0;
        private byte[] _companyLogo = null;

        public frmPharmacy()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmPharmacy_Load(System.Object sender, System.EventArgs e)
        {
            LoadPharmacyInformation();
            grvPharmacyInformation.ClearSelection();
        }

        /// <summary>
        /// Method to load pharmacy information.
        /// </summary>
        private void LoadPharmacyInformation()
        {
            List<Organization> lstPharmacy = new List<Organization>();
            lstPharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList();
            DataTable orgTable = BuildOrganizationTable();
            foreach (Organization pharmacy in lstPharmacy)
            {
                DataRow row = orgTable.NewRow();
                row["OrganizationID"] = pharmacy.OrganizationID;
                row["Organization Name"] = pharmacy.OrganizationName;
                row["Contact Person"] = pharmacy.ContactPerson;
                row["Phone"] = pharmacy.Phone;
                row["Email"] = pharmacy.Email;
                row["Address"] = pharmacy.Address;
                orgTable.Rows.Add(row);
            }

            grvPharmacyInformation.DataSource = orgTable;
            grvPharmacyInformation.Columns[0].Visible = false;

        }


        private DataTable BuildOrganizationTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("OrganizationID");
            table.Columns.Add("Organization Name");
            table.Columns.Add("Contact Person");
            table.Columns.Add("Phone");
            table.Columns.Add("Email");
            table.Columns.Add("Address");
            return table;
        }

        private void btnSupplierSave_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (ValidFormData())
                {
                    if (InsertPharmacyInfo() > 0)
                    {
                        MessageBoxHelper.ShowInformation("Successfully saved");

                        LoadPharmacyInformation();

                        ResetAllControls();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Operation fail please try again");
            }
        }


        private bool ValidFormData()
        {
            if (txtPharmacyName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter shop name.");
                txtPharmacyName.Focus();
                return false;
            }

            if (txtPharmacyAddress.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter shop address.");
                txtPharmacyAddress.Focus();
                return false;
            }

            if (txtPhone.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter  contact number.");
                txtPhone.Focus();
                return false;
            }

            Organization pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            if (pharmacy != null)
            {
                MessageBoxHelper.ShowInformation("You already add shop information if required please edit.");              
                return false;
            }


            return true;
        }

        /// <summary>
        /// Method to insert pharmacy information.
        /// </summary>
        /// <returns></returns>
        private int InsertPharmacyInfo()
        {
            int result = 0;
            try
            {
                Organization pharmacy = new Organization();
                pharmacy.OrganizationName = txtPharmacyName.Text;
                pharmacy.Email = txtEmail.Text;
                pharmacy.ContactPerson = txtContactPerson.Text;
                pharmacy.Phone = txtPhone.Text;
                pharmacy.Fax = txtFax.Text;
                pharmacy.RegistrationNumber = txtRegistratinNumber.Text;
                pharmacy.OrganizationLogo = (GetCompanyLogo() != null) ? GetCompanyLogo() : null;
                pharmacy.Address = txtPharmacyAddress.Text;
                pharmacy.Status = cbActive.Checked;

                result = DataAccessProxy.InsertPharmacy(pharmacy);
            }
            catch (Exception)
            {

                MessageBoxHelper.ShowInformation("Operation fail please try again");
            }

            return result;

        }

        private byte[] GetCompanyLogo()
        {
            MemoryStream ms = new MemoryStream();
            if (pbCompanyLogo.BackgroundImage != null)
            {
                pbCompanyLogo.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to update pharmacy information.
        /// </summary>
        /// <returns></returns>
        private int UpdatePharmacyInfo()
        {
            int result = 0;
            try
            {
                Organization pharmacy = DataAccessProxy.GetPharmachyByID(_pharmacyID);

                pharmacy.OrganizationName = txtPharmacyName.Text;
                pharmacy.Email = txtEmail.Text;
                pharmacy.ContactPerson = txtContactPerson.Text;
                pharmacy.Phone = txtPhone.Text;
                pharmacy.OrganizationLogo = (GetCompanyLogo() != null) ? GetCompanyLogo() : null;
                pharmacy.Fax = txtFax.Text;
                pharmacy.RegistrationNumber = txtRegistratinNumber.Text;
                pharmacy.Address = txtPharmacyAddress.Text;
                pharmacy.Status = cbActive.Checked;

                result = DataAccessProxy.UpdatePharmacy(pharmacy);
            }
            catch (Exception)
            {

                MessageBoxHelper.ShowInformation("Operation fail please try again");
            }

            return result;
        }

        private void btnSupplierClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ResetAllControls()
        {
            txtPharmacyName.Clear();
            txtEmail.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtFax.Clear();
            txtPharmacyAddress.Clear();
            pbCompanyLogo.BackgroundImage = null;
            cbActive.Checked = false;
            txtRegistratinNumber.Clear();
        }

        private void btnSupplierUpdate_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (txtPharmacyName.Text == string.Empty | txtPharmacyAddress.Text == string.Empty | txtPhone.Text == string.Empty)
                    return;

                if (UpdatePharmacyInfo() > 0)
                {
                    MessageBoxHelper.ShowInformation("Update successfully");

                    LoadPharmacyInformation();

                    btnSupplierSave.Enabled = true;
                    btnSupplierUpdate.Enabled = false;

                    ResetAllControls();
                }

            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Operation fail please try again");
            }
        }


        private void grvPharmacyInformation_CellClick(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

            if (grvPharmacyInformation.SelectedRows.Count > 0 && !grvPharmacyInformation.SelectedRows[0].IsNewRow)
            {
                _pharmacyID = Convert.ToInt32(grvPharmacyInformation.CurrentRow.Cells[0].Value);

                Organization pharmacy = DataAccessProxy.GetPharmachyByID(_pharmacyID);
                if (pharmacy != null)
                {
                    txtPharmacyName.Text = pharmacy.OrganizationName;
                    txtContactPerson.Text = pharmacy.ContactPerson;
                    txtEmail.Text = pharmacy.Email;
                    txtPhone.Text = pharmacy.Phone;
                    txtFax.Text = pharmacy.Fax;
                    txtPharmacyAddress.Text = pharmacy.Address;
                    txtRegistratinNumber.Text = pharmacy.RegistrationNumber;
                    cbActive.Checked = pharmacy.Status;


                    if (pharmacy.OrganizationLogo != null)
                    {
                        MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                        Image returnImage = Image.FromStream(ms);
                        pbCompanyLogo.BackgroundImage = returnImage;
                    }
                }


                btnSupplierUpdate.Enabled = true;
                btnSupplierSave.Enabled = false;
            }
        }

        /// <summary>
        /// Method to check file formate
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsValidFileFormate(string fileName)
        {
            string extenstion = fileName.Substring(fileName.LastIndexOf(".") + 1);

            if (extenstion.ToUpper() == "PNG" || extenstion.ToUpper() == "JPG")
            {
                return true;
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select PNG or JPEG format image.");
            }
            return false;
        }

        /// <summary>
        /// Method to dicrese  image  size
        /// </summary>
        /// <param name="scaleFactor"></param>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private Image GenerateThumbnails(double scaleFactor, Stream sourcePath)
        {
            Image newImage = null;

            using (var img = System.Drawing.Image.FromStream(sourcePath))
            {

                int newWidth = 300;
                int newHeight = (newWidth * img.Height) / img.Width;

                newImage = new Bitmap(newWidth, newHeight);
                using (Graphics graphicsHandle = Graphics.FromImage(newImage))
                {
                    graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphicsHandle.DrawImage(img, 0, 0, newWidth, newHeight);
                }
            }

            return newImage;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (IsValidFileFormate(fileDialog.FileName))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(fileDialog.FileName);
                    MemoryStream ms = new MemoryStream(fileBytes);
                    Image returnImage = GenerateThumbnails(0.5, ms);

                    ms = new MemoryStream();
                    returnImage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    _companyLogo = ms.ToArray();
                    pbCompanyLogo.BackgroundImage = returnImage;
                }
            }
        }

    }
}
