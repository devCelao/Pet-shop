namespace Services;

public class BaseService
{
    protected IList<string> Erros = new List<string>();

    protected bool PossuiErros() => !Erros.Any();

    protected void AdicionarErroProcessamento(string erro) => Erros.Add(erro);

    protected void LimpaErrosProcessamento() => Erros.Clear();
    protected void AdicionaErrosProcessamento(IList<string> erros)
    {
        foreach (var erro in erros)
        {
            AdicionarErroProcessamento(erro);
        }
    }
}

