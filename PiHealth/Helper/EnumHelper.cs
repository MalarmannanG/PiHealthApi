using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Options;

namespace PiHealth.Web.Helper
{
    public static class EnumHelper
    {
        public static IEnumerable<NameIdPair> ToSelectListItems(this Type enumType, string selected = "")
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
            {
                throw new ArgumentException("Type must be enum");
            }
            var names = Enum.GetNames(enumType);
            var values = Enum.GetNames(enumType).Cast<string>();

            var items = names.Zip(values, (name, value) => new NameIdPair
            {

                Text = GetDisplay(enumType, name),
                Value = (name == "type") ? "" : value.ToString(),
                Selected = selected == value.ToString()


            });
            return items;
        }


        public static string GetDisplay(this Type enumType, string name)
        {
            return enumType.GetDisplayOrNameForEnum(name);
        }

        public static string GetDisplayOrNameForEnum(this Type type, string name)
        {
            var result = name;
            var attribute = type.GetField(name).GetCustomAttributes(inherit: false).OfType<DisplayAttribute>().FirstOrDefault();
            if (attribute != null)
                result = attribute.GetName();
            return result;
        }

        public static IEnumerable<NameIdPair> ToSelectListItems<T>(
        this IEnumerable<T> items,
        Func<T, string> nameSelector,
        Func<T, string> valueSelector,
        Func<T, bool> selected)
        {
            return items.OrderBy(item => nameSelector(item))
                   .Select(item =>
                           new NameIdPair
                           {
                               Selected = selected(item),
                               Text = nameSelector(item),
                               Value = valueSelector(item)
                           });
        }

        public static IEnumerable<NameIdPair> GetItems(this Type enumType, string selected = "")
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
            {
                throw new ArgumentException("Type must be enum");
            }
            var names = Enum.GetNames(enumType);
            var values = Enum.GetNames(enumType).Cast<string>();

            var items = names.Zip(values, (name, value) => new NameIdPair
            {

                Text = GetDisplay(enumType, name),
                Value = (name == "type") ? "" : value.ToString(),
                Selected = selected == value.ToString()


            });
            return items;
        }
    }
}
