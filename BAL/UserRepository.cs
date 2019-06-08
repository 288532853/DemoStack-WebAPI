using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BO;
using DAL;
using Utility;
namespace WebAPI.BAL
{
    public class UserRepository
    {
        #region Add/Update User
        /// <summary>
        /// Add/Update User
        /// </summary>
        /// <returns></returns>
        public static bool AddUpdateUser(User entity)
        {
            var sqlUser = new SqlUser();
            bool isAddUpdated = sqlUser.AddUpdateUser(entity);
            return isAddUpdated;
        }
        #endregion

        #region Gets the user
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="password"></param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="isActive">The is active.</param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static User FindUser(int? userId = null, string password = null, string email = null, bool IsEmailLogin = false, bool? isActive = null, Enums.UserType? userType = null)
        {
            SqlUser sqlUser = new SqlUser();
            return sqlUser.GetUser(userId, password, email, isActive, userType);

        }
        #endregion

        #region Logins the specified username
        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="emailAddress">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns></returns>
        public static Enums.UserLoginStatus Login(string email, string password = null, bool IsEmailLogin = false, bool rememberMe = false, Enums.UserType? userType = null)
        {
            Enums.UserLoginStatus userLoginStatus = Enums.UserLoginStatus.InvalidCredentials;
            User user = null;
            if (IsEmailLogin)
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    user = FindUser(email: email, userType: userType, IsEmailLogin: IsEmailLogin);
                    if (user != null)
                    {
                        userLoginStatus = !user.IsActive
                                    ? Enums.UserLoginStatus.AccountPending
                                    : Enums.UserLoginStatus.Success;
                    }
                    else
                    {
                        return Enums.UserLoginStatus.InvalidCredentials;
                    }
                }
                else
                {
                    return Enums.UserLoginStatus.InvalidCredentials;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return Enums.UserLoginStatus.EmptyUserNameOrPassword;
                }
                else
                {
                    //Load user details
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        user = FindUser(email: email, password: password, userType: userType);
                        if (user != null)
                        {
                            if (user.Password == password)
                            {
                                userLoginStatus = !user.IsActive
                                    ? Enums.UserLoginStatus.AccountPending
                                    : Enums.UserLoginStatus.Success;
                            }
                            else
                            {
                                return Enums.UserLoginStatus.InvalidCredentials;
                            }
                        }
                        else
                        {
                            return Enums.UserLoginStatus.InvalidCredentials;
                        }
                    }
                    else
                    {
                        return Enums.UserLoginStatus.InvalidCredentials;
                    }
                }
            }

            //if (userLoginStatus == Enums.UserLoginStatus.Success)
            //{
            //    /*
            //     * Create a persistemt cookie if Remember Me is checked (10 month approx)
            //     * Note: time in min. should be same as cookie expiration
            //     * The forms authentication feature only looks at the expiration time set on the forms authentication ticket. It never looks at the expiration date set on the cookie itself.  
            //     * The reason is that the forms authentication ticket is encrypted and digitally signed, so its payload can be trusted.  However a malicious user can easily forge an Http 
            //     * cookie with a fake expiration date.  The only purpose of the cookie's expiration date in forms authentication is that by explicitly setting an expiry date, the cookie 
            //     * will be persisted in the user's browser cache.  This how "remember me" functionality works - the forms auth ticket is packaged into a cookie and the cookie has an explicit
            //     * expiration date.  That way when a user returns to the site at some future point in time, the browser just automatically sends the forms auth cookie back to the server.  
            //     * However the server decrypts the payload of the cookie, and then looks at the expiration date contained in the payload (not the expiry date of the cookie itself) to determine 
            //     * if the forms auth ticket should still be considered valid.
            //     * Unless the intent is to make the cookie stick around on a user's machine, you don't need to set the Expiration property of the HttpCookie.  If you do want a persistent cookie, 
            //     * then when you manually issue it you need to ensure that the expiration date in the forms auth ticket and the expiry on the coookie itself are using the same duration.  
            //     * Otherwise you can end up in a weird situation where the date in the forms auth ticket and the date on the cookie don't match.
            //     */
            //    DateTime cookieExpires = DateTime.Now.AddHours(8);
            //    if (rememberMe)
            //    {
            //        cookieExpires = DateTime.Now.AddMonths(3);
            //    }
            //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, email, DateTime.Now, cookieExpires, rememberMe, password, FormsAuthentication.FormsCookiePath);
            //    string ticketString = FormsAuthentication.Encrypt(ticket);
            //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);

            //    cookie.Expires = cookieExpires;
            //    if (!string.IsNullOrEmpty(FormsAuthentication.CookieDomain))
            //    {
            //        cookie.Domain = FormsAuthentication.CookieDomain;
            //    }
            //    // This cookie should not be accessible via client script. (ex: document.cookie)
            //    // to prevent XSS attacks.
            //    cookie.HttpOnly = true;
            //    // Transmit the cookie over an SSL connection (HTTPS) if SSL is required for forms-authentication cookie. 
            //    cookie.Secure = FormsAuthentication.RequireSSL;

            //    HttpContext.Current.Response.Cookies.Add(cookie);
            //    if (UserContext.Current.Context.Session != null)
            //    {
            //        UserContext.Current.User = user;
            //    }


            //}
            return userLoginStatus;
        }

        #endregion

    }//end class
}//end namespace