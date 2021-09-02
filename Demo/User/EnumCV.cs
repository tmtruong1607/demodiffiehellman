using System;
using System.ComponentModel;
using System.Reflection;

namespace Demo.User
{
    public enum CVenum
    {
        [Description("Giáo viên")]
        GiaoVien = 1,
        [Description("Giáo vụ")]
        GiaoVu = 2
    }
    public static class EnumExtensionMethods
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }

}
