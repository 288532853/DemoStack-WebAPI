using System;
using System.Globalization;
using System.Web;
using System.IO;

namespace Utility
{
    public static class EmailTemplate
    {
        static readonly string EmailTemplatePath = ConfigUtility.GetStringFromConfig(UtilityConstants.EmailTemplatePath);
      
        // read the text in template file and return it as a string
        private static string ReadFileFrom(string templateName)
        {
            string filePath = Path.Combine(EmailTemplatePath, templateName);
            string body = File.ReadAllText(filePath);
            return body;
        }
        // get the template body, cache it and return the text
        private static string GetMailBodyOfTemplate(string templateName)
        {
            string cacheKey = string.Concat(UtilityConstants.MailTemplate, templateName);
            string body;
            body = (string)HttpContext.Current.Cache[cacheKey];
            if (string.IsNullOrEmpty(body))
            {
                //read template file text
                body = ReadFileFrom(templateName);

                if (!string.IsNullOrEmpty(body))
                {
                    HttpContext.Current.Cache.Insert(cacheKey, body, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }

            return body;
        }
        // replace the tokens in template body with corresponding values
        public static string PrepareMailBodyWith(string templateName, params string[] pairs)
        {
            string body = GetMailBodyOfTemplate(templateName);

            for (var i = 0; i < pairs.Length; i += 2)
            {
                body = body.Replace(UtilityConstants.TemplateReplaceFormat.FormatWith(pairs[i]), pairs[i + 1]);
            }
            return body;
        }
        public static string FormatWith(this string target, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, target, args);
        }
    }//end class
}//end namespace