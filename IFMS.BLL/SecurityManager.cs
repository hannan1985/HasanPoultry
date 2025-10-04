using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;



namespace IFMS.BLL
{
    public class SecurityManager : BllBase
    {

        #region "Role"

        public int InsertRole(Role role)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Insert<Role>(role);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }

        public int UpdateRole(Role role)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Update<Role>(role);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }

        public List<Role> GetAllRole()
        {
            return base.DataAccessManager.GetAll<Role>().Cast<Role>().ToList();
        }

        public List<Role> GetAllRoleByUserID(string userID)
        {
            string filter = string.Format("{0}='{1}'", MTBFConstants.DataField.USER_ID, userID);
            return base.DataAccessManager.GetFilteredData<Role>(filter).Cast<Role>().ToList();
        }

        #endregion

        public Module GetModuleByID(int moduleID)
        {
            return base.DataAccessManager.GetByID<Module>(moduleID, MTBFConstants.DataField.MODULE_ID);
        }

        public List<Module> GetAllModule()
        {
            return base.DataAccessManager.GetAll<Module>().Cast<Module>().ToList();
        }


        public List<Module> GetAllModuleByRoleID(int roleID)
        {
            string filter = string.Format("{0} in (Select ModuleID from RoleModuleMapping where RoleID  ={1})", MTBFConstants.DataField.MODULE_ID, roleID);
            return base.DataAccessManager.GetFilteredData<Module>(filter).Cast<Module>().ToList();

        }

        public Permission GetPermissionByID(int permissionID)
        {
            return base.DataAccessManager.GetByID<Permission>(permissionID, MTBFConstants.DataField.PERMISSION_ID);
        }


        public List<Permission> GetAllPermission()
        {
            return base.DataAccessManager.GetAll<Permission>().Cast<Permission>().ToList();
        }


        public List<Permission> GetAllPermissionByModuleID(int moduleID)
        {
            string filter = string.Format("{0}={1}", MTBFConstants.DataField.MODULE_ID, moduleID);
            return base.DataAccessManager.GetFilteredData<Permission>(filter).Cast<Permission>().ToList();
        }

        public List<IMFS.Common.DTO.Action> GetAllActionByPermissionID(int permissionID)
        {
            string filter = string.Format("{0} in (Select PermissionID from RolePermissionMapping where PermissionID ={1})", MTBFConstants.DataField.PERMISSION_ID, permissionID);
            return base.DataAccessManager.GetFilteredData<IMFS.Common.DTO.Action>(filter).Cast<IMFS.Common.DTO.Action>().ToList();
        }


        public IMFS.Common.DTO.Action GetActionByID(int actionID)
        {
            return base.DataAccessManager.GetByID<IMFS.Common.DTO.Action>(actionID, MTBFConstants.DataField.ACTION_ID);
        }

        public List<IMFS.Common.DTO.Action> GetAllAction()
        {
            return base.DataAccessManager.GetAll<IMFS.Common.DTO.Action>().Cast<IMFS.Common.DTO.Action>().ToList();
        }

        public List<Permission> GetAllPermissionByRoleID(int roleID)
        {
            List<Permission> lstPermission = new List<Permission>();
            //string filter = string.Format("{0} in (Select ModuleID from RoleModuleMapping where RoleID ={1})", MTBFConstants.DataField.MODULE_ID, roleID);
            string filter = string.Format("{0} in (Select PermissionID  from RolePermissionMapping  where RoleID ={1})", MTBFConstants.DataField.PERMISSION_ID, roleID);
            return base.DataAccessManager.GetFilteredData<Permission>(filter).Cast<Permission>().ToList();
        }

        #region "User Role"

        public int InsertUserRole(UserRole userRole)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Insert<UserRole>(userRole);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }

        public int UpdateUserRole(UserRole userRole)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Update<UserRole>(userRole);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }

        public Role GetRoleByID(int roleID)
        {
            return base.DataAccessManager.GetByID<Role>(roleID, MTBFConstants.DataField.ROLE_ID);
        }

        public List<UserRole> GetAllUserRole()
        {
            return base.DataAccessManager.GetAll<UserRole>().Cast<UserRole>().ToList();
        }

        public List<UserRole> GetAllUserRoleByUserID(string userID)
        {
            string filter = string.Format("{0}='{1}'", MTBFConstants.DataField.USER_ID, userID);
            return base.DataAccessManager.GetFilteredData<UserRole>(filter).Cast<UserRole>().ToList();
        }

        #endregion

        #region "Role Module Mapping"

        public int InsertRoleModuleMapping(RoleModuleMapping roleModuleMapping)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<RoleModuleMapping>(roleModuleMapping);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public int UpdateRoleModuleMapping(RoleModuleMapping roleModuleMapping)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<RoleModuleMapping>(roleModuleMapping);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public List<RoleModuleMapping> GetAllRoleModuleMapping()
        {
            return base.DataAccessManager.GetAll<RoleModuleMapping>().Cast<RoleModuleMapping>().ToList();
        }

        public List<RoleModuleMapping> GetAllRoleModuleMappingByUserID(string userID)
        {
            string filter = string.Format("{0} in(Select RoleID  from UserRole where UserID='{1}')", MTBFConstants.DataField.ROLE_ID, userID);
            return base.DataAccessManager.GetFilteredData<RoleModuleMapping>(filter).Cast<RoleModuleMapping>().ToList();


        }

        public RoleModuleMapping GetRoleModuleMapByID(int roleModuleMapID)
        {
            return base.DataAccessManager.GetByID<RoleModuleMapping>(roleModuleMapID, MTBFConstants.DataField.ROLE_MODULE_MAP_ID);
        }

        public RoleModuleMapping GetRoleModuleMapByRoleAndModuleID(int roleID, int moduleID)
        {
            string filter = string.Format("{0}={1} And {2}={3}", MTBFConstants.DataField.ROLE_ID, roleID, MTBFConstants.DataField.MODULE_ID, moduleID);
            return base.DataAccessManager.GetFilteredData<RoleModuleMapping>(filter).Cast<RoleModuleMapping>().ToList().FirstOrDefault();
        }

        #endregion

        #region "Role Permission Mapping"

        public int InsertRolePermissionMapping(RolePermissionMapping rolePermissionMapping)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<RolePermissionMapping>(rolePermissionMapping);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public int UpdateRolePermissionMapping(RolePermissionMapping rolePermissionMapping)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<RolePermissionMapping>(rolePermissionMapping);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public List<RolePermissionMapping> GetAllRolePermissionMapping()
        {
            return base.DataAccessManager.GetAll<RolePermissionMapping>().Cast<RolePermissionMapping>().ToList();
        }

        public List<RolePermissionMapping> GetAllRolePermissionMappingByUserAndModuleID(string userID, int moduleID)
        {
            string filter = string.Format("{0} in (Select RoleID  from UserRole where UserID ='{1}') And {2}={3}", MTBFConstants.DataField.ROLE_ID, userID, MTBFConstants.DataField.MODULE_ID, moduleID);

            return base.DataAccessManager.GetFilteredData<RolePermissionMapping>(filter).Cast<RolePermissionMapping>().ToList();
        }

        public RolePermissionMapping GetRolePermissionMappingByID(int rolePermissionMappingID)
        {
            return base.DataAccessManager.GetByID<RolePermissionMapping>(rolePermissionMappingID, MTBFConstants.DataField.ROLE_PERMISSION_MAP_ID);
        }

        public RolePermissionMapping GetRolePermissionMapByRolePermissionAndModuleID(int roleID, int moduleID, int permissionID)
        {
            string filter = string.Format("{0}={1} And {2}={3} And {4}={5}", MTBFConstants.DataField.ROLE_ID, roleID, MTBFConstants.DataField.MODULE_ID, moduleID, MTBFConstants.DataField.PERMISSION_ID, permissionID);
            return base.DataAccessManager.GetFilteredData<RolePermissionMapping>(filter).Cast<RolePermissionMapping>().ToList().FirstOrDefault();
        }

        #endregion

        #region "Role Action Mapping"

        public int InsertRoleActionMapping(RoleActionMapping roleActionMapping)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<RoleActionMapping>(roleActionMapping);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public int UpdateRoleActionMapping(RoleActionMapping roleActionMapping)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<RoleActionMapping>(roleActionMapping);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public List<RoleActionMapping> GetAllRoleActionMapping()
        {
            return base.DataAccessManager.GetAll<RoleActionMapping>().Cast<RoleActionMapping>().ToList();
        }

        public List<RoleActionMapping> GetAllRoleActionMappingByUserID(string userID)
        {
            string filter = string.Format("{0}='{1}'", MTBFConstants.DataField.USER_ID, userID);
            return base.DataAccessManager.GetFilteredData<RoleActionMapping>(filter).Cast<RoleActionMapping>().ToList();
        }

        public RoleActionMapping GetRoleActionMappingByID(int roleActionMappingID)
        {
            return base.DataAccessManager.GetByID<RoleActionMapping>(roleActionMappingID, MTBFConstants.DataField.ROLE_ACTION_MAP_ID);
        }

        public RoleActionMapping GetRoleActionMapByRolePermissionAndActionID(int roleID, int permissionID, int actionID)
        {
            string filter = string.Format("{0}={1} And {2}={3} And {4}={5}", MTBFConstants.DataField.ROLE_ID, roleID, MTBFConstants.DataField.PERMISSION_ID, permissionID, MTBFConstants.DataField.ACTION_ID, actionID);
            return base.DataAccessManager.GetFilteredData<RoleActionMapping>(filter).Cast<RoleActionMapping>().ToList().FirstOrDefault();
        }

        #endregion


        public Module GetModuleByName(string moduleName)
        {
            string filter = string.Format("{0}='{1}'", MTBFConstants.DataField.MODULE_NAME, moduleName);
            return base.DataAccessManager.GetFilteredData<Module>(filter).Cast<Module>().ToList().FirstOrDefault();
        }

        public Permission GetPermissionByName(string permissionName)
        {
            string filter = string.Format("{0}='{1}'", "FormName", permissionName);
            return base.DataAccessManager.GetFilteredData<Permission>(filter).Cast<Permission>().ToList().FirstOrDefault();

        }

        public List<RoleActionMapping> GetAllRoleActionMappingByPermissionID(int permissionID)
        {
            string filter = string.Format("{0}={1}", MTBFConstants.DataField.PERMISSION_ID, permissionID);
            return base.DataAccessManager.GetFilteredData<RoleActionMapping>(filter).Cast<RoleActionMapping>().ToList();
        }

        public List<RolePermissionMapping> GetAllRolePermissionMappingByRoleID(int roleID)
        {
            string filter = string.Format("{0}={1}", MTBFConstants.DataField.ROLE_ID, roleID);
            return base.DataAccessManager.GetFilteredData<RolePermissionMapping>(filter).Cast<RolePermissionMapping>().ToList();
        }


        public int InsertActionPermission(List<RolePermissionMapping> lstRolePermission, List<RoleActionMapping> lstRoleActionMap)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int roleID = lstRolePermission[0].RoleID;

            List<RolePermissionMapping> lstExistingRolePermissionMap = new List<RolePermissionMapping>();
            List<RoleActionMapping> lstExistingRoleActionMapping = new List<RoleActionMapping>();
            string filter = string.Format("{0}={1}", MTBFConstants.DataField.ROLE_ID, roleID);
            lstExistingRoleActionMapping = base.DataAccessManager.GetFilteredData<RoleActionMapping>(filter).Cast<RoleActionMapping>().ToList();
            lstExistingRolePermissionMap = base.DataAccessManager.GetFilteredData<RolePermissionMapping>(filter).Cast<RolePermissionMapping>().ToList();

            try
            {
                base.DataAccessManager.BeginTransaction();
                foreach (RolePermissionMapping exPermission in lstExistingRolePermissionMap)
                {
                    base.DataAccessManager.Delete<RolePermissionMapping>(exPermission.RolePermissionID);
                }

                foreach (RoleActionMapping exRoleAction in lstExistingRoleActionMapping)
                {
                    base.DataAccessManager.Delete<RoleActionMapping>(exRoleAction.RoleActionMappingID);
                }

                foreach (RolePermissionMapping rolePermission in lstRolePermission)
                {
                    base.DataAccessManager.Insert<RolePermissionMapping>(rolePermission);
                }

                foreach (RoleActionMapping roleAction in lstRoleActionMap)
                {
                    base.DataAccessManager.Insert<RoleActionMapping>(roleAction);
                }
                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }


        public List<RoleActionMapping> GetFilteredPermissionAction(string permissionFilter)
        {
            return base.DataAccessManager.GetFilteredData<RoleActionMapping>(permissionFilter).Cast<RoleActionMapping>().ToList();
        }


        public int InsertPermission(Permission _permission)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                if (_permission.PermissionID > 0)
                {
                    base.DataAccessManager.Update<Permission>(_permission);
                }
                else
                {
                    base.DataAccessManager.Insert<Permission>(_permission);
                }
            }
            catch (Exception)
            {

                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public int InsertAction(IMFS.Common.DTO.Action _action)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                if (_action.ActionID > 0)
                {
                    base.DataAccessManager.Update<IMFS.Common.DTO.Action>(_action);
                }
                else
                {
                    base.DataAccessManager.Insert<IMFS.Common.DTO.Action>(_action);
                }
            }
            catch (Exception)
            {

                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }
    }
}
