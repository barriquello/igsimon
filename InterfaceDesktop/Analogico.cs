using System;
using System.Drawing;
using System.Windows.Forms;
namespace InterfaceDesktop
{
    /// <summary>Controle para o mostrador analógico de temperatura.</summary>
    public partial class Analogico : UserControl
    {
        /// <summary>Escala do tamanho do ponteiro.</summary>
        private float EscalaTamanho = 0.75f / 2f;
        /// <summary>Tamanho do poneiro.</summary>
        private float TamanhoPonteiro = 5;
        /// <summary>Valor mínimo da escala dos ponteiros.</summary>
        private float _min = 0;
        /// <summary>Valor máximo da escala dos ponteiros.</summary>
        private float _max = 150;
        /// <summary>Posição do ponteiro.</summary>
        private float _Valor = 0;
        /// <summary>
        /// Range do indicador.
        /// </summary>
        private float range = 200;
        /// <summary>
        /// Ponteiro indicador de valor máximo.
        /// </summary>
        private float _ValorMaximo = 0;
        /// <summary>
        /// Ponteiro indicador de valor mínimo.
        /// </summary>
        private float _ValorMinimo = 0;
        /// <summary>Retorna o valor máximo definido.</summary>
        public float ValorMaximo()
        {
            return _ValorMaximo;
        }
        /// <summary>Retorna o valor mínimo da escala.</summary>
        public float Min()
        {
            return _min;
        }
        /// <summary>Retorna o valor máximo da escala.</summary>
        public float Max()
        {
            return _max;
        }
        /// <summary>Retorna o valor exibido.</summary>
        public float Value()
        {
            return _Valor;
        }
        /// <summary>Aponta no mostrador o valor máximo ocorrido.</summary>
        public void ValorMaximo(float Maximo)
        {
            _ValorMaximo = Maximo;
            if (_ValorMaximo > _max)
                _ValorMaximo = _max;
            Redesenha();
        }
        /// <summary>Aponta no mostrador o valor máximo ocorrido.</summary>
        public void ValorMinimo(float Minimo)
        {
            _ValorMinimo = Minimo;
            if (_ValorMinimo < _min)
                _ValorMinimo = _min;
            Redesenha();
        }
        /// <summary>Define o valor máximo da escala.</summary>
        public void Max(float Valor)
        {
            _max = Valor;
            // Atualiza o valor máximo
            if (_Valor > _max)
                _Valor = _max;
            //atualiza o range
            range = _max - _min;
            lblMax.Text = _max.ToString();
            lblMeio.Text = (_min + range / 2).ToString();
            //redesenha o ponteiro
            Redesenha();
        }
        /// <summary>Define o valor mínimo da escala.</summary>
        public void Min(float Valor)
        {
            _min = Valor;
            if (_Valor < _min)
                _Valor = _min;
            range = _max - _min;
            lblMin.Text = _min.ToString();
            lblMeio.Text = (_min + range / 2).ToString();
            Redesenha();
        }
        /// <summary>Define a posição do ponteiro.</summary>
        public void Value(float Valor)
        {
            if (Valor > _max)
                _Valor = _max;
            else if (Valor < _min)
                _Valor = _min;
            else if (float.IsNaN(Valor))
                _Valor = 0;
            else
                _Valor = Valor;
            Redesenha();
        }
        /// <summary>Define a imagem de fundo.</summary>
        public void SetPicture(Image Pic)
        {
            BackgroundImage = Pic;
        }
        /// <summary>Redesenha o componente.</summary>
        private void Redesenha()
        {
            this.SuspendLayout();
            Centraliza();
            range = _max - _min;
            if (range > 0)
            {
                lineShape1.X1 = (int)(TamanhoPonteiro * Math.Cos(Math.PI * (_max - _Valor) / (range))) + lineShape1.X2;
                lineShape2.X1 = (int)(TamanhoPonteiro * 1.1 * Math.Cos(Math.PI * (_max - _ValorMaximo) / (range))) + lineShape2.X2;
                lineShape3.X1 = (int)(TamanhoPonteiro * 1.1 * Math.Cos(Math.PI * (_max - _ValorMinimo) / (range))) + lineShape3.X2;
                lineShape1.Y1 = (int)(-TamanhoPonteiro * 2 * Height / Width * Math.Sin(Math.PI * (_max - _Valor) / range)) + lineShape1.Y2;
                lineShape2.Y1 = (int)(-TamanhoPonteiro * 1.1 * 2 * Height / Width * Math.Sin(Math.PI * (_max - _ValorMaximo) / range)) + lineShape2.Y2;
                lineShape3.Y1 = (int)(-TamanhoPonteiro * 1.1 * 2 * Height / Width * Math.Sin(Math.PI * (_max - _ValorMinimo) / range)) + lineShape3.Y2;
            }
            else
            {
                lineShape1.X1 = lineShape1.X2;
                lineShape2.X1 = lineShape2.X2;
                lineShape3.X1 = lineShape3.X2;
                lineShape1.Y1 = lineShape1.Y2;
                lineShape2.Y1 = lineShape2.Y2;
                lineShape3.Y1 = lineShape3.Y2;
            }
            this.Refresh();
            this.ResumeLayout();
        }

        /// <summary>Centraliza a base do ponteiro.</summary>
        private void Centraliza()
        {
            lineShape3.X2 =
                lineShape2.X2 =
                lineShape1.X2 = Width / 2;
            lineShape3.Y2 =
                lineShape2.Y2 =
                lineShape1.Y2 = Height - lineShape1.BorderWidth / 2;
            TamanhoPonteiro = Width * EscalaTamanho;
            ovalShape1.Location = new Point((Width - ovalShape1.Width) / 2, Height - ovalShape1.Height / 2 + lineShape1.BorderWidth / 2);

            lblMax.Top =
                lblMin.Top =
                Height - 20;

            lblMax.Left = Width - lblMax.Width - 30;
            lblMin.Left = 30;
            lblMeio.Top = 30;
            lblMeio.Left = (Width - lblMeio.Width) / 2;
            lblMax.Text = _max.ToString();
            lblMin.Text = _min.ToString();
            lblMeio.Text = (_min + range / 2).ToString();
        }
        /// <summary>Inicializa o controle desenhando os contorles na posição pré-definida.</summary>
        public Analogico()
        {
            InitializeComponent();
        }

        /// <summary>Evento acionado ao carregar o controle.</summary>
        /// <param name="sender">Objeto responsável pelo o evento.</param>
        /// <param name="e">Parâmetros adicionais do evento.</param>
        private void Analogico_Load(object sender, EventArgs e)
        {
            Redesenha();

        }
        /// <summary>Evento acionado ao modificar o tamanhno da janela do controle.</summary>
        /// <param name="sender">Objeto responsável pelo evento.</param>
        /// <param name="e">Parâmetros adicionais do evento.</param>
        private void Analogico_SizeChanged(object sender, EventArgs e)
        {
            Centraliza();
        }
    }
}
