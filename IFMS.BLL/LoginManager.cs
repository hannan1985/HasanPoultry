using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class LoginManager : BllBase
    {
        public int InsertUsers(Users users)
        {
            try
            {
                int result = base.DataAccessManager.Insert<Users>(users);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateUsers(Users users)
        {
            try
            {
                int result = base.DataAccessManager.Update<Users>(users);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public List<Users> GetAllUsers()
        {
            return base.DataAccessManager.GetAll<Users>().Cast<Users>().ToList();
        }

        public int InsertTrialPeriod(TrialPeriodInformation trialPeriod)
        {
            try
            {
                int result = base.DataAccessManager.Insert<TrialPeriodInformation>(trialPeriod);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateTrialPeriod(TrialPeriodInformation trialPeriod)
        {
            try
            {
                int result = base.DataAccessManager.Update<TrialPeriodInformation>(trialPeriod);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public TrialPeriodInformation GetTrialPeriod()
        {
            return base.DataAccessManager.GetAll<TrialPeriodInformation>().Cast<TrialPeriodInformation>().ToList().FirstOrDefault();
        }

        public Users GetUserByUserNameAndPassword(string username, string password)
        {
            string filter = string.Format("{0}='{1}' AND {2}='{3}'", "UserID", username, "Password", password);
            return base.DataAccessManager.GetFilteredData<Users>(filter).Cast<Users>().ToList().FirstOrDefault();
        }

        public int CreateDataBaseBackUp(string backupDir)
        {
            string dbName = System.Configuration.ConfigurationManager.AppSettings["DBName"];

            string filter = string.Format(IFMSConstant.QueryConstants.DatabaseBackUp, backupDir, System.DateTime.Now.ToString("dd_MM_yyyy_hh_mm_tt").Trim(), dbName);
            base.DataAccessManager.ExecuteScalar(filter);
            return 1;
        }

        public Users GetUserByUserName(string userId)
        {
            string filter = string.Format("{0}='{1}'", "UserID", userId);
            return base.DataAccessManager.GetFilteredData<Users>(filter).Cast<Users>().ToList().FirstOrDefault();
        }

        public Users GetUserByEmployeeID(int _employeeID)
        {
            string filter = string.Format("{0}='{1}'", "EmployeeID", _employeeID);
            return base.DataAccessManager.GetFilteredData<Users>(filter).Cast<Users>().ToList().FirstOrDefault();
        }

        public UserSettings GetUserSettingsByEmployeeID(int employeeID)
        {
            string filter = string.Format("{0}='{1}'", "EmployeeID", employeeID);
            return base.DataAccessManager.GetFilteredData<UserSettings>(filter).Cast<UserSettings>().ToList().FirstOrDefault();
        }



        /// <summary>
        /// Method to insert users.
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        public int InsertUsers(Users users, List<Role> lstRole)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                this.DataAccessManager.Insert<Users>(users);

                InsertUserRole(users, lstRole);

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception exp)
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        private void InsertUserRole(Users users, List<Role> lstRole)
        {
            foreach (Role role in lstRole)
            {
                UserRole userRole = new UserRole();
                userRole.UserID = users.UserID;
                userRole.RoleID = role.RoleID;
                base.DataAccessManager.Insert<UserRole>(userRole);
            }
        }

        /// <summary>
        /// Method to update users information.
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        public int UpdateUsers(Users users, List<Role> lstRole)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<UserRole> lstDeleteUserRole = GetAllUserRoleByUserID(users);
            try
            {
                base.DataAccessManager.BeginTransaction();

                this.DataAccessManager.Update<Users>(users);

                DeleteUserRole(lstDeleteUserRole);

                InsertUserRole(users, lstRole);

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception exp)
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        private void DeleteUserRole(List<UserRole> lstDeleteUserRole)
        {
            foreach (UserRole userRole in lstDeleteUserRole)
            {
                base.DataAccessManager.Delete<UserRole>(userRole.UserRoleMapID);
            }
        }

        private List<UserRole> GetAllUserRoleByUserID(Users users)
        {
            string filter = string.Format("{0}='{1}'", MTBFConstants.DataField.USER_ID, users.UserID);
            return base.DataAccessManager.GetFilteredData<UserRole>(filter).Cast<UserRole>().ToList();
        }


        /// <summary>
        /// Method to get users by userid and password.
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        public Users GetUsersByUserIDAndPassword(string userID, string password)
        {
            string filter = string.Format(" {0} = '{1}' AND {2} = '{3}' AND {4} = 'false'", MTBFConstants.DataField.USER_ID, userID, MTBFConstants.DataField.PASSWORD, password, MTBFConstants.DataField.IS_DELETED);
            Users users = this.DataAccessManager.GetFilteredData<Users>(filter).Cast<Users>().ToList().FirstOrDefault();

            return users;
        }

        /// <summary>
        /// Method to get user by user id.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Users GetUsersByUserID(string userID)
        {
            string filter = string.Format(" {0} = '{1}'", MTBFConstants.DataField.USER_ID, userID);
            Users users = this.DataAccessManager.GetFilteredData<Users>(filter).Cast<Users>().ToList().FirstOrDefault();

            return users;
        }


        public List<Users> GetFilteredUser(string filter)
        {
            return DataAccessManager.GetFilteredData<Users>(filter).Cast<Users>().ToList();
        }
    }
}
