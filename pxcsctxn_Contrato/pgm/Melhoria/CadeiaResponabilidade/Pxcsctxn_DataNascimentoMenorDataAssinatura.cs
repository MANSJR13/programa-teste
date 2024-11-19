using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>
    /// Classe base para validação de regra
    /// </summary>
    public class Pxcsctxn_DataNascimentoMenorDataAssinatura : Pxcsctxn_ValidadorRegra
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
            if (this.RetornoEstaVazio() && DataNascimentoMenorDataAssinatura(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN03));
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
        /// Verificar se a Data de Nascimento é menor ou igual à Data de Assinatura.
        /// </summary>
        public bool DataNascimentoMenorDataAssinatura(TOContrato toContrato)
        {
            return toContrato.DataAssinatura.FoiSetado && toContrato.DataNascimento.FoiSetado
                    && toContrato.DataNascimento.LerConteudoOuPadrao() > toContrato.DataAssinatura.LerConteudoOuPadrao();
        }
    }
}
