using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>
    /// Classe base para validação de regra
    /// </summary>
    public class Pxcsctxn_NomeClienteTemDuasOuMaisPalavras : Pxcsctxn_ValidadorRegra
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
            if (this.RetornoEstaVazio() && NomeClienteTemDuasOuMaisPalavras(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN01, "NOME_CLIENTE", "2"));
            }
            else if(this.proximaRegra != null)
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
        /// Verificar se o Nome do Cliente possui, no mínimo, 2 palavras.
        /// </summary>
        /// <returns></returns>
        private bool NomeClienteTemDuasOuMaisPalavras(TOContrato toContrato)
        {
            return toContrato.NomeCliente.FoiSetado && !toContrato.NomeCliente.ToString().Trim().Contains(" ");
        }
    }
}
