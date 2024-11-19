using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pwx.Pwxodaxn;
using Bergs.Pwx.Pwxodaxn.Excecoes;
using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.BD;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace Bergs.Pxc.Pxcqclxn
{
    /// <summary>Classe que possui os métodos de manipulação de dados da tabela CLIENTE_PXC da base de dados PXC.</summary>
    public class ClientePxc : AplicacaoDados
    {
        #region Create
        /// <summary>Método incluir referente à tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Incluir(TOClientePxc toClientePxc)
        {
            try
            {
                Int32 registrosAfetados;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Parametros.Clear();
                toClientePxc.CodOperador = Infra.Usuario.Matricula;
                //Inicia montagem do comando
                this.Sql.Comando.Append("INSERT INTO PXC.CLIENTE_PXC15 (");
                //Monta campos que serão inseridos
                this.MontarInsert(toClientePxc);

                //Une os buffers de montagem do comando
                this.Sql.Comando.Append(") VALUES (");
                this.Sql.Comando.Append(this.Sql.Temporario.ToString());

                this.Sql.Comando.Append(")");

                //Executa o comando
                registrosAfetados = this.IncluirDados();

                return this.Infra.RetornarSucesso(registrosAfetados);
            }
            catch (RegistroDuplicadoException ex)
            {
                return this.Infra.RetornarFalha<Int32>(new RegistroDuplicadoMensagem(ex));
            }
            catch (ChaveEstrangeiraInexistenteException ex)
            {
                return this.Infra.RetornarFalha<Int32>(new ChaveEstrangeiraInexistenteMensagem(ex));
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

        #region Read
        /// <summary>Método contar referente à tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int64> Contar(TOClientePxc toClientePxc)
        {
            try
            {
                Int64 quantidadeRegistros;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("SELECT COUNT(*) FROM PXC.CLIENTE_PXC15");
                //Filtra consulta pelos dados informados no TO
                this.MontarWhere(toClientePxc);

                //Executa o comando
                quantidadeRegistros = this.ContarDados();

                return this.Infra.RetornarSucesso(quantidadeRegistros);
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int64>(ex);
            }
        }

        /// <summary>Método listar referente à tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <param name="toPaginacao">Classe da infra-estrutura contendo as informações de paginação.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<List<TOClientePxc>> Listar(TOClientePxc toClientePxc, TOPaginacao toPaginacao)
        {
            try
            {
                List<TOClientePxc> dados;
                TOClientePxc toRetorno;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("SELECT ");
                this.Sql.Comando.Append("AGENCIA, ");
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("COD_OPERADOR, ");
                this.Sql.Comando.Append("DT_ABE_CAD, ");
                this.Sql.Comando.Append("DT_CONSTITUICAO, ");
                this.Sql.Comando.Append("IND_FUNC_BANRISUL, ");
                this.Sql.Comando.Append("NOME_CLIENTE, ");
                this.Sql.Comando.Append("NOME_FANTASIA, ");
                this.Sql.Comando.Append("NOME_MAE, ");
                this.Sql.Comando.Append("TIPO_PESSOA, ");
                this.Sql.Comando.Append("ULT_ATUALIZACAO, ");
                this.Sql.Comando.Append("ULT_NOSSO_NRO, ");
                this.Sql.Comando.Append("VLR_CAPITAL_SOCIAL ");
                this.Sql.Comando.Append("FROM PXC.CLIENTE_PXC15");
                //Filtra consulta pelos dados informados no TO
                this.MontarWhere(toClientePxc);

                dados = new List<TOClientePxc>();

                if (toPaginacao == null)
                {
                    //Executa o comando sem utilizar paginação
                    using (ListaConectada listaConectada = this.ListarDados())
                    {
                        //Cria TO para cada tupla retornada
                        while (listaConectada.Ler())
                        {
                            toRetorno = new TOClientePxc();
                            toRetorno.PopularRetorno(listaConectada.LinhaAtual);
                            dados.Add(toRetorno);
                        }
                    }
                }
                else
                {
                    //Executa o comando utilizando paginação
                    ListaDesconectada listaDesconectada = this.ListarDados(toPaginacao);

                    //Cria TO para cada tupla retornada
                    foreach (Linha linha in listaDesconectada.Linhas)
                    {
                        toRetorno = new TOClientePxc();
                        toRetorno.PopularRetorno(linha);
                        dados.Add(toRetorno);
                    }
                }

                return this.Infra.RetornarSucesso(dados);
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<List<TOClientePxc>>(ex);
            }
        }

        /// <summary>Método obter referente à tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<TOClientePxc> Obter(TOClientePxc toClientePxc)
        {
            try
            {
                Linha linha;
                TOClientePxc dados;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("SELECT ");
                this.Sql.Comando.Append("AGENCIA, ");
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("COD_OPERADOR, ");
                this.Sql.Comando.Append("DT_ABE_CAD, ");
                this.Sql.Comando.Append("DT_CONSTITUICAO, ");
                this.Sql.Comando.Append("IND_FUNC_BANRISUL, ");
                this.Sql.Comando.Append("NOME_CLIENTE, ");
                this.Sql.Comando.Append("NOME_FANTASIA, ");
                this.Sql.Comando.Append("NOME_MAE, ");
                this.Sql.Comando.Append("TIPO_PESSOA, ");
                this.Sql.Comando.Append("ULT_ATUALIZACAO, ");
                this.Sql.Comando.Append("ULT_NOSSO_NRO, ");
                this.Sql.Comando.Append("VLR_CAPITAL_SOCIAL ");
                this.Sql.Comando.Append("FROM PXC.CLIENTE_PXC15");
                //Filtra consulta pelos dados informados no TO
                this.MontarWhereChaves(toClientePxc);

                //Executa o comando
                linha = this.ObterDados();
                if (linha == null)
                {
                    return this.Infra.RetornarFalha<TOClientePxc>(new RegistroInexistenteMensagem());
                }

                //Cria TO para a tupla retornada
                dados = new TOClientePxc();
                dados.PopularRetorno(linha);

                return this.Infra.RetornarSucesso(dados);
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<TOClientePxc>(ex);
            }
        }
        #endregion

        #region Update
        /// <summary>Método alterar referente à tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Alterar(TOClientePxc toClientePxc)
        {
            try
            {
                Int32 registrosAfetados;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();
                toClientePxc.CodOperador = Infra.Usuario.Matricula;

                //Inicia montagem do comando
                this.Sql.Comando.Append("UPDATE PXC.CLIENTE_PXC15");
                //Monta campos que serão modificados
                this.MontarSet(toClientePxc);
                //Filtra a alteração pelas chaves da tabela
                this.MontarWhereChaves(toClientePxc);
                //Filtra a alteração pelo campo de controle de acessos concorrentes
                this.Sql.MontarCampoWhere("ULT_ATUALIZACAO", toClientePxc.UltAtualizacao);

                //Executa o comando
                registrosAfetados = this.AlterarDados();
                if (registrosAfetados == 0)
                {
                    return this.Infra.RetornarFalha<Int32>(new ConcorrenciaMensagem());
                }

                return this.Infra.RetornarSucesso(registrosAfetados);
            }
            catch (ChaveEstrangeiraInexistenteException ex)
            {
                return this.Infra.RetornarFalha<Int32>(new ChaveEstrangeiraInexistenteMensagem(ex));
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

        #region Delete
        /// <summary>Método excluir referente à tabela CLIENTE_PXC.</summary>
        /// <param name="toClientePxc">Transfer Object de entrada referente à tabela CLIENTE_PXC.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Excluir(TOClientePxc toClientePxc)
        {
            try
            {
                Int32 registrosAfetados;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();
                toClientePxc.CodOperador = Infra.Usuario.Matricula;

                //Inicia montagem do comando
                this.Sql.Comando.Append("DELETE FROM PXC.CLIENTE_PXC15");
                //Filtra a exclusão pelas chaves da tabela
                this.MontarWhereChaves(toClientePxc);
                //Filtra a exclusão pelo campo de controle de acessos concorrentes
                this.Sql.MontarCampoWhere("ULT_ATUALIZACAO", toClientePxc.UltAtualizacao);

                //Executa o comando
                registrosAfetados = this.ExcluirDados();
                if (registrosAfetados == 0)
                {
                    return this.Infra.RetornarFalha<Int32>(new ConcorrenciaMensagem());
                }

                return this.Infra.RetornarSucesso(registrosAfetados);
            }
            catch (ChaveEstrangeiraReferenciadaException ex)
            {
                return this.Infra.RetornarFalha<Int32>(new ChaveEstrangeiraReferenciadaMensagem(ex));
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

        #region Métodos Auxiliares
        /// <summary>Monta campos para cláusula WHERE.</summary>
        /// <param name="toClientePxc">TO contendo os campos.</param>
        private void MontarWhere(TOClientePxc toClientePxc)
        {
            //Monta no WHERE todos os campos da tabela que foram informados
            
            this.MontarWhereChaves(toClientePxc);
            this.MontarCampos(this.Sql.MontarCampoWhere, toClientePxc);
            
			this.Sql.MontarCampoWhere("ULT_ATUALIZACAO", toClientePxc.UltAtualizacao);
        }
        
        /// <summary>Monta campos chave para cláusula WHERE.</summary>
        /// <param name="toClientePxc">TO contendo os campos.</param>
        private void MontarWhereChaves(TOClientePxc toClientePxc)
        {
            //Monta no WHERE todos os campos chave da tabela
            
            this.MontarCamposChave(this.Sql.MontarCampoWhere, toClientePxc);
        }
        
        /// <summary>Monta campos para cláusula SET.</summary>
        /// <param name="toClientePxc">TO contendo os campos.</param>
        private void MontarSet(TOClientePxc toClientePxc)
        {
            //Monta no SET todos os campos não chave da tabela que foram informados
            
            this.MontarCampos(this.Sql.MontarCampoSet, toClientePxc);
            this.Sql.MontarCampoSet("ULT_ATUALIZACAO");
            this.Sql.Comando.Append("CURRENT_TIMESTAMP");
        }
        
        /// <summary>Monta campos para cláusula INSERT.</summary>
        /// <param name="toClientePxc">TO contendo os campos.</param>
        private void MontarInsert(TOClientePxc toClientePxc)
        {
            //Monta no INSERT todos os campos da tabela que foram informados
            
            this.MontarCamposChave(this.Sql.MontarCampoInsert, toClientePxc);
            this.MontarCampos(this.Sql.MontarCampoInsert, toClientePxc);
            this.Sql.MontarCampoInsert("ULT_ATUALIZACAO");
            this.Sql.Temporario.Append("CURRENT_TIMESTAMP");
        }
        
        /// <summary>Executa uma ação nos campos chave de um TO.</summary>
        /// <param name="montagem">Ação a ser executada.</param>
        /// <param name="toClientePxc">TO alvo das ações.</param>
        private void MontarCamposChave(ConstrutorSql.MontarCampo montagem, TOClientePxc toClientePxc)
        {   
            //Invoca qualquer comando simples de montagem nos campos chave da tabela
            
            montagem.Invoke("COD_CLIENTE", toClientePxc.CodCliente);
            montagem.Invoke("TIPO_PESSOA", toClientePxc.TipoPessoa);
        }
        
        /// <summary>Executa uma ação nos campos não chave de um TO.</summary>
        /// <param name="montagem">Ação a ser executada.</param>
        /// <param name="toClientePxc">TO alvo das ações.</param>
        private void MontarCampos(ConstrutorSql.MontarCampo montagem, TOClientePxc toClientePxc)
        {   
            //Invoca qualquer comando simples de montagem nos campos não chave da tabela, exceto no que faz controle de acessos concorrentes
            
            montagem.Invoke("AGENCIA", toClientePxc.Agencia);
            montagem.Invoke("COD_OPERADOR", toClientePxc.CodOperador);
            montagem.Invoke("DT_ABE_CAD", toClientePxc.DtAbeCad);
            montagem.Invoke("DT_CONSTITUICAO", toClientePxc.DtConstituicao);
            montagem.Invoke("IND_FUNC_BANRISUL", toClientePxc.IndFuncBanrisul);
            montagem.Invoke("NOME_CLIENTE", toClientePxc.NomeCliente);
            montagem.Invoke("NOME_FANTASIA", toClientePxc.NomeFantasia);
            montagem.Invoke("NOME_MAE", toClientePxc.NomeMae);
            montagem.Invoke("ULT_NOSSO_NRO", toClientePxc.UltNossoNro);
            montagem.Invoke("VLR_CAPITAL_SOCIAL", toClientePxc.VlrCapitalSocial);
        }

        /// <summary>Cria um parâmetro para a instrução SQL.</summary>
        /// <param name="nomeCampo">Nome do campo da tabela.</param>
        /// <param name="conteudo">Valor para o parâmetro.</param>
        /// <returns>Parâmetro recém-criado.</returns>
        protected override Parametro CriarParametro(String nomeCampo, Object conteudo)
        {
            Parametro parametro = new Parametro();
            switch (nomeCampo)
            {   
                #region Chaves Primárias
                case "COD_CLIENTE":
                    parametro.Precision = 14;
                    parametro.Size = 14;
                    parametro.DbType = DbType.String;
                    break;
                case "TIPO_PESSOA":
                    parametro.Precision = 1;
                    parametro.Size = 1;
                    parametro.DbType = DbType.String;
                    break;                        
                #endregion

                #region Campos Obrigatórios
                case "AGENCIA":
                    parametro.Precision = 2;
                    parametro.Size = 2;
                    parametro.DbType = DbType.Int16;
                    break;
                case "COD_OPERADOR":
                    parametro.Precision = 6;
                    parametro.Size = 6;
                    parametro.DbType = DbType.String;
                    break;
                case "DT_ABE_CAD":
                    parametro.Precision = 4;
                    parametro.Size = 4;
                    parametro.DbType = DbType.Date;
                    break;
                case "NOME_CLIENTE":
                    parametro.Precision = 50;
                    parametro.Size = 50;
                    parametro.DbType = DbType.String;
                    break;
                case "ULT_ATUALIZACAO":
                    parametro.Precision = 10;
                    parametro.Scale = 6;
                    parametro.Size = 10;
                    parametro.DbType = DbType.DateTime;
                    break;
                #endregion

                #region Campos Opcionais
                case "DT_CONSTITUICAO":
                    parametro.Precision = 4;
                    parametro.Size = 4;
                    parametro.DbType = DbType.Date;
                    break;
                case "IND_FUNC_BANRISUL":
                    parametro.Precision = 1;
                    parametro.Size = 1;
                    parametro.DbType = DbType.String;
                    break;
                case "NOME_FANTASIA":
                    parametro.Precision = 30;
                    parametro.Size = 30;
                    parametro.DbType = DbType.String;
                    break;
                case "NOME_MAE":
                    parametro.Precision = 30;
                    parametro.Size = 30;
                    parametro.DbType = DbType.String;
                    break;
                case "ULT_NOSSO_NRO":
                    parametro.Precision = 4;
                    parametro.Size = 4;
                    parametro.DbType = DbType.Int32;
                    break;
                case "VLR_CAPITAL_SOCIAL":
                    parametro.Precision = 15;
                    parametro.Scale = 2;
                    parametro.Size = 15;
                    parametro.DbType = DbType.Decimal;
                    break;

#if DEBUG
                default:
                    //Força um erro em modo debug para alertar o programador caso tenha caido no default
                    //Todo parâmetro deve cair em um case neste switch
                    parametro = null;
                    break;
#endif
                #endregion                
            }
            parametro.Direction = ParameterDirection.Input;
            parametro.SourceColumn = nomeCampo;
            
            if (parametro.Scale > 0 && conteudo != null &&  parametro.DbType != DbType.DateTime)
            {
                parametro.Value = String.Format(CultureInfo.InvariantCulture, "{0:F" + parametro.Scale + "}", conteudo);
            }
            else
            {
                parametro.Value = conteudo;
            }
            
            return parametro;
        }
        #endregion
    }
}