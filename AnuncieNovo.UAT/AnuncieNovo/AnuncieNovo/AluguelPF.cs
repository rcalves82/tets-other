using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumTests;

namespace AnuncieAntigo
{
    [TestClass]
    public class AluguelPF : TestBase
    {
        [TestMethod]
        public void AnuncioAluguelPFBoleto()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoAluguel();
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "112" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil)

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());

            FormaPagamentoBoleto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            var kibonToNoPosto = driver.FindElement(By.ClassName("bg-home-finalizar")).Text;

            Assert.IsTrue(kibonToNoPosto.Contains("Obrigado por anunciar no ZAP!"), driver.FindElement(By.Id("hdnStatusImovel")).Text);

        }

        [TestMethod]
        public void AnuncioAluguelPFCartao()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoAluguel();
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1", "60", "Teste");
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
        public void AnuncioAluguelBolCupom()
        {
            // Acessa Anuncie 
            // 
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoAluguel();
            TipoDeImovel();
            LocalidadeImovel("02305001", "", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1", "60", "Teste");
            QuantoCusta("300000");
            SeusDados(GenerateEmailAddress(), GerarSenhas(), "Solange Silva " + GerarSenhas(), "119" + GerarNumero(), "11" + GerarNumero());
            Continuar("SALVAR E CONTINUAR");

            // Selecionar Plano (Segunda Etapa do Funil) 

            // Sempre ao Executar Verificar se o Cupom de Desconto ainda é válido e se existe na tabela 
            CupomDesconto("ANUNCIETESTE_PRODUCAO0458962");

            AceitaContrato();
            DadosFaturamentoPF("Solange Silva", GenerateEmailAddress(), "", "11" + GerarNumero(), "", GerarCpf());
            driver.FindElement(By.Id("fldCupomDesconto")).Click();
            FormaPagamentoBoletoCupomDesconto();

            // Terceira Etapa Funil (FOTOS E DETALHES OPCIONAIS)
            DetalhesDoImovel("100", "", "", "", "");
            SalvarFinalizar();

            // Verifica se o texto existe na tela

            var kibonToNoPosto = driver.FindElement(By.ClassName("bg-home-finalizar")).Text;

            Assert.IsTrue(kibonToNoPosto.Contains("Obrigado por anunciar no ZAP!"), driver.FindElement(By.Id("hdnStatusImovel")).Text);

        }

        [TestMethod]
        public void AnuncioAluguelEnderecoCorrespondencia()
        {
            // Acessa Anuncie
            GoToUrl("/SobreImovel?transacao=vender&edicao=False");

            // Preenche Primeira Etapa do Funil
            TransacaoAluguel();
            TipoDeImovel();
            LocalidadeImovel("02305001","", "Avenida Tucuruvi", "2", "PF Boleto");
            CaracteristicaDoImovel("3", "1", "2", "60","1", "60", "Teste");
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
