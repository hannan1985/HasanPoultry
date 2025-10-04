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
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class BaseForm : Form
    {
        #region "Member variables"

        private bool _isUpdating = false;
        private FacadeManager _proxy = null;//new FacadeManager();


        #endregion

        #region "Properties"

        public bool IsUpdating
        {
            get { return _isUpdating; }
            set { _isUpdating = value; }
        }

        public FacadeManager DataAccessProxy
        {
            get { return _proxy; }
            set { _proxy = value; }
        }


        #endregion

        public BaseForm()
        {

        }

        #region "Event Handlers"




        #endregion

        #region "Private Methods"


        #endregion

        #region "Protected Methods"


        #endregion


        #region "User Action Permission"

        /// <summary>
        /// Method to check user action permission
        /// </summary>
        /// <param name="formName"></param>
        public void CheckAction(Form formName)
        {
            List<String> lstButtonName = new List<string>();
            FacadeManager _MTBFProxy = new FacadeManager();
            Users user = _MTBFProxy.GetUserByUserName(MTBFConstants.AppConstants.LoggedinUserID);
            if (user != null)
            {
                if (!user.IsSuperAdmin)
                {
                    Permission permission = _MTBFProxy.GetPermissionByName(formName.Name);
                    if (permission != null)
                    {
                        foreach (UserRole userRole in _MTBFProxy.GetAllUserRoleByUserID(MTBFConstants.AppConstants.LoggedinUserID))
                        {
                            foreach (RoleActionMapping roleAction in _MTBFProxy.GetAllRoleActionMappingByPermissionID(permission.PermissionID).Cast<RoleActionMapping>().Where(r => r.RoleID == userRole.RoleID))
                            {
                                IMFS.Common.DTO.Action action = _MTBFProxy.GetActionByID(roleAction.ActionID);
                                lstButtonName.Add(action.ButtonName);
                            }
                            VisibleControl(lstButtonName, formName);
                        }
                    }
                }
            }
        }

        private void RecurseControls(Control ctr, List<String> lstButtonName)
        {
            if (ctr.HasChildren)
            {
                foreach (Control objControl in ctr.Controls)
                {
                    RecurseControls(objControl, lstButtonName);
                }
            }
            else
            {
                if (lstButtonName.Contains(ctr.Name))
                {
                    ctr.Enabled = true;
                }
                else
                {
                    if (ctr.Name.Contains("btn") && ctr.Name != "btnClose")
                        ctr.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Method to manage control visibility.
        /// </summary>
        /// <param name="lstButtonName"></param>
        /// <param name="formName"></param>
        private void VisibleControl(List<String> lstButtonName, Form formName)
        {
            foreach (Control ctr in formName.Controls)
            {
                RecurseControls(ctr, lstButtonName);
            }
        }

        #endregion

    }
}
