using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumTests;

namespace AnuncieNovo
{
    [TestClass]
    public class RevendaPJ : TestBase
    {
        [TestMethod]
        public void RevendaPJBoleto()
        {
            // Acessa Revenda
            GoToUrl("/SobreImovel?transacao=revender&edicao=False");

            // Preenche Primeira Etapa do Funil - Revenda Boleto
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","5", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPJ("Venda PJ Boleto", GenerateEmailAddress(), "", "119" + GerarNumero(), "11" + GerarNumero(), GerarCnpj());

            FormaPagamentoBoleto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)

            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            //Verifica se o texto existe na tela

            Thread.Sleep(2000);
            Assert.AreEqual("Obrigado por anunciar no ZAP!", driver.FindElement(By.CssSelector("h1.pull-left")).Text);


        }

        [TestMethod]
        public void RevendaPJCartao()
        {
            // Acessar Revenda
            GoToUrl("/SobreImovel?transacao=revender&edicao=False");

            // Preenche Primeira Etapa do Funil - Revenda Cartão
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","2", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPJ("Venda PJ Boleto", GenerateEmailAddress(), "", "119" + GerarNumero(), "11" + GerarNumero(), GerarCnpj());

            FormaPagamentoCartao("4111111111111111", "Cartão PF", "123");

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            Thread.Sleep(2000);
            Assert.AreEqual("Obrigado por anunciar no ZAP!", driver.FindElement(By.CssSelector("h1.pull-left")).Text);

        }

        [TestMethod]
        public void RevendaCupom()
        {
            // Acessar Revenda
            GoToUrl("/SobreImovel?transacao=revender&edicao=False");

            // Preenche Primeira Etapa do Funil - Revenda Cartão
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","3", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            // Sempre ao Executar Verificar se o Cupom de Desconto ainda é válido e se existe na tabela 
            CupomDesconto("ANUNCIETESTE_PRODUCAO0458962");

            AceitaContrato();
            DadosFaturamentoPJ("Venda PJ Boleto", GenerateEmailAddress(), "", "119" + GerarNumero(), "11" + GerarNumero(), GerarCnpj());
          //  driver.FindElement(By.Id("fldCupomDesconto")).Click();
            FormaPagamentoBoletoCupomDesconto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            Thread.Sleep(2000);
            Assert.AreEqual("Obrigado por anunciar no ZAP!", driver.FindElement(By.CssSelector("h1.pull-left")).Text);

        }

    }

}
