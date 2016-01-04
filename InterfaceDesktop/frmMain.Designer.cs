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
            //System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            //System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            //System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrRelogio = new System.Windows.Forms.Timer(this.components);
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.tmrGraficos = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkS = new System.Windows.Forms.CheckBox();
            this.chkQ = new System.Windows.Forms.CheckBox();
            this.chkP = new System.Windows.Forms.CheckBox();
            this.chkPotencia = new System.Windows.Forms.CheckBox();
            this.lblS = new System.Windows.Forms.Label();
            this.lblQ = new System.Windows.Forms.Label();
            this.lblP = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkVc = new System.Windows.Forms.CheckBox();
            this.chkVb = new System.Windows.Forms.CheckBox();
            this.chkVa = new System.Windows.Forms.CheckBox();
            this.chkTensao = new System.Windows.Forms.CheckBox();
            this.lblVc = new System.Windows.Forms.Label();
            this.lblVb = new System.Windows.Forms.Label();
            this.lblVa = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkIc = new System.Windows.Forms.CheckBox();
            this.chkIb = new System.Windows.Forms.CheckBox();
            this.chkIa = new System.Windows.Forms.CheckBox();
            this.chkCorrente = new System.Windows.Forms.CheckBox();
            this.lblIc = new System.Windows.Forms.Label();
            this.lblIb = new System.Windows.Forms.Label();
            this.lblIa = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkTe = new System.Windows.Forms.CheckBox();
            this.chkTo = new System.Windows.Forms.CheckBox();
            this.chkNo = new System.Windows.Forms.CheckBox();
            this.chkETC = new System.Windows.Forms.CheckBox();
            this.lblTe = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.chartTemperatura = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnGraficos = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensagens = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperatura)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(655, 543);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Image = global::InterfaceDesktop.Properties.Resources.Config;
            this.button2.Location = new System.Drawing.Point(14, 543);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 66);
            this.button2.TabIndex = 0;
            this.button2.Text = "Configurar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnConfig_Click);
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
            // picStatus
            // 
            this.picStatus.Image = global::InterfaceDesktop.Properties.Resources.Verde;
            this.picStatus.Location = new System.Drawing.Point(97, 59);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(32, 32);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStatus.TabIndex = 4;
            this.picStatus.TabStop = false;
            // 
            // tmrGraficos
            // 
            this.tmrGraficos.Enabled = true;
            this.tmrGraficos.Tick += new System.EventHandler(this.tmrGraficos_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkS);
            this.groupBox1.Controls.Add(this.chkQ);
            this.groupBox1.Controls.Add(this.chkP);
            this.groupBox1.Controls.Add(this.chkPotencia);
            this.groupBox1.Controls.Add(this.lblS);
            this.groupBox1.Controls.Add(this.lblQ);
            this.groupBox1.Controls.Add(this.lblP);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 120);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Potência";
            // 
            // chkS
            // 
            this.chkS.AutoSize = true;
            this.chkS.Location = new System.Drawing.Point(268, 93);
            this.chkS.Name = "chkS";
            this.chkS.Size = new System.Drawing.Size(15, 14);
            this.chkS.TabIndex = 3;
            this.chkS.UseVisualStyleBackColor = true;
            // 
            // chkQ
            // 
            this.chkQ.AutoSize = true;
            this.chkQ.Location = new System.Drawing.Point(269, 57);
            this.chkQ.Name = "chkQ";
            this.chkQ.Size = new System.Drawing.Size(15, 14);
            this.chkQ.TabIndex = 3;
            this.chkQ.UseVisualStyleBackColor = true;
            // 
            // chkP
            // 
            this.chkP.AutoSize = true;
            this.chkP.Location = new System.Drawing.Point(269, 21);
            this.chkP.Name = "chkP";
            this.chkP.Size = new System.Drawing.Size(15, 14);
            this.chkP.TabIndex = 3;
            this.chkP.UseVisualStyleBackColor = true;
            // 
            // chkPotencia
            // 
            this.chkPotencia.AutoSize = true;
            this.chkPotencia.Location = new System.Drawing.Point(269, 0);
            this.chkPotencia.Name = "chkPotencia";
            this.chkPotencia.Size = new System.Drawing.Size(15, 14);
            this.chkPotencia.TabIndex = 2;
            this.chkPotencia.UseVisualStyleBackColor = true;
            this.chkPotencia.CheckedChanged += new System.EventHandler(this.chkPotencia_CheckedChanged);
            // 
            // lblS
            // 
            this.lblS.AutoSize = true;
            this.lblS.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblS.Location = new System.Drawing.Point(153, 93);
            this.lblS.Name = "lblS";
            this.lblS.Size = new System.Drawing.Size(17, 16);
            this.lblS.TabIndex = 1;
            this.lblS.Text = "S";
            // 
            // lblQ
            // 
            this.lblQ.AutoSize = true;
            this.lblQ.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQ.Location = new System.Drawing.Point(154, 57);
            this.lblQ.Name = "lblQ";
            this.lblQ.Size = new System.Drawing.Size(19, 16);
            this.lblQ.TabIndex = 1;
            this.lblQ.Text = "Q";
            // 
            // lblP
            // 
            this.lblP.AutoSize = true;
            this.lblP.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP.Location = new System.Drawing.Point(154, 21);
            this.lblP.Name = "lblP";
            this.lblP.Size = new System.Drawing.Size(17, 16);
            this.lblP.TabIndex = 1;
            this.lblP.Text = "P";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Potência Aparente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Potência Reativa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Potência Ativa";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkVc);
            this.groupBox2.Controls.Add(this.chkVb);
            this.groupBox2.Controls.Add(this.chkVa);
            this.groupBox2.Controls.Add(this.chkTensao);
            this.groupBox2.Controls.Add(this.lblVc);
            this.groupBox2.Controls.Add(this.lblVb);
            this.groupBox2.Controls.Add(this.lblVa);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 120);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tensão";
            // 
            // chkVc
            // 
            this.chkVc.AutoSize = true;
            this.chkVc.Location = new System.Drawing.Point(268, 93);
            this.chkVc.Name = "chkVc";
            this.chkVc.Size = new System.Drawing.Size(15, 14);
            this.chkVc.TabIndex = 3;
            this.chkVc.UseVisualStyleBackColor = true;
            // 
            // chkVb
            // 
            this.chkVb.AutoSize = true;
            this.chkVb.Location = new System.Drawing.Point(269, 57);
            this.chkVb.Name = "chkVb";
            this.chkVb.Size = new System.Drawing.Size(15, 14);
            this.chkVb.TabIndex = 3;
            this.chkVb.UseVisualStyleBackColor = true;
            // 
            // chkVa
            // 
            this.chkVa.AutoSize = true;
            this.chkVa.Location = new System.Drawing.Point(269, 21);
            this.chkVa.Name = "chkVa";
            this.chkVa.Size = new System.Drawing.Size(15, 14);
            this.chkVa.TabIndex = 3;
            this.chkVa.UseVisualStyleBackColor = true;
            // 
            // chkTensao
            // 
            this.chkTensao.AutoSize = true;
            this.chkTensao.Location = new System.Drawing.Point(269, 0);
            this.chkTensao.Name = "chkTensao";
            this.chkTensao.Size = new System.Drawing.Size(15, 14);
            this.chkTensao.TabIndex = 2;
            this.chkTensao.UseVisualStyleBackColor = true;
            this.chkTensao.CheckedChanged += new System.EventHandler(this.chkTensao_CheckedChanged);
            // 
            // lblVc
            // 
            this.lblVc.AutoSize = true;
            this.lblVc.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVc.Location = new System.Drawing.Point(153, 93);
            this.lblVc.Name = "lblVc";
            this.lblVc.Size = new System.Drawing.Size(26, 16);
            this.lblVc.TabIndex = 1;
            this.lblVc.Text = "Vc";
            // 
            // lblVb
            // 
            this.lblVb.AutoSize = true;
            this.lblVb.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVb.Location = new System.Drawing.Point(154, 57);
            this.lblVb.Name = "lblVb";
            this.lblVb.Size = new System.Drawing.Size(27, 16);
            this.lblVb.TabIndex = 1;
            this.lblVb.Text = "Vb";
            // 
            // lblVa
            // 
            this.lblVa.AutoSize = true;
            this.lblVa.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVa.Location = new System.Drawing.Point(154, 21);
            this.lblVa.Name = "lblVa";
            this.lblVa.Size = new System.Drawing.Size(27, 16);
            this.lblVa.TabIndex = 1;
            this.lblVa.Text = "Va";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Tensão Vc";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "Tensão Vb";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "Tensão Va";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkIc);
            this.groupBox3.Controls.Add(this.chkIb);
            this.groupBox3.Controls.Add(this.chkIa);
            this.groupBox3.Controls.Add(this.chkCorrente);
            this.groupBox3.Controls.Add(this.lblIc);
            this.groupBox3.Controls.Add(this.lblIb);
            this.groupBox3.Controls.Add(this.lblIa);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(14, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 120);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Corrente";
            // 
            // chkIc
            // 
            this.chkIc.AutoSize = true;
            this.chkIc.Location = new System.Drawing.Point(268, 93);
            this.chkIc.Name = "chkIc";
            this.chkIc.Size = new System.Drawing.Size(15, 14);
            this.chkIc.TabIndex = 3;
            this.chkIc.UseVisualStyleBackColor = true;
            // 
            // chkIb
            // 
            this.chkIb.AutoSize = true;
            this.chkIb.Location = new System.Drawing.Point(269, 57);
            this.chkIb.Name = "chkIb";
            this.chkIb.Size = new System.Drawing.Size(15, 14);
            this.chkIb.TabIndex = 3;
            this.chkIb.UseVisualStyleBackColor = true;
            // 
            // chkIa
            // 
            this.chkIa.AutoSize = true;
            this.chkIa.Location = new System.Drawing.Point(269, 21);
            this.chkIa.Name = "chkIa";
            this.chkIa.Size = new System.Drawing.Size(15, 14);
            this.chkIa.TabIndex = 3;
            this.chkIa.UseVisualStyleBackColor = true;
            // 
            // chkCorrente
            // 
            this.chkCorrente.AutoSize = true;
            this.chkCorrente.Location = new System.Drawing.Point(269, 0);
            this.chkCorrente.Name = "chkCorrente";
            this.chkCorrente.Size = new System.Drawing.Size(15, 14);
            this.chkCorrente.TabIndex = 2;
            this.chkCorrente.UseVisualStyleBackColor = true;
            this.chkCorrente.CheckedChanged += new System.EventHandler(this.chkCorrente_CheckedChanged);
            // 
            // lblIc
            // 
            this.lblIc.AutoSize = true;
            this.lblIc.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIc.Location = new System.Drawing.Point(153, 93);
            this.lblIc.Name = "lblIc";
            this.lblIc.Size = new System.Drawing.Size(22, 16);
            this.lblIc.TabIndex = 1;
            this.lblIc.Text = "Ic";
            // 
            // lblIb
            // 
            this.lblIb.AutoSize = true;
            this.lblIb.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIb.Location = new System.Drawing.Point(154, 57);
            this.lblIb.Name = "lblIb";
            this.lblIb.Size = new System.Drawing.Size(23, 16);
            this.lblIb.TabIndex = 1;
            this.lblIb.Text = "Ib";
            // 
            // lblIa
            // 
            this.lblIa.AutoSize = true;
            this.lblIa.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIa.Location = new System.Drawing.Point(154, 21);
            this.lblIa.Name = "lblIa";
            this.lblIa.Size = new System.Drawing.Size(23, 16);
            this.lblIa.TabIndex = 1;
            this.lblIa.Text = "Ia";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(7, 93);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "Corrente Ic";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(7, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 16);
            this.label17.TabIndex = 0;
            this.label17.Text = "Corrente Ib";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(7, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "Corrente Ia";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkTe);
            this.groupBox4.Controls.Add(this.chkTo);
            this.groupBox4.Controls.Add(this.chkNo);
            this.groupBox4.Controls.Add(this.chkETC);
            this.groupBox4.Controls.Add(this.lblTe);
            this.groupBox4.Controls.Add(this.lblTo);
            this.groupBox4.Controls.Add(this.lblNo);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.picStatus);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Location = new System.Drawing.Point(14, 394);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(990, 128);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Variáveis não elétricas";
            // 
            // chkTe
            // 
            this.chkTe.AutoSize = true;
            this.chkTe.Location = new System.Drawing.Point(962, 19);
            this.chkTe.Name = "chkTe";
            this.chkTe.Size = new System.Drawing.Size(15, 14);
            this.chkTe.TabIndex = 3;
            this.chkTe.UseVisualStyleBackColor = true;
            // 
            // chkTo
            // 
            this.chkTo.AutoSize = true;
            this.chkTo.Location = new System.Drawing.Point(589, 19);
            this.chkTo.Name = "chkTo";
            this.chkTo.Size = new System.Drawing.Size(15, 14);
            this.chkTo.TabIndex = 3;
            this.chkTo.UseVisualStyleBackColor = true;
            // 
            // chkNo
            // 
            this.chkNo.AutoSize = true;
            this.chkNo.Location = new System.Drawing.Point(204, 19);
            this.chkNo.Name = "chkNo";
            this.chkNo.Size = new System.Drawing.Size(15, 14);
            this.chkNo.TabIndex = 3;
            this.chkNo.UseVisualStyleBackColor = true;
            // 
            // chkETC
            // 
            this.chkETC.AutoSize = true;
            this.chkETC.Location = new System.Drawing.Point(269, 0);
            this.chkETC.Name = "chkETC";
            this.chkETC.Size = new System.Drawing.Size(15, 14);
            this.chkETC.TabIndex = 2;
            this.chkETC.UseVisualStyleBackColor = true;
            this.chkETC.CheckedChanged += new System.EventHandler(this.chkETC_CheckedChanged);
            // 
            // lblTe
            // 
            this.lblTe.AutoSize = true;
            this.lblTe.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTe.Location = new System.Drawing.Point(889, 17);
            this.lblTe.Name = "lblTe";
            this.lblTe.Size = new System.Drawing.Size(25, 16);
            this.lblTe.TabIndex = 1;
            this.lblTe.Text = "Te";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(516, 17);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(25, 16);
            this.lblTo.TabIndex = 1;
            this.lblTo.Text = "To";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNo.Location = new System.Drawing.Point(117, 17);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(27, 16);
            this.lblNo.TabIndex = 1;
            this.lblNo.Text = "No";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(638, 17);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(237, 16);
            this.label22.TabIndex = 0;
            this.label22.Text = "Temperatura dos Enrolamentos";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::InterfaceDesktop.Properties.Resources.Vermelho;
            this.pictureBox2.Location = new System.Drawing.Point(173, 59);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InterfaceDesktop.Properties.Resources.Amarelo;
            this.pictureBox1.Location = new System.Drawing.Point(135, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(265, 17);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(161, 16);
            this.label23.TabIndex = 0;
            this.label23.Text = "Temperatura do Óleo";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(7, 17);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(103, 16);
            this.label24.TabIndex = 0;
            this.label24.Text = "Nível do Óleo";
            // 
            // chartTemperatura
            // 
            this.chartTemperatura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            //chartArea1.Name = "ChartArea1";
            //this.chartTemperatura.ChartAreas.Add(chartArea1);
            this.chartTemperatura.Dock = System.Windows.Forms.DockStyle.Fill;
            //legend1.Name = "Legend1";
            //this.chartTemperatura.Legends.Add(legend1);
            this.chartTemperatura.Location = new System.Drawing.Point(3, 19);
            this.chartTemperatura.Name = "chartTemperatura";
            //series1.ChartArea = "ChartArea1";
            //series1.Legend = "Legend1";
            //series1.Name = "Series1";
            //this.chartTemperatura.Series.Add(series1);
            this.chartTemperatura.Size = new System.Drawing.Size(688, 350);
            this.chartTemperatura.TabIndex = 0;
            this.chartTemperatura.Text = "chart1";
            // 
            // btnGraficos
            // 
            this.btnGraficos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGraficos.Image = global::InterfaceDesktop.Properties.Resources.Graficos;
            this.btnGraficos.Location = new System.Drawing.Point(761, 543);
            this.btnGraficos.Name = "btnGraficos";
            this.btnGraficos.Size = new System.Drawing.Size(77, 66);
            this.btnGraficos.TabIndex = 6;
            this.btnGraficos.Text = "Gráficos";
            this.btnGraficos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGraficos.UseVisualStyleBackColor = true;
            this.btnGraficos.Click += new System.EventHandler(this.btnGraficos_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Image = global::InterfaceDesktop.Properties.Resources.Comparacoes;
            this.btnExportar.Location = new System.Drawing.Point(844, 543);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(77, 66);
            this.btnExportar.TabIndex = 6;
            this.btnExportar.Text = "Comparar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = global::InterfaceDesktop.Properties.Resources.Excel;
            this.btnExcel.Location = new System.Drawing.Point(927, 543);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(77, 66);
            this.btnExcel.TabIndex = 6;
            this.btnExcel.Text = "Gerar XLS";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensagens,
            this.lblSpring,
            this.lblHora});
            this.statusStrip1.Location = new System.Drawing.Point(0, 621);
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
            // lblSpring
            // 
            this.lblSpring.Name = "lblSpring";
            this.lblSpring.Size = new System.Drawing.Size(981, 17);
            this.lblSpring.Spring = true;
            this.lblSpring.Text = " ";
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
            this.groupBox5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(310, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(694, 372);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 643);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnGraficos);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnExcel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::InterfaceDesktop.Properties.Resources.ico16;
            this.MinimumSize = new System.Drawing.Size(1024, 670);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperatura)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer tmrRelogio;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.Timer tmrGraficos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkS;
        private System.Windows.Forms.CheckBox chkQ;
        private System.Windows.Forms.CheckBox chkP;
        private System.Windows.Forms.CheckBox chkPotencia;
        private System.Windows.Forms.Label lblS;
        private System.Windows.Forms.Label lblQ;
        private System.Windows.Forms.Label lblP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkVc;
        private System.Windows.Forms.CheckBox chkVb;
        private System.Windows.Forms.CheckBox chkVa;
        private System.Windows.Forms.CheckBox chkTensao;
        private System.Windows.Forms.Label lblVc;
        private System.Windows.Forms.Label lblVb;
        private System.Windows.Forms.Label lblVa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkIc;
        private System.Windows.Forms.CheckBox chkIb;
        private System.Windows.Forms.CheckBox chkIa;
        private System.Windows.Forms.CheckBox chkCorrente;
        private System.Windows.Forms.Label lblIc;
        private System.Windows.Forms.Label lblIb;
        private System.Windows.Forms.Label lblIa;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkTe;
        private System.Windows.Forms.CheckBox chkTo;
        private System.Windows.Forms.CheckBox chkNo;
        private System.Windows.Forms.CheckBox chkETC;
        private System.Windows.Forms.Label lblTe;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemperatura;
        private System.Windows.Forms.Button btnGraficos;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMensagens;
        private System.Windows.Forms.ToolStripStatusLabel lblHora;
        private System.Windows.Forms.ToolStripStatusLabel lblSpring;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}