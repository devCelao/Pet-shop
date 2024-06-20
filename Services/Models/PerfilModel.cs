using Infrastructure.Data.Usuario;
using System.ComponentModel;

namespace Services.Models;

public class PerfilModel
{
    [Description("Código de acesso")]
    public string COD_USUARIO { get; set; }
    [Description("Nome")]
    public string NOM_USUARIO { get; set; }
    [Description("E-mail")]
    public string? TXT_EMAIL { get; set; }
    [Description("Está ativo?")]
    public bool IND_ATIVO { get; set; } 
    [Description("Data de cadastro")]
    public DateTime DT_REGISTRO { get; set; }
    [Description("Data da última alteração")]
    public DateTime DT_ALTERACAO { get; set; }

    public PerfilModel MapToPerfilModel(Usuario usuario)
    {
        var retorno = new PerfilModel
        {
            COD_USUARIO = usuario.COD_USUARIO,
            NOM_USUARIO = usuario.NOM_USUARIO,
            IND_ATIVO = usuario.IND_ATIVO,
            DT_REGISTRO = usuario.DT_REGISTRO,
            TXT_EMAIL = usuario.TXT_EMAIL,
            DT_ALTERACAO = usuario.DT_ALTERACAO
        };

        return retorno;
    }

    public Usuario MapToUsuario()
    {
        var retorno = new Usuario
        {
            COD_USUARIO = this.COD_USUARIO,
            NOM_USUARIO = this.NOM_USUARIO,
            IND_ATIVO = this.IND_ATIVO,
            DT_REGISTRO = this.DT_REGISTRO,
            TXT_EMAIL = this.TXT_EMAIL,
            DT_ALTERACAO = this.DT_ALTERACAO
        };

        return retorno;
    }

    public IList<PerfilModel> MapListToPerfilModel(IList<Usuario> usuarios)
    {
        var retorno = new List<PerfilModel>();

        foreach (var usuario in usuarios)
        {
            retorno.Add(MapToPerfilModel(usuario));
        }

        return retorno;
    }
}
