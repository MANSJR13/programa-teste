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

namespace Bergs.Pxc.Pxcqctxn
{
    /// <summary>Classe que possui os métodos de manipulação de dados da tabela CONTRATO da base de dados PXC.</summary>
    public class Contrato : AplicacaoDados
    {
        #region Create
        /// <summary>Método incluir referente à tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Incluir(TOContrato toContrato)
        {
            try
            {
                Int32 registrosAfetados;
                Linha linha;
                /* Busca o próximo valor para NUMERO_CONTRATO na sequence correspondente. */
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("SELECT PXC.SEQ_CONTRATO.NEXTVAL NUMERO_CONTRATO FROM SYSIBM.SYSDUMMY1");

                //Executa o comando
                linha = this.ObterDados();
                if (linha == null)
                {
                    return this.Infra.RetornarFalha<Int32>(new RegistroInexistenteMensagem());
                }
                //Insere COD_CATEGORIA no TO
                toContrato.PopularRetorno(linha);

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("INSERT INTO PXC.CONTRATO15 (");
                //Monta campos que serão inseridos
                this.MontarInsert(toContrato);

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
        /// <summary>Método contar referente à tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int64> Contar(TOContrato toContrato)
        {
            try
            {
                Int64 quantidadeRegistros;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("SELECT COUNT(*) FROM PXC.CONTRATO15");
                //Filtra consulta pelos dados informados no TO
                this.MontarWhere(toContrato);

                //Executa o comando
                quantidadeRegistros = this.ContarDados();

                return this.Infra.RetornarSucesso(quantidadeRegistros);
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int64>(ex);
            }
        }

        /// <summary>Método listar referente à tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <param name="toPaginacao">Classe da infra-estrutura contendo as informações de paginação.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<List<TOContrato>> Listar(TOContrato toContrato, TOPaginacao toPaginacao)
        {
            try
            {
                List<TOContrato> dados;
                TOContrato toRetorno;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("SELECT ");
                this.Sql.Comando.Append("AGENCIA, ");
                this.Sql.Comando.Append("CEP, ");
                this.Sql.Comando.Append("CIDADE, ");
                this.Sql.Comando.Append("CNPJ, ");
                this.Sql.Comando.Append("DATA_ASSINATURA, ");
                this.Sql.Comando.Append("DATA_NASCIMENTO, ");
                this.Sql.Comando.Append("ENDERECO, ");
                this.Sql.Comando.Append("NOME_CLIENTE, ");
                this.Sql.Comando.Append("NUMERO_CONTRATO, ");
                this.Sql.Comando.Append("TIPO_IMOVEL, ");
                this.Sql.Comando.Append("UF, ");
                this.Sql.Comando.Append("ULT_ATUALIZACAO, ");
                this.Sql.Comando.Append("VALOR_IMOVEL ");
                this.Sql.Comando.Append("FROM PXC.CONTRATO15");
                //Filtra consulta pelos dados informados no TO
                this.MontarWhere(toContrato);

                dados = new List<TOContrato>();

                if (toPaginacao == null)
                {
                    //Executa o comando sem utilizar paginação
                    using (ListaConectada listaConectada = this.ListarDados())
                    {
                        //Cria TO para cada tupla retornada
                        while (listaConectada.Ler())
                        {
                            toRetorno = new TOContrato();
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
                        toRetorno = new TOContrato();
                        toRetorno.PopularRetorno(linha);
                        dados.Add(toRetorno);
                    }
                }

                return this.Infra.RetornarSucesso(dados);
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<List<TOContrato>>(ex);
            }
        }

        /// <summary>Método obter referente à tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<TOContrato> Obter(TOContrato toContrato)
        {
            try
            {
                Linha linha;
                TOContrato dados;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("SELECT ");
                this.Sql.Comando.Append("AGENCIA, ");
                this.Sql.Comando.Append("CEP, ");
                this.Sql.Comando.Append("CIDADE, ");
                this.Sql.Comando.Append("CNPJ, ");
                this.Sql.Comando.Append("DATA_ASSINATURA, ");
                this.Sql.Comando.Append("DATA_NASCIMENTO, ");
                this.Sql.Comando.Append("ENDERECO, ");
                this.Sql.Comando.Append("NOME_CLIENTE, ");
                this.Sql.Comando.Append("NUMERO_CONTRATO, ");
                this.Sql.Comando.Append("TIPO_IMOVEL, ");
                this.Sql.Comando.Append("UF, ");
                this.Sql.Comando.Append("ULT_ATUALIZACAO, ");
                this.Sql.Comando.Append("VALOR_IMOVEL ");
                this.Sql.Comando.Append("FROM PXC.CONTRATO15");
                //Filtra consulta pelos dados informados no TO
                this.MontarWhereChaves(toContrato);

                //Executa o comando
                linha = this.ObterDados();
                if (linha == null)
                {
                    return this.Infra.RetornarFalha<TOContrato>(new RegistroInexistenteMensagem());
                }

                //Cria TO para a tupla retornada
                dados = new TOContrato();
                dados.PopularRetorno(linha);

                return this.Infra.RetornarSucesso(dados);
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<TOContrato>(ex);
            }
        }
        #endregion

        #region Update
        /// <summary>Método alterar referente à tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Alterar(TOContrato toContrato)
        {
            try
            {
                Int32 registrosAfetados;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("UPDATE PXC.CONTRATO15");
                //Monta campos que serão modificados
                this.MontarSet(toContrato);
                //Filtra a alteração pelas chaves da tabela
                this.MontarWhereChaves(toContrato);
                //Filtra a alteração pelo campo de controle de acessos concorrentes
                this.Sql.MontarCampoWhere("ULT_ATUALIZACAO", toContrato.UltAtualizacao);

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
        /// <summary>Método excluir referente à tabela CONTRATO.</summary>
        /// <param name="toContrato">Transfer Object de entrada referente à tabela CONTRATO.</param>
        /// <returns>Classe de retorno contendo as informações de resposta ou as informações de erro.</returns>
        public virtual Retorno<Int32> Excluir(TOContrato toContrato)
        {
            try
            {
                Int32 registrosAfetados;

                //Limpa as propriedades utilizadas para a montagem do comando
                this.Sql.Comando.Length = 0;
                this.Parametros.Clear();

                //Inicia montagem do comando
                this.Sql.Comando.Append("DELETE FROM PXC.CONTRATO15");
                //Filtra a exclusão pelas chaves da tabela
                this.MontarWhereChaves(toContrato);
                //Filtra a exclusão pelo campo de controle de acessos concorrentes
                this.Sql.MontarCampoWhere("ULT_ATUALIZACAO", toContrato.UltAtualizacao);

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
        /// <param name="toContrato">TO contendo os campos.</param>
        private void MontarWhere(TOContrato toContrato)
        {
            //Monta no WHERE todos os campos da tabela que foram informados
            
            this.MontarWhereChaves(toContrato);
            this.MontarCampos(this.Sql.MontarCampoWhere, toContrato);
            
			this.Sql.MontarCampoWhere("ULT_ATUALIZACAO", toContrato.UltAtualizacao);
        }
        
        /// <summary>Monta campos chave para cláusula WHERE.</summary>
        /// <param name="toContrato">TO contendo os campos.</param>
        private void MontarWhereChaves(TOContrato toContrato)
        {
            //Monta no WHERE todos os campos chave da tabela
            
            this.MontarCamposChave(this.Sql.MontarCampoWhere, toContrato);
        }
        
        /// <summary>Monta campos para cláusula SET.</summary>
        /// <param name="toContrato">TO contendo os campos.</param>
        private void MontarSet(TOContrato toContrato)
        {
            //Monta no SET todos os campos não chave da tabela que foram informados
            
            this.MontarCampos(this.Sql.MontarCampoSet, toContrato);
            this.Sql.MontarCampoSet("ULT_ATUALIZACAO");
            this.Sql.Comando.Append("CURRENT_TIMESTAMP");
        }
        
        /// <summary>Monta campos para cláusula INSERT.</summary>
        /// <param name="toContrato">TO contendo os campos.</param>
        private void MontarInsert(TOContrato toContrato)
        {
            //Monta no INSERT todos os campos da tabela que foram informados
            
            this.MontarCamposChave(this.Sql.MontarCampoInsert, toContrato);
            this.MontarCampos(this.Sql.MontarCampoInsert, toContrato);
            this.Sql.MontarCampoInsert("ULT_ATUALIZACAO");
            this.Sql.Temporario.Append("CURRENT_TIMESTAMP");
        }
        
        /// <summary>Executa uma ação nos campos chave de um TO.</summary>
        /// <param name="montagem">Ação a ser executada.</param>
        /// <param name="toContrato">TO alvo das ações.</param>
        private void MontarCamposChave(ConstrutorSql.MontarCampo montagem, TOContrato toContrato)
        {   
            //Invoca qualquer comando simples de montagem nos campos chave da tabela
            
            montagem.Invoke("NUMERO_CONTRATO", toContrato.NumeroContrato);
        }
        
        /// <summary>Executa uma ação nos campos não chave de um TO.</summary>
        /// <param name="montagem">Ação a ser executada.</param>
        /// <param name="toContrato">TO alvo das ações.</param>
        private void MontarCampos(ConstrutorSql.MontarCampo montagem, TOContrato toContrato)
        {   
            //Invoca qualquer comando simples de montagem nos campos não chave da tabela, exceto no que faz controle de acessos concorrentes
            
            montagem.Invoke("AGENCIA", toContrato.Agencia);
            montagem.Invoke("CEP", toContrato.Cep);
            montagem.Invoke("CIDADE", toContrato.Cidade);
            montagem.Invoke("CNPJ", toContrato.Cnpj);
            montagem.Invoke("DATA_ASSINATURA", toContrato.DataAssinatura);
            montagem.Invoke("DATA_NASCIMENTO", toContrato.DataNascimento);
            montagem.Invoke("ENDERECO", toContrato.Endereco);
            montagem.Invoke("NOME_CLIENTE", toContrato.NomeCliente);
            montagem.Invoke("TIPO_IMOVEL", toContrato.TipoImovel);
            montagem.Invoke("UF", toContrato.Uf);
            montagem.Invoke("VALOR_IMOVEL", toContrato.ValorImovel);
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
                case "NUMERO_CONTRATO":
                    parametro.Precision = 7;
                    parametro.Size = 7;
                    parametro.DbType = DbType.Decimal;
                    break;                        
                #endregion

                #region Campos Obrigatórios
                case "AGENCIA":
                    parametro.Precision = 4;
                    parametro.Size = 4;
                    parametro.DbType = DbType.String;
                    break;
                case "CNPJ":
                    parametro.Precision = 14;
                    parametro.Size = 14;
                    parametro.DbType = DbType.Decimal;
                    break;
                case "DATA_ASSINATURA":
                    parametro.Precision = 4;
                    parametro.Size = 4;
                    parametro.DbType = DbType.Date;
                    break;
                case "DATA_NASCIMENTO":
                    parametro.Precision = 4;
                    parametro.Size = 4;
                    parametro.DbType = DbType.Date;
                    break;
                case "NOME_CLIENTE":
                    parametro.Precision = 35;
                    parametro.Size = 35;
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
                case "CEP":
                    parametro.Precision = 8;
                    parametro.Size = 8;
                    parametro.DbType = DbType.String;
                    break;
                case "CIDADE":
                    parametro.Precision = 30;
                    parametro.Size = 30;
                    parametro.DbType = DbType.String;
                    break;
                case "ENDERECO":
                    parametro.Precision = 40;
                    parametro.Size = 40;
                    parametro.DbType = DbType.String;
                    break;
                case "TIPO_IMOVEL":
                    parametro.Precision = 1;
                    parametro.Size = 1;
                    parametro.DbType = DbType.String;
                    break;
                case "UF":
                    parametro.Precision = 2;
                    parametro.Size = 2;
                    parametro.DbType = DbType.String;
                    break;
                case "VALOR_IMOVEL":
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