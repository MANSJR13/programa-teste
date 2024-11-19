using System;

namespace Bergs.Pxc.Pxcsctxn
{
    /// <summary>Mensagens previstas para o componente</summary>
    public enum TipoMensagem
    {
        /// <summary>Falha da regra de negócio</summary>
        Falha,
        /// <summary>Verificar se o Nome do Cliente possui, no mínimo, 2 palavras.</summary>
        RN01,
        /// <summary>Verificar se a Data de Assinatura é menor ou igual à Data Atual.</summary>
        RN02,
        /// <summary>Verificar se a Data de Nascimento é menor ou igual à Data de Assinatura.</summary>
        RN03,
        /// <summary>Todos os campos de endereço são preenchidos (Endereço, cidade, CEP e UF), ou nenhuns dos campos de endereço são preenchidos.</summary>
        RN04,
        /// <summary>O Valor do Imóvel, se informado, deve ser maior que zero.</summary>
        RN05
    }

    class Mensagem : Bergs.Pwx.Pwxoiexn.Mensagens.Mensagem
    {
        /// <summary>Mensagem</summary>
        private String mensagem;

        /// <summary>Tipo de mensagem</summary>
        private Pxcsctxn.TipoMensagem tipoMensagem;

        /// <summary>Mensagem para o usuário</summary>
        public override String ParaUsuario
        {
            get { return this.ParaOperador; }
        }

        /// <summary>Mensagem para o operador</summary>
        public override String ParaOperador
        {
            get { return this.mensagem; }
        }

        /// <summary>Identificador</summary>
        public override String Identificador
        {
            get { return tipoMensagem.ToString(); }
        }

        /// <summary>Construtor da classe Mensagem</summary>
        /// <param name="mensagem">Mensagem</param>
        /// <param name="argumentos">Argumentos</param>
        public Mensagem(Pxcsctxn.TipoMensagem mensagem, params String[] argumentos)
        {
            tipoMensagem = mensagem;

            switch (mensagem)
            {
                case Pxcsctxn.TipoMensagem.Falha:
                    this.mensagem = string.Empty;
                    break;
                case Pxcsctxn.TipoMensagem.RN01:
                    this.mensagem = string.Format("Campo {0} deve possuir, no mínimo, {1} palavras.", argumentos[0], argumentos[1]);
                    break;
                case Pxcsctxn.TipoMensagem.RN02:
                    this.mensagem = "A Data de Assinatura deve ser menor ou igual à Data Atual.";
                    break;
                case Pxcsctxn.TipoMensagem.RN03:
                    this.mensagem = "A Data de Nascimento deve ser menor ou igual à Data de Assinatura.";
                    break;
                case Pxcsctxn.TipoMensagem.RN04:
                    this.mensagem = "Todos os campos de Endereço devem ser preenchidos ou todos devem estar em branco.";
                    break;
                case Pxcsctxn.TipoMensagem.RN05:
                    this.mensagem = "O valor informado para o imóvel deve ser maior que zero.";
                    break;
                default:
                    this.mensagem = "Mensagem não definida.";
                    break;
            }
        }
    }
}
