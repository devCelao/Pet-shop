using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;

public class BaseController : Controller
{
    protected ClientSettings _client { get; set; }
    protected ICollection<string> Informacoes = new List<string>();
    public BaseController(IOptions<ClientSettings> clientSettings)
    {
        _client = clientSettings.Value;

        ViewData["ClientName"] = _client.TituloEmpresa;

    }

    protected void AdicionaErroProcessamento(string erro)
    {
        ModelState.AddModelError(string.Empty, erro);
    }

    protected void AdicionaErrosProcessamento(IList<string> erros)
    {
        foreach (var erro in erros)
        {
            AdicionaErroProcessamento(erro);
        }
    }

    protected void AdicionaAlerta(string alerta)
    {
        Informacoes.Add(alerta);

        SetAlerta();
    }

    protected void LimpaInformacoes()
    {
        Informacoes.Clear();

        SetAlerta();
    }

    protected void SetAlerta()
    {
        TempData["Infos"] = Informacoes;
    }

    #region X
    //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        // TODO: Lógica para enviar o email de recuperação de senha
    //        // Exemplo de lógica:
    //        var user = await _userManager.FindByEmailAsync(model.Email);
    //        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
    //        {
    //            // Não revele se o usuário não existe ou se o email não está confirmado
    //            return RedirectToAction(nameof(ForgotPasswordConfirmation));
    //        }

    //        // Gerar o token de redefinição de senha
    //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    //        var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

    //        // Enviar email
    //        await _emailSender.SendEmailAsync(model.Email, "Redefinir senha",
    //           $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>.");

    //        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    //    }

    //    // Se chegarmos até aqui, algo falhou; reapresente o formulário
    //    return View(model);
    //}

    #endregion
}
