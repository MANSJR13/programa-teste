using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>
    /// Classe base para validação de regra
    /// </summary>
    public class Pxcsctxn_DataAssinaturaMenorQueDataAtual : Pxcsctxn_ValidadorRegra
    {
        /// <summary>
        /// 
        /// </summary>
        private Pxcsctxn_ValidadorRegra proximaRegra;
        /// <summary>
        /// Método de validação
        /// </summary>
        public override void ValidarRegra(TOContrato toContrato)
        {
            if (this.RetornoEstaVazio() && DataAssinaturaMenorQueDataAtual(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN02));
            }
            else if (this.proximaRegra != null)
            {
                proximaRegra.ValidarRegra(toContrato);
            }
        }
        /// <summary>
        /// Próxima regra a ser executada
        /// </summary>
        public override void ProximaRegra(Pxcsctxn_ValidadorRegra proximaRegra)
        {
            this.proximaRegra = proximaRegra;
        }

        /// <summary>
        /// Verificar se a Data de Assinatura é menor ou igual à Data Atual.
        /// </summary>
        /// <returns></returns>
        public bool DataAssinaturaMenorQueDataAtual(TOContrato toContrato)
        {
            return toContrato.DataAssinatura.FoiSetado && toContrato.DataAssinatura.LerConteudoOuPadrao() > DateTime.Now.Date;
        }
    }
}
