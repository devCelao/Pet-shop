using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Extensions;

public class MenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("MenuLateral");
    }
}
