using Bergs.Bth.Bthsmoxn;
using Bergs.Bth.Bthstixn;
using Bergs.Bth.Bthstixn.MM4;
using Bergs.Pwx.Pwxoiexn;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcsctxn;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcuctxn.Tests
{
    ///  <summary>
    /// Contém os métodos de teste da classe Contrato.
    /// </summary>
    [TestFixture(Description = "Classe de testes para a classe RN Contrato.", Author = "E37076")]
    public class ContratoTests : AbstractTesteRegraNegocio<Contrato>
    {
        private TOContrato toContrato;

        #region Métodos de preparação dos testes
        ///  <summary>
        /// Executa uma ação UMA vez por classe, ANTES do início da execução dos métodos de teste.
        /// </summary>
        protected override void BeforeAll()
        {
        }
        ///  <summary>
        /// Executa uma ação ANTES de cada método de teste da classe.
        /// </summary>
        protected override void BeforeEach()
        {
            this.toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
        }
        ///  <summary>
        /// Executa uma ação UMA vez por classe, DEPOIS do término da execução dos métodos de teste.
        /// </summary>
        protected override void AfterAll()
        {
        }
        ///  <summary>
        /// Executa uma ação DEPOIS de cada método de teste da classe.
        /// </summary>
        protected override void AfterEach()
        {
        }
        ///  <summary>
        /// Método para setar os dados necessários para conexão com o PHA no servidor de build.
        /// </summary>
        /// <returns>TO com dados necessários para conexão no servidor de build.</returns>
        protected override TOPhaServidorBuild SetarDadosServidorBuild()
        {
            return new TOPhaServidorBuild("GESTAG", "TREINAMENTO MM5");
        }
        #endregion
        #region Métodos de teste de sucesso.
        ///  <summary>
        /// Realiza o teste padrão para o método Incluir(TOContrato).
        /// Validações realizadas: 
        /// - Chama o método Incluir usando os filtros informados.
        /// - Verifica se o retorno do método Incluir foi de sucesso.
        /// - Realiza as seguintes Assertivas:
        /// 1 - Retorno não está nulo.
        /// 2 - Retorno.OK é sucesso (== true).
        /// 3 - Retorno.Dados não está nulo.
        /// - Obtém o TO novamente da base, utilizando o método Obter.
        /// - Compara o retorno do Obter com os dados do TO preenchido.
        /// </summary>
        [Test(Description = "Testa o método Incluir(TOContrato).", Author = "E37076")]
        public void IncluirComSucessoTest()
        {
            base.TestarIncluir(toContrato);
        }
        ///  <summary>
        /// Realiza o teste padrão para o método Contar(TOContrato).
        /// Validações realizadas: 
        /// - Chama o Contar usando os filtros informados.
        /// - Verifica se o retorno do método Contar foi de sucesso.
        /// - Realiza as seguintes Assertivas:
        /// 1 - Retorno não está nulo.
        /// 2 - Retorno.OK é sucesso (== true).
        /// 3 - Retorno.Dados não está nulo.
        /// 4 - Retorno.Dados não é zero.
        /// 
        /// </summary>
        [Test(Description = "Testa o método Contar(TOContrato).", Author = "E37076")]
        public void ContarComSucessoTest()
        {
            base.TestarContar(toContrato);
        }
        ///  <summary>
        /// Realiza o teste padrão para o método Listar(TOContrato, TOPaginacao).
        /// Validações realizadas: 
        /// - Chama o Listar usando os filtros informados.
        /// - Verifica se o retorno do método Listar foi de sucesso
        /// - Realiza as seguintes Assertivas:
        /// 1 - Retorno não está nulo.
        /// 2 - Retorno.OK é sucesso (== true).
        /// 3 - Retorno.Dados não está nulo.
        /// 4 - Retorno.Dados possui elementos.
        /// - Compara o retorno com os dados da lista de TO preenchida antes do teste.
        /// </summary>
        [Test(Description = "Testa o método Listar(TOContrato, TOPaginacao).", Author = "E37076")]
        public void ListarComSucessoTest()
        {
            TOContrato toContrato = new TOContrato();
            TOPaginacao toPaginacao = new TOPaginacao(1, 10);

            base.TestarListar(toContrato, toPaginacao);
        }
        ///  <summary>
        /// Realiza o teste padrão para o método Obter(TOContrato).
        /// Validações realizadas: 
        /// - Chama o método Obter usando os filtros de chave informados.
        /// - Verifica se o retorno do método Obter foi de sucesso.
        /// - Realiza as seguintes Assertivas:
        /// 1 - Retorno não está nulo.
        /// 2 - Retorno.OK é sucesso (== true).
        /// 3 - Retorno.Dados não está nulo.
        /// - Compara o retorno do Obter com os dados do TO preenchido antes do teste.
        /// </summary>
        [Test(Description = "Testa o método Obter(TOContrato).", Author = "E37076")]
        public void ObterComSucessoTest()
        {
            base.TestarIncluir(toContrato);
            base.TestarObter(toContrato);
        }
        ///  <summary>
        /// Realiza o teste padrão para o método Alterar(TOContrato).
        /// Validações realizadas: 
        /// - Altera o registro na base, conforme os dados informados.
        /// - Verifica se o retorno do método Alterar foi de sucesso.
        /// - Realiza as seguintes Assertivas:
        /// 1 - Retorno não está nulo.
        /// 2 - Retorno.OK é sucesso (== true).
        /// 3 - Retorno.Dados não está nulo.
        /// - Obtém o TO novamente da base, utilizando o método Obter.
        /// - Compara o retorno do Obter com os dados do TO preenchido.
        /// </summary>
        [Test(Description = "Testa o método Alterar(TOContrato).", Author = "E37076")]
        public void AlterarComSucessoTest()
        {
            base.TestarIncluir(toContrato);

            toContrato.UltAtualizacao = this.RN.Obter(toContrato).Dados.UltAtualizacao;
            toContrato.NomeCliente = "Teste Automatizado Atualizado";
            
            base.TestarAlterar(toContrato);
        }
        ///  <summary>
        /// Realiza o teste padrão para o método Excluir(TOContrato).
        /// Validações realizadas: 
        /// - Exclui o registro na base, conforme a chave informada.
        /// - Verifica se o retorno do método Excluir foi de sucesso.
        /// - Realiza as seguintes Assertivas:
        /// 1 - Retorno não está nulo.
        /// 2 - Retorno.OK é sucesso (== true).
        /// 3 - Retorno.Dados não está nulo.
        /// - Tenta obter o registro novamente da base, através do método Obter.
        /// - Verifica se o registro não existe mais.
        /// </summary>
        [Test(Description = "Testa o método Excluir(TOContrato).", Author = "E37076")]
        public void ExcluirComSucessoTest()
        {
            base.TestarIncluir(toContrato);

            toContrato.UltAtualizacao = this.RN.Obter(toContrato).Dados.UltAtualizacao;
            base.TestarExcluir(toContrato);
        }
        #endregion
        #region Teste de falha de campo obrigatório
        ///  <summary>
        /// Realiza um teste para o método Incluir com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Incluir(TOContrato) com falha de campo obrigatório (Agencia).", Author = "E37076")]
        public void IncluirFalhaCampoObrigatorioAgenciaTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.NumeroContrato = 0;
            toContrato.Cnpj = 0;
            toContrato.DataAssinatura = new System.DateTime();
            toContrato.DataNascimento = new System.DateTime();
            toContrato.NomeCliente = "Inserir um valor para toContrato.NomeCliente caso necessário.";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "AGENCIA");
            // TODO: Incluir as Assertivas necessárias para o Incluir
        }
        ///  <summary>
        /// Realiza um teste para o método Incluir com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Incluir(TOContrato) com falha de campo obrigatório (Cnpj).", Author = "E37076")]
        public void IncluirFalhaCampoObrigatorioCnpjTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.NumeroContrato = 0;
            toContrato.Agencia = "Inserir um valor para toContrato.Agencia caso necessário.";
            toContrato.DataAssinatura = new System.DateTime();
            toContrato.DataNascimento = new System.DateTime();
            toContrato.NomeCliente = "Inserir um valor para toContrato.NomeCliente caso necessário.";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "CNPJ");
            // TODO: Incluir as Assertivas necessárias para o Incluir
        }
        ///  <summary>
        /// Realiza um teste para o método Incluir com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Incluir(TOContrato) com falha de campo obrigatório (DataAssinatura" +
            ").", Author = "E37076")]
        public void IncluirFalhaCampoObrigatorioDataAssinaturaTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.NumeroContrato = 0;
            toContrato.Agencia = "Inserir um valor para toContrato.Agencia caso necessário.";
            toContrato.Cnpj = 0;
            toContrato.DataNascimento = new System.DateTime();
            toContrato.NomeCliente = "Inserir um valor para toContrato.NomeCliente caso necessário.";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "DATA_ASSINATURA");
            // TODO: Incluir as Assertivas necessárias para o Incluir
        }
        ///  <summary>
        /// Realiza um teste para o método Incluir com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Incluir(TOContrato) com falha de campo obrigatório (DataNascimento" +
            ").", Author = "E37076")]
        public void IncluirFalhaCampoObrigatorioDataNascimentoTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.NumeroContrato = 0;
            toContrato.Agencia = "Inserir um valor para toContrato.Agencia caso necessário.";
            toContrato.Cnpj = 0;
            toContrato.DataAssinatura = new System.DateTime();
            toContrato.NomeCliente = "Inserir um valor para toContrato.NomeCliente caso necessário.";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "DATA_NASCIMENTO");
            // TODO: Incluir as Assertivas necessárias para o Incluir
        }
        ///  <summary>
        /// Realiza um teste para o método Incluir com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Incluir(TOContrato) com falha de campo obrigatório (NomeCliente).", Author = "E37076")]
        public void IncluirFalhaCampoObrigatorioNomeClienteTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.NumeroContrato = 0;
            toContrato.Agencia = "Inserir um valor para toContrato.Agencia caso necessário.";
            toContrato.Cnpj = 0;
            toContrato.DataAssinatura = new System.DateTime();
            toContrato.DataNascimento = new System.DateTime();
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "NOME_CLIENTE");
            // TODO: Incluir as Assertivas necessárias para o Incluir
        }
        ///  <summary>
        /// Realiza um teste para o método Obter com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Obter(TOContrato) com falha de campo obrigatório (NumeroContrato)." +
            "", Author = "E37076")]
        public void ObterFalhaCampoObrigatorioNumeroContratoTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            Retorno<TOContrato> retorno = this.RN.Obter(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "NUMERO_CONTRATO");
            // TODO: Incluir as Assertivas necessárias para o Obter
        }
        ///  <summary>
        /// Realiza um teste para o método Alterar com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Alterar(TOContrato) com falha de campo obrigatório (NumeroContrato" +
            ").", Author = "E37076")]
        public void AlterarFalhaCampoObrigatorioNumeroContratoTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.UltAtualizacao = new System.DateTime();
            Retorno<Int32> retorno = this.RN.Alterar(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "NUMERO_CONTRATO");
            // TODO: Incluir as Assertivas necessárias para o Alterar
        }
        ///  <summary>
        /// Realiza um teste para o método Alterar com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Alterar(TOContrato) com falha de campo obrigatório (UltAtualizacao" +
            ").", Author = "E37076")]
        public void AlterarFalhaCampoObrigatorioUltAtualizacaoTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.NumeroContrato = 0;
            Retorno<Int32> retorno = this.RN.Alterar(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "ULT_ATUALIZACAO");
            // TODO: Incluir as Assertivas necessárias para o Alterar
        }
        ///  <summary>
        /// Realiza um teste para o método Excluir com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Excluir(TOContrato) com falha de campo obrigatório (NumeroContrato" +
            ").", Author = "E37076")]
        public void ExcluirFalhaCampoObrigatorioNumeroContratoTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.UltAtualizacao = new System.DateTime();
            Retorno<Int32> retorno = this.RN.Excluir(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "NUMERO_CONTRATO");
            // TODO: Incluir as Assertivas necessárias para o Excluir
        }
        ///  <summary>
        /// Realiza um teste para o método Excluir com teste de falha de campo obrigatório.
        /// </summary>
        [Test(Description = "Testa o método Excluir(TOContrato) com falha de campo obrigatório (UltAtualizacao" +
            ").", Author = "E37076")]
        public void ExcluirFalhaCampoObrigatorioUltAtualizacaoTest()
        {
            TOContrato toContrato = new TOContrato();
            // TODO: Setar os valores necessários para o toContrato
            toContrato.NumeroContrato = 0;
            Retorno<Int32> retorno = this.RN.Excluir(toContrato);
            MMAssert.FalhaCampoObrigatorio(retorno, "ULT_ATUALIZACAO");
            // TODO: Incluir as Assertivas necessárias para o Excluir
        }
        #endregion
        #region Teste de falha regras de negócio
        /// <summary>
        /// Teste de inclusão com falha da RN01
        /// </summary>
        [Test(Description = "Teste de inclusão com falha da RN01", Author = "E37076")]
        public void IncluirFalhaRN01()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("Campo NOME_CLIENTE deve possuir, no mínimo, 2 palavras.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de inclusão com falha da RN02
        /// </summary>
        [Test(Description = "Teste de inclusão com falha da RN02", Author = "E37076")]
        public void IncluirFalhaRN02()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date.AddDays(1);
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("A Data de Assinatura deve ser menor ou igual à Data Atual.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de inclusão com falha da RN03
        /// </summary>
        [Test(Description = "Teste de inclusão com falha da RN03", Author = "E37076")]
        public void IncluirFalhaRN03()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = DateTime.Now.Date.AddDays(1);
            toContrato.NomeCliente = "Teste Automatizado";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("A Data de Nascimento deve ser menor ou igual à Data de Assinatura.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de inclusão com falha da RN04
        /// </summary>
        [Test(Description = "Teste de inclusão com falha da RN04", Author = "E37076")]
        public void IncluirFalhaRN04()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            toContrato.Endereco = "Rua 01";
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("Todos os campos de Endereço devem ser preenchidos ou todos devem estar em branco.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de inclusão com falha da RN05
        /// </summary>
        [Test(Description = "Teste de inclusão com falha da RN05", Author = "E37076")]
        public void IncluirFalhaRN05()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            toContrato.ValorImovel = 0;
            Retorno<Int32> retorno = this.RN.Incluir(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("O valor informado para o imóvel deve ser maior que zero.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de alteração com falha da RN01
        /// </summary>
        [Test(Description = "Teste de alteração com falha da RN01", Author = "E37076")]
        public void AlterarFalhaRN01()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            base.TestarIncluir(toContrato);

            Retorno<TOContrato> retornoObtencao = this.RN.Obter(toContrato);
            MMAssert.IsTrue(retornoObtencao.OK);

            toContrato.UltAtualizacao = retornoObtencao.Dados.UltAtualizacao;
            toContrato.NomeCliente = "Teste";
            Retorno<Int32> retorno = this.RN.Alterar(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("Campo NOME_CLIENTE deve possuir, no mínimo, 2 palavras.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de alteração com falha da RN02
        /// </summary>
        [Test(Description = "Teste de alteração com falha da RN02", Author = "E37076")]
        public void AlterarFalhaRN02()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            base.TestarIncluir(toContrato);

            Retorno<TOContrato> retornoObtencao = this.RN.Obter(toContrato);
            MMAssert.IsTrue(retornoObtencao.OK);

            toContrato.UltAtualizacao = retornoObtencao.Dados.UltAtualizacao;
            toContrato.DataAssinatura = DateTime.Now.Date.AddDays(1);
            Retorno<Int32> retorno = this.RN.Alterar(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("A Data de Assinatura deve ser menor ou igual à Data Atual.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de alteração com falha da RN03
        /// </summary>
        [Test(Description = "Teste de alteração com falha da RN03", Author = "E37076")]
        public void AlterarFalhaRN03()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            base.TestarIncluir(toContrato);

            Retorno<TOContrato> retornoObtencao = this.RN.Obter(toContrato);
            MMAssert.IsTrue(retornoObtencao.OK);

            toContrato.UltAtualizacao = retornoObtencao.Dados.UltAtualizacao;
            toContrato.DataNascimento = DateTime.Now.Date.AddDays(1);
            Retorno<Int32> retorno = this.RN.Alterar(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("A Data de Nascimento deve ser menor ou igual à Data de Assinatura.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de alteração com falha da RN04
        /// </summary>
        [Test(Description = "Teste de alteração com falha da RN04", Author = "E37076")]
        public void AlterarFalhaRN04()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            base.TestarIncluir(toContrato);

            Retorno<TOContrato> retornoObtencao = this.RN.Obter(toContrato);
            MMAssert.IsTrue(retornoObtencao.OK);

            toContrato.UltAtualizacao = retornoObtencao.Dados.UltAtualizacao;
            toContrato.Endereco = "Rua 01";
            Retorno<Int32> retorno = this.RN.Alterar(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("Todos os campos de Endereço devem ser preenchidos ou todos devem estar em branco.", retorno.Mensagem.ParaUsuario);
        }

        /// <summary>
        /// Teste de alteração com falha da RN05
        /// </summary>
        [Test(Description = "Teste de alteração com falha da RN05", Author = "E37076")]
        public void AlterarFalhaRN05()
        {
            TOContrato toContrato = new TOContrato();
            toContrato.Agencia = "1234";
            toContrato.Cnpj = 12345678910111;
            toContrato.DataAssinatura = DateTime.Now.Date;
            toContrato.DataNascimento = Convert.ToDateTime("02/03/2004");
            toContrato.NomeCliente = "Teste Automatizado";
            base.TestarIncluir(toContrato);

            Retorno<TOContrato> retornoObtencao = this.RN.Obter(toContrato);
            MMAssert.IsTrue(retornoObtencao.OK);

            toContrato.UltAtualizacao = retornoObtencao.Dados.UltAtualizacao;
            toContrato.ValorImovel = 0;
            Retorno<Int32> retorno = this.RN.Alterar(toContrato);
            MMAssert.IsFalse(retorno.OK, retorno.Mensagem.ParaUsuario);
            MMAssert.AreEqual("O valor informado para o imóvel deve ser maior que zero.", retorno.Mensagem.ParaUsuario);
        }
        #endregion

        /// <summary>
        /// Classes que também devem ser cobertas.
        /// </summary>
        protected override List<string> OutrasClassesParaGerarCobertura
        {
            get
            {
                List<string> classesAdicionais = new List<string>();
                classesAdicionais.Add("Bergs.Pxc.Pxcsctxn.pgm.RegrasNegocioContrato");

                return classesAdicionais;
            }
        }
    }
}

