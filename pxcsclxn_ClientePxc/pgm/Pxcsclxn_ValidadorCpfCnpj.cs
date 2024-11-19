using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.RN;
using Bergs.Pxu.Pxuocfnm;
using Bergs.Pxu.Pxuocgnm;
using System;

namespace Bergs.Pxc.Pxcsclxn
{
    internal class ValidadorCpfCnpj : AplicacaoRegraNegocio
    {
        #region LER INSTRUÇÕES DE CONFIGURAÇÃO
        /* 
        https://wiki.corp.banrisul.com.br/RotinasGenericas:PXUOCFXW#Wrapper
        https://wiki.corp.banrisul.com.br/RotinasGenericas:PXUOCGXW#Wrapper
        * 
        * adicionar referência para:
        c:\bergs\bin\pxuocfnm.dll
        c:\bergs\bin\pxuocgnm.dll
        c:\soft\pwx\bin\pwxoinxn.dll
        lembrar de marcar as 3 referências para copylocal=FALSE
        */
        #endregion

        #region ValidarCpf
        /// <summary>Valida CPF.</summary>
        /// <param name="cpfComNC">CPF a ser validado.</param>
        /// <returns>Boolean informando se o CPF é válido ou não.</returns>
        public Retorno<Boolean> ValidarCpf(String cpfComNC)
        {
            try
            {
                var wrapper = this.Infra.InstanciarRotinaGenerica<FactoryPxuocf, IPxuocfnm>();
                // Vamos validar o CPF.
                var retorno = wrapper.Validar(cpfComNC);
                if (!retorno.Ok)
                {
                    // TODO
                    // O CPF não é válido. CPFs são considerados inválidos quando:
                    //  1. O número de controle é inválido
                    //  2. A String contém algum caractere não numérico
                    //  3. A String não possui o tamanho exato de 11 dígitos
                    // Leia a documentação para conhecer todas as causas possíveis
                    return this.Infra.RetornarSucesso<Boolean>(false, new Pwx.Pwxoiexn.Mensagens.OperacaoRealizadaMensagem());
                }
                return this.Infra.RetornarSucesso(true, new Pwx.Pwxoiexn.Mensagens.OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                //não esqueça de tratar as exeções que podem ser lançadas pelo wrapper.
                return this.Infra.TratarExcecao<bool>(ex);
            }
        }
        #endregion

        #region ValidarCnpj
        /// <summary>Valida CNPJ.</summary>
        /// <param name="cnpjComNC">CNPJ a ser validado.</param>
        /// <returns>Boolean informando se o CNPJ é válido ou não.</returns>
        public Retorno<Boolean> ValidarCnpj(String cnpjComNC)
        {
            try
            {
                var wrapper = this.Infra.InstanciarRotinaGenerica<FactoryPxuocg, IPxuocgnm>();
                wrapper.Versao = Versoes.A;
                wrapper.Pedido = Pedidos.Validar;
                wrapper.CNPJ = cnpjComNC;
                var retorno = wrapper.Executar();
                if (!retorno.Ok)
                {
                    return this.Infra.RetornarSucesso<Boolean>(false, new Pwx.Pwxoiexn.Mensagens.OperacaoRealizadaMensagem());
                }
                return this.Infra.RetornarSucesso<Boolean>(true, new Pwx.Pwxoiexn.Mensagens.OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                // Tratar uma possível exceção lançada pelo wrapper.
                return this.Infra.TratarExcecao<bool>(ex);
            }
        }
        #endregion
    }
}
