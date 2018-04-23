using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;


namespace SeleniumTests
{

    [TestClass]
    public class TestBase
    {
        protected IWebDriver driver;
        private TestContext m_testContext;
        protected WebDriverWait wait;
        private string baseURL;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;

        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestInitialize]
        public void BaseTestInit()
        {
            string browser = Convert.ToString(m_testContext.Properties["browser"]);

            switch (browser)
            {
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    var options = new InternetExplorerOptions { EnsureCleanSession = true };
                    driver = new InternetExplorerDriver(options);
                    break;
                case "Edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    //driver = new FirefoxDriver();
                    //options = new InternetExplorerOptions { EnsureCleanSession = true };
                    //driver = new InternetExplorerDriver(options);
                    break;
            }

            baseURL = "https://anuncie.homol.zapimoveis.com.br/";

            if (m_testContext.Properties["url"] != null)
            {
                baseURL = Convert.ToString(m_testContext.Properties["url"]);
            }

            verificationErrors = new StringBuilder();
            try
            {
                driver.Manage().Cookies.DeleteAllCookies();
            }
            catch (Exception)
            { }

            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, new TimeSpan(0, 2, 9));
        }

        protected void GoToUrl(string pathAndQuery)
        {
            driver.Navigate().GoToUrl(baseURL + pathAndQuery);

        }


        [TestCleanup]
        public void BaseTestCleanup()
        {
            try
            {
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Quit();

            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

 
        public void TransacaoVenda()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("btnTipoTransacao1")));
            driver.FindElement(By.Id("btnTipoTransacao1")).Click();
        }

        public void TransacaoAluguel()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("btnTipoTransacao2")));
            driver.FindElement(By.Id("btnTipoTransacao2")).Click();
        }

        public void TipoDeImovel()
        {

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ddlSubTipoImovel")));
            new SelectElement(driver.FindElement(By.Id("ddlSubTipoImovel"))).SelectByText("Hotel");
            new SelectElement(driver.FindElement(By.Id("ddlSubTipoImovel"))).SelectByText("Apartamento Padrão");
            Thread.Sleep(1000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ddlCategoriaImovel")));
            new SelectElement(driver.FindElement(By.Id("ddlCategoriaImovel"))).SelectByText("Padrão");
        }

        public void LocalidadeImovel(string cep, string endereco, string carregaendereco, string num, string complemento)
        {
            driver.FindElement(By.Id("txtCepImovel")).SendKeys(cep);
            driver.FindElement(By.Id("txtCepImovel")).SendKeys(Keys.Tab);
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Id("txtLogradouro"), carregaendereco));        
            driver.FindElement(By.Id("txtLogradouro")).SendKeys(endereco);         
            driver.FindElement(By.Id("txtNumero")).SendKeys(num);         
            driver.FindElement(By.Id("txtComplemento")).SendKeys(complemento);
        }


        public void CaracteristicaDoImovel(string quartos, string suites, string vagagaragem, string areautil, string banheiro, string areatotal, string descricao)
        {
            driver.FindElement(By.Id("txtQtdDormitorios")).SendKeys(quartos);
            driver.FindElement(By.Id("txtQtdSuites")).SendKeys(suites);
            driver.FindElement(By.Id("txtQtdVagas")).SendKeys(vagagaragem);
            driver.FindElement(By.Id("txtAreaUtil")).SendKeys(areautil);
            driver.FindElement(By.Id("txtQtdBanheiros")).SendKeys(banheiro);
            driver.FindElement(By.Id("txtAreaTotal")).SendKeys(areatotal);
            driver.FindElement(By.Id("areaObservacao")).SendKeys(descricao);
        }

        public void QuantoCusta(string preco)
        {
            driver.FindElement(By.Id("txtPreco")).SendKeys(preco);
        }

        public void SeusDados(string email, string senha, string Nome, string celular, string fone)
        {
            // Clica no Botão de Cadastre-se 

            Thread.Sleep(3000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("btnCadastre")));
            driver.FindElement(By.Id("btnCadastre")).Click();
            //Preenche campos
            driver.FindElement(By.Id("txtUsuarioEmail")).SendKeys(email);
            driver.FindElement(By.Id("txtUsuarioSenha")).SendKeys(senha);
            driver.FindElement(By.Id("txtUsuarioNome")).SendKeys(Nome);
            driver.FindElement(By.Id("txtUsuarioCelular")).SendKeys(celular);
            driver.FindElement(By.Id("txtUsuarioTelefone")).SendKeys(fone);

        }

        public void Continuar(string botao)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("btnSalvar")));
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Id("btnSalvar"), botao));
            Thread.Sleep (1000);   
            driver.FindElement(By.Id("btnSalvar")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("icone-plano")));
        }

        public void CupomDesconto(string CupomDesconto)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fldCupomDesconto")));
            driver.FindElement(By.Id("fldCupomDesconto")).SendKeys(CupomDesconto);
            driver.FindElement(By.Id("DadosAnunciante_Nome")).Click();

        }

       // Precisa de Manutenção
        //public void SelecionaPlano()
        //{
            
        //    // driver.FindElement(By.Id("fldPlano_2993")).Click();*/

        //}

        public void AceitaContrato()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("fldContratoPlano")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fldContratoPlano")));
            ExecutaEventoPorID("fldContratoPlano");
        }

        public void DadosFaturamentoPF(string nome, string email, string emailalternativo, string tel, string cel, string cpf)
        {
            wait.Until(ExpectedConditions.ElementExists (By.Id("fldPessoaFisica")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("fldPessoaFisica")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fldPessoaFisica")));
           // Thread.Sleep(2000);
           // driver.FindElement(By.Id("fldPessoaFisica")).Click();
           // Thread.Sleep(2000);
           // driver.FindElement(By.Id("fldPessoaFisica")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("DadosAnunciante_Nome")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("DadosAnunciante_Nome")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("DadosAnunciante_Nome")));
            driver.FindElement(By.Id("DadosAnunciante_Nome")).Clear();
            driver.FindElement(By.Id("DadosAnunciante_Nome")).SendKeys(nome);
            driver.FindElement(By.Id("DadosAnunciante_Email")).Clear();
            driver.FindElement(By.Id("DadosAnunciante_Email")).SendKeys(email);
            driver.FindElement(By.Id("DadosAnunciante_EmailAlternativo")).SendKeys(emailalternativo);
            driver.FindElement(By.Id("Telefone")).SendKeys(tel);
            driver.FindElement(By.Id("Celular")).SendKeys(cel);
            driver.FindElement(By.Id("DadosAnunciante_CPFCNPJ")).Clear();
            driver.FindElement(By.Id("DadosAnunciante_CPFCNPJ")).SendKeys(cpf);
        }

        public void SelecionaPessoaPJ()
        {
            
            driver.FindElement(By.Id("fldPessoaJuridica")).Click();

        }

        public void DadosFaturamentoPJ(string razaosocial, string email, string emailalternativo, string tel, string cel, string cnpj)
        {


            ExecutaEventoPorID("fldPessoaJuridica");

            driver.FindElement(By.Id("DadosAnunciante_Nome")).Clear();
            driver.FindElement(By.Id("DadosAnunciante_Nome")).SendKeys(razaosocial);
            driver.FindElement(By.Id("DadosAnunciante_Email")).Clear();
            driver.FindElement(By.Id("DadosAnunciante_Email")).SendKeys(email);
            driver.FindElement(By.Id("DadosAnunciante_EmailAlternativo")).Clear();
            driver.FindElement(By.Id("DadosAnunciante_EmailAlternativo")).SendKeys(emailalternativo);
            driver.FindElement(By.Id("Telefone")).Clear();
            driver.FindElement(By.Id("Telefone")).SendKeys(tel);
            driver.FindElement(By.Id("Celular")).Clear();
            driver.FindElement(By.Id("Celular")).SendKeys(cel);
            driver.FindElement(By.Id("DadosAnunciante_CPFCNPJ")).Clear();
            driver.FindElement(By.Id("DadosAnunciante_CPFCNPJ")).SendKeys(cnpj);
        }

        public void EnderecoDiferente(string cep, string endereco, string carregaendereco, string num, string complemento)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fldUsarNovoEnderecoFaturamento")));
            driver.FindElement(By.Id("fldUsarNovoEnderecoFaturamento")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("DadosAnunciante_Endereco_CEP")));
            driver.FindElement(By.Id("DadosAnunciante_Endereco_CEP")).SendKeys(cep);
            driver.FindElement(By.Id("DadosAnunciante_Endereco_CEP")).SendKeys(Keys.Tab);
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Id("DadosAnunciante_Endereco_Logradouro"), carregaendereco));
            driver.FindElement(By.Id("DadosAnunciante_Endereco_Logradouro")).SendKeys(endereco);
            driver.FindElement(By.Id("DadosAnunciante_Endereco_Numero")).SendKeys(num);
            driver.FindElement(By.Id("DadosAnunciante_Endereco_Complemento")).SendKeys(complemento);
            driver.FindElement(By.Id("DadosAnunciante_Endereco_Complemento")).SendKeys(Keys.Tab);
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Id("DadosAnunciante_Endereco_Logradouro"), carregaendereco));

        }

        public void FormaPagamentoCartao(string numcartao, string nomeimpresso, string codseguranca)
        {
            driver.FindElement(By.Id("fldPagtoCartao")).Click();
            driver.FindElement(By.Id("DadosAnunciante_CartaoCredito_NumeroCartao")).SendKeys(numcartao);
            driver.FindElement(By.Id("DadosAnunciante_CartaoCredito_NomePortador")).SendKeys(nomeimpresso);
            new SelectElement(driver.FindElement(By.Id("MesValidadeCartao"))).SelectByText("08");
            new SelectElement(driver.FindElement(By.Id("AnoValidadeCartao"))).SelectByText("18");
            driver.FindElement(By.Id("DadosAnunciante_CartaoCredito_CodigoSeguranca")).SendKeys(codseguranca);
            driver.FindElement(By.Id("btnPagarCartao")).Click();

        }

        public void FormaPagamentoBoletoCupomDesconto()
        {
            Thread.Sleep(3000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("fldPagtoBoleto")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fldPagtoBoleto")));
            driver.FindElement(By.Id("fldPagtoBoleto")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fldPagtoBoleto")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("fldPagtoBoleto")));
            Thread.Sleep(2000);
            driver.FindElement(By.Id("fldPagtoBoleto")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("btnPagarBoleto")));
            driver.FindElement(By.Id("btnPagarBoleto")).Click();
            
            }


        public void FormaPagamentoBoleto()
        {

            Thread.Sleep(1000);
            wait.Until(ExpectedConditions.ElementExists(By.Id("fldPagtoBoleto")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fldPagtoBoleto")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("fldPagtoBoleto")));
            driver.FindElement(By.Id("fldPagtoBoleto")).Click();
            driver.FindElement(By.Id("btnPagarBoleto")).Click();
        }

        public void DetalhesDoImovel(string condominio, string iptu, string anoconstrucao, string numandares, string uniandar)
        {
            Thread.Sleep(1000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("icone-foto")));
            driver.FindElement(By.Id("txtValorCondominio")).SendKeys(condominio);
            driver.FindElement(By.Id("txtValorIptu")).SendKeys(iptu);
            driver.FindElement(By.Id("txtAnoConstrucao")).SendKeys(anoconstrucao);
            driver.FindElement(By.Id("txtAndares")).SendKeys(numandares);
            driver.FindElement(By.Id("txtUnidadesPorAndar")).SendKeys(uniandar);
        }

        public void OutrasCaracteristicas()
        {
            driver.FindElement(By.Id("Caracteristicas_155")).Click();
            driver.FindElement(By.Id("Caracteristicas_162")).Click();
            driver.FindElement(By.Id("Caracteristicas_160")).Click();

        }

        public void AreasComuns()
        {
            driver.FindElement(By.Id("Caracteristicas_8")).Click();
            driver.FindElement(By.Id("Caracteristicas_38")).Click();
            driver.FindElement(By.Id("Caracteristicas_55")).Click();
        }

        public void SalvarFinalizar()
        {
            driver.FindElement(By.Id("SalvarFinalizar")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("bg-home-finalizar")));
        }

        protected void ExecutaEventoPorID(string IDdoCampo, string evento = "click")
        {
            IWebElement webElement = driver.FindElement(By.Id(IDdoCampo));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0]." + evento + "();", webElement);
        }


        // Gerador de Numeros Aleatorio


        public String GerarNumero()

        {
            string numero = string.Empty;
            Random rndNumero = new Random();
            numero = rndNumero.Next().ToString();

            return numero;
        }

        public string GerarCnpj()
        {
            Int64 soma1, soma2, i, parte1, parte2, parte3, dig1, parte5, parte6, parte7, dig2;
            Int64[] numero = new Int64[13];
            var rand = new Random();
            string cnpjCompleto=string.Empty;

            for (i = 1; i <= 8; i++)
            {
                numero[i] = (rand.Next()) % 9;
            }
            numero[9] = 0;
            numero[10] = 0;
            numero[11] = 0;
            numero[12] = (rand.Next()) % 9;
            //*==========================================*
            //|       Primeiro digito veridicador        |
            //*==========================================*
            soma1 = ((numero[1] * 5) +
                  (numero[2] * 4) +
                  (numero[3] * 3) +
                  (numero[4] * 2) +
                  (numero[5] * 9) +
                  (numero[6] * 8) +
                  (numero[7] * 7) +
                  (numero[8] * 6) +
                  (numero[9] * 5) +
                  (numero[10] * 4) +
                  (numero[11] * 3) +
                  (numero[12] * 2));
            parte1 = (soma1 / 11);
            parte2 = (parte1 * 11);
            parte3 = (soma1 - parte2);
            dig1 = (11 - parte3);
            if (dig1 > 9) dig1 = 0;
            //*==========================================*
            //|        Segundo digito veridicador        |
            //*==========================================*
            soma2 = ((numero[1] * 6) +
                  (numero[2] * 5) +
                  (numero[3] * 4) +
                  (numero[4] * 3) +
                  (numero[5] * 2) +
                  (numero[6] * 9) +
                  (numero[7] * 8) +
                  (numero[8] * 7) +
                  (numero[9] * 6) +
                  (numero[10] * 5) +
                  (numero[11] * 4) +
                  (numero[12] * 3) +
                  (dig1 * 2));
            parte5 = (soma2 / 11);
            parte6 = (parte5 * 11);
            parte7 = (soma2 - parte6);
            dig2 = (11 - parte7);
            if (dig2 > 9) dig2 = 0;
            for (i = 1; i <= 12; i++)
            {
                //numeros do CNPJ
                cnpjCompleto += Convert.ToString(numero[i]);
            }
            // dois últimos digitos
            return cnpjCompleto += dig1 + "" + dig2;
        }
    public String GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        // Generate Random Email Address 
        public static string GenerateEmailAddress()
        {
            return "teste" + GetRandomNumber() + "@terra.com";
        }

        // Generate random number for email address
        public static int GetRandomNumber()
        {
            return new Random().Next(100000, 100000000);
        }


        // Gerador de Senhas

        public string GerarSenhas()
        {
            int Tamanho = 10; // Numero de digitos da senha
            string senha = string.Empty;
            for (int i = 0; i < Tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!senha.Contains(_char))
                    {
                        senha += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return senha;
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
