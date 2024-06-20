using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppMVC.Models;
using Services;

namespace WebAppMVC.Controllers;

public class AccountController : BaseController
{
    
    private readonly IAuthService service;
    public AccountController(IOptions<ClientSettings> clientSettings,
                             IAuthService servico)
        : base(clientSettings)
    {
        service = servico;
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Index() => View("Login");

    [HttpGet]
    [Route("login/esqueci-a-senha")]
    public async Task<IActionResult> ForgotPass() => View();

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await service.RealizaLogout();

        return RedirectToAction("Index");
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Logar(AccountViewModel user)
    {
        if (!ModelState.IsValid) return View("Login",user);


        var errosLogin = await service.Logar(user.Username, user.Password);

        if (errosLogin.Any())
        {
            AdicionaErrosProcessamento(errosLogin);

            return View("Login", user);
        }

        return RedirectToAction("Index","Home");
    }



    [HttpPost]
    [Route("login/esqueci-a-senha")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid) return View("ForgotPass", model);

        // implementar

        AdicionaAlerta("Favor verificar sua caixa de email!");

        TempData["Infos"] = Informacoes;

        return RedirectToAction("Index");
    }
}
