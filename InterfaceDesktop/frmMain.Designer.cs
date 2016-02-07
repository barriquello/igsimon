namespace InterfaceDesktop
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrRelogio = new System.Windows.Forms.Timer(this.components);
            this.tmrGraficos = new System.Windows.Forms.Timer(this.components);
            this.lblTe = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.chartTemperatura = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensagens = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMEM = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.picNivelOleo = new System.Windows.Forms.PictureBox();
            this.picValvula = new System.Windows.Forms.PictureBox();
            this.lblVL = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblValvula = new System.Windows.Forms.Label();
            this.lblNivel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tooConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolGraficos = new System.Windows.Forms.ToolStripButton();
            this.toolComparar = new System.Windows.Forms.ToolStripButton();
            this.toolExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbJanela = new System.Windows.Forms.ToolStripComboBox();
            this.tv1 = new System.Windows.Forms.TreeView();
            this.tmrBlink = new System.Windows.Forms.Timer(this.components);
            this.aTe = new InterfaceDesktop.Analogico();
            this.aTo = new InterfaceDesktop.Analogico();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperatura)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNivelOleo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picValvula)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(980, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = " ";
            // 
            // tmrRelogio
            // 
            this.tmrRelogio.Enabled = true;
            this.tmrRelogio.Interval = 250;
            this.tmrRelogio.Tick += new System.EventHandler(this.timerRelogio_Tick);
            // 
            // tmrGraficos
            // 
            this.tmrGraficos.Tick += new System.EventHandler(this.tmrGraficos_Tick);
            // 
            // lblTe
            // 
            this.lblTe.AutoSize = true;
            this.lblTe.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTe.Location = new System.Drawing.Point(236, 23);
            this.lblTe.Name = "lblTe";
            this.lblTe.Size = new System.Drawing.Size(213, 16);
            this.lblTe.TabIndex = 1;
            this.lblTe.Text = "Temperatura dos enrolamentos";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(6, 23);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(145, 16);
            this.lblTo.TabIndex = 1;
            this.lblTo.Text = "Temperatura do óleo";
            // 
            // picStatus
            // 
            this.picStatus.Image = global::InterfaceDesktop.Properties.Resources.Vermelho;
            this.picStatus.Location = new System.Drawing.Point(608, 17);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(22, 22);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStatus.TabIndex = 4;
            this.picStatus.TabStop = false;
            // 
            // chartTemperatura
            // 
            this.chartTemperatura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chartTemperatura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTemperatura.Location = new System.Drawing.Point(3, 19);
            this.chartTemperatura.Name = "chartTemperatura";
            this.chartTemperatura.Size = new System.Drawing.Size(860, 425);
            this.chartTemperatura.TabIndex = 0;
            this.chartTemperatura.Text = "chart1";
            this.chartTemperatura.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.chartTemperatura_CursorPositionChanged);
            this.chartTemperatura.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chartTemperatura_AxisViewChanged);
            this.chartTemperatura.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartTemperatura_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensagens,
            this.lblMEM,
            this.lblSpring,
            this.toolStripProgressBar1,
            this.lblHora});
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1188, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensagens
            // 
            this.lblMensagens.Name = "lblMensagens";
            this.lblMensagens.Size = new System.Drawing.Size(10, 17);
            this.lblMensagens.Text = " ";
            // 
            // lblMEM
            // 
            this.lblMEM.Name = "lblMEM";
            this.lblMEM.Size = new System.Drawing.Size(0, 17);
            // 
            // lblSpring
            // 
            this.lblSpring.Name = "lblSpring";
            this.lblSpring.Size = new System.Drawing.Size(1051, 17);
            this.lblSpring.Spring = true;
            this.lblSpring.Text = " ";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // lblHora
            // 
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(10, 17);
            this.lblHora.Text = " ";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.chartTemperatura);
            this.groupBox5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(308, 178);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(866, 447);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.picNivelOleo);
            this.groupBox6.Controls.Add(this.picValvula);
            this.groupBox6.Controls.Add(this.picStatus);
            this.groupBox6.Controls.Add(this.aTe);
            this.groupBox6.Controls.Add(this.aTo);
            this.groupBox6.Controls.Add(this.lblTe);
            this.groupBox6.Controls.Add(this.lblVL);
            this.groupBox6.Controls.Add(this.lblNo);
            this.groupBox6.Controls.Add(this.lblValvula);
            this.groupBox6.Controls.Add(this.lblNivel);
            this.groupBox6.Controls.Add(this.lblTo);
            this.groupBox6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(308, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(866, 170);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Nível / Temperatura";
            // 
            // picNivelOleo
            // 
            this.picNivelOleo.Image = global::InterfaceDesktop.Properties.Resources.NivelOleo0;
            this.picNivelOleo.Location = new System.Drawing.Point(507, 43);
            this.picNivelOleo.Name = "picNivelOleo";
            this.picNivelOleo.Size = new System.Drawing.Size(64, 121);
            this.picNivelOleo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picNivelOleo.TabIndex = 9;
            this.picNivelOleo.TabStop = false;
            // 
            // picValvula
            // 
            this.picValvula.Image = global::InterfaceDesktop.Properties.Resources.Vermelho;
            this.picValvula.Location = new System.Drawing.Point(833, 17);
            this.picValvula.Name = "picValvula";
            this.picValvula.Size = new System.Drawing.Size(22, 22);
            this.picValvula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picValvula.TabIndex = 4;
            this.picValvula.TabStop = false;
            // 
            // lblVL
            // 
            this.lblVL.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblVL.Location = new System.Drawing.Point(647, 43);
            this.lblVL.Name = "lblVL";
            this.lblVL.Size = new System.Drawing.Size(183, 16);
            this.lblVL.TabIndex = 5;
            this.lblVL.Text = "em estado indeterminado";
            this.lblVL.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblNo.Location = new System.Drawing.Point(577, 81);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(144, 16);
            this.lblNo.TabIndex = 5;
            this.lblNo.Text = "Nível\\nDesconhecido";
            // 
            // lblValvula
            // 
            this.lblValvula.AutoSize = true;
            this.lblValvula.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblValvula.Location = new System.Drawing.Point(639, 23);
            this.lblValvula.Name = "lblValvula";
            this.lblValvula.Size = new System.Drawing.Size(191, 16);
            this.lblValvula.TabIndex = 4;
            this.lblValvula.Text = "Válvula de alívio de pressão";
            // 
            // lblNivel
            // 
            this.lblNivel.AutoSize = true;
            this.lblNivel.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblNivel.Location = new System.Drawing.Point(508, 23);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(94, 16);
            this.lblNivel.TabIndex = 4;
            this.lblNivel.Text = "Nível do Óleo";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tooConfig,
            this.toolStripSeparator1,
            this.toolGraficos,
            this.toolComparar,
            this.toolExcel,
            this.toolStripSeparator2,
            this.cmbJanela});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(12, 12);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(291, 39);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "Ações";
            // 
            // tooConfig
            // 
            this.tooConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tooConfig.Image = global::InterfaceDesktop.Properties.Resources.Config;
            this.tooConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tooConfig.Name = "tooConfig";
            this.tooConfig.Size = new System.Drawing.Size(36, 36);
            this.tooConfig.Text = "Configurações";
            this.tooConfig.Click += new System.EventHandler(this.tooConfig_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolGraficos
            // 
            this.toolGraficos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolGraficos.Image = global::InterfaceDesktop.Properties.Resources.Graficos;
            this.toolGraficos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGraficos.Name = "toolGraficos";
            this.toolGraficos.Size = new System.Drawing.Size(36, 36);
            this.toolGraficos.Text = "toolStripButton2";
            this.toolGraficos.ToolTipText = "Visualizar gráficos";
            this.toolGraficos.Click += new System.EventHandler(this.toolGraficos_Click);
            // 
            // toolComparar
            // 
            this.toolComparar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolComparar.Image = global::InterfaceDesktop.Properties.Resources.Comparacoes;
            this.toolComparar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolComparar.Name = "toolComparar";
            this.toolComparar.Size = new System.Drawing.Size(36, 36);
            this.toolComparar.Text = "toolStripButton1";
            this.toolComparar.ToolTipText = "Comparar gráficos";
            this.toolComparar.Click += new System.EventHandler(this.toolComparar_Click);
            // 
            // toolExcel
            // 
            this.toolExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolExcel.Image = global::InterfaceDesktop.Properties.Resources.Excel;
            this.toolExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExcel.Name = "toolExcel";
            this.toolExcel.Size = new System.Drawing.Size(36, 36);
            this.toolExcel.Text = "toolStripButton1";
            this.toolExcel.ToolTipText = "Exportar gráfico atual";
            this.toolExcel.Click += new System.EventHandler(this.toolExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // cmbJanela
            // 
            this.cmbJanela.Items.AddRange(new object[] {
            "1 Semana",
            "1 Dia",
            "12 Horas",
            "1 Hora"});
            this.cmbJanela.Name = "cmbJanela";
            this.cmbJanela.Size = new System.Drawing.Size(130, 39);
            this.cmbJanela.Text = "0,5 Hora";
            this.cmbJanela.TextChanged += new System.EventHandler(this.toolStripComboBox1_TextChanged);
            // 
            // tv1
            // 
            this.tv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tv1.CheckBoxes = true;
            this.tv1.FullRowSelect = true;
            this.tv1.Location = new System.Drawing.Point(12, 54);
            this.tv1.Name = "tv1";
            this.tv1.Size = new System.Drawing.Size(290, 568);
            this.tv1.TabIndex = 9;
            this.tv1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv1_AfterCheck);
            // 
            // tmrBlink
            // 
            this.tmrBlink.Enabled = true;
            this.tmrBlink.Interval = 500;
            this.tmrBlink.Tick += new System.EventHandler(this.tmrBlink_Tick);
            // 
            // aTe
            // 
            this.aTe.BackgroundImage = global::InterfaceDesktop.Properties.Resources.Relogio;
            this.aTe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aTe.Location = new System.Drawing.Point(249, 43);
            this.aTe.Margin = new System.Windows.Forms.Padding(5);
            this.aTe.Name = "aTe";
            this.aTe.Size = new System.Drawing.Size(214, 107);
            this.aTe.TabIndex = 8;
            this.aTe.TabStop = false;
            // 
            // aTo
            // 
            this.aTo.BackgroundImage = global::InterfaceDesktop.Properties.Resources.Relogio;
            this.aTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aTo.Location = new System.Drawing.Point(9, 43);
            this.aTo.Margin = new System.Windows.Forms.Padding(4);
            this.aTo.Name = "aTo";
            this.aTo.Size = new System.Drawing.Size(214, 107);
            this.aTo.TabIndex = 8;
            this.aTo.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1188, 650);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.tv1);
            this.Controls.Add(this.groupBox5);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1024, 677);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface Online";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperatura)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNivelOleo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picValvula)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer tmrRelogio;
        private System.Windows.Forms.Timer tmrGraficos;
        private System.Windows.Forms.Label lblTe;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemperatura;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMensagens;
        private System.Windows.Forms.ToolStripStatusLabel lblHora;
        private System.Windows.Forms.ToolStripStatusLabel lblSpring;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblNivel;
        private InterfaceDesktop.Analogico aTo;
        private Analogico aTe;
        private System.Windows.Forms.ToolStripStatusLabel lblMEM;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tooConfig;
        private System.Windows.Forms.ToolStripButton toolGraficos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolComparar;
        private System.Windows.Forms.ToolStripButton toolExcel;
        private System.Windows.Forms.ToolStripComboBox cmbJanela;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TreeView tv1;
        private System.Windows.Forms.PictureBox picValvula;
        private System.Windows.Forms.Label lblValvula;
        private System.Windows.Forms.Timer tmrBlink;
        private System.Windows.Forms.Label lblVL;
        private System.Windows.Forms.PictureBox picNivelOleo;
    }
}