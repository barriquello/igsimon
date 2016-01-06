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
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.ovalShape1,
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
            // lineShape1
            // 
            this.lineShape1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lineShape1.BorderWidth = 5;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.SelectionColor = System.Drawing.Color.Transparent;
            this.lineShape1.X1 = 31;
            this.lineShape1.X2 = 118;
            this.lineShape1.Y1 = 154;
            this.lineShape1.Y2 = 154;
            // 
            // Analogico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::InterfaceDesktop.Properties.Resources.Relogio;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.shapeContainer1);
            this.DoubleBuffered = true;
            this.Name = "Analogico";
            this.Size = new System.Drawing.Size(280, 140);
            this.Load += new System.EventHandler(this.Analogico_Load);
            this.SizeChanged += new System.EventHandler(this.Analogico_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape1;
    }
}
