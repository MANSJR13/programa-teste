using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using Bergs.Pwx.Pwxoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>Classe que contém as regras de negócio da classe cliente</summary>
    public class RegrasNegocioCliente : AplicacaoRegraNegocio
    {

        private Retorno<Int32> retorno;
        private TOClientePxc toCliente;

        /// <summary>
        /// Informa qual o instancia de cliente será utilizada na validação
        /// </summary>
        /// <param name="toCliente"></param>
        /// <returns></returns>
        public RegrasNegocioCliente InformarClasseTO(TOClientePxc toCliente)
        {
            this.toCliente = toCliente;
            return this;
        }

        /// <summary>
        /// valida cnpj
        /// </summary>
        /// <returns></returns>
        public RegrasNegocioCliente ValidarCnpj()
        {
            if (retorno != null || toCliente.TipoPessoa == TipoPessoa.Fisica) return this;

            ValidadorCpfCnpj rnValidadorCpfCnpj = this.Infra.InstanciarRN<ValidadorCpfCnpj>();

            Retorno<Boolean> validacaoCnpj = rnValidadorCpfCnpj.ValidarCnpj(toCliente.CodCliente.ToString().PadLeft(14, '0'));
            // verifica se houve falha na execução
            // no método em questão (disponibilizado), só devolve sucesso sempre
            // porém, testamos o .OK para ver se não deu uma exceção
            if (!validacaoCnpj.OK)
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(validacaoCnpj.Mensagem);
            }
            // se foi sucesso, testa o .Dados (true = válido, false = inválido)
            if (!validacaoCnpj.Dados)
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN01, "CNPJ"));
            }

            return this;
        }
        /// <summary>
        /// Valida CPF da pessoa
        /// </summary>
        /// <returns></returns>
        public RegrasNegocioCliente ValidaCPF()
        {
            if (retorno != null || toCliente.TipoPessoa == TipoPessoa.Juridica) return this;

            ValidadorCpfCnpj rnValidadorCpfCnpj = this.Infra.InstanciarRN<ValidadorCpfCnpj>();

            Retorno<Boolean> validacaoCpf = rnValidadorCpfCnpj.ValidarCpf(toCliente.CodCliente.ToString().PadLeft(11, '0'));
            // verifica se houve falha na execução
            // no método em questão (disponibilizado), só devolve sucesso sempre
            // porém, testamos o .OK para ver se não deu uma exceção
            if (!validacaoCpf.OK)
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(validacaoCpf.Mensagem);
            }
            // se foi sucesso, testa o .Dados (true = válido, false = inválido)
            if (!validacaoCpf.Dados)
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN01, "CPF"));
            }

            return this;
        }

        /// <summary>
        /// Verificar se o Nome do Cliente possui, no mínimo, 2 palavras.
        /// </summary>
        public RegrasNegocioCliente NomeClienteTemDuasOuMaisPalavras()
        {
            if (retorno != null) return this;

            if (NomeClienteNaoTemDuasOuMaisPalavras(toCliente))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN02));
            }

            return this;
        }
        /// <summary>
        /// O Nome da Mãe deve possuir, no mínimo, duas palavras.
        /// </summary>
        /// <returns></returns>
        public RegrasNegocioCliente NomeMaeTemDuasOuMaisPalavras()
        {
            if (retorno != null && toCliente.TipoPessoa == TipoPessoa.Juridica) return this;

            if (NomeMaeNaoTemDuasOuMaisPalavras(toCliente))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN08));
            }

            return this;
        }

        /// <summary>
        /// Para cliente pessoa jurídica, verificar se o Capital Social informado é válido.
        /// </summary>
        /// <returns></returns>
        public RegrasNegocioCliente CapitalSocialPositivo()
        {
            if (retorno != null || this.toCliente.TipoPessoa == TipoPessoa.Fisica) return this;

            if (CapitalSocialNaoEstaPositivo(toCliente))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN07));
            }

            return this;
        }

        /// <summary>
        /// Verificar se o Nome fantasia Cliente PJ possui, no mínimo, 2 palavras.
        /// </summary>
        public RegrasNegocioCliente NomeFantasiaTemDuasOuMaisPalavras()
        {
            if (retorno != null || this.toCliente.TipoPessoa == TipoPessoa.Fisica) return this;

            if (NomeFantasiaNaoTemDuasOuMaisPalavras(toCliente))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN05));
            }

            return this;
        }

        /// <summary>
        /// A Data de Constituição informada deve ser menor ou igual à Data Atual.
        /// </summary>
        public RegrasNegocioCliente DataConstituicaoMenorQueAtual()
        {
            if (retorno != null || this.toCliente.TipoPessoa == TipoPessoa.Fisica) return this;

            if (DataConstituicaoNaoEhMenorQueAtual(toCliente))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN06));
            }

            return this;
        }

        /// <summary>
        /// Se o cliente for pessoa jurídica:
        /// - Tornar obrigatório o preenchimento dos campos “Nome Fantasia”, “Capital Social” e “Data Constituição”;
        /// - Proibir o preenchimento do campo “Nome Mãe”.
        /// </summary>
        public RegrasNegocioCliente VerificaCamposPessoaJuridica()
        {
            if (retorno != null || toCliente.TipoPessoa == TipoPessoa.Fisica) return this;

            if (VerificaCamposPessoaJuridica(toCliente))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(
                    new Mensagem(TipoMensagem.RN04, "Nome da Mãe"));
            }

            return this;
        }

        /// <summary>
        /// Se o cliente for pessoa física:
        /// - Tornar obrigatório o preenchimento do campo “Nome Mãe”;
        /// - Proibir o preenchimento dos campos “Nome Fantasia”, “Capital Social” e “Data Constituição”.
        /// </summary>
        public RegrasNegocioCliente VerificaCamposPessoaFisica()
        {
            if (retorno != null || toCliente.TipoPessoa == TipoPessoa.Juridica) return this;

            if (VerificaCamposPessoaFisica(toCliente))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(
                    new Mensagem(TipoMensagem.RN04, "Nome da Mãe"));
            }

            return this;
        }

        /// <summary>
        /// Valida regras para cliente
        /// </summary>
        /// <returns></returns>
        public Retorno<Int32> ValidarRegrasNegocioCliente()
        {
            try
            {
                Retorno<Int32> retornoValidacao = this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem());

                if (retorno != null)
                {
                    retornoValidacao = retorno;
                }

                return retornoValidacao;
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }

        /// <summary>Método que valida todas as regras de negócio da classe cliente</summary>
        public Retorno<Int32> ValidarRegrasNegocioClienteBase(TOClientePxc toClientePxc, string operacao)
        {
            try
            {

                #region RN05
                if (toClientePxc.NomeFantasia.TemConteudo)
                {
                    if (toClientePxc.NomeFantasia.TemConteudo)
                    {
                        if (toClientePxc.NomeFantasia.LerConteudoOuPadrao().ToString().Trim().Length < 2)
                        {
                            return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN05));
                        }
                    }
                }
                #endregion

                #region RN6
                if (toClientePxc.TipoPessoa.LerConteudoOuPadrao().Equals(TipoPessoa.Juridica))
                {
                    if (toClientePxc.DtConstituicao.TemConteudo)
                    {
                        if (toClientePxc.DtConstituicao.LerConteudoOuPadrao() > DateTime.Now.Date)
                        {
                            return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN06));
                        }
                    }
                }
                #endregion

                #region RN07
                if (toClientePxc.TipoPessoa.LerConteudoOuPadrao().Equals(TipoPessoa.Juridica))
                {
                    if (toClientePxc.VlrCapitalSocial.TemConteudo)
                    {
                        if (toClientePxc.VlrCapitalSocial.LerConteudoOuPadrao() < 0)
                        {
                            return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN07));
                        }
                    }
                }
                #endregion

                #region RN08
                if (toClientePxc.TipoPessoa.LerConteudoOuPadrao().Equals(TipoPessoa.Fisica))
                {
                    if (toClientePxc.NomeMae.TemConteudo)
                    {
                        if (!toClientePxc.NomeMae.ToString().Trim().Contains(" "))
                        {
                            return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN08));
                        }
                    }
                }
                #endregion

                #region RN9
                if (toClientePxc.Agencia.LerConteudoOuPadrao().ToString().Trim().Length < 4)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN09));
                }
                #endregion

                return this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }

        /// <summary>
        /// Verificar se o Nome do Cliente possui, no mínimo, 2 palavras.
        /// </summary>
        /// <returns></returns>
        public bool NomeClienteNaoTemDuasOuMaisPalavras(TOClientePxc toCliente)
        {
            return toCliente.NomeCliente.FoiSetado && !toCliente.NomeCliente.ToString().Trim().Contains(" ");
        }

        /// <summary>
        /// O Nome da Mãe deve possuir, no mínimo, duas palavras.
        /// </summary>
        /// <param name="toCliente"></param>
        /// <returns></returns>
        public bool NomeMaeNaoTemDuasOuMaisPalavras(TOClientePxc toCliente)
        {
            return toCliente.NomeMae.FoiSetado && !toCliente.NomeMae.ToString().Trim().Contains(" ");
        }
      
        /// <summary>
        /// Para cliente pessoa jurídica, verificar se o Capital Social informado é válido.
        /// </summary>
        /// <param name="toCliente"></param>
        /// <returns></returns>
        public bool CapitalSocialNaoEstaPositivo(TOClientePxc toCliente)
        {
            return toCliente.VlrCapitalSocial.FoiSetado 
                && toCliente.VlrCapitalSocial.LerConteudoOuPadrao() <= 0;
        }
        
        /// <summary>
        /// Verificar se o Nome do Cliente possui, no mínimo, 2 palavras.
        /// </summary>
        /// <returns></returns>
        public bool NomeFantasiaNaoTemDuasOuMaisPalavras(TOClientePxc toCliente)
        {
            return toCliente.NomeFantasia.FoiSetado && !toCliente.NomeFantasia.ToString().Trim().Contains(" ");
        }


        /// <summary>
        /// A Data de Constituição informada deve ser menor ou igual à Data Atual.
        /// </summary>
        /// <param name="toCliente"></param>
        /// <returns></returns>
        public bool DataConstituicaoNaoEhMenorQueAtual(TOClientePxc toCliente)
        {
            return toCliente.DtConstituicao.FoiSetado 
                && toCliente.DtConstituicao.LerConteudoOuPadrao().Date > DateTime.Now.Date;
        }

        /// <summary>
        /// Se o cliente for pessoa jurídica:
        /// - Tornar obrigatório o preenchimento dos campos “Nome Fantasia”, “Capital Social” e “Data Constituição”;
        /// - Proibir o preenchimento do campo “Nome Mãe”.
        /// </summary>
        public bool VerificaCamposPessoaJuridica(TOClientePxc toCliente)
        {
            return !(toCliente.NomeFantasia.TemConteudo
                && toCliente.VlrCapitalSocial.TemConteudo
                && toCliente.DtConstituicao.TemConteudo
                && !toCliente.NomeMae.TemConteudo);
        }

        /// <summary>
        /// Se o cliente for pessoa física:
        /// - Tornar obrigatório o preenchimento do campo “Nome Mãe”;
        /// - Proibir o preenchimento dos campos “Nome Fantasia”, “Capital Social” e “Data Constituição”.
        /// </summary>
        public bool VerificaCamposPessoaFisica(TOClientePxc toCliente)
        {
            return toCliente.NomeFantasia.TemConteudo
                || toCliente.VlrCapitalSocial.TemConteudo
                || toCliente.DtConstituicao.TemConteudo
                || !toCliente.NomeMae.TemConteudo;
        }
    }
}
