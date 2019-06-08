namespace Utility
{
    public class WebConstant
    {
        public const string PagerTextBoxId = "PagerTextBoxID";

        public const string NotDefinedText = "Not Defined";
        public const string AttributesValue = "value";
        public const string JSON = "JSON";
        public const string XML = "XML";
        public const string ContentType = "application/x-www-form-urlencoded";
        public const string MethodPOST = "POST";
        public const string FontRedColorMessage = "<font color='red'>{0}</font>";
        public const string FontGreenColorMessage = "<font color='green'>{0}</font>";
        public const string XSEGMENT = "X-SEGMENT";
        public const string XSEGMENTValue = "WebAPI";
        public const string TextField = "Text";
        public const string ValueField = "Value";
        public const string KeyField = "Key";
        public const string PlusSymbol = " + ";
        public const string BackSlash = "/";
        public const string TexasStateName = "TX";
        public const string DefaultValue = "0";
        public const string Browser = "InternetExplorer";
        
        public const int PageLength = 20;
        public const string ActiveText = "Active";
        public const string InActiveText = "InActive";
        public const string YesText = "Yes";
        public const string NoText = "No";
        public const string DaysText = "Days";
        public const string DefaultSelectedCountry = "United States";
        public const string KeyText = "Key";
        public const string ValueText = "Value";
        public const string TypeText = "Type";
        public const string WhiteColorName = "white";
        public const string StyleDisplayNone = "display:none;";
        public const string StyleDisplayBlock = "display:block;";
        public const string StyleDisplayTableRow = "display:table-row;";
        public const string ColorRed = "color:Red;";
        public const string DataHref = "data-href";
        public const string Href = "href";
        public const string Style = "style";
        public const string CssClassAvailable = "available";
        public const string CssClassTaken = "taken";
        public const string CssClass = "class";
        public const string Text = "Text";
        public const string Value = "Value";
        public const string ApplicationName = "WebAPI";
        public const string CountryNameUsa = "United States";
        
        public class DateTimeFormat
        {
            public const string FormatDate = @"MM/dd/yyyy";
            public const string FormatDateJs = "MM/DD/YYYY";
            public const string FormatDateTimeJs = "MM/DD/YYYY h:mm a";
            public const string FormatTimeJs = "h:mm a";
            public const string FormatDateTime = "MM/dd/yyyy, hh:mm tt";
            public const string FormatDateTimeWithMonthName = "MMM dd, yyyy, hh:mm tt";
            public const string FormatDateMonthName = "MMM";
            public const string FormatDateName = "dd ddd";
            public const string FormatTime = "hh:mm tt";
            public const string FormatTimeSpan = @"hh\:mm tt";
            public const string FormatTimeCap = "HH:mm";
            public const string FormatTimeAMPM = "h:mm A";
        }

        public class Attributes
        {
            public const string CssClass = "class";
            public const string BackgroundColor = "background-color";
            public const string BackgroundColorImage = "background-image";
            public const string CssClassAvailable = "available";
            public const string CssClassTaken = "taken";
            public const string DataId = "data-id";
            public const string DataTicketId = "data-ticketid";
            public const string DataWarrantyId = "data-warrantyid";
            public const string DataPrice = "data-price";
            public const string DataPartId = "data-partid";
            public const string DataPartUnit = "data-unit";
            public const string AttributeTitle = "title";
            public const string AttributeStyle = "style";
        }
        public class EmailTemplate
        {
            public const string MailTemplate = "mailTemplate:";
            public const string TemplateReplaceFormat = "[[{0}]]";
            public const string ForgotPassword = "ForgotPassword.html";
            public const string LoginDetail = "LoginDetail.html";
            public const string Admin = "Admin.html";       

            public class EmailKey
            {
                public const string PasswordKey = "Password";
                public const string FirstNameKey = "FirstName";
                public const string FirstName = "FirstName";
                public const string LastName = "LastName";
                public const string EmailIdKey = "Email";
                public const string ApplicationNameKey = "ApplicationName";
                public const string InstructionDocKey = "InstructionDocURL";
                public const string TicketNoKey = "TicketNo";
                public const string NameKey = "Name";
                public const string WebSiteUrl = "WebSiteUrl";
                public const string CompanyNameKey = "CompanyName";
                public const string DateCreatedKey = "CreateDate";
                public const string AddressKey = "Address";
                public const string AmountKey = "Amount";
                public const string CityStateCountryKey = "CityStateCountry";
            }
        }
        public class Message
        {
            public const string ErrorMessage = "Something went wrong!!! Please try again.";
            public const string SuccessMessage = "Record has been added successfully.";
            public const string UpdateMessage = "Record has been updated successfully.";
            public const string WarningMessage = "Server busy!!!Please reload.";
            public const string SomethingWentWrongMessage = "Something went wrong!!! Please try again.";
            public const string DuplicateMessage = "Record already exists.";
            public const string InvalidMessage = "Invalid credentials!!!";
            public const string DeleteMessage = "Record has been deleted successfully.";
            //public const string Message = "TicketNo";
            //public const string Message = "Name";
           
        }
        public class ViewStateKey
        {
            public const string AddEstimatePartViewStateKey = "AddEstimatePartViewStateKey";
        }
        public class SessionKey
        {
            public const string UrlReferrer = "UrlReferrer";
        }
        public class TableTypeColumn
        {
            public const string EntityId = "EntityId";
            public const string Id = "Id";
        }

        public class ConfigKey
        {
            public const string StateXmlPath = "StateXMLPath";
            public const string PhysicalFilePath = "PhysicalFilePath";
            public const string BccEmailAddress = "BccEmailAddress";
            public const string EmailTemplatePath = "EmailTemplatePath";
            public const string WebSiteUrl = "WebSiteUrl";
            public const string IsEmailEnable = "IsEmailEnable";
            public const string IsEnableBccEmail = "IsEnableBccEmail";           
            public const string SenderEmailAddress = "SenderEmailAddress";
            public const string CcEmailAddress = "CCEmailAddress";         
            public const string PaymentGatewayType = "PaymentGatewayType";

            public const string AdminEmail = "AdminEmail";
            public const string FromEmail = "FromEmail";
            public const string SendGridAPIKey = "SendGridAPIKey";
            public const string FromName = "FromName";

        }
        public class QueryString
        {
            public const string PagerQueryString = "p";
            public const string PageLengthQueryString = "pageLength";
            public const string ReturnUrlQueryString = "ReturnUrl";
            public const string CategoryIdQueryString = "cId";
            public const string UserIdQueryString = "uId";
            public const string ListPageNameIdQueryString = "lstPage";
            public const string ViewQueryString = "view";
            public const string SearchQueryQueryString = "q";
            public const string StatusQueryString = "status";
            public const string CustomerIdQueryString = "customerId";           

        }

        public class CommandName
        {
            public const string Delete = "Delete";
            public const string Activate = "Activate";
            public const string DeActivate = "DeActivate";
        }
    }//end class
}//end namespace