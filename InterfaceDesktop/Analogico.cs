using System;
using System.Drawing;
using System.Windows.Forms;
namespace InterfaceDesktop
{
    public partial class Analogico : UserControl
    {
        /// <summary>
        /// Escala do tamanho do ponteiro
        /// </summary>
        private float EscalaTamanho = 0.75f / 2f;
        /// <summary>
        /// Tamanho do poneiro
        /// </summary>
        private float TamanhoPonteiro = 5;
        private float _min = 0;
        private float _max = 100;
        private float _Valor = 0;
        private float range = 100;
        /// <summary>
        /// Retorna o valor mínimo da escala
        /// </summary>
        public float Min()
        {
            return _min;
        }
        /// <summary>
        /// Retorna o valor máximo da escala
        /// </summary>
        public float Max()
        {
            return _max;
        }
        /// <summary>
        /// Retorna o valor exibido
        /// </summary>
        public float Value()
        {
            return _Valor;
        }
        /// <summary>
        /// Define o valor máximo
        /// </summary>
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
        /// <summary>
        /// Define o valor mínimo da escala
        /// </summary>
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
        /// <summary>
        /// Define a posição do ponteiro
        /// </summary>
        public void Value(float Valor)
        {
            if (Valor > _max)
                _Valor = _max;
            else
                if (Valor < _min)
                    _Valor = _min;
                else
                    _Valor = Valor;
            Redesenha();
        }
        /// <summary>
        /// Define a imagem de fundo
        /// </summary>
        public void SetPicture(Image Pic)
        {
            BackgroundImage = Pic;
        }
        /// <summary>
        /// Redesenha o componente
        /// </summary>
        public void Redesenha()
        {
            this.SuspendLayout();
            Centraliza();
            range = _max - _min;
            lineShape1.X1 = (int)(TamanhoPonteiro * Math.Cos(Math.PI * (_max - _Valor) / (range))) + lineShape1.X2;
            lineShape1.Y1 = (int)(-TamanhoPonteiro * 2 * Height / Width * Math.Sin(Math.PI * (_max - _Valor) / range)) + lineShape1.Y2;
            this.Refresh();
            this.ResumeLayout();
        }

        /// <summary>
        /// Centraliza a base do ponteiro
        /// </summary>
        private void Centraliza()
        {
            lineShape1.X2 = Width / 2;
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
        }

        public Analogico()
        {
            InitializeComponent();
        }


        private void Analogico_Load(object sender, EventArgs e)
        {
            Redesenha();

        }

        private void Analogico_SizeChanged(object sender, EventArgs e)
        {
            Centraliza();
        }


    }
}
