using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;

[Authorize]

public class HomeController : BaseController
{
    public HomeController(IOptions<ClientSettings> clientSettings) : base(clientSettings) { }

    public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [AllowAnonymous]
    [Route("/erro/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var modelErro = new ErrorViewModel();

        if (id == 500)
        {
            modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte";
            modelErro.Titulo = "Ocorreu um erro!";
            modelErro.ErroCode = id;
        }
        else if (id == 404)
        {
            modelErro.Mensagem = "A p�gina que est� procurando n�o existe! <br /> Em caso de d�vida entre em contato com nosso suporte";
            modelErro.Titulo = "Ops! P�gina n�o encontrada.";
            modelErro.ErroCode = id;

            //return RedirectToAction(actionName: "Index", controllerName: "Catalogo");
        }
        else if (id == 403)
        {
            modelErro.Mensagem = "Voc� n�o tem permiss�o para fazer isto.";
            modelErro.Titulo = "Acesso Negado";
            modelErro.ErroCode = id;
        }
        else
        {
            return StatusCode(404);
        }

        return View(viewName: "Error", model: modelErro);
    }
}
