using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SampleProject.Common
{
    public class Utilities
    {
        public static List<SelectListItem> EnumToList(Type en)
        {
            var itemValues = en.GetEnumValues();
            var list = new List<SelectListItem>();
            foreach (var value in itemValues)
            {
                var name = en.GetEnumName(value);
                var member = en.GetMember(name).Single();
                var desc = ((DescriptionAttribute)member.GetCustomAttributes(typeof(DescriptionAttribute), false).Single()).Description;
                list.Add(new SelectListItem { Text = desc, Value = ((int)value).ToString() });
            }
            return list;
        }

    }
}
