namespace InterfaceDesktop
{
    partial class Analogico
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ovalShape1 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMeio = new System.Windows.Forms.Label();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.ovalShape1,
            this.lineShape3,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(280, 140);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // ovalShape1
            // 
            this.ovalShape1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ovalShape1.BackColor = System.Drawing.Color.Black;
            this.ovalShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.ovalShape1.BorderColor = System.Drawing.Color.Black;
            this.ovalShape1.FillColor = System.Drawing.Color.Transparent;
            this.ovalShape1.FillGradientColor = System.Drawing.Color.Transparent;
            this.ovalShape1.Location = new System.Drawing.Point(124, 123);
            this.ovalShape1.Name = "ovalShape1";
            this.ovalShape1.SelectionColor = System.Drawing.Color.Transparent;
            this.ovalShape1.Size = new System.Drawing.Size(30, 30);
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.Color.Red;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.SelectionColor = System.Drawing.Color.Transparent;
            this.lineShape2.X1 = 43;
            this.lineShape2.X2 = 138;
            this.lineShape2.Y1 = 124;
            this.lineShape2.Y2 = 140;
            // 
            // lineShape1
            // 
            this.lineShape1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lineShape1.BorderWidth = 5;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.SelectionColor = System.Drawing.Color.Transparent;
            this.lineShape1.X1 = 97;
            this.lineShape1.X2 = 132;
            this.lineShape1.Y1 = 71;
            this.lineShape1.Y2 = 136;
            // 
            // lblMin
            // 
            this.lblMin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMin.BackColor = System.Drawing.Color.Transparent;
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.ForeColor = System.Drawing.Color.DimGray;
            this.lblMin.Location = new System.Drawing.Point(30, 124);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(50, 13);
            this.lblMin.TabIndex = 1;
            this.lblMin.Text = "0";
            // 
            // lblMax
            // 
            this.lblMax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMax.BackColor = System.Drawing.Color.Transparent;
            this.lblMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMax.ForeColor = System.Drawing.Color.DimGray;
            this.lblMax.Location = new System.Drawing.Point(219, 123);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(50, 13);
            this.lblMax.TabIndex = 1;
            this.lblMax.Text = "100";
            this.lblMax.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMeio
            // 
            this.lblMeio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMeio.BackColor = System.Drawing.Color.Transparent;
            this.lblMeio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeio.ForeColor = System.Drawing.Color.DimGray;
            this.lblMeio.Location = new System.Drawing.Point(129, 35);
            this.lblMeio.Name = "lblMeio";
            this.lblMeio.Size = new System.Drawing.Size(50, 13);
            this.lblMeio.TabIndex = 1;
            this.lblMeio.Text = "50";
            this.lblMeio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lineShape3
            // 
            this.lineShape3.BorderColor = System.Drawing.Color.Red;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.SelectionColor = System.Drawing.Color.Transparent;
            this.lineShape3.X1 = 53;
            this.lineShape3.X2 = 148;
            this.lineShape3.Y1 = 115;
            this.lineShape3.Y2 = 131;
            // 
            // Analogico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::InterfaceDesktop.Properties.Resources.Relogio;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.shapeContainer1);
            this.Controls.Add(this.lblMeio);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.lblMax);
            this.DoubleBuffered = true;
            this.Name = "Analogico";
            this.Size = new System.Drawing.Size(280, 140);
            this.Load += new System.EventHandler(this.Analogico_Load);
            this.SizeChanged += new System.EventHandler(this.Analogico_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblMeio;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
    }
}
