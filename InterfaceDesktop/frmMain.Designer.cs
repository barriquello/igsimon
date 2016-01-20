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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.button1 = new System.Windows.Forms.Button();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrRelogio = new System.Windows.Forms.Timer(this.components);
            this.tmrGraficos = new System.Windows.Forms.Timer(this.components);
            this.lblTe = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.chartTemperatura = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensagens = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMEM = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.picValvula = new System.Windows.Forms.PictureBox();
            this.aTe = new InterfaceDesktop.Analogico();
            this.aTo = new InterfaceDesktop.Analogico();
            this.lblNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tooConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolGraficos = new System.Windows.Forms.ToolStripButton();
            this.toolComparar = new System.Windows.Forms.ToolStripButton();
            this.toolExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbJanela = new System.Windows.Forms.ToolStripComboBox();
            this.tv1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperatura)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picValvula)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 588);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.lblTe.Location = new System.Drawing.Point(472, 36);
            this.lblTe.Name = "lblTe";
            this.lblTe.Size = new System.Drawing.Size(25, 16);
            this.lblTe.TabIndex = 1;
            this.lblTe.Text = "Te";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(154, 38);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(25, 16);
            this.lblTo.TabIndex = 1;
            this.lblTo.Text = "To";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(326, 36);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(142, 16);
            this.label22.TabIndex = 0;
            this.label22.Text = "Temp. Enrolamentos";
            // 
            // picStatus
            // 
            this.picStatus.Image = global::InterfaceDesktop.Properties.Resources.Vermelho;
            this.picStatus.Location = new System.Drawing.Point(158, 18);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(16, 16);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStatus.TabIndex = 4;
            this.picStatus.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(7, 38);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(126, 16);
            this.label23.TabIndex = 0;
            this.label23.Text = "Temperatura Óleo";
            // 
            // chartTemperatura
            // 
            this.chartTemperatura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chartTemperatura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTemperatura.Location = new System.Drawing.Point(3, 19);
            this.chartTemperatura.Name = "chartTemperatura";
            this.chartTemperatura.Size = new System.Drawing.Size(688, 425);
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
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
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
            this.lblSpring.Size = new System.Drawing.Size(879, 17);
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
            this.groupBox5.Size = new System.Drawing.Size(694, 447);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.picValvula);
            this.groupBox6.Controls.Add(this.picStatus);
            this.groupBox6.Controls.Add(this.aTe);
            this.groupBox6.Controls.Add(this.aTo);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.lblTe);
            this.groupBox6.Controls.Add(this.lblNo);
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Controls.Add(this.lblTo);
            this.groupBox6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(308, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(694, 170);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Nível / Temperatura";
            // 
            // picValvula
            // 
            this.picValvula.Image = global::InterfaceDesktop.Properties.Resources.Vermelho;
            this.picValvula.Location = new System.Drawing.Point(477, 18);
            this.picValvula.Name = "picValvula";
            this.picValvula.Size = new System.Drawing.Size(16, 16);
            this.picValvula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picValvula.TabIndex = 4;
            this.picValvula.TabStop = false;
            // 
            // aTe
            // 
            this.aTe.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aTe.BackgroundImage")));
            this.aTe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aTe.Location = new System.Drawing.Point(382, 58);
            this.aTe.Margin = new System.Windows.Forms.Padding(5);
            this.aTe.Name = "aTe";
            this.aTe.Size = new System.Drawing.Size(214, 107);
            this.aTe.TabIndex = 8;
            this.aTe.TabStop = false;
            // 
            // aTo
            // 
            this.aTo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aTo.BackgroundImage")));
            this.aTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aTo.Location = new System.Drawing.Point(63, 58);
            this.aTo.Margin = new System.Windows.Forms.Padding(4);
            this.aTo.Name = "aTo";
            this.aTo.Size = new System.Drawing.Size(214, 107);
            this.aTo.TabIndex = 8;
            this.aTo.TabStop = false;
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblNo.Location = new System.Drawing.Point(180, 18);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(25, 16);
            this.lblNo.TabIndex = 5;
            this.lblNo.Text = "No";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label1.Location = new System.Drawing.Point(326, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Válvula de segurança";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label24.Location = new System.Drawing.Point(7, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(94, 16);
            this.label24.TabIndex = 4;
            this.label24.Text = "Nível do Óleo";
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
            this.toolStrip1.Location = new System.Drawing.Point(12, 12);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(282, 39);
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
            // 
            // toolComparar
            // 
            this.toolComparar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolComparar.Image = global::InterfaceDesktop.Properties.Resources.Comparacoes;
            this.toolComparar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolComparar.Name = "toolComparar";
            this.toolComparar.Size = new System.Drawing.Size(36, 36);
            this.toolComparar.Text = "toolStripButton1";
            // 
            // toolExcel
            // 
            this.toolExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolExcel.Image = global::InterfaceDesktop.Properties.Resources.Excel;
            this.toolExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExcel.Name = "toolExcel";
            this.toolExcel.Size = new System.Drawing.Size(36, 36);
            this.toolExcel.Text = "toolStripButton1";
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
            this.cmbJanela.Size = new System.Drawing.Size(121, 39);
            this.cmbJanela.Text = "2 Hora";
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
            this.tv1.Size = new System.Drawing.Size(290, 528);
            this.tv1.TabIndex = 9;
            this.tv1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv1_AfterCheck);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 650);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.tv1);
            this.Controls.Add(this.groupBox5);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::InterfaceDesktop.Properties.Resources.ico16;
            this.MinimumSize = new System.Drawing.Size(1024, 677);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperatura)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picValvula)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer tmrRelogio;
        private System.Windows.Forms.Timer tmrGraficos;
        private System.Windows.Forms.Label lblTe;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemperatura;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMensagens;
        private System.Windows.Forms.ToolStripStatusLabel lblHora;
        private System.Windows.Forms.ToolStripStatusLabel lblSpring;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label label24;
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
        private System.Windows.Forms.Label label1;
    }
}