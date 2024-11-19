using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>
    /// Classe base para validação de regra
    /// </summary>
    public class Pxcsctxn_ValorDoImovelDeveSerPositivo : Pxcsctxn_ValidadorRegra
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
            if (this.RetornoEstaVazio() && ValorDoImovelDeveSerPositivo(toContrato))
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
        /// O Valor do Imóvel, se informado, deve ser maior que zero.
        /// </summary>
        public bool ValorDoImovelDeveSerPositivo(TOContrato toContrato)
        {
            return toContrato.ValorImovel.TemConteudo && toContrato.ValorImovel.LerConteudoOuPadrao() <= 0;
        }
    }
}
