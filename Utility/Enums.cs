using System.ComponentModel;

namespace Utility
{
    public class Enums
    {
        public enum ExceptionType
        {
            NoException,
            Warning,
            Error,
            Fault
        }

      
        public enum UserLoginStatus :byte
        {
            InvalidCredentials = 0,
            Success = 1,
            AccountPending = 2,
            AccountBanned = 3,
            AccountDisapproved = 4,
            EmptyUserNameOrPassword = 5,
            DealerLoginRestrict = 6

        }

        public enum UserType : byte
        {
            [Description("Administrator")] Administrator = 1,
           
        }

      
        public enum PaymentStatus : byte
        {
            [Description("None")] NotDefined = 0,
            [Description("Paid")] Paid = 1,
            [Description("Fail")] Fail = 2
        }

        public enum AddressType : byte
        {
            Billing = 0, //Billing address is default AddressType
            Shipping = 1
        }       

        

    }//end enum
}//end class