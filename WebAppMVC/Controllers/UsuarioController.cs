using Core.Entities;
using Infrastructure.Data.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;

[Authorize]
public class UsuarioController : BaseController
{
    private readonly IUsuarioService _service;

    public UsuarioController(IOptions<ClientSettings> clientSettings,
                                         IUsuarioService servico)
        : base(clientSettings)
    {
        _service = servico;
    }

    [HttpGet]
    [Route("usuarios")]
    public async Task<IActionResult> Index()
    {

        var retorno = await _service.GetUsuariosStatus();

        return View(retorno);
    }

    [HttpPost]
    [Route("usuarios/cadastrar")]
    public async Task<IActionResult> Cadastrar(UsuarioSistema usuarioRegistro)
    {
        if(!ModelState.IsValid) View(usuarioRegistro);

        var usuario = ForRegistro(usuarioRegistro);

        var erros = await _service.Registrar(usuario);

        if (erros.Any())
        {
            AdicionaErrosProcessamento(erros);

            return View("Cadastrar", usuario);
        }

        AdicionaAlerta("Usuario cadastrado com sucesso!");

        return RedirectToAction("Index");
    }

    [HttpPost("usuarios/remover")]
    public async Task<IActionResult> AlterarStatusAtivo([FromForm] string codUsuario, [FromForm] bool indAtivo)
    {

        var erros = await _service.AlterarStatusAtivo(codUsuario, indAtivo);

        if(erros.Any())
        {
            AdicionaErrosProcessamento(erros);

            return RedirectToAction("Index");
        }

        AdicionaAlerta("Ação realizada com sucesso!");
        

        return RedirectToAction("Index");
    }

    private Usuario ForRegistro(UsuarioSistema user)
    {
        return new Usuario
        {
            ID = new Guid(),
            COD_USUARIO = user.Username,
            NOM_USUARIO = user.NomeUsuario,
            DT_REGISTRO = DateTime.Now,
            IND_ATIVO = true,
            IND_EMAIL_CONFIRMADO = false,
            TXT_SENHA = user.Password,
            TXT_EMAIL = user.Email,
            DT_ALTERACAO = DateTime.Now
        };
    }
}
