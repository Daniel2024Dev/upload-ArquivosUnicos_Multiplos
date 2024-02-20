using System.ComponentModel.DataAnnotations;
namespace CarregarArquivosUnicos_Multiplos.Models;

public class UnicoArquivoModels : RespostaModel
{
    [Required(ErrorMessage = "Insira o nome do arquivo")]
    public string? ArquivoNome { get; set; }

    [Required(ErrorMessage = "Selecione o arquivo")]
    public IFormFile Arquivo{ get; set; }
}