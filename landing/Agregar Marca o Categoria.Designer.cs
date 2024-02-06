namespace landing
{
    partial class Agregar_Marca_o_Categoria
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
            this.cbxMarcaCategoria = new System.Windows.Forms.ComboBox();
            this.txtMarcaCategoria = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxMarcaCategoria
            // 
            this.cbxMarcaCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMarcaCategoria.FormattingEnabled = true;
            this.cbxMarcaCategoria.Location = new System.Drawing.Point(65, 111);
            this.cbxMarcaCategoria.Name = "cbxMarcaCategoria";
            this.cbxMarcaCategoria.Size = new System.Drawing.Size(152, 24);
            this.cbxMarcaCategoria.TabIndex = 0;
            // 
            // txtMarcaCategoria
            // 
            this.txtMarcaCategoria.Location = new System.Drawing.Point(237, 113);
            this.txtMarcaCategoria.Name = "txtMarcaCategoria";
            this.txtMarcaCategoria.Size = new System.Drawing.Size(152, 22);
            this.txtMarcaCategoria.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(246, 211);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 38);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(83, 211);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(125, 38);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // Agregar_Marca_o_Categoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 324);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtMarcaCategoria);
            this.Controls.Add(this.cbxMarcaCategoria);
            this.Name = "Agregar_Marca_o_Categoria";
            this.Text = "Agregar_Marca_o_Categoria";
            this.Load += new System.EventHandler(this.Agregar_Marca_o_Categoria_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxMarcaCategoria;
        private System.Windows.Forms.TextBox txtMarcaCategoria;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
    }
}