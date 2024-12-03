using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlgoCode.Application.Helpers;

public static class EnumHelper
{
    public static SelectList GetEnumSelectList<TEnum>() where TEnum : Enum
    {
        var enumValues = Enum.GetValues(typeof(TEnum))
                             .Cast<TEnum>()
                             .Select(e => new SelectListItem
                             {
                                 Value = e.ToString(),
                                 Text = e.ToString()
                             });

        return new SelectList(enumValues, "Value", "Text");
    }
}
