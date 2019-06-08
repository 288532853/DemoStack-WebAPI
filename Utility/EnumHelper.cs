using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Utility
{
    public static class EnumHelper
    {
       
        private static Hashtable GetEnumForBindWithHashTable(Type enumeration)
        {
            string[] names = Enum.GetNames(enumeration);
            Array values = Enum.GetValues(enumeration);
            var ht = new Hashtable();
            for (int i = 0; i < names.Length; i++)
            {
                ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
            }
            return ht;
        }
        private static IDictionary<byte, string> GetEnumForBindWithDictonary(Type enumeration)
        {
            var dictionary = new Dictionary<byte, string>();
            foreach (byte value in Enum.GetValues(enumeration))
            {
                var name = Enum.GetName(enumeration, value);
                dictionary.Add(value, name);
            }
            return dictionary;
        }
        private static IDictionary<int, string> GetEnumForBindWithIntDictonary(Type enumeration)
        {
            var dictionary = new Dictionary<int, string>();
            foreach (byte value in Enum.GetValues(enumeration))
            {
                var name = Enum.GetName(enumeration, value);
                dictionary.Add(value, name);
            }
            return dictionary;
        }
        public static Dictionary<int, string> GetDictIntEnumList<T>()
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new Exception("Type parameter should be of enum type");

            return Enum.GetValues(enumType).Cast<int>()
                       .ToDictionary(v => v, v => Enum.GetName(enumType, v));
        }
        public static Dictionary<byte, string> GetEnumList<T>()
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new Exception("Type parameter should be of enum type");
            return Enum.GetValues(enumType).Cast<object>().ToDictionary(k => (byte)k, v => ((Enum)v).GetEnumDescription());
        }
        public static string GetEnumDescription(this Enum enumeration)
        {
            string value = enumeration.ToString();
            Type enumType = enumeration.GetType();
            var descAttribute = (DescriptionAttribute[])enumType
                .GetField(value)
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return descAttribute.Length > 0 ? descAttribute[0].Description : value;
        }

        public static string GetDescription(Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return value.ToString();
        }

        public static Dictionary<T, string> EnumToDictionary<T>()
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
                throw new ArgumentException("T must be of type System.Enum");

            return Enum.GetValues(enumType).Cast<T>().ToDictionary(k => k, v => GetDescription((v as Enum)));
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        public static IEnumerable<Tuple<string, byte>> GetEnumValuePairs(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException();
            }

            List<Tuple<string, byte>> result = new List<Tuple<string, byte>>();

            foreach (var value in Enum.GetValues(enumType))
            {
                var enumDesc = GetDescription(value as Enum);

                // ideally check if descAttribute is null here
                result.Add(Tuple.Create(enumDesc, (byte)value));
            }

            return result;
        }
        public static List<ListItem> GetListForListControls<T>()
        {
            var type = typeof(T);
            var list = new List<ListItem>();
            foreach (var value in Enum.GetValues(type))
            {
                var enumValue = (byte)value;
                var fi = value.GetType().GetField(value.ToString());
                var customAttributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                DescriptionAttribute attribute = null;
                if (customAttributes.Length > 0)
                    attribute = (DescriptionAttribute)customAttributes[0];
                var item = new ListItem
                {
                    Text = attribute != null
                               ? attribute.Description
                               : Enum.GetName(type, enumValue),
                    Value = (Convert.ToInt32(enumValue)).ToString()
                };

                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <param name="source"></param>
        /// <returns>A string representing the friendly name</returns>
        public static string DescriptionAttr<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) 
                return attributes[0].Description;
            else return source.ToString();
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
          //  throw new ArgumentException(string.Format("The value '{0}' does not match a valid enum name or description.", description));
            return default(T);
        }
    }//end class
}//end namespace