using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using Bergs.Pwx.Pwxoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>Classe que contém as regras de negócio da classe contrato</summary>
    internal class RegrasNegocioContrato : AplicacaoRegraNegocio
    {
        private Retorno<Int32> retorno;
        private TOContrato toContrato;

        /// <summary>Método que valida todas as regras de negócio da classe contrato</summary>
        public Retorno<Int32> ValidarRegrasNegocioContrato()
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

        public RegrasNegocioContrato InformarClasseTO(TOContrato toContrato)
        {
            this.toContrato = toContrato;
            return this;
        }

        /// <summary>
        /// Verificar se o Nome do Cliente possui, no mínimo, 2 palavras.
        /// </summary>
        public RegrasNegocioContrato NomeClienteTemDuasOuMaisPalavras()
        {
            if (retorno != null) return this;

            if (NomeClienteNaoTemDuasOuMaisPalavras(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN01, "NOME_CLIENTE", "2"));
            }

            return this;
        }

        /// <summary>
        /// Verificar se a Data de Assinatura é menor ou igual à Data Atual.
        /// </summary>
        public RegrasNegocioContrato DataAssinaturaMenorQueDataAtual()
        {
            if (retorno != null) return this;

            if (DataAssinaturaMenorQueDataAtual(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN02));
            }

            return this;
        }

        /// <summary>
        /// Verificar se a Data de Nascimento é menor ou igual à Data de Assinatura.
        /// </summary>
        public RegrasNegocioContrato DataNascimentoMenorDataAssinatura()
        {
            if (retorno != null) return this;

            if (DataNascimentoMenorDataAssinatura(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN03));
            }

            return this;
        }

        /// <summary>
        /// Todos os campos de Endereço (Endereço, cidade, CEP e UF) devem ser preenchidos ou nenhum dos campos de endereço deve ser preenchido.
        /// </summary>
        public RegrasNegocioContrato EnderecoCompletamentePreenchidoOuSemEnderecoInformado()
        {
            if (retorno != null) return this;

            if (EnderecoCompletamentePreenchidoOuSemEnderecoInformado(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN04));
            }

            return this;
        }

        /// <summary>
        /// O Valor do Imóvel, se informado, deve ser maior que zero.
        /// </summary>
        public RegrasNegocioContrato ValorDoImovelDeveSerPositivo()
        {
            if (retorno != null) return this;

            if (ValorDoImovelDeveSerPositivo(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN04));
            }

            return this;
        }

        /// <summary>
        /// Verificar se o Nome do Cliente possui, no mínimo, 2 palavras.
        /// </summary>
        /// <returns></returns>
        public bool NomeClienteNaoTemDuasOuMaisPalavras(TOContrato toContrato)
        {
            return toContrato.NomeCliente.FoiSetado && !toContrato.NomeCliente.ToString().Trim().Contains(" ");
        }
        /// <summary>
        /// Verificar se a Data de Assinatura é menor ou igual à Data Atual.
        /// </summary>
        /// <returns></returns>
        public bool DataAssinaturaMenorQueDataAtual(TOContrato toContrato)
        {
            return toContrato.DataAssinatura.FoiSetado && toContrato.DataAssinatura.LerConteudoOuPadrao() > DateTime.Now.Date;
        }

        /// <summary>
        /// Verificar se a Data de Nascimento é menor ou igual à Data de Assinatura.
        /// </summary>
        public bool DataNascimentoMenorDataAssinatura(TOContrato toContrato)
        {
            return toContrato.DataAssinatura.FoiSetado && toContrato.DataNascimento.FoiSetado
                    && toContrato.DataNascimento.LerConteudoOuPadrao() > toContrato.DataAssinatura.LerConteudoOuPadrao();
        }
        /// <summary>
        /// Todos os campos de Endereço (Endereço, cidade, CEP e UF) devem ser preenchidos ou nenhum dos campos de endereço deve ser preenchido.
        /// </summary>
        public bool EnderecoCompletamentePreenchidoOuSemEnderecoInformado(TOContrato toContrato)
        {
            return (toContrato.Endereco.TemConteudo
                    || toContrato.Cidade.TemConteudo
                    || toContrato.Cep.TemConteudo
                    || toContrato.Uf.TemConteudo)
                    &&
                    !(toContrato.Endereco.TemConteudo
                    && toContrato.Cidade.TemConteudo
                    && toContrato.Cep.TemConteudo
                    && toContrato.Uf.TemConteudo);
        }
        /// <summary>
        /// O Valor do Imóvel, se informado, deve ser maior que zero.
        /// </summary>
        public bool ValorDoImovelDeveSerPositivo(TOContrato toContrato)
        {
            return toContrato.ValorImovel.TemConteudo && toContrato.ValorImovel.LerConteudoOuPadrao() <= 0;
        }
    }
}
