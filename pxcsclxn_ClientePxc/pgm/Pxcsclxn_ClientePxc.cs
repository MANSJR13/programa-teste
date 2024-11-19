using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using Bergs.Pwx.Pwxoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>Classe que possui as regras de negócio para o acesso da tabela CLIENTE_PXC da base de dados PXC.</summary>
    public class ClientePxc : AplicacaoRegraNegocio
    {
        #region Create
        /// <summary>Inclui registro na tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Incluir(TOClientePxc toClientePxc)
        {
            try
            {
                Pxcqclxn.ClientePxc bdClientePxc;
                Retorno<Int32> inclusaoClientePxc;

                toClientePxc.DtAbeCad = DateTime.Now;

                #region Validação de campos
                //Valida que os campos obrigatórios foram informados
                if (!toClientePxc.Agencia.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("AGENCIA"));
                }
                if (!toClientePxc.CodCliente.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toClientePxc.CodOperador.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_OPERADOR"));
                }
                if (!toClientePxc.DtAbeCad.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("DT_ABE_CAD"));
                }
                if (!toClientePxc.NomeCliente.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NOME_CLIENTE"));
                }
                if (!toClientePxc.TipoPessoa.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                #endregion

                #region Validação de regras de negócio

                RegrasNegocioCliente regrasNegocio = this.Infra.InstanciarRN<RegrasNegocioCliente>();

                Retorno retorno = regrasNegocio.InformarClasseTO(toClientePxc)
                    .ValidaCPF()
                    .ValidarCnpj()
                    .NomeClienteTemDuasOuMaisPalavras()
                    .VerificaCamposPessoaFisica()
                    .VerificaCamposPessoaJuridica()
                    .NomeFantasiaTemDuasOuMaisPalavras()
                    .DataConstituicaoMenorQueAtual()
                    .CapitalSocialPositivo()
                    .NomeMaeTemDuasOuMaisPalavras()
                    .ValidarRegrasNegocioCliente();

                if (!retorno.OK)
                {
                    return this.Infra.RetornarFalha<Int32>(retorno.Mensagem);
                }

                #endregion

                bdClientePxc = this.Infra.InstanciarBD<Pxcqclxn.ClientePxc>();

                //Cria escopo transacional para garantir atomicidade
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    inclusaoClientePxc = bdClientePxc.Incluir(toClientePxc);
                    if (!inclusaoClientePxc.OK)
                    {
                        return inclusaoClientePxc;
                    }

                    escopo.EfetivarTransacao();
                    return this.Infra.RetornarSucesso(inclusaoClientePxc.Dados, new OperacaoRealizadaMensagem("Inclusão"));
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

        #region Read
        /// <summary>Conta quantidade de registros da tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int64> Contar(TOClientePxc toClientePxc)
        {
            try
            {
                Pxcqclxn.ClientePxc bdClientePxc;
                Retorno<Int64> contagemClientePxc;

                #region Validação de regras de negócio
                #endregion

                bdClientePxc = this.Infra.InstanciarBD<Pxcqclxn.ClientePxc>();

                contagemClientePxc = bdClientePxc.Contar(toClientePxc);
                if (!contagemClientePxc.OK)
                {
                    return contagemClientePxc;
                }

                return this.Infra.RetornarSucesso(contagemClientePxc.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int64>(ex);
            }
        }

        /// <summary>Lista registros da tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <param name="toPaginacao">Classe da infra-estrutura contendo as informações de paginação.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<List<TOClientePxc>> Listar(TOClientePxc toClientePxc, TOPaginacao toPaginacao)
        {
            try
            {
                Pxcqclxn.ClientePxc bdClientePxc;
                Retorno<List<TOClientePxc>> listagemClientePxc;

                #region Validação de regras de negócio
                #endregion

                bdClientePxc = this.Infra.InstanciarBD<Pxcqclxn.ClientePxc>();

                listagemClientePxc = bdClientePxc.Listar(toClientePxc, toPaginacao);
                if (!listagemClientePxc.OK)
                {
                    return listagemClientePxc;
                }

                return this.Infra.RetornarSucesso(listagemClientePxc.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<List<TOClientePxc>>(ex);
            }
        }

        /// <summary>Obtém registro da tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<TOClientePxc> Obter(TOClientePxc toClientePxc)
        {
            try
            {
                Pxcqclxn.ClientePxc bdClientePxc;
                Retorno<TOClientePxc> obtencaoClientePxc;

                #region Validação de campos
                //Valida que os campos que fazem parte da chave primária foram informados
                if (!toClientePxc.CodCliente.FoiSetado)
                {
                    return this.Infra.RetornarFalha<TOClientePxc>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toClientePxc.TipoPessoa.FoiSetado)
                {
                    return this.Infra.RetornarFalha<TOClientePxc>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                #endregion

                #region Validação de regras de negócio
                #endregion

                bdClientePxc = this.Infra.InstanciarBD<Pxcqclxn.ClientePxc>();

                obtencaoClientePxc = bdClientePxc.Obter(toClientePxc);
                if (!obtencaoClientePxc.OK)
                {
                    return obtencaoClientePxc;
                }

                return this.Infra.RetornarSucesso(obtencaoClientePxc.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<TOClientePxc>(ex);
            }
        }
        #endregion

        #region Update
        /// <summary>Altera registro da tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Alterar(TOClientePxc toClientePxc)
        {
            try
            {
                Pxcqclxn.ClientePxc bdClientePxc;
                Retorno<Int32> alteracaoClientePxc;

                #region Validação de campos
                //Valida que os campos que fazem parte da chave primária foram informados
                if (!toClientePxc.CodCliente.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toClientePxc.TipoPessoa.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                //Valida que o campo que mantém o controle de acessos concorrentes foi informado
                if (!toClientePxc.UltAtualizacao.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("ULT_ATUALIZACAO"));
                }
                #endregion

                #region Validação de regras de negócio

                RegrasNegocioCliente regrasNegocio = this.Infra.InstanciarRN<RegrasNegocioCliente>();

                Retorno retorno = regrasNegocio.InformarClasseTO(toClientePxc)
                    .ValidaCPF()
                    .ValidarCnpj()
                    .NomeClienteTemDuasOuMaisPalavras()
                    .VerificaCamposPessoaFisica()
                    .VerificaCamposPessoaJuridica()
                    .NomeFantasiaTemDuasOuMaisPalavras()
                    .DataConstituicaoMenorQueAtual()
                    .CapitalSocialPositivo()
                    .NomeMaeTemDuasOuMaisPalavras()
                    .ValidarRegrasNegocioCliente();

                if (!retorno.OK)
                {
                    return this.Infra.RetornarFalha<Int32>(retorno.Mensagem);
                }

                #endregion

                bdClientePxc = this.Infra.InstanciarBD<Pxcqclxn.ClientePxc>();

                toClientePxc.DtAbeCad = new CampoObrigatorio<DateTime>();

                //Cria escopo transacional para garantir atomicidade
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    alteracaoClientePxc = bdClientePxc.Alterar(toClientePxc);
                    if (!alteracaoClientePxc.OK)
                    {
                        return alteracaoClientePxc;
                    }

                    escopo.EfetivarTransacao();
                    return this.Infra.RetornarSucesso(alteracaoClientePxc.Dados, new OperacaoRealizadaMensagem("Alteração"));
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

        #region Delete
        /// <summary>Exclui registro da tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Excluir(TOClientePxc toClientePxc)
        {
            try
            {
                Pxcqclxn.ClientePxc bdClientePxc;
                Retorno<Int32> exclusaoClientePxc;

                #region Validação de campos
                //Valida que os campos que fazem parte da chave primária foram informados
                if (!toClientePxc.CodCliente.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toClientePxc.TipoPessoa.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                //Valida que o campo que mantém o controle de acessos concorrentes foi informado
                if (!toClientePxc.UltAtualizacao.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("ULT_ATUALIZACAO"));
                }
                #endregion

                #region Validação de regras de negócio
                #endregion

                bdClientePxc = this.Infra.InstanciarBD<Pxcqclxn.ClientePxc>();

                //Cria escopo transacional para garantir atomicidade
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    exclusaoClientePxc = bdClientePxc.Excluir(toClientePxc);
                    if (!exclusaoClientePxc.OK)
                    {
                        return exclusaoClientePxc;
                    }

                    escopo.EfetivarTransacao();
                    return this.Infra.RetornarSucesso(exclusaoClientePxc.Dados, new OperacaoRealizadaMensagem("Exclusão"));
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion
	} 
}