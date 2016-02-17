namespace Ejemplo01_02
{
    partial class Form1
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
            this.btn = new System.Windows.Forms.Button();
            this.btnEvento = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.btnHilo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(104, 33);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 0;
            this.btn.Text = "Saludo";
            this.btn.UseVisualStyleBackColor = true;
            // 
            // btnEvento
            // 
            this.btnEvento.Location = new System.Drawing.Point(104, 62);
            this.btnEvento.Name = "btnEvento";
            this.btnEvento.Size = new System.Drawing.Size(75, 23);
            this.btnEvento.TabIndex = 1;
            this.btnEvento.Text = "Evento";
            this.btnEvento.UseVisualStyleBackColor = true;
            this.btnEvento.Click += new System.EventHandler(this.btnEvento_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(230, 13);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(14, 13);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "0";
            // 
            // btnHilo
            // 
            this.btnHilo.Location = new System.Drawing.Point(104, 91);
            this.btnHilo.Name = "btnHilo";
            this.btnHilo.Size = new System.Drawing.Size(75, 23);
            this.btnHilo.TabIndex = 3;
            this.btnHilo.Text = "Hilo";
            this.btnHilo.UseVisualStyleBackColor = true;
            this.btnHilo.Click += new System.EventHandler(this.btnHilo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Clausuras";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 167);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnHilo);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.btnEvento);
            this.Controls.Add(this.btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btnEvento;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button btnHilo;
        private System.Windows.Forms.Button button1;
    }
}

