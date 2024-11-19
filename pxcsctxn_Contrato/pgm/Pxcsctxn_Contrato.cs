using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqctxn;
using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Relatorios;
using Bergs.Pwx.Pwxoiexn.RN;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcsctxn;

namespace Bergs.Pxc.Pxcsctxn
{ 
    /// <summary>Classe que possui as regras de negócio para o acesso da tabela CONTRATO da base de dados PXC.</summary>
    public class Contrato : AplicacaoRegraNegocio
    {
        #region Create
        /// <summary>Inclui registro na tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Incluir(TOContrato toContrato)
        {
            try
            {
                Pxcqctxn.Contrato bdContrato;
                Retorno<Int32> inclusaoContrato;

                #region Validação de campos
                //Valida que os campos obrigatórios foram informados
                if (!toContrato.Agencia.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("AGENCIA"));
                }
                if (!toContrato.Cnpj.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("CNPJ"));
                }
                if (!toContrato.DataAssinatura.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("DATA_ASSINATURA"));
                }
                if (!toContrato.DataNascimento.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("DATA_NASCIMENTO"));
                }
                if (!toContrato.NomeCliente.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NOME_CLIENTE"));
                }
                #endregion

                #region Validação de regras de negócio

                RegrasNegocioContrato regrasNegocio = this.Infra.InstanciarRN<RegrasNegocioContrato>();



                Retorno retorno = regrasNegocio.
                    InformarClasseTO(toContrato).
                    NomeClienteTemDuasOuMaisPalavras().
                    DataNascimentoMenorDataAssinatura().
                    DataAssinaturaMenorQueDataAtual().
                    EnderecoCompletamentePreenchidoOuSemEnderecoInformado().
                    ValorDoImovelDeveSerPositivo().
                    ValidarRegrasNegocioContrato();


                if (!retorno.OK)
                {
                    return this.Infra.RetornarFalha<Int32>(retorno.Mensagem);
                }

                #endregion

                bdContrato = this.Infra.InstanciarBD<Pxcqctxn.Contrato>();

                //Cria escopo transacional para garantir atomicidade
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    inclusaoContrato = bdContrato.Incluir(toContrato);
                    if (!inclusaoContrato.OK)
                    {
                        return inclusaoContrato;
                    }

                    escopo.EfetivarTransacao();
                    return this.Infra.RetornarSucesso(inclusaoContrato.Dados, new OperacaoRealizadaMensagem("Inclusão"));
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

        #region Read
        /// <summary>Conta quantidade de registros da tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int64> Contar(TOContrato toContrato)
        {
            try
            {
                Pxcqctxn.Contrato bdContrato;
                Retorno<Int64> contagemContrato;

                #region Validação de regras de negócio
                #endregion

                bdContrato = this.Infra.InstanciarBD<Pxcqctxn.Contrato>();

                contagemContrato = bdContrato.Contar(toContrato);
                if (!contagemContrato.OK)
                {
                    return contagemContrato;
                }

                return this.Infra.RetornarSucesso(contagemContrato.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int64>(ex);
            }
        }

        /// <summary>Lista registros da tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <param name="toPaginacao">Classe da infra-estrutura contendo as informações de paginação.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<List<TOContrato>> Listar(TOContrato toContrato, TOPaginacao toPaginacao)
        {
            try
            {
                Pxcqctxn.Contrato bdContrato;
                Retorno<List<TOContrato>> listagemContrato;

                #region Validação de regras de negócio
                #endregion

                bdContrato = this.Infra.InstanciarBD<Pxcqctxn.Contrato>();

                listagemContrato = bdContrato.Listar(toContrato, toPaginacao);
                if (!listagemContrato.OK)
                {
                    return listagemContrato;
                }

                return this.Infra.RetornarSucesso(listagemContrato.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<List<TOContrato>>(ex);
            }
        }

        /// <summary>Obtém registro da tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<TOContrato> Obter(TOContrato toContrato)
        {
            try
            {
                Pxcqctxn.Contrato bdContrato;
                Retorno<TOContrato> obtencaoContrato;

                #region Validação de campos
                //Valida que os campos que fazem parte da chave primária foram informados
                if (!toContrato.NumeroContrato.FoiSetado)
                {
                    return this.Infra.RetornarFalha<TOContrato>(new CampoObrigatorioMensagem("NUMERO_CONTRATO"));
                }
                #endregion

                #region Validação de regras de negócio
                #endregion

                bdContrato = this.Infra.InstanciarBD<Pxcqctxn.Contrato>();

                obtencaoContrato = bdContrato.Obter(toContrato);
                if (!obtencaoContrato.OK)
                {
                    return obtencaoContrato;
                }

                return this.Infra.RetornarSucesso(obtencaoContrato.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<TOContrato>(ex);
            }
        }
        #endregion

        #region Update
        /// <summary>Altera registro da tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Alterar(TOContrato toContrato)
        {
            try
            {
                Pxcqctxn.Contrato bdContrato;
                Retorno<Int32> alteracaoContrato;

                #region Validação de campos
                //Valida que os campos que fazem parte da chave primária foram informados
                if (!toContrato.NumeroContrato.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NUMERO_CONTRATO"));
                }
                //Valida que o campo que mantém o controle de acessos concorrentes foi informado
                if (!toContrato.UltAtualizacao.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("ULT_ATUALIZACAO"));
                }
                #endregion

                #region Validação de regras de negócio

                //RegrasNegocioContrato regrasNegocio = this.Infra.InstanciarRN<RegrasNegocioContrato>();


                toContrato.NomeCliente = "umapalavra";
                Pxcsctxn_ValidadorRegra regraNome = this.Infra.InstanciarRN<Pxcsctxn_NomeClienteTemDuasOuMaisPalavras>();
                Pxcsctxn_ValidadorRegra regraDataAssinatura = this.Infra.InstanciarRN<Pxcsctxn_DataAssinaturaMenorQueDataAtual>();
                Pxcsctxn_ValidadorRegra regraDataNascimento = this.Infra.InstanciarRN<Pxcsctxn_DataNascimentoMenorDataAssinatura>();
                Pxcsctxn_ValidadorRegra regraEndereco = this.Infra.InstanciarRN<Pxcsctxn_EnderecoCompletamentePreenchidoOuSemEnderecoInformado>();
                Pxcsctxn_ValidadorRegra regraValorImovel = this.Infra.InstanciarRN<Pxcsctxn_ValorDoImovelDeveSerPositivo>();

                regraNome.ProximaRegra(regraDataAssinatura);
                regraDataAssinatura.ProximaRegra(regraDataNascimento);
                regraDataNascimento.ProximaRegra(regraEndereco);
                regraEndereco.ProximaRegra(regraValorImovel);

                regraNome.ValidarRegra(toContrato);
                Retorno<Int32> retorno = regraNome.ObterRetornoValidacaoRegras();
                retorno = retorno ?? this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem());
                if (!retorno.OK)
                {
                    return this.Infra.RetornarFalha<Int32>(retorno.Mensagem);
                }

                #endregion

                bdContrato = this.Infra.InstanciarBD<Pxcqctxn.Contrato>();

                //Cria escopo transacional para garantir atomicidade
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    alteracaoContrato = bdContrato.Alterar(toContrato);
                    if (!alteracaoContrato.OK)
                    {
                        return alteracaoContrato;
                    }

                    escopo.EfetivarTransacao();
                    return this.Infra.RetornarSucesso(alteracaoContrato.Dados, new OperacaoRealizadaMensagem("Alteração"));
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

        #region Delete
        /// <summary>Exclui registro da tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Excluir(TOContrato toContrato)
        {
            try
            {
                Pxcqctxn.Contrato bdContrato;
                Retorno<Int32> exclusaoContrato;

                #region Validação de campos
                //Valida que os campos que fazem parte da chave primária foram informados
                if (!toContrato.NumeroContrato.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NUMERO_CONTRATO"));
                }
                //Valida que o campo que mantém o controle de acessos concorrentes foi informado
                if (!toContrato.UltAtualizacao.FoiSetado)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("ULT_ATUALIZACAO"));
                }
                #endregion

                #region Validação de regras de negócio
                #endregion

                bdContrato = this.Infra.InstanciarBD<Pxcqctxn.Contrato>();

                //Cria escopo transacional para garantir atomicidade
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    exclusaoContrato = bdContrato.Excluir(toContrato);
                    if (!exclusaoContrato.OK)
                    {
                        return exclusaoContrato;
                    }

                    escopo.EfetivarTransacao();
                    return this.Infra.RetornarSucesso(exclusaoContrato.Dados, new OperacaoRealizadaMensagem("Exclusão"));
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