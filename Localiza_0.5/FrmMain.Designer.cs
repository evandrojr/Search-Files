namespace Localiza
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBoxOptions = new System.Windows.Forms.GroupBox();
            this.cbxSearchWholeWord = new System.Windows.Forms.CheckBox();
            this.cbxCaseSensitive = new System.Windows.Forms.CheckBox();
            this.lblExampleDirBusca = new System.Windows.Forms.Label();
            this.btnDir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTermo = new System.Windows.Forms.TextBox();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.BtnLocalizar = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.Arquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Caminho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Extensao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip.SuspendLayout();
            this.grpBoxOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 378);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(916, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 17);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Diretório da busca:";
            // 
            // grpBoxOptions
            // 
            this.grpBoxOptions.Controls.Add(this.cbxSearchWholeWord);
            this.grpBoxOptions.Controls.Add(this.cbxCaseSensitive);
            this.grpBoxOptions.Controls.Add(this.lblExampleDirBusca);
            this.grpBoxOptions.Controls.Add(this.btnDir);
            this.grpBoxOptions.Controls.Add(this.label1);
            this.grpBoxOptions.Controls.Add(this.label2);
            this.grpBoxOptions.Controls.Add(this.txtTermo);
            this.grpBoxOptions.Controls.Add(this.txtDir);
            this.grpBoxOptions.Controls.Add(this.BtnLocalizar);
            this.grpBoxOptions.Location = new System.Drawing.Point(8, 7);
            this.grpBoxOptions.Name = "grpBoxOptions";
            this.grpBoxOptions.Size = new System.Drawing.Size(898, 139);
            this.grpBoxOptions.TabIndex = 12;
            this.grpBoxOptions.TabStop = false;
            this.grpBoxOptions.Text = "Procurar nos arquivos";
            // 
            // cbxSearchWholeWord
            // 
            this.cbxSearchWholeWord.AutoSize = true;
            this.cbxSearchWholeWord.Location = new System.Drawing.Point(245, 110);
            this.cbxSearchWholeWord.Name = "cbxSearchWholeWord";
            this.cbxSearchWholeWord.Size = new System.Drawing.Size(173, 17);
            this.cbxSearchWholeWord.TabIndex = 4;
            this.cbxSearchWholeWord.Text = "Procurar pela palavra completa";
            this.cbxSearchWholeWord.UseVisualStyleBackColor = true;
            // 
            // cbxCaseSensitive
            // 
            this.cbxCaseSensitive.AutoSize = true;
            this.cbxCaseSensitive.Location = new System.Drawing.Point(9, 110);
            this.cbxCaseSensitive.Name = "cbxCaseSensitive";
            this.cbxCaseSensitive.Size = new System.Drawing.Size(230, 17);
            this.cbxCaseSensitive.TabIndex = 2;
            this.cbxCaseSensitive.Text = "Diferenciar letras maiúsculas de minúsculas";
            this.cbxCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // lblExampleDirBusca
            // 
            this.lblExampleDirBusca.AutoSize = true;
            this.lblExampleDirBusca.Location = new System.Drawing.Point(6, 53);
            this.lblExampleDirBusca.Name = "lblExampleDirBusca";
            this.lblExampleDirBusca.Size = new System.Drawing.Size(453, 13);
            this.lblExampleDirBusca.TabIndex = 17;
            this.lblExampleDirBusca.Text = "Exemplo: c:\\Users\\[CPF]\\Vob\\34613e-Processo\\01-Sistema\\06-Implementacao\\01-Aplica" +
                "cao";
            this.lblExampleDirBusca.DoubleClick += new System.EventHandler(this.lblExampleDirBusca_DoubleClick);
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(739, 48);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(150, 23);
            this.btnDir.TabIndex = 13;
            this.btnDir.Text = "Alterar diretório";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Padrão procurado:";
            // 
            // txtTermo
            // 
            this.txtTermo.Location = new System.Drawing.Point(103, 77);
            this.txtTermo.Name = "txtTermo";
            this.txtTermo.Size = new System.Drawing.Size(786, 20);
            this.txtTermo.TabIndex = 0;
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(103, 22);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(786, 20);
            this.txtDir.TabIndex = 5;
            // 
            // BtnLocalizar
            // 
            this.BtnLocalizar.Location = new System.Drawing.Point(814, 103);
            this.BtnLocalizar.Name = "BtnLocalizar";
            this.BtnLocalizar.Size = new System.Drawing.Size(75, 23);
            this.BtnLocalizar.TabIndex = 1;
            this.BtnLocalizar.Text = "Procurar";
            this.BtnLocalizar.UseVisualStyleBackColor = true;
            this.BtnLocalizar.Click += new System.EventHandler(this.BtnLocalizar_Click);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Arquivo,
            this.Caminho,
            this.Extensao,
            this.DateModified});
            this.dg.Location = new System.Drawing.Point(8, 156);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.Size = new System.Drawing.Size(896, 219);
            this.dg.TabIndex = 13;
            this.dg.DoubleClick += new System.EventHandler(this.dg_DoubleClick);
            // 
            // Arquivo
            // 
            this.Arquivo.FillWeight = 200F;
            this.Arquivo.HeaderText = "Arquivo";
            this.Arquivo.Name = "Arquivo";
            this.Arquivo.ReadOnly = true;
            this.Arquivo.Width = 200;
            // 
            // Caminho
            // 
            this.Caminho.FillWeight = 500F;
            this.Caminho.HeaderText = "Caminho";
            this.Caminho.Name = "Caminho";
            this.Caminho.ReadOnly = true;
            this.Caminho.Width = 500;
            // 
            // Extensao
            // 
            this.Extensao.HeaderText = "Extensão";
            this.Extensao.Name = "Extensao";
            this.Extensao.ReadOnly = true;
            // 
            // DateModified
            // 
            this.DateModified.HeaderText = "Modificação";
            this.DateModified.Name = "DateModified";
            this.DateModified.ReadOnly = true;
            // 
            // FrmMain
            // 
            this.AcceptButton = this.BtnLocalizar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 400);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.grpBoxOptions);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "Localiza padrões em arquivos de texto";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.grpBoxOptions.ResumeLayout(false);
            this.grpBoxOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpBoxOptions;
        private System.Windows.Forms.CheckBox cbxCaseSensitive;
        private System.Windows.Forms.Label lblExampleDirBusca;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTermo;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button BtnLocalizar;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.CheckBox cbxSearchWholeWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arquivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Caminho;
        private System.Windows.Forms.DataGridViewTextBoxColumn Extensao;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateModified;
    }
}

