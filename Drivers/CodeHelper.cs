using System;
using System.ComponentModel;
using System.Reflection;

namespace NessHappyNess.Drivers
{


    public static class Extensions

    {

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)

                where TAttribute : Attribute

        {

            return enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttribute<TAttribute>();

        }

    }



    class ValueAttribute : Attribute

    {

        internal ValueAttribute(string str)

        {

            this.Value = str;

        }



        public string Value

        {

            get;

            private set;

        }

    }



    public static class EnumsHelper

    {

        public enum E_Mode { Equals, Contains };

        public enum E_Attributes

        {

            [Description("class")]

            Class,

            [Description("style")]

            Style

        }



        public enum E_IconsClass

        {

            [Description("glyphicons-pencil")]

            pencil,

            [Description("glyphicons-remove")]

            x,

            [Description("glyphicons-chevron-down")]

            arrow_down,

            [Description("left")]

            arrow_left

        }



        public enum E_Signs

        {

            [Description("₪")]

            Shekel,

            [Description("%")]

            Percentage

        }





        public static string GetDescription(Enum e)

        {

            string descriptor;

            var type = e.GetType();

            var memInfo = type.GetMember(e.ToString());

            try

            {

                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                descriptor = ((DescriptionAttribute)attributes[0]).Description;

                return descriptor;

            }

            catch (Exception)

            {

                return e.ToString().Replace("_", " ");

            }

        }



        public static string GetValue(Enum e)

        {

            string value;

            var type = e.GetType();

            var memInfo = type.GetMember(e.ToString());

            try

            {

                var attributes = memInfo[0].GetCustomAttributes(typeof(ValueAttribute), false);

                value = ((ValueAttribute)attributes[0]).Value;

                return value;

            }

            catch (Exception)

            {

                return e.ToString().Replace("_", " ");

            }

        }



        public static T GetEnumValueFromDescription<T>(string description)

        {

            MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)

            {

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)

                    return (T)Enum.Parse(typeof(T), fi.Name);

            }

            throw new Exception("Not found");

        }



        public static T GetEnumValueFromValue<T>(string value)

        {

            MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)

            {

                ValueAttribute[] attributes = (ValueAttribute[])fi.GetCustomAttributes(typeof(ValueAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Value == value)

                    return (T)Enum.Parse(typeof(T), fi.Name);

            }

            throw new Exception("Not found");

        }



    }

}

