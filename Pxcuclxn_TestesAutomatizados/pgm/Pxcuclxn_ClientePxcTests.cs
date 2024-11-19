using Bergs.Bth.Bthsmoxn;
using Bergs.Bth.Bthstixn;
using Bergs.Bth.Bthstixn.MM4;
using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcsclxn;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcuclxn.Tests
{
    ///  <summary>
    /// Contém os métodos de teste da classe ClientePxc.
    /// </summary>
    [TestFixture(Description="Classe de testes para a classe RN ClientePxc.", Author="T08543")]
	public class ClientePxcTests : AbstractTesteRegraNegocio<ClientePxc>
	{
        /// <summary>
        /// clientepxc utilizado para testes
        /// </summary>
        private TOClientePxc toClientePxcPF;
        private TOClientePxc toClientePxcPJ;

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
            toClientePxcPF = new TOClientePxc();

            toClientePxcPF.CodCliente = "72317051034";
            toClientePxcPF.TipoPessoa = TipoPessoa.Fisica;
            toClientePxcPF.Agencia = 1234;
            toClientePxcPF.CodOperador = "código do operador";
            toClientePxcPF.DtAbeCad = DateTime.Now;
            toClientePxcPF.NomeCliente = "Nome do Cliente";
            toClientePxcPF.NomeMae = "Mae do cliente";

            toClientePxcPJ = new TOClientePxc();

            toClientePxcPJ.CodCliente = "76600925000135";
            toClientePxcPJ.TipoPessoa = TipoPessoa.Juridica;
            toClientePxcPJ.Agencia = 1234;
            toClientePxcPJ.CodOperador = "código do operador";
            toClientePxcPJ.DtAbeCad = DateTime.Now;
            toClientePxcPJ.NomeCliente = "Nome do Cliente Juridica";
            toClientePxcPJ.NomeFantasia = "Nome fantasia do cliente" ;
            toClientePxcPJ.VlrCapitalSocial = 10000;
            toClientePxcPJ.DtConstituicao = DateTime.Now.AddYears(-10);
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
		/// Realiza o teste padrão para o método Incluir(TOClientePxc).
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
		[Test(Description="Testa o método Incluir pessoa física(TOClientePxc).", Author="T08543")]
		public void IncluirPessoaFisicaComSucessoTest()
		{
			base.TestarIncluir(toClientePxcPF);
		}

        /// <summary>
        /// Verifica se inclui pessoa juridica com sucesso
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa juridica(TOClientePxc).", Author = "T08543")]
        public void IncluirPessoaJuridicaComSucessoTest()
        {
            base.TestarIncluir(toClientePxcPF);
        }
        /// <summary>
        /// Verifica inclusão de cpf inválido
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa física com cpf errado.", Author = "T08543")]
        public void IncluirPessoaFisicaComCPfInvalidoTest()
        {
            toClientePxcPF.CodCliente = "11111111112";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O CPF informado não é válido.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj inválido
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa juridica com cnpj errado.", Author = "T08543")]
        public void IncluirPessoaFisicaComCnpjInvalidoTest()
        {
            toClientePxcPF.CodCliente = "11111111111112";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O CPF informado não é válido.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj inválido
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa com nome sem ao menos 2 palavras.", Author = "T08543")]
        public void IncluirPessoaComNomeInvalidoTest()
        {
            toClientePxcPF.NomeCliente = "UmaPalavra";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Nome do Cliente deve possuir, no mínimo, duas palavras.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj inválido
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa sem nome.", Author = "T08543")]
        public void IncluirPessoaSemNomeInvalidoTest()
        {
            toClientePxcPF.NomeCliente = "";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Nome do Cliente deve possuir, no mínimo, duas palavras.");
        }
        /// <summary>
        /// Verifica inclusão de cnpj sem nome fantasia
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa sem nome fantasia.", Author = "T08543")]
        public void IncluirPessoaJuridicaSemNomeFantasiaTest()
        {
            toClientePxcPJ.NomeFantasia = new CampoOpcional<string>();
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPJ);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj sem nome capital social
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa sem capital social.", Author = "T08543")]
        public void IncluirPessoaJuridicaSemNomeCapitalSocialTest()
        {
            toClientePxcPJ.VlrCapitalSocial = new CampoOpcional<Decimal>();
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPJ);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj sem data constituicao
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa sem data da constituicao.", Author = "T08543")]
        public void IncluirPessoaJuridicaSemDataConstituicaoTest()
        {
            toClientePxcPJ.DtConstituicao = new CampoOpcional<DateTime>();
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPJ);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj com nome mae
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa com nome da mae.", Author = "T08543")]
        public void IncluirPessoaJuridicaComNomeMaeTest()
        {
            toClientePxcPJ.NomeMae = "Nome da mãe";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPJ);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }
        /// <summary>
        /// Verifica inclusão de cnpj sem nome fantasia
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa fisica com nome fantasia.", Author = "T08543")]
        public void IncluirPessoaFisicaComNomeFantasiaTest()
        {
            toClientePxcPF.NomeFantasia = "nome fataisa";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj sem nome capital social
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa fisica com capital social.", Author = "T08543")]
        public void IncluirPessoaJuridicaFisicaComNomeCapitalSocialTest()
        {
            toClientePxcPF.VlrCapitalSocial = 10000;
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cpf com data constituicao
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa sem data da constituicao.", Author = "T08543")]
        public void IncluirPessoaFisicaComDataConstituicaoTest()
        {
            toClientePxcPF.DtConstituicao = DateTime.Now;
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj com nome mae
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa fisica sem nome da mae.", Author = "T08543")]
        public void IncluirPessoaFisicaSemNomeMaeTest()
        {
            toClientePxcPF.NomeMae = new CampoOpcional<string>();
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }
        ///  <summary>
		/// Realiza o teste padrão para o método Alterar(TOClientePxc).
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
		[Test(Description = "Testa o método Alterar(TOClientePxc).", Author = "T08543")]
        public void AlterarComSucessoTest()
        {
            this.RN.Incluir(toClientePxcPF);
            var cliente = this.RN.Obter(toClientePxcPF).Dados;

            cliente.NomeCliente = "Outro nome";
            base.TestarAlterar(cliente);
        }
        /// <summary>
        /// Testa alteração de nome com falha
        /// </summary>
        [Test(Description = "Testa o método Alterar com alteração do nome indevida.", Author = "T08543")]
        public void AlterarNomeComFalhaTest()
        {
            this.RN.Incluir(toClientePxcPF);
            var cliente = this.RN.Obter(toClientePxcPF).Dados;

            cliente.NomeCliente = "NomeFalha";

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Nome do Cliente deve possuir, no mínimo, duas palavras.");
        }
        /// <summary>
        /// Testa alteração da data de cadastro
        /// </summary>
        [Test(Description = "Testa o método Alterar com alteração da data de cadastro.", Author = "T08543")]
        public void AlterarDataCadastroComSucessoTest()
        {
            this.RN.Incluir(toClientePxcPF);

            var cliente = this.RN.Obter(toClientePxcPF).Dados;
            cliente.DtAbeCad = DateTime.Now.AddDays(10);

            this.RN.Alterar(cliente);

            var clienteSemAlteracao = this.RN.Obter(cliente).Dados;

            MMAssert.AreEqual(toClientePxcPF.DtAbeCad.LerConteudoOuPadrao().Date,
                clienteSemAlteracao.DtAbeCad.LerConteudoOuPadrao().Date);
        }
        /// <summary>
        /// Verifica inclusão de cnpj sem nome fantasia
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa sem nome fantasia.", Author = "T08543")]
        public void AlterarPessoaJuridicaSemNomeFantasiaTest()
        {
            this.RN.Incluir(toClientePxcPJ);
            var cliente = this.RN.Obter(toClientePxcPJ).Dados;
            cliente.NomeFantasia = new CampoOpcional<string>();

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj sem nome capital social
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa sem capital social.", Author = "T08543")]
        public void AlterarPessoaJuridicaSemNomeCapitalSocialTest()
        {
            this.RN.Incluir(toClientePxcPJ);
            var cliente = this.RN.Obter(toClientePxcPJ).Dados;
            cliente.VlrCapitalSocial = new CampoOpcional<Decimal>();

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj sem data constituicao
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa sem data da constituicao.", Author = "T08543")]
        public void AlterarPessoaJuridicaSemDataConstituicaoTest()
        {
            this.RN.Incluir(toClientePxcPJ);
            var cliente = this.RN.Obter(toClientePxcPJ).Dados;
            cliente.DtConstituicao = new CampoOpcional<DateTime>();

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj com nome mae
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa com nome da mae.", Author = "T08543")]
        public void AlterarPessoaJuridicaComNomeMaeTest()
        {
            this.RN.Incluir(toClientePxcPJ);
            var cliente = this.RN.Obter(toClientePxcPJ).Dados;
            cliente.NomeMae = "Nome da mãe";

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }
        /// <summary>
        /// Verifica inclusão de cnpj sem nome fantasia
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa fisica com nome fantasia.", Author = "T08543")]
        public void AlterarPessoaFisicaComNomeFantasiaTest()
        {
            this.RN.Incluir(toClientePxcPF);
            var cliente = this.RN.Obter(toClientePxcPF).Dados;
            cliente.NomeFantasia = "nome fantasia";

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj sem nome capital social
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa fisica com capital social.", Author = "T08543")]
        public void AlterarPessoaJuridicaFisicaComNomeCapitalSocialTest()
        {
            this.RN.Incluir(toClientePxcPF);
            var cliente = this.RN.Obter(toClientePxcPF).Dados;
            cliente.VlrCapitalSocial = 10000;

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cpf com data constituicao
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa sem data da constituicao.", Author = "T08543")]
        public void AlterarPessoaFisicaComDataConstituicaoTest()
        {
            this.RN.Incluir(toClientePxcPF);
            var cliente = this.RN.Obter(toClientePxcPF).Dados;
            cliente.DtConstituicao = DateTime.Now;

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj com nome mae
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa fisica sem nome da mae.", Author = "T08543")]
        public void AlterarPessoaFisicaSemNomeMaeTest()
        {
            this.RN.Incluir(toClientePxcPF);
            var cliente = this.RN.Obter(toClientePxcPF).Dados;
            cliente.NomeMae = new CampoOpcional<string>();

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "Não é permitido informar Nome da Mãe.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj com nome fantasia invalido
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa juridica com nome fantasia sem ao menos 2 palavras.", Author = "T08543")]
        public void IncluirPessoaJuridicaComNomeFantasiaInvalidoTest()
        {
            toClientePxcPJ.NomeFantasia = "UmaPalavra";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPJ);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Nome Fantasia da Empresa deve possuir, no mínimo, duas letras.");
        }

        /// <summary>
        /// Verifica altearcao de cnpj com nome fantasia invalido
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa juridica com nome fantasia sem ao menos 2 palavras.", Author = "T08543")]
        public void AltearPessoaJuridicaComNomeFantasiaInvalidoTest()
        {
            this.RN.Incluir(toClientePxcPJ);
            var cliente = this.RN.Obter(toClientePxcPJ).Dados;
            cliente.NomeFantasia = "UmaPalavra";

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Nome Fantasia da Empresa deve possuir, no mínimo, duas letras.");
        }

        /// <summary>
        /// Verifica inclusão de cnpj com data da constituicao invalida
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa juridica com data da constituicao maior que atual.", Author = "T08543")]
        public void IncluirPessoaJuridicaComDataConstituicaoInvalidoTest()
        {
            toClientePxcPJ.DtConstituicao = DateTime.Now.AddDays(5);
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPJ);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "A Data de Constituição informada deve ser menor ou igual à Data Atual.");
        }

        /// <summary>
        /// Verifica alteracao de cnpj com data da constituicao invalida
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa juridica com data da constituicao maior que atual.", Author = "T08543")]
        public void AltearPessoaJuridicaComDataConstituicaoInvalidoTest()
        {
            this.RN.Incluir(toClientePxcPJ);
            var cliente = this.RN.Obter(toClientePxcPJ).Dados;
            cliente.DtConstituicao = DateTime.Now.AddDays(5);

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "A Data de Constituição informada deve ser menor ou igual à Data Atual.");
        }

        /// <summary>
        /// Para cliente pessoa jurídica, verificar se o Capital Social informado é válido.
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa juridica com Capital Social menor que zero.", Author = "T08543")]
        public void IncluirPessoaJuridicaComCapitalSocialInvalidoTest()
        {
            toClientePxcPJ.VlrCapitalSocial = 0;
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPJ);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Capital Social da Empresa deve ser maior que zero.");
        }

        /// <summary>
        /// Para cliente pessoa jurídica, verificar se o Capital Social informado é válido.
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa juridica com Capital Social menor que zero.", Author = "T08543")]
        public void AltearPessoaJuridicaComComCapitalSocialInvalidoTest()
        {
            this.RN.Incluir(toClientePxcPJ);
            var cliente = this.RN.Obter(toClientePxcPJ).Dados;
            cliente.VlrCapitalSocial = 0;

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Capital Social da Empresa deve ser maior que zero.");
        }

        /// <summary>
        /// O Nome da Mãe deve possuir, no mínimo, duas palavras.
        /// </summary>
        [Test(Description = "Testa o método Incluir pessoa fisica onde O Nome da Mãe deve possuir, no mínimo, duas palavras.", Author = "T08543")]
        public void IncluirPessoaFisicaComNomeMaeInvalidoTest()
        {
            toClientePxcPF.NomeMae = "umapalavra";
            Retorno<Int32> retorno = this.RN.Incluir(toClientePxcPF);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Nome da Mãe deve possuir, no mínimo, duas palavras.");
        }

        /// <summary>
        /// O Nome da Mãe deve possuir, no mínimo, duas palavras.
        /// </summary>
        [Test(Description = "Testa o método alterar pessoa fisica onde O Nome da Mãe deve possuir, no mínimo, duas palavras.", Author = "T08543")]
        public void AltearPessoaFisicaComNomeMaeInvalidoTest()
        {
            this.RN.Incluir(toClientePxcPF);
            var cliente = this.RN.Obter(toClientePxcPF).Dados;
            cliente.NomeMae = "umapalavra";

            var retorno = this.RN.Alterar(cliente);

            MMAssert.IsFalse(retorno.OK);
            MMAssert.AreEqual(retorno.Mensagem.ParaUsuario, "O Nome da Mãe deve possuir, no mínimo, duas palavras.");
        }

        ///  <summary>
        /// Realiza o teste padrão para o método Contar(TOClientePxc).
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
        [Test(Description="Testa o método Contar(TOClientePxc).", Author="T08543")]
		public void ContarComSucessoTest()
		{
			TOClientePxc toClientePxc = new TOClientePxc();
			// TODO: Setar valores necessários para o toClientePxc
			base.TestarContar(toClientePxc);
		}
		///  <summary>
		/// Realiza o teste padrão para o método Listar(TOClientePxc, TOPaginacao).
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
		[Test(Description="Testa o método Listar(TOClientePxc, TOPaginacao).", Author="T08543")]
		public void ListarComSucessoTest()
		{
			TOClientePxc toClientePxc = new TOClientePxc();
			TOPaginacao toPaginacao = new TOPaginacao(1, 10);
			// TODO: Setar os valores necessários para o toClientePxc
			// TODO: Setar os valores necessários para o toPaginacao

			base.TestarListar(toClientePxc, toPaginacao);
		}
		///  <summary>
		/// Realiza o teste padrão para o método Obter(TOClientePxc).
		/// Validações realizadas: 
		/// - Chama o método Obter usando os filtros de chave informados.
		/// - Verifica se o retorno do método Obter foi de sucesso.
		/// - Realiza as seguintes Assertivas:
		/// 1 - Retorno não está nulo.
		/// 2 - Retorno.OK é sucesso (== true).
		/// 3 - Retorno.Dados não está nulo.
		/// - Compara o retorno do Obter com os dados do TO preenchido antes do teste.
		/// </summary>
		[Test(Description="Testa o método Obter(TOClientePxc).", Author="T08543")]
		public void ObterComSucessoTest()
		{
            base.TestarIncluir(toClientePxcPF);
            base.TestarObter(toClientePxcPF);
		}
		
		///  <summary>
		/// Realiza o teste padrão para o método Excluir(TOClientePxc).
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
		[Test(Description="Testa o método Excluir(TOClientePxc).", Author="T08543")]
		public void ExcluirComSucessoTest()
		{
            base.TestarIncluir(toClientePxcPF);

            toClientePxcPF.UltAtualizacao = this.RN.Obter(toClientePxcPF).Dados.UltAtualizacao;
            base.TestarExcluir(toClientePxcPF);
		}
		#endregion
	}
}

