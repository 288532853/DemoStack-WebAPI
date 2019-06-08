using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{    public class DbConstants
    {
        public const string ConnectionString = "DSConnectionString";
        public const string DatabaseObjectOwner = "dbo";
        public const string ErrorConnection = "Error:Connection String Not Exists";

        public class Parameter
        {
            public const string StartPage = "@StartPage";
            public const string PageLength = "@PageLength";
            public const string NoOfPages = "@NoOfPages";
            public const string TotalRecords = "@TotalRecords";
            public const string SearchKeyword = "@SearchKeyword";
            public const string StartDate = "@StartDate";
            public const string EndDate = "@EndDate";
            public const string EntityType = "@EntityType";
        }

        public class DbColumn
        {
            public const string EntityIdIntTableType = "EntityIdIntTableType";
        }

        public class StoreProcedure
        {
            public const string EntityIdIntTableType = "@EntityIdIntTableType";
        }


        #region User Related Constant
        public class User
        {
            public class StoreProcedure
            {
                /*Store Procedure Related to User*/
                public const string usp_SelectUser = "usp_SelectUser";
                public const string usp_AddUpdateUser = "usp_AddUpdateUser";

            }

            public class DbColumn
            {
                public const string Id = "Id";
                public const string UserName = "UserName";
                public const string FullName = "FullName";
                public const string Email = "Email";
                public const string Password = "Password";
                public const string FirstName = "FirstName";
                public const string LastName = "LastName";
                public const string Phone = "Phone";
                public const string Address = "Address";
                public const string City = "City";
                public const string State = "State";
                public const string Country = "Country";
                public const string UserType = "UserType";
                public const string IsActive = "IsActive"; 
                public const string DateCreated = "DateCreated";
                public const string Tokan = "Tokan";
            }

            public class DbParameter
            {
                public const string Id = "@Id";
                public const string UserName = "@UserName";
                public const string Email = "@Email";
                public const string EmailAddress = "@EmailAddress";
                public const string Password = "@Password";
                public const string FirstName = "@FirstName";
                public const string LastName = "@LastName";
                public const string Phone = "@Phone";
                public const string Address = "@Address";
                public const string City = "@City";
                public const string State = "@State";
                public const string Country = "@Country";
                public const string UserType = "@UserType";                                
                public const string IsActive = "@IsActive";
                public const string Tokan = "@Tokan";

            }
        }
        #endregion

        #region Enquiry Related Constant
        public class Enquiry
        {
            public class StoreProcedure
            {
                /*Store Procedure Related to Enquiry*/
                public const string usp_AddUpdateEnquiry = "usp_AddUpdateEnquiry";
                public const string usp_SelectEnquiry = "usp_SelectEnquiry";
                public const string usp_DeleteEnquiry = "usp_DeleteEnquiry";

            }

            public class DbColumn
            {
                public const string Id = "Id";
                public const string Name = "Name";
                public const string PhoneNo = "PhoneNo";
                public const string Email = "Email";
                public const string Comment = "Comment";
                public const string StateName = "StateName";

            }

            public class DbParameter
            {
                public const string Id = "@Id";
                public const string Name = "@Name";
                public const string PhoneNo = "@PhoneNo";
                public const string Email = "@Email";
                public const string Comment = "@Comment";
                public const string StateId = "@StateId";

            }
        }
        #endregion
    }
}