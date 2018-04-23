using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumTests;

namespace AnuncieAntigo
{
    [TestClass]
    public class AluguelPJ : TestBase
    {
        [TestMethod]
        public void AnuncioAluguelPJBoleto()
        {
            // Acessa Anunciee
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");
            
            // Preenche Primeira Etapa do Funil
            TransacaoAluguel();
            TipoDeImovel();
            LocalidadeImovel("02305001","", "Avenida Tucuruvi", "2", "PJ Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1" , "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)
            AceitaContrato();
            SelecionaPessoaPJ();
            DadosFaturamentoPJ("Venda PJ Boleto", GenerateEmailAddress(), "", "119" + GerarNumero(), "11" + GerarNumero(), GerarCnpj());

            FormaPagamentoBoleto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            var kibonToNoPosto = driver.FindElement(By.ClassName("bg-home-finalizar")).Text;

            Assert.IsTrue(kibonToNoPosto.Contains("Obrigado por anunciar no ZAP!"), driver.FindElement(By.Id("hdnStatusImovel")).Text);

        }

        [TestMethod]
        public void AnuncioAluguelPJCartao()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoAluguel();
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PJ Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60", "1", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            SelecionaPessoaPJ();
            DadosFaturamentoPJ("Venda PJ Boleto", GenerateEmailAddress(), "", "119" + GerarNumero(), "11" + GerarNumero(), GerarCnpj());

            FormaPagamentoCartao("4111111111111111","Cartão PJ","123");

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            var kibonToNoPosto = driver.FindElement(By.ClassName("bg-home-finalizar")).Text;

            Assert.IsTrue(kibonToNoPosto.Contains("Obrigado por anunciar no ZAP!"), driver.FindElement(By.Id("hdnStatusImovel")).Text);

        }
    }
}
