using System;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>Mensagens previstas para o componente</summary>
    public enum TipoMensagem
    {
        /// <summary>Falha da regra de negócio</summary>
        Falha,
        /// <summary>Usuário informa o CPF para cliente pessoa física, ou CNPJ para cliente pessoa jurídica. Sistema não permite o cadastramento do cliente se o CPF/CNPJ informado for inválido.</summary>
        RN01,
        /// <summary>O Nome do Cliente informado deve possuir, no mínimo, duas palavras.</summary>
        RN02,
        /// <summary>
        ///Se o cliente for pessoa jurídica:
        ///- Tornar obrigatório o preenchimento dos campos “Nome Fantasia”, “Capital Social” e “Data Constituição”;
        ///- Proibir o preenchimento do campo “Nome Mãe”.
        ///Se o cliente for pessoa física:
        ///- Tornar obrigatório o preenchimento do campo “Nome Mãe”;
        ///- Proibir o preenchimento dos campos “Nome Fantasia”, “Capital Social” e “Data Constituição”.
        ///Caso algum campo proibido seja informado, o sistema não deve permitir o cadastramento do cliente.
        ///</summary>
        RN04,
        /// <summary>O nome de cliente pessoa jurídica, Nome Fantasia, deve possuir, no mínimo, duas letras.</summary>
        RN05,
        /// <summary>Para cliente pessoa jurídica, verificar se a Data de Constituição informada é válida.</summary>
        RN06,
        /// <summary>Para cliente pessoa jurídica, verificar se o Capital Social informado é válido.</summary>
        RN07,
        /// <summary>Para cliente pessoa física, Nome da Mãe deve possuir, no mínimo, duas palavras.</summary>
        RN08,
        /// <summary>O número de agência deve possuir, no mínimo, quatro dígitos.</summary>
        RN09
    }

    class Mensagem : Bergs.Pwx.Pwxoiexn.Mensagens.Mensagem
    {
        /// <summary>
        /// Mensagem
        /// </summary>
        private String mensagem;

        /// <summary>
        /// Tipo de mensagem
        /// </summary>
        private Pxcsclxn.TipoMensagem tipoMensagem;

        /// <summary>
        /// Mensagem para o usuário
        /// </summary>
        public override String ParaUsuario
        {
            get { return this.ParaOperador; }
        }

        /// <summary>
        /// Mensagem para o operador
        /// </summary>
        public override String ParaOperador
        {
            get { return this.mensagem; }
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public override String Identificador
        {
            get { return tipoMensagem.ToString(); }
        }

        /// <summary>
        /// Construtor da classe Mensagem
        /// </summary>
        /// <param name="mensagem">Mensagem</param>
        /// <param name="argumentos">Argumentos</param>
        public Mensagem(Pxcsclxn.TipoMensagem mensagem, params String[] argumentos)
        {
            tipoMensagem = mensagem;

            switch (mensagem)
            {
                case Pxcsclxn.TipoMensagem.Falha:
                    this.mensagem = string.Empty;
                    break;
                case Pxcsclxn.TipoMensagem.RN01:
                    this.mensagem = string.Format("O {0} informado não é válido.", argumentos[0]);
                    break;
                case Pxcsclxn.TipoMensagem.RN02:
                    this.mensagem = "O Nome do Cliente deve possuir, no mínimo, duas palavras.";
                    break;
                case Pxcsclxn.TipoMensagem.RN04:
                    this.mensagem = string.Format("Não é permitido informar {0}.", argumentos[0]);
                    break;
                case Pxcsclxn.TipoMensagem.RN05:
                    this.mensagem = "O Nome Fantasia da Empresa deve possuir, no mínimo, duas letras.";
                    break;
                case Pxcsclxn.TipoMensagem.RN06:
                    this.mensagem = "A Data de Constituição informada deve ser menor ou igual à Data Atual.";
                    break;
                case Pxcsclxn.TipoMensagem.RN07:
                    this.mensagem = "O Capital Social da Empresa deve ser maior que zero.";
                    break;
                case Pxcsclxn.TipoMensagem.RN08:
                    this.mensagem = "O Nome da Mãe deve possuir, no mínimo, duas palavras.";
                    break;
                case Pxcsclxn.TipoMensagem.RN09:
                    this.mensagem = "Agência inválida.";
                    break;
                default:
                    this.mensagem = "Mensagem não definida.";
                    break;
            }
        }
    }
}
