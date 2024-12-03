namespace AlgoCode.WebUI.Helpers;

public static class ActivePageHelper
{
    public static string IsActive(ViewContext viewContext, string controller, string action = null)
    {
        var currentController = viewContext.RouteData.Values["controller"]?.ToString();
        var currentAction = viewContext.RouteData.Values["action"]?.ToString();

        if (string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase) &&
            (string.IsNullOrEmpty(action) || string.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase)))
        {
            return "active";
        }

        return string.Empty;
    }
}

