using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumTests;

namespace AnuncieNovo
{
    [TestClass]
    public class RevendaPF : TestBase
    {
        [TestMethod]
        public void RevendaPFBoleto()
        {
            // Acessar Revenda
             GoToUrl("/SobreImovel?transacao=revender&edicao=False");

            // Preenche Primeira Etapa do Funil - Revenda Boleto
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());

            FormaPagamentoBoleto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)

            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            //Verifica se o texto existe na tela

           Assert.AreEqual("Obrigado por anunciar no ZAP!", driver.FindElement(By.CssSelector("h1.pull-left")).Text);


        }
        [TestMethod]
        public void RevendaPFCartao()
        {
            // Acessar Revenda
            GoToUrl("/SobreImovel?transacao=revender&edicao=False");

            // Preenche Primeira Etapa do Funil - Revenda Cartão
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());

            FormaPagamentoCartao("4111111111111111", "Cartão PF", "123");

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            
            Assert.AreEqual("Obrigado por anunciar no ZAP!", driver.FindElement(By.CssSelector("h1.pull-left")).Text);

        }


    }
}
