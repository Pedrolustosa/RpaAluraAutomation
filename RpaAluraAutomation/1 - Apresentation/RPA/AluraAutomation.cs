using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using RpaAluraAutomation.Application.Interfaces;

namespace RpaAluraAutomation.Apresentation.RPA;

public class AluraAutomation(ICursoService cursoService)
{
    private readonly ICursoService _cursoService = cursoService ?? throw new ArgumentNullException(nameof(cursoService));

    public async Task BuscarECadastrarTodosCursosAsync(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
            throw new ArgumentException("O termo de busca não pode ser nulo ou vazio.", nameof(termo));

        Console.WriteLine("Inicializando automação...");

        var options = new ChromeOptions();
        options.AddArgument("--ignore-certificate-errors");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-software-rasterizer");
        options.AddArgument("--disable-extensions");

        using var driver = new ChromeDriver(options);
        Console.WriteLine("Navegador inicializado.");

        try
        {
            driver.Navigate().GoToUrl("https://www.alura.com.br/");
            Console.WriteLine("Navegando para a página da Alura.");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var cursosVisitados = new HashSet<string>();

            Console.WriteLine($"Buscando pelo termo: {termo}");
            var searchBox = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("header__nav--busca-input")));
            searchBox.SendKeys(termo);
            searchBox.Submit();
            Console.WriteLine("Busca submetida com sucesso.");

            Console.WriteLine("Aplicando filtro de cursos...");
            var checkboxCurso = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("busca--filtro")));
            checkboxCurso.Click();

            var botaoFiltrar = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".busca-form-botao.--desktop")));
            botaoFiltrar.Click();

            while (true)
            {
                try
                {
                    Console.WriteLine("Procurando cursos na página atual...");
                    var cursosListados = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                        By.CssSelector(".paginacao-pagina .busca-resultado")));

                    bool todosCursosVisitados = true;

                    foreach (var cursoItem in cursosListados)
                    {
                        var link = cursoItem.FindElement(By.ClassName("busca-resultado-link")).GetAttribute("href");

                        if (cursosVisitados.Contains(link))
                        {
                            Console.WriteLine($"Curso já visitado: {link}");
                            continue;
                        }

                        cursosVisitados.Add(link);

                        Console.WriteLine($"Navegando para o curso: {link}");
                        driver.Navigate().GoToUrl(link);

                        Console.WriteLine("Capturando informações do curso...");

                        string titulo = ObterTextoElemento(driver, wait, By.ClassName("curso-banner-course-title"), "Título não encontrado");
                        string cargaHoraria = ObterTextoElemento(driver, wait, By.ClassName("courseInfo-card-wrapper-infos"), "Carga horária não encontrada");
                        string professor = ObterTextoElemento(driver, wait, By.ClassName("instructor-title--name"), "Professor não encontrado");
                        string descricao = ObterDescricaoCurso(driver);

                        Console.WriteLine($"Título: {titulo}");
                        Console.WriteLine($"Carga Horária: {cargaHoraria}");
                        Console.WriteLine($"Professor: {professor}");
                        Console.WriteLine($"Descrição: {descricao}");

                        Console.WriteLine("Salvando curso no banco de dados...");
                        await _cursoService.InserirCursoAsync(titulo, professor, cargaHoraria, descricao);
                        Console.WriteLine("Curso armazenado no banco de dados com sucesso!");

                        Console.WriteLine("Voltando para a página de resultados...");
                        driver.Navigate().Back();
                        todosCursosVisitados = false;
                    }

                    if (todosCursosVisitados)
                    {
                        Console.WriteLine("Todos os cursos da página atual foram visitados.");
                    }

                    var botaoProximaPagina = ObterElemento(driver, By.CssSelector(".busca-paginacao-prevNext.busca-paginacao-linksProximos"));

                    if (botaoProximaPagina != null && !botaoProximaPagina.GetAttribute("class").Contains("busca-paginacao-prevNext--disabled"))
                    {
                        Console.WriteLine("Indo para a próxima página...");
                        botaoProximaPagina.Click();
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma próxima página encontrada. Finalizando...");
                        break;
                    }
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine($"Erro: Elemento esperado não encontrado. {ex.Message} Finalizando...");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar curso: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro geral na automação: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Encerrando automação.");
            driver.Quit();
        }
    }

    private static string ObterTextoElemento(IWebDriver driver, WebDriverWait wait, By by, string mensagemPadrao)
    {
        try
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by)).Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Aviso: {mensagemPadrao}. {ex.Message}");
            return mensagemPadrao;
        }
    }

    private static string ObterDescricaoCurso(IWebDriver driver)
    {
        try
        {
            var elementosDescricao = driver.FindElements(By.ClassName("course-list"));
            return string.Join("\n", elementosDescricao.Select(e => e.Text));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Aviso: Descrição não encontrada. {ex.Message}");
            return "Descrição não encontrada";
        }
    }

    private static IWebElement? ObterElemento(IWebDriver driver, By by)
    {
        try
        {
            return driver.FindElement(by);
        }
        catch (NoSuchElementException)
        {
            return null;
        }
    }
}
