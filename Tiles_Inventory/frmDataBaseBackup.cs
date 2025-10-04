using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;

namespace Tiles_Inventory
{
    public partial class frmDataBaseBackup : BaseForm
    {
        public frmDataBaseBackup()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void BackUp(string backupDir)
        {
            int result = 0;
            try
            {
                if (!System.IO.Directory.Exists(backupDir))
                {
                    System.IO.Directory.CreateDirectory(backupDir);
                }
                result = DataAccessProxy.CreateDataBaseBackUp(backupDir);

                if (result > 0)
                {
                    MessageBox.Show("Successfully backup your database. ", "Pharmacy Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeleteOldFile(backupDir);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backup Operation failed please try again. ", "Pharmacy Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteOldFile(string location)
        {
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(location);

            foreach (System.IO.FileInfo file in directory.GetFiles())
            {
                if (file.Extension.Equals(".BAK") && (DateTime.Now - file.CreationTime).Days > 7)
                {
                    file.Delete();
                }
            }
        }

        private void btnBackup_Click(System.Object sender, System.EventArgs e)
        {
            string location = string.Empty;
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            location = folderBrowser.SelectedPath;
            if (!string.IsNullOrEmpty(location))
            {
                BackUp(location);
            }

        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
