using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumTests;

namespace AnuncieAntigo
{
    [TestClass]
    public class Venda : TestBase
    {
        [TestMethod]
        public void AnuncioVendaPFBoleto()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoVenda();
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","2", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            //SeusDados("teste.boleto.adyen@testebla.com.br", "123456", "Romero Teste", "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());
            //DadosFaturamentoPF("Romero Teste", "teste.bla@testebla.com.br", "", "11" + GerarNumero(), "", GerarCpf());

            FormaPagamentoBoleto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            Assert.AreEqual("Obrigado por anunciar no ZAP!", driver.FindElement(By.CssSelector("h1.pull-left")).Text);


        }

        [TestMethod]
        public void AnuncioVendaPFCartao()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoVenda();
            TipoDeImovel();
            LocalidadeImovel("02305001","", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","2", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());

            FormaPagamentoCartao("4111111111111111","Cartão PF","123");

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            var kibonToNoPosto = driver.FindElement(By.ClassName("bg-home-finalizar")).Text;

            Assert.IsTrue(kibonToNoPosto.Contains("Obrigado por anunciar no ZAP!"), driver.FindElement(By.Id("hdnStatusImovel")).Text);

        }

        [TestMethod]
        public void AnuncioVendaBolCupom()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoVenda();
            TipoDeImovel();
            LocalidadeImovel("02305001","", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            // Sempre ao Executar Verificar se o Cupom de Desconto ainda é válido e se existe na tabela 
            CupomDesconto("ANUNCIETESTE_PRODUCAO0458962");

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());

            FormaPagamentoBoletoCupomDesconto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            var kibonToNoPosto = driver.FindElement(By.ClassName("bg-home-finalizar")).Text;

            Assert.IsTrue(kibonToNoPosto.Contains("Obrigado por anunciar no ZAP!"), driver.FindElement(By.Id("hdnStatusImovel")).Text);

        }

        [TestMethod]
        public void AnuncioVendaEnderecoCorrespondencia()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoVenda();
            TipoDeImovel();
            LocalidadeImovel("02305001","", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60", "3", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());

            EnderecoDiferente("01310000", "", "Avenida Paulista", "15", "Endereço Diferente");

            FormaPagamentoBoleto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            var kibonToNoPosto = driver.FindElement(By.ClassName("bg-home-finalizar")).Text;

            Assert.IsTrue(kibonToNoPosto.Contains("Obrigado por anunciar no ZAP!"), driver.FindElement(By.Id("hdnStatusImovel")).Text);

        }

    }
}
