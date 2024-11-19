using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using Bergs.Pwx.Pwxoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>
    /// 
    /// </summary>
    public class Pxcsctxn_RegrasNegocioContratoM2 : AplicacaoRegraNegocio
    {
        /// <summary>Método que valida todas as regras de negócio da classe contrato</summary>
        public Retorno<Int32> ValidarRegrasNegocioContrato(TOContrato toContrato)
        {
            try
            {
                #region RN01
                if (NomeClienteTemDuasOuMaisPalavras(toContrato))
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN01, "NOME_CLIENTE", "2"));
                }
                #endregion

                #region RN02
                if (DataAssinaturaMenorQueDataAtual(toContrato))
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN02));
                }
                #endregion

                #region RN03
                if (DataNascimentoMenorDataAssinatura(toContrato))
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN03));
                }
                #endregion

                #region RN04
                if (EnderecoCompletamentePreenchidoOuSemEnderecoInformado(toContrato))
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN04));
                }
                #endregion

                #region RN05
                if (ValorDoImovelDeveSerPositivo(toContrato))
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN05));
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
        public bool NomeClienteTemDuasOuMaisPalavras(TOContrato toContrato)
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
