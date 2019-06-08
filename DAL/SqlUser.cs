using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BO;
using Utility;

namespace DAL
{
    public class SqlUser
    {
        private readonly string _connectionString;

        SqlHelper sqlHelper = new SqlHelper();

        public SqlUser()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[DbConstants.ConnectionString];
            if (connectionString == null)
                throw new Exception(DbConstants.ErrorConnection);
            _connectionString = connectionString.ConnectionString;
        }

        #region Add Update a  User
        /// <summary>
        /// Add Update a  User into the database.
        /// </summary>
        /// <param name="entity">object of User class to be added/updated.</param>
        /// <returns>
        /// Returns true if add is successful, else false.
        /// </returns>
        public bool AddUpdateUser(User entity)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                bool isAddUpdated = false;
                using (SqlCommand cmd = sqlHelper.GetSqlCommand(DbConstants.User.StoreProcedure.usp_AddUpdateUser, cn, true))
                {
                    SqlParameter paramId = new SqlParameter(DbConstants.User.DbParameter.Id, entity.Id);
                    cmd.Parameters.Add(paramId);

                    SqlParameter paramEmailAddress = new SqlParameter(DbConstants.User.DbParameter.Email, entity.Email);
                    cmd.Parameters.Add(paramEmailAddress);

                    SqlParameter paramFirstName = new SqlParameter(DbConstants.User.DbParameter.FirstName, entity.FirstName);
                    cmd.Parameters.Add(paramFirstName);

                    SqlParameter paramLastName = new SqlParameter(DbConstants.User.DbParameter.LastName, entity.LastName);
                    cmd.Parameters.Add(paramLastName);

                    SqlParameter paramIsActive = new SqlParameter(DbConstants.User.DbParameter.IsActive, true);
                    cmd.Parameters.Add(paramIsActive);

                    SqlParameter paramPassword = new SqlParameter(DbConstants.User.DbParameter.Password, entity.Password);
                    cmd.Parameters.Add(paramPassword);

                    SqlParameter paramPhone = new SqlParameter(DbConstants.User.DbParameter.Phone, entity.Phone);
                    cmd.Parameters.Add(paramPhone);

                    SqlParameter paramAddress = new SqlParameter(DbConstants.User.DbParameter.Address, entity.Address);
                    cmd.Parameters.Add(paramAddress);

                    SqlParameter paramCity = new SqlParameter(DbConstants.User.DbParameter.City, entity.City);
                    cmd.Parameters.Add(paramCity);

                    SqlParameter paramState = new SqlParameter(DbConstants.User.DbParameter.State, entity.State);
                    cmd.Parameters.Add(paramState);

                    SqlParameter paramCountry = new SqlParameter(DbConstants.User.DbParameter.Country, entity.Country);
                    cmd.Parameters.Add(paramCountry);
                    
                    SqlParameter paramUserType = new SqlParameter(DbConstants.User.DbParameter.UserType, Enums.UserType.Administrator);
                    cmd.Parameters.Add(paramUserType);

                    SqlParameter paramTokan = new SqlParameter(DbConstants.User.DbParameter.Tokan, entity.Tokan);
                    cmd.Parameters.Add(paramTokan);

                    cn.Open();
                    var id = cmd.ExecuteScalar();
                    entity.Id = TypeConversionUtility.ToInteger(id);
                    isAddUpdated = entity.Id > 0;
                }
                return isAddUpdated;
            }
        }
        #endregion

        #region Get the User Details

        /// <summary>
        ///  Get the User Details
        /// </summary>
        /// <param name="userId">The User ID to be loaded.</param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="isActive"></param>      
        /// <param name="userType"></param>
        /// <returns>Object of <see cref="User"/>.</returns>
        public User GetUser(int? userId, string password, string email, bool? isActive, Enums.UserType? userType = null)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sqlHelper.GetSqlCommand(DbConstants.User.StoreProcedure.usp_SelectUser, cn, true))
                {
                    SqlParameter paramUserId = new SqlParameter(DbConstants.User.DbParameter.Id, userId);
                    cmd.Parameters.Add(paramUserId);

                    SqlParameter paramPassword = new SqlParameter(DbConstants.User.DbParameter.Password, password);
                    cmd.Parameters.Add(paramPassword);

                    SqlParameter paramEmail = new SqlParameter(DbConstants.User.DbParameter.Email, email);
                    cmd.Parameters.Add(paramEmail);

                    SqlParameter paramUserType = new SqlParameter(DbConstants.User.DbParameter.UserType, userType);
                    cmd.Parameters.Add(paramUserType);

                    SqlParameter paramIsActive = new SqlParameter(DbConstants.User.DbParameter.IsActive, isActive);
                    cmd.Parameters.Add(paramIsActive);

                    User user = null;
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                user = new User();
                                user.Id = TypeConversionUtility.ToInteger(reader[DbConstants.User.DbColumn.Id]);
                                user.FirstName = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.FirstName]);
                                user.LastName = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.LastName]);
                                user.Email = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.Email]);                               
                                user.IsActive = TypeConversionUtility.ToBoolean(reader[DbConstants.User.DbColumn.IsActive]);
                                user.Password = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.Password]);
                                user.Phone = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.Phone]);
                                user.FullName = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.FullName]);
                                user.Address = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.Address]);
                                user.City = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.City]);
                                user.State = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.State]);
                                user.Country = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.Country]);
                                user.UserType = (Enums.UserType)TypeConversionUtility.ToByte(reader[DbConstants.User.DbColumn.UserType]);
                                user.Tokan = TypeConversionUtility.ToStringWithNull(reader[DbConstants.User.DbColumn.Tokan]);
                                user.DateCreated = TypeConversionUtility.ToDateTime(reader[DbConstants.User.DbColumn.DateCreated]);
                            }
                        }
                    }
                    return user;
                }
            }
        }
  
        #endregion

    }//end class
}//end namespace