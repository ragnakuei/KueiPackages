namespace KueiPackages.Microsoft.AspNetCore;

public static class ControllerExtensions
{
    /// <summary>
    /// Generate View To String
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="viewName"></param>
    /// <param name="model"></param>
    /// <param name="partial"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public static async Task<string> RenderViewAsync<TModel>(this Controller controller,
                                                             string          viewName,
                                                             TModel          model,
                                                             bool            partial = false)
    {
        if (string.IsNullOrEmpty(viewName))
        {
            viewName = controller.ControllerContext.ActionDescriptor.ActionName;
        }

        controller.ViewData.Model = model;

        using (var writer = new StringWriter())
        {
            var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

            var viewResult = viewEngine?.FindView(controller.ControllerContext, viewName, !partial);

            if (viewResult         == null
             || viewResult.Success == false)
            {
                return $"找不到對應的 View：{viewName}";
            }

            var viewContext = new ViewContext(controller.ControllerContext,
                                              viewResult.View,
                                              controller.ViewData,
                                              controller.TempData,
                                              writer,
                                              new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);

            return writer.GetStringBuilder().ToString();
        }
    }
}
