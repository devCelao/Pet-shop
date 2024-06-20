using Infrastructure.Data.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using Services.Models;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;
[Authorize]
public class PerfilController : BaseController
{
    protected readonly IPerfilService _service;
    public PerfilController(IOptions<ClientSettings> clientSettings, 
                            IPerfilService service) : base(clientSettings)
    {
        _service = service;
    }
    [HttpGet("cadastros")]
    public async Task<IActionResult> Index() => View(await _service.GetUsuariosSistema());

    [HttpGet("cadastros/novo")]
    public IActionResult NovoCadastro() => View();

    [HttpPost("cadastros/cadastrar")]
    public async Task<IActionResult> Cadastrar(PerfilModel model)
    {
        if (!ModelState.IsValid) return View("NovoCadastro", model);

        var erros = await _service.CadastrarPerfil(model);

        if(erros != null && erros.Any())
        {
            AdicionaErrosProcessamento(erros);
            return View("NovoCadastro", model);
        }

        AdicionaAlerta("Cadastro de perfil realizado com sucesso! E-mail para criação de senha enviado. Verifique a caixa de e-mails.");

        return RedirectToAction("Index");
    }

    [HttpPost("cadastros/desativar-cadastro/{id}")]
    public async Task<IActionResult> DesativarCadastro(string id)
    {
        var erros = await _service.Desativar(id);
        if (erros != null && erros.Any())
            AdicionaErrosProcessamento(erros);

        return RedirectToAction("Index");
    }

    [HttpGet("cadastros/deletar-cadastro/{id}")]
    public async Task<IActionResult> DeletarCadastro(string id)
    {
        var erros = await _service.Deletar(id);
        if (erros != null && erros.Any())
            AdicionaErrosProcessamento(erros);

        return RedirectToAction("Index");
    }

    [HttpPost("cadastros/ativar-cadastro/{id}")]
    public async Task<IActionResult> AtivarCadastro(string id)
    {
        var erros = await _service.Ativar(id);
        if (erros != null && erros.Any())
            AdicionaErrosProcessamento(erros);

        return RedirectToAction("Index");
    }

    [HttpGet("cadastros/editar-cadastro/{id}")]
    public async Task<IActionResult> EditarCadastro(string id) => View(new PerfilModel().MapToPerfilModel(await _service.BuscarUsuario(id)));

    [HttpPost("cadastros/salvar-alteracoes")]
    public async Task<IActionResult> Update(PerfilModel model)
    {
        if (!ModelState.IsValid) return View("EditarCadastro", model);

        var erros = await _service.AtualizarPerfil(model);

        if(erros != null && erros.Any())
        {
            AdicionaErrosProcessamento(erros);
            return View("EditarCadastro", model);
        }

        AdicionaAlerta("Dados atualizados com sucesso!");

        return RedirectToAction("Index");
    }
}
