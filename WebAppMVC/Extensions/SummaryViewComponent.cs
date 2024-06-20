using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Extensions;

public class SummaryViewComponent :ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("ErrosProcessamento");
    }
}
