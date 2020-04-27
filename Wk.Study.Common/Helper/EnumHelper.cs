using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Wk.Study.Common.Helper
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this int val) where T : struct
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
            {
                throw new Exception("必须是枚举类型");
            }
            var enumValue = default(T);
            if (!Enum.TryParse(val.ToString(), out enumValue))
            {
                throw new Exception($"枚举值{val}不正确");
            }
            var attr = typeof(T).GetTypeInfo().DeclaredFields.FirstOrDefault(w => String.Compare(w.Name, enumValue.ToString(), true) == 0)
                                 .GetCustomAttribute<DescriptionAttribute>();
            if (attr == null)
            {
                throw new Exception("枚举值未定义Description特性");
            }
            return attr.Description;
        }

        public static string GetDescription(this Enum val)
        {
            var member = val.GetType().GetMembers().Where(w => w.MemberType == MemberTypes.Field).FirstOrDefault(w => w.Name == val.ToString());
            if (member != null)
            {
                var attr = member.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                return attr?.Description;
            }
            return "";
        }
        public static int GetIntValue<T>(this T val) where T : struct
        {
            return Convert.ToInt32(val);
        }
        public static short GetShortValue<T>(this T val) where T : struct
        {
            return Convert.ToInt16(val);
        }
        public static IEnumerable<SelectListItem> BuildSelectItemsForEnum<T>(int selectedValue = 0) where T : struct
        {
            if (typeof(T).GetTypeInfo().IsEnum == false)
            {
                throw new Exception("只支持枚举类型");
            }
            var values = (Enum.GetValues(typeof(T)) as T[]);
            return values.Select(w => new SelectListItem()
            {
                Text = Convert.ToInt32(w).GetDescription<T>(),
                Value = Convert.ToInt32(w).ToString(),
                Selected = Convert.ToInt32(w) == selectedValue
            });
        }
    }
}
