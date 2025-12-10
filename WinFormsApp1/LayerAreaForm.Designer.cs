namespace WinFormsApp1
{
    partial class LayerAreaForm
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
            txtLayer = new TextBox();
            label1 = new Label();
            btnSelectArea = new Button();
            SuspendLayout();
            // 
            // txtLayer
            // 
            txtLayer.Location = new Point(173, 140);
            txtLayer.Name = "txtLayer";
            txtLayer.Size = new Size(296, 27);
            txtLayer.TabIndex = 0;
            txtLayer.TextChanged += txtLayer_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 143);
            label1.Name = "label1";
            label1.Size = new Size(88, 20);
            label1.TabIndex = 1;
            label1.Text = "Enter Layers";
            // 
            // btnSelectArea
            // 
            btnSelectArea.Location = new Point(375, 196);
            btnSelectArea.Name = "btnSelectArea";
            btnSelectArea.Size = new Size(94, 29);
            btnSelectArea.TabIndex = 2;
            btnSelectArea.Text = "Run";
            btnSelectArea.UseVisualStyleBackColor = true;
            btnSelectArea.Click += btnSelectArea_Click;
            // 
            // LayerAreaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSelectArea);
            Controls.Add(label1);
            Controls.Add(txtLayer);
            Name = "LayerAreaForm";
            Text = "LayerAreaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLayer;
        private Label label1;
        private Button btnSelectArea;
    }
}