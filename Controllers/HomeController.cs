using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarregarArquivosUnicos_Multiplos.Models;

namespace CarregarArquivosUnicos_Multiplos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult UnicoArquivo()
    {   
        //Passando os objetos de UnicoArquivoModels para View
        UnicoArquivoModels N1 = new UnicoArquivoModels();
        return View(N1);
    }

    // Recebe objetos  UnicoArquivoModels por parâmetro
    [HttpPost]
    public IActionResult UnicoArquivo(UnicoArquivoModels unico_arquivo_models)
    {
        if (ModelState.IsValid)
        {
            unico_arquivo_models.Resposta = true;
            //diretório dos arquivos
            string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

            //criar pasta se não existir
            if (!Directory.Exists(Caminho))
                Directory.CreateDirectory(Caminho);
            /*
            objetos vindo de SingleFileModel 
                Arquivo    ArquivoNome
            */
            //obter extensão de arquivo
            FileInfo InformaçõesDoArquivo = new FileInfo(unico_arquivo_models.Arquivo.FileName);

            //NomeArquivo >>> informações do arquivo
            string NomeArquivo = unico_arquivo_models.ArquivoNome + InformaçõesDoArquivo.Extension;
            
            //Concatena Caminho + ArquivoNome por parâmetro dentro de NomeDoArquivoComCaminho
            string NomeDoArquivoComCaminho = Path.Combine(Caminho, NomeArquivo);

            //Função que faz a copia dos arquivos e evita erros
            using (var fluxo = new FileStream(NomeDoArquivoComCaminho, FileMode.Create))
            {
                unico_arquivo_models.Arquivo.CopyTo(fluxo);
            }

            //Retorno para UnicoArquivo por parâmetro
            unico_arquivo_models.Sucesso = true;
            unico_arquivo_models.Mensagem = "Upload de arquivo com sucesso";
        }//Retorno para UnicoArquivo em View
        return View("UnicoArquivo", unico_arquivo_models);
    }

    public IActionResult VariosArquivos()
    {
        //Passando os objetos de VariosArquivos para View
        VariosArquivosModel N1 = new VariosArquivosModel();
        return View(N1);
    }

    // Recebe objetos VariosArquivos por parâmetro
    [HttpPost]
    public IActionResult VariosArquivos(VariosArquivosModel varios_arquivos_model)
    {
        if (ModelState.IsValid)
        {
            varios_arquivos_model.Resposta = true;

            if (varios_arquivos_model.Arquivo.Count > 0)
            {
                /*
                    Recebe Arquivo por parâmetro de List em VariosArquivosModel
                    ((( varios_arquivos_model.Arquivo ))) >>> instancia de VariosArquivosModel
                */
                foreach (var N1 in varios_arquivos_model.Arquivo)
                {
                    //diretório dos arquivos
                    string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                    //criar pasta se não existir
                    if (!Directory.Exists(Caminho))
                        Directory.CreateDirectory(Caminho);

                    //Concatena Caminho + NomeArquivo por parâmetro dentro de NomeDoArquivoComCaminho
                    string NomeDoArquivoComCaminho = Path.Combine(Caminho, N1.FileName);

                    //Função que faz a copia dos arquivos e evita erros
                    using (var fluxo = new FileStream(NomeDoArquivoComCaminho, FileMode.Create))
                    {
                        N1.CopyTo(fluxo);
                    }
                }
                //Retorno para VariosArquivos por parâmetro
                varios_arquivos_model.Sucesso = true;
                varios_arquivos_model.Mensagem = "Arquivos carregados com sucesso";
            }
            else
            {   //Retorno para VariosArquivos por parâmetro
                varios_arquivos_model.Sucesso = false;
                varios_arquivos_model.Mensagem = "Selecione os arquivos";
            }
        }//Retorno para VariosArquivos em View
        return View("VariosArquivos", varios_arquivos_model);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
