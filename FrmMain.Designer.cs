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
            this.grbArquivos = new System.Windows.Forms.GroupBox();
            this.lblExampleDirBusca = new System.Windows.Forms.Label();
            this.btnDir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTermo = new System.Windows.Forms.TextBox();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.cbxSearchWholeWord = new System.Windows.Forms.CheckBox();
            this.cbxSearchBinaryFiles = new System.Windows.Forms.CheckBox();
            this.cbxCaseSensitive = new System.Windows.Forms.CheckBox();
            this.BtnLocalizar = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.grbOpcoes = new System.Windows.Forms.GroupBox();
            this.cbxOnlySearchInFileNames = new System.Windows.Forms.CheckBox();
            this.llManualExpressoesRegulares = new System.Windows.Forms.LinkLabel();
            this.cbxUseRegularExpressions = new System.Windows.Forms.CheckBox();
            this.cbxAlsoSearchInFilenames = new System.Windows.Forms.CheckBox();
            this.grbAlso = new System.Windows.Forms.GroupBox();
            this.cbxDecodeHtml = new System.Windows.Forms.CheckBox();
            this.Arquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Linha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Caminho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Extensao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tamanho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip.SuspendLayout();
            this.grbArquivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.grbOpcoes.SuspendLayout();
            this.grbAlso.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 521);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(988, 22);
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
            // grbArquivos
            // 
            this.grbArquivos.Controls.Add(this.lblExampleDirBusca);
            this.grbArquivos.Controls.Add(this.btnDir);
            this.grbArquivos.Controls.Add(this.label1);
            this.grbArquivos.Controls.Add(this.label2);
            this.grbArquivos.Controls.Add(this.txtTermo);
            this.grbArquivos.Controls.Add(this.txtDir);
            this.grbArquivos.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.grbArquivos.Location = new System.Drawing.Point(8, 7);
            this.grbArquivos.Name = "grbArquivos";
            this.grbArquivos.Size = new System.Drawing.Size(898, 111);
            this.grbArquivos.TabIndex = 12;
            this.grbArquivos.TabStop = false;
            this.grbArquivos.Text = "Procurar nos arquivos";
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
            this.btnDir.Location = new System.Drawing.Point(761, 48);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(128, 23);
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
            // cbxSearchWholeWord
            // 
            this.cbxSearchWholeWord.AutoSize = true;
            this.cbxSearchWholeWord.Location = new System.Drawing.Point(9, 47);
            this.cbxSearchWholeWord.Name = "cbxSearchWholeWord";
            this.cbxSearchWholeWord.Size = new System.Drawing.Size(131, 17);
            this.cbxSearchWholeWord.TabIndex = 4;
            this.cbxSearchWholeWord.Text = "Pela palavra completa";
            this.cbxSearchWholeWord.UseVisualStyleBackColor = true;
            this.cbxSearchWholeWord.CheckedChanged += new System.EventHandler(this.cbxSearchWholeWord_CheckedChanged);
            // 
            // cbxSearchBinaryFiles
            // 
            this.cbxSearchBinaryFiles.AutoSize = true;
            this.cbxSearchBinaryFiles.Location = new System.Drawing.Point(191, 19);
            this.cbxSearchBinaryFiles.Name = "cbxSearchBinaryFiles";
            this.cbxSearchBinaryFiles.Size = new System.Drawing.Size(123, 17);
            this.cbxSearchBinaryFiles.TabIndex = 3;
            this.cbxSearchBinaryFiles.Text = "Em arquivos binários";
            this.cbxSearchBinaryFiles.UseVisualStyleBackColor = true;
            // 
            // cbxCaseSensitive
            // 
            this.cbxCaseSensitive.AutoSize = true;
            this.cbxCaseSensitive.Location = new System.Drawing.Point(9, 19);
            this.cbxCaseSensitive.Name = "cbxCaseSensitive";
            this.cbxCaseSensitive.Size = new System.Drawing.Size(230, 17);
            this.cbxCaseSensitive.TabIndex = 2;
            this.cbxCaseSensitive.Text = "Diferenciar letras maiúsculas de minúsculas";
            this.cbxCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // BtnLocalizar
            // 
            this.BtnLocalizar.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.BtnLocalizar.Location = new System.Drawing.Point(769, 133);
            this.BtnLocalizar.Name = "BtnLocalizar";
            this.BtnLocalizar.Size = new System.Drawing.Size(128, 23);
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
            this.Linha,
            this.Caminho,
            this.Extensao,
            this.DateModified,
            this.Tamanho,
            this.Codificacao});
            this.dg.Location = new System.Drawing.Point(0, 200);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.Size = new System.Drawing.Size(988, 318);
            this.dg.TabIndex = 13;
            this.dg.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dg_CellMouseDoubleClick);
            // 
            // grbOpcoes
            // 
            this.grbOpcoes.Controls.Add(this.cbxOnlySearchInFileNames);
            this.grbOpcoes.Controls.Add(this.llManualExpressoesRegulares);
            this.grbOpcoes.Controls.Add(this.cbxUseRegularExpressions);
            this.grbOpcoes.Controls.Add(this.cbxSearchWholeWord);
            this.grbOpcoes.Controls.Add(this.cbxCaseSensitive);
            this.grbOpcoes.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.grbOpcoes.Location = new System.Drawing.Point(8, 124);
            this.grbOpcoes.Name = "grbOpcoes";
            this.grbOpcoes.Size = new System.Drawing.Size(413, 70);
            this.grbOpcoes.TabIndex = 14;
            this.grbOpcoes.TabStop = false;
            this.grbOpcoes.Text = "Opções de busca";
            // 
            // cbxOnlySearchInFileNames
            // 
            this.cbxOnlySearchInFileNames.AutoSize = true;
            this.cbxOnlySearchInFileNames.Location = new System.Drawing.Point(241, 19);
            this.cbxOnlySearchInFileNames.Name = "cbxOnlySearchInFileNames";
            this.cbxOnlySearchInFileNames.Size = new System.Drawing.Size(165, 17);
            this.cbxOnlySearchInFileNames.TabIndex = 8;
            this.cbxOnlySearchInFileNames.Text = "Somente nomes dos arquivos";
            this.cbxOnlySearchInFileNames.UseVisualStyleBackColor = true;
            this.cbxOnlySearchInFileNames.CheckedChanged += new System.EventHandler(this.cbxOnlySearchInFileNames_CheckedChanged);
            // 
            // llManualExpressoesRegulares
            // 
            this.llManualExpressoesRegulares.AutoSize = true;
            this.llManualExpressoesRegulares.Location = new System.Drawing.Point(381, 48);
            this.llManualExpressoesRegulares.Name = "llManualExpressoesRegulares";
            this.llManualExpressoesRegulares.Size = new System.Drawing.Size(13, 13);
            this.llManualExpressoesRegulares.TabIndex = 6;
            this.llManualExpressoesRegulares.TabStop = true;
            this.llManualExpressoesRegulares.Text = "?";
            this.llManualExpressoesRegulares.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llManualExpressoesRegulares_LinkClicked);
            // 
            // cbxUseRegularExpressions
            // 
            this.cbxUseRegularExpressions.AutoSize = true;
            this.cbxUseRegularExpressions.Location = new System.Drawing.Point(241, 47);
            this.cbxUseRegularExpressions.Name = "cbxUseRegularExpressions";
            this.cbxUseRegularExpressions.Size = new System.Drawing.Size(143, 17);
            this.cbxUseRegularExpressions.TabIndex = 5;
            this.cbxUseRegularExpressions.Text = "Utilizar expressão regular";
            this.cbxUseRegularExpressions.UseVisualStyleBackColor = true;
            this.cbxUseRegularExpressions.CheckedChanged += new System.EventHandler(this.cbxUseRegularExpressions_CheckedChanged);
            // 
            // cbxAlsoSearchInFilenames
            // 
            this.cbxAlsoSearchInFilenames.AutoSize = true;
            this.cbxAlsoSearchInFilenames.Checked = true;
            this.cbxAlsoSearchInFilenames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAlsoSearchInFilenames.Location = new System.Drawing.Point(6, 19);
            this.cbxAlsoSearchInFilenames.Name = "cbxAlsoSearchInFilenames";
            this.cbxAlsoSearchInFilenames.Size = new System.Drawing.Size(142, 17);
            this.cbxAlsoSearchInFilenames.TabIndex = 7;
            this.cbxAlsoSearchInFilenames.Text = "Nos nomes dos arquivos";
            this.cbxAlsoSearchInFilenames.UseVisualStyleBackColor = true;
            this.cbxAlsoSearchInFilenames.CheckedChanged += new System.EventHandler(this.cbxAlsoSearchInFilenames_CheckedChanged);
            // 
            // grbAlso
            // 
            this.grbAlso.Controls.Add(this.cbxDecodeHtml);
            this.grbAlso.Controls.Add(this.cbxAlsoSearchInFilenames);
            this.grbAlso.Controls.Add(this.cbxSearchBinaryFiles);
            this.grbAlso.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.grbAlso.Location = new System.Drawing.Point(437, 124);
            this.grbAlso.Name = "grbAlso";
            this.grbAlso.Size = new System.Drawing.Size(320, 70);
            this.grbAlso.TabIndex = 15;
            this.grbAlso.TabStop = false;
            this.grbAlso.Text = "Também procurar";
            // 
            // cbxDecodeHtml
            // 
            this.cbxDecodeHtml.AutoSize = true;
            this.cbxDecodeHtml.Checked = true;
            this.cbxDecodeHtml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxDecodeHtml.Location = new System.Drawing.Point(6, 46);
            this.cbxDecodeHtml.Name = "cbxDecodeHtml";
            this.cbxDecodeHtml.Size = new System.Drawing.Size(286, 17);
            this.cbxDecodeHtml.TabIndex = 8;
            this.cbxDecodeHtml.Text = "Decodificando os código HTML (útil para páginas web)";
            this.cbxDecodeHtml.UseVisualStyleBackColor = true;
            // 
            // Arquivo
            // 
            this.Arquivo.FillWeight = 200F;
            this.Arquivo.HeaderText = "Arquivo";
            this.Arquivo.Name = "Arquivo";
            this.Arquivo.ReadOnly = true;
            this.Arquivo.Width = 200;
            // 
            // Linha
            // 
            this.Linha.FillWeight = 500F;
            this.Linha.HeaderText = "Linha";
            this.Linha.Name = "Linha";
            this.Linha.ReadOnly = true;
            this.Linha.Width = 500;
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
            // Tamanho
            // 
            this.Tamanho.HeaderText = "Tamanho";
            this.Tamanho.Name = "Tamanho";
            this.Tamanho.ReadOnly = true;
            // 
            // Codificacao
            // 
            this.Codificacao.HeaderText = "Codificação";
            this.Codificacao.Name = "Codificacao";
            this.Codificacao.ReadOnly = true;
            // 
            // FrmMain
            // 
            this.AcceptButton = this.BtnLocalizar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 543);
            this.Controls.Add(this.grbAlso);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.grbArquivos);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.BtnLocalizar);
            this.Controls.Add(this.grbOpcoes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "Localiza padrões em arquivos";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.grbArquivos.ResumeLayout(false);
            this.grbArquivos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.grbOpcoes.ResumeLayout(false);
            this.grbOpcoes.PerformLayout();
            this.grbAlso.ResumeLayout(false);
            this.grbAlso.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbArquivos;
        private System.Windows.Forms.CheckBox cbxCaseSensitive;
        private System.Windows.Forms.Label lblExampleDirBusca;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTermo;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button BtnLocalizar;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.CheckBox cbxSearchBinaryFiles;
        private System.Windows.Forms.CheckBox cbxSearchWholeWord;
        private System.Windows.Forms.GroupBox grbOpcoes;
        private System.Windows.Forms.LinkLabel llManualExpressoesRegulares;
        private System.Windows.Forms.CheckBox cbxUseRegularExpressions;
        private System.Windows.Forms.CheckBox cbxOnlySearchInFileNames;
        private System.Windows.Forms.CheckBox cbxAlsoSearchInFilenames;
        private System.Windows.Forms.GroupBox grbAlso;
        private System.Windows.Forms.CheckBox cbxDecodeHtml;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arquivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Caminho;
        private System.Windows.Forms.DataGridViewTextBoxColumn Extensao;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tamanho;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codificacao;
    }
}

