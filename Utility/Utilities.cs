using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Utility
{
    public class StateEntity
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }

    }
    public class CountryEntity
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

    }
    public class Utilities
    {

        public static string AdminEmail
        {
            get
            {
                return ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.AdminEmail);
            }
        }

        public static bool IsEmailEnable
        {
            get { return ConfigUtility.GetBoolFromConfig(WebConstant.ConfigKey.IsEmailEnable); }
        }

        public static bool IsEnableBccEmail
        {
            get { return ConfigUtility.GetBoolFromConfig(WebConstant.ConfigKey.IsEnableBccEmail); }
        }
       
        public static string SenderEmailAddress
        {
            get { return ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.SenderEmailAddress); }
        }
        public static List<string> BccEmailAddressList
        {
            get
            {
                var bccEmail = ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.BccEmailAddress);
                var bccEmails = bccEmail.Split(',');

                var bccEmailList = new List<string>();
                if (bccEmails.Any())
                {
                    for (int index = 0; index < bccEmails.Count(); index++)
                    {
                        bccEmailList.Add(bccEmails[index]);
                    }
                }
                return bccEmailList;
            }
        }

        public static string FromEmail
        {
            get
            {
                return ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.FromEmail);
            }
        }

        public static string SendGridAPIKey
        {
            get
            {
                return ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.SendGridAPIKey);
            }
        }

        public static string FromName
        {
            get
            {
                return ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.FromName);
            }
        }

        public static string BccEmailAddress
        {
            get
            {
                return ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.BccEmailAddress);
            }
        }

        public static string CcEmailAddress
        {
            get
            {
                return ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.CcEmailAddress);
            }
        }
      
        public static string GetWebSiteUrl()
        {
            string websiteUrl = ConfigUtility.GetStringFromConfig(WebConstant.ConfigKey.WebSiteUrl);
            if (!string.IsNullOrWhiteSpace(websiteUrl) && websiteUrl.EndsWith(WebConstant.BackSlash))
                websiteUrl = websiteUrl.TrimEnd(new[] { '/' });
            return websiteUrl;
        }

        #region Gets the random name of the user
        /// <summary>
        /// Generates the random number.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GenerateRandomNumber()
        {
            return string.Format("{0}", new Random().Next(1, 999999).ToString().PadLeft(6, '0'));
        }

        #endregion

        #region Generate Random Number
        /// <summary>
        /// Generate Random Number
        /// </summary>
        /// <returns></returns>
        public static string RandomNumber()
        {
            return RandomNumber(5);
        }

        public static string RandomNumber(int noOfChar)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, noOfChar)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static string IntRandomNumber(int noOfChar)
        {
            var chars = "123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, noOfChar)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        #endregion

        #region Convert List to DataTable
        /// <summary>
        /// To the data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion

        public static string GetCurrentPageName()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            var info = new FileInfo(path);
            return info.Name;
        }

        public static string GetRelativePath()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            return path;
        }

        public static string ResolveVirtual(string physicalPath)
        {
            string virtualUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(physicalPath))
            {
                var r = System.Web.Hosting.HostingEnvironment.MapPath("~/").TrimEnd();
                if (r != null)
                {
                    virtualUrl = physicalPath.Substring(r.Length).Replace('\\', '/').Insert(0, "/");
                }
            }
            if (!string.IsNullOrWhiteSpace(virtualUrl) && virtualUrl.EndsWith(WebConstant.BackSlash))
                virtualUrl = virtualUrl.TrimEnd(new[] { '/' });

            return string.Format("{0}{1}", virtualUrl, WebConstant.BackSlash);
        }
       
        public static string FormatAsCurrency(string strRawMoney)
        {
            var us = new System.Globalization.CultureInfo("en-US");
            decimal newMoney = 0.0m;

            if (strRawMoney.Trim() != "")
                try
                {
                    newMoney = Decimal.Parse(strRawMoney, us);
                }
                catch { }

            return newMoney.ToString("C", us);
        }

        #region Populate State With XML
        /// <summary>
        /// Populate State With XML
        /// </summary>
        /// <param name="ddlState"></param>
        public static void PopulateStateWithXml(DropDownList ddlState)
        {
            string stateXmlPath = ConfigUtility.GetStringFromConfig(UtilityConstants.StateXmlFilePath);
            DataSet ds = new DataSet();
            ds.ReadXml(stateXmlPath);
            var stateDataTeble = ds.Tables[UtilityConstants.StateXMLTableName];

            ddlState.DataSource = stateDataTeble;
            ddlState.DataTextField = UtilityConstants.StateOption;
            ddlState.DataValueField = UtilityConstants.StateValue;
            ddlState.DataBind();
        }
        public static List<StateEntity> PopulateStateWithXml()
        {
            List<StateEntity> lstState = new List<StateEntity>();
            string stateXmlPath = ConfigUtility.GetStringFromConfig(UtilityConstants.StateXmlFilePath);
            DataSet ds = new DataSet();
            ds.ReadXml(stateXmlPath);
            var stateDataTeble = ds.Tables[UtilityConstants.StateXMLTableName];
            if (stateDataTeble != null && stateDataTeble.Rows.Count > 0)
            {
                foreach (DataRow dr in stateDataTeble.Rows)
                {
                    StateEntity objState = new StateEntity
                    {
                        StateCode = TypeConversionUtility.ToStringWithNull(dr[UtilityConstants.CountryValue]),
                        StateName = TypeConversionUtility.ToStringWithNull(dr[UtilityConstants.CountryOption])
                    };
                    lstState.Add(objState);
                }
            }
            return lstState;
        }
        public static List<CountryEntity> PopulateCountryWithXml()
        {
            List<CountryEntity> lstCountry = new List<CountryEntity>();
            string countryXmlPath = ConfigUtility.GetStringFromConfig(UtilityConstants.CountryXmlFilePath);
            DataSet ds = new DataSet();
            ds.ReadXml(countryXmlPath);
            var countryDataTeble = ds.Tables[UtilityConstants.StateXMLTableName];
            if (countryDataTeble != null && countryDataTeble.Rows.Count > 0)
            {
                foreach (DataRow dr in countryDataTeble.Rows)
                {
                    CountryEntity objCountry = new CountryEntity
                    {
                        CountryCode = TypeConversionUtility.ToStringWithNull(dr[UtilityConstants.CountryValue]),
                        CountryName = TypeConversionUtility.ToStringWithNull(dr[UtilityConstants.CountryOption])
                    };
                    lstCountry.Add(objCountry);
                }
            }
            return lstCountry;
        }
        #endregion

        #region Populate Country With XML
        /// <summary>
        /// Populate Country With XML
        /// </summary>
        /// <param name="ddlCountry"></param>
        public static void PopulateCountryWithXml(DropDownList ddlCountry)
        {
            string countryXmlPath = ConfigUtility.GetStringFromConfig(UtilityConstants.CountryXmlFilePath);
            DataSet ds = new DataSet();
            ds.ReadXml(countryXmlPath);
            var countries = ds.Tables[UtilityConstants.CountryXmlTableName];

            ddlCountry.DataSource = countries;
            ddlCountry.DataTextField = UtilityConstants.CountryOption;
            ddlCountry.DataValueField = UtilityConstants.CountryValue;
            ddlCountry.DataBind();
            ddlCountry.SelectedValue = WebConstant.CountryNameUsa;
        }
        #endregion       

        /// <summary>
        /// Replaces the character.
        /// </summary>
        /// <param name="textToReplace">The text to replace.</param>
        /// <returns></returns>
        public static string ReplaceCharacter(string textToReplace)
        {
            if (!string.IsNullOrEmpty(textToReplace))
            {
                var stringBuilder = new StringBuilder(Regex.Replace(textToReplace.Trim(), @"\s+", "-"));
                stringBuilder.Replace(stringBuilder.ToString(),
                    Regex.Replace(stringBuilder.ToString(),
                        @"[~]+|[!]+|[?]+|[']+|[\]+|[.]+|[//]+|[|]+|[:]+|[;]+|[,]+|[@]+|[#]+|[$]+|[%]+|[/^]+|[&]+|[*]+|[*]+|[(]+|[)]+|[[]+|[]]+|[{]+|[}]+|[<]+|[>]+|[""]",
                        "-"));

                stringBuilder.Replace(stringBuilder.ToString(),
                    Regex.Replace(stringBuilder.ToString(),
                        "[-]+",
                        "-"));

                return stringBuilder.Replace(stringBuilder.ToString(),
                    Regex.Replace(stringBuilder.ToString(),
                        @"[-]+$",
                        "")).ToString().ToLower();
            }
            else
            {
                return textToReplace;
            }
        }

        #region Returns the physical path where the file has to be uploaded
        /// <summary>
        /// Returns the physical path where the file has to be uploaded
        /// </summary>
        /// <returns></returns>
        public static string GetDirectoryPhysicalPath()
        {
            return GetDirectoryPhysicalPath(WebConstant.ConfigKey.PhysicalFilePath);
        }
        public static string GetDirectoryPhysicalPath(string physicalPathKey)
        {
            string physicalPath = ConfigUtility.GetStringFromConfig(physicalPathKey);
            var dirpath = new DirectoryInfo(physicalPath);
            if (!dirpath.Exists)
            {
                dirpath.Create();
            }
            return string.Format("{0}{1}", physicalPath, WebConstant.BackSlash);
        }
        #endregion

        public static string GetSavedFileVirtualPath()
        {
            return GetSavedFileVirtualPath(WebConstant.ConfigKey.PhysicalFilePath);
        }

        public static string GetSavedFileVirtualPath(string physicalPathConfigKey)
        {
            string filePath = ResolveVirtual(ConfigUtility.GetStringFromConfig(physicalPathConfigKey));
            if (!string.IsNullOrWhiteSpace(filePath) && filePath.StartsWith(WebConstant.BackSlash))
                filePath = filePath.TrimStart(new[] { '/' });

            return string.Format("{0}{1}", WebConstant.BackSlash, filePath);
        }

        public static string UploadFile(FileUpload fileUpload, string physicalPathKey, string fileName = null)
        {
            string savedFileName = string.Empty;
            if ((fileUpload.PostedFile != null) && (fileUpload.PostedFile.ContentLength > 0))
            {
                string physicalDirectoryPath = GetDirectoryPhysicalPath(physicalPathKey);

                string filenameWithoutExt = Path.GetFileNameWithoutExtension(fileUpload.PostedFile.FileName);
                string filenameWithExt = string.Format("{0}{1}", ReplaceCharacter(filenameWithoutExt), Path.GetExtension(fileUpload.PostedFile.FileName));
                string newGuid = Guid.NewGuid().ToString();
                newGuid = newGuid.Substring(0, 10);
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    savedFileName = fileName;
                }
                else
                {
                    savedFileName = string.Format("{0}{1}", newGuid, filenameWithExt);
                }

                string saveLocation = string.Format("{0}\\{1}", physicalDirectoryPath, savedFileName);
                fileUpload.PostedFile.SaveAs(saveLocation);
            }
            if (!string.IsNullOrWhiteSpace(savedFileName))
                savedFileName = Path.Combine(GetSavedFileVirtualPath(physicalPathKey), savedFileName);

            return savedFileName;
        }

        public static string UploadFile(HtmlInputFile fileUpload, string physicalPathKey)
        {
            string savedFileName = string.Empty;
            if ((fileUpload.PostedFile != null) && (fileUpload.PostedFile.ContentLength > 0))
            {
                var physicalPath = GetDirectoryPhysicalPath(physicalPathKey);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(fileUpload.PostedFile.FileName);
                string filenameWithExt = string.Format("{0}{1}", ReplaceCharacter(filenameWithoutExt),
                    Path.GetExtension(fileUpload.PostedFile.FileName));
                string newGuid = Guid.NewGuid().ToString();
                newGuid = newGuid.Substring(0, 10);
                savedFileName = string.Format("{0}{1}", newGuid, filenameWithExt);
                string saveLocation = string.Format("{0}\\{1}", physicalPath, savedFileName);
                fileUpload.PostedFile.SaveAs(saveLocation);
            }
            if (!string.IsNullOrWhiteSpace(savedFileName))
                savedFileName = Path.Combine(GetSavedFileVirtualPath(physicalPathKey), savedFileName);

            return savedFileName;
        }

        public static void DownloadFile(string filePathWithName)
        {
            if (!string.IsNullOrWhiteSpace(filePathWithName))
            {
                string path = HttpContext.Current.Server.MapPath(filePathWithName);
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                   HttpContext.Current.Response.Clear();
                   HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                   HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                   HttpContext.Current.Response.ContentType = "application/octet-stream";
                   HttpContext.Current.Response.WriteFile(file.FullName);
                   HttpContext.Current.Response.End();
                }
            }
        }
        
        #region Encode the querystring
        public static string EncodeQueryString(string Id)
        {
            //add enrypted Entryid in query string
            string id = EncryptDecrypt.Encrypt(Id);

            //replace "/" with "-" to avoid "/" issue in URLs
            id = id.Replace("/", "@");

            return id;
        }
        #endregion

        #region Decode the querystring
        public static string DecodeQueryString(string queryString)
        {
            string id = string.Empty;
            id = queryString.Replace(" ", "+");

            //DECODE: since we replaced "/" with "-" to avoid "/" issue in URLs
            // reverse it to get the right url
            id = id.Replace("@", "/");
            //add decrypt BlogEntryID from query string
            id = EncryptDecrypt.Decrypt(id);
            return id;
        }
        #endregion
     
        public static string Getmd5HashCode(string prminput)
        {

            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(prminput));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x4"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string GenerateToken(string username, string password, string ip, DateTime time, string ticket)
        {
            string hash = string.Join(":", new string[] {  username, password, EncryptDecrypt.Encrypt(ip), time.ToString() });
            return Getmd5HashCode(hash);
        }

    }//end class
}//end namespace