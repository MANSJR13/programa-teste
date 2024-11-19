using Bergs.Pwx.Pwxoiexn;
using Bergs.Pxc.Pxcbtoxn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.Pxc.Pxcsctxn.pgm.Melhoria.CadeiaResponabilidade
{
    /// <summary>
    /// 
    /// </summary>
    public class UsandoImplementacao
    {
        /// <summary>
        /// 
        /// </summary>
        public void ChamarCadeia(TOContrato toContrato)
        {
            Pxcsctxn_ValidadorRegra regraNome = new Pxcsctxn_NomeClienteTemDuasOuMaisPalavras();
            Pxcsctxn_ValidadorRegra regraDataAssinatura = new Pxcsctxn_DataAssinaturaMenorQueDataAtual();
            Pxcsctxn_ValidadorRegra regraDataNascimento = new Pxcsctxn_DataNascimentoMenorDataAssinatura();
            Pxcsctxn_ValidadorRegra regraEndereco = new Pxcsctxn_EnderecoCompletamentePreenchidoOuSemEnderecoInformado();
            Pxcsctxn_ValidadorRegra regraValorImovel = new Pxcsctxn_ValorDoImovelDeveSerPositivo();

            regraNome.ProximaRegra(regraDataAssinatura);
            regraDataAssinatura.ProximaRegra(regraDataNascimento);
            regraDataNascimento.ProximaRegra(regraEndereco);
            regraEndereco.ProximaRegra(regraValorImovel);

            regraNome.ValidarRegra(toContrato);
            Retorno<Int32> retorno = regraNome.ObterRetornoValidacaoRegras();
        }
    }
}
