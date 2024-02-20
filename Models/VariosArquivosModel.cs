using System.ComponentModel.DataAnnotations;

namespace CarregarArquivosUnicos_Multiplos.Models;

public class VariosArquivosModel : RespostaModel
{
   [Required(ErrorMessage = "Selecione os arquivos")]
    public List<IFormFile> Arquivo { get; set; }
}