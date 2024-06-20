namespace Infrastructure;

public class BaseRepository
{
    protected IList<string> Erros { get; set; } = new List<string>();

    protected void AdicionaErroProcessamento(string mensagem)
    {
        Erros.Add(mensagem);
    }
}
