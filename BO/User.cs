using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using System.ComponentModel;

namespace BO
{

    public class User
    {        
        public int Id { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }      
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Enums.UserType UserType { get; set; }       
        public bool IsAnonymous { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }       
        public bool IsDeleted { get; set; }
        public string Tokan { get; set; }
        public string UserTypeString
        {
            get
            {
                if (Enum.IsDefined(typeof(Enums.UserType), UserType))
                {
                    return Enum.IsDefined(typeof(Enums.UserType), UserType) ? EnumHelper.DescriptionAttr(UserType) : string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

       

    }//end class
}//end namespace
