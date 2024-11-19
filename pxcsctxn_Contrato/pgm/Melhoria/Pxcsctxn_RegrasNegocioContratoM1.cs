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
    public class Pxcsctxn_RegrasNegocioContratoM1 : AplicacaoRegraNegocio
    {
        /// <summary>Método que valida todas as regras de negócio da classe contrato</summary>
        public Retorno<Int32> ValidarRegrasNegocioContrato(TOContrato toContrato)
        {
            try
            {
                #region RN01
                if (toContrato.NomeCliente.FoiSetado && !toContrato.NomeCliente.ToString().Trim().Contains(" "))
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN01, "NOME_CLIENTE", "2"));
                }
                #endregion

                #region RN02
                if (toContrato.DataAssinatura.FoiSetado && toContrato.DataAssinatura.LerConteudoOuPadrao() > DateTime.Now.Date)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN02));
                }
                #endregion

                #region RN03
                if (toContrato.DataAssinatura.FoiSetado && toContrato.DataNascimento.FoiSetado
                    && toContrato.DataNascimento.LerConteudoOuPadrao() > toContrato.DataAssinatura.LerConteudoOuPadrao())
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN03));
                }
                #endregion

                #region RN04
                if ((toContrato.Endereco.TemConteudo
                    || toContrato.Cidade.TemConteudo
                    || toContrato.Cep.TemConteudo
                    || toContrato.Uf.TemConteudo)
                    &&
                    !(toContrato.Endereco.TemConteudo
                    && toContrato.Cidade.TemConteudo
                    && toContrato.Cep.TemConteudo
                    && toContrato.Uf.TemConteudo)
                    )
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN04));
                }
                #endregion

                #region RN05
                if (toContrato.ValorImovel.TemConteudo && toContrato.ValorImovel.LerConteudoOuPadrao() <= 0)
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
    }
}
