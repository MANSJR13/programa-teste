using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using Bergs.Pwx.Pwxoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>
    /// Classe base para validação de regra
    /// </summary>
    public abstract class Pxcsctxn_ValidadorRegra : AplicacaoRegraNegocio
    {
        /// <summary>
        /// Método de validação
        /// </summary>
        public abstract void ValidarRegra(TOContrato toContrato);
        /// <summary>
        /// Próxima regra a ser executada
        /// </summary>
        public abstract void ProximaRegra(Pxcsctxn_ValidadorRegra proximaRegra);










        /// <summary>
        /// Armazena retorno de falha
        /// </summary>
        protected Retorno<Int32> retorno;
        /// <summary>
        /// Retorna o estado da variavel retorno
        /// </summary>
        public bool RetornoEstaVazio()
        {
            return this.retorno == null;
        }
        /// <summary>
        /// 
        /// </summary>
        public Retorno<Int32> ObterRetornoValidacaoRegras()
        {
            return retorno ?? null;
        }
    }
}
