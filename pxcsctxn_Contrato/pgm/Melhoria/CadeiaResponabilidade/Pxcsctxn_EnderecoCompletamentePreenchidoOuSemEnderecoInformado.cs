using Bergs.Pxc.Pxcbtoxn;
using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>
    /// Classe base para validação de regra
    /// </summary>
    public class Pxcsctxn_EnderecoCompletamentePreenchidoOuSemEnderecoInformado : Pxcsctxn_ValidadorRegra
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
            if (this.RetornoEstaVazio() && EnderecoCompletamentePreenchidoOuSemEnderecoInformado(toContrato))
            {
                this.retorno = this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.RN04));
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
    }
}
