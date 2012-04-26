using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using ShowLib;

namespace Localiza
{
    public partial class FrmMain : Form
    {
        Search s;
        bool hasBeenAlertedAboutRegularExpressionsInsideWholeWordPattern = false;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnLocalizar_Click(object sender, EventArgs e)
        {
            string pattern = txtTermo.Text;
            FileInfo f = null;
            if (pattern == String.Empty || txtDir.Text == String.Empty)
            {
                MessageBox.Show("É necessário selecionar o diretório e termo procurado.", "Aviso");
                return;
            }
            if (!Directory.Exists(txtDir.Text))
            {
                MessageBox.Show("Este é um diretório inválido.", "Aviso");
                return;
            }
            if (cbxSearchWholeWord.Checked)
                txtTermo.Text = Search.RemoveRegExSpecialChars(txtTermo.Text);

            string path = txtDir.Text;
            File.WriteAllText("Localiza.cfg", txtDir.Text);
            dg.RowCount = 1;
            for(int i=0; i<dg.ColumnCount; ++i)
                dg[i, 0].Value = "";
            Cursor = Cursors.WaitCursor;
            s = new Search(pattern, path, cbxCaseSensitive.Checked,cbxSearchBinaryFiles.Checked, cbxSearchWholeWord.Checked,
                           cbxUseRegularExpressions.Checked, cbxAlsoSearchInFilenames.Checked, cbxOnlySearchInFileNames.Checked, cbxDecodeHtml.Checked);
            statusStrip.Items[0].Text = "Favor aguardar...";
            Application.DoEvents();
            s.StartSearch();

            for (int y = 0; y < s.ResultLst.Count; ++y)
            {
                f = new FileInfo(s.ResultLst[y].Filename);
                dg[0, y].Value = Fcn.FileName(s.ResultLst[y].Filename);
                dg[1, y].Value = Fcn.FileName(s.ResultLst[y].Results);
                dg[2, y].Value = s.ResultLst[y].Filename;
                dg[3, y].Value = Fcn.Extension(s.ResultLst[y].Filename);
                dg[4, y].Value = File.GetLastWriteTime(s.ResultLst[y].Filename);
                dg[5, y].Value = f.Length.ToString();
                if (s.ResultLst[y].Encoding != null)
                    dg[6, y].Value = s.ResultLst[y].Encoding.EncodingName;
                else
                    dg[6, y].Value = "<Desconhecida>";
                dg.RowCount++;
            }
            dg.RowCount--;
            Cursor = Cursors.Default;
            statusStrip.Items[0].Text = "Padrão presente em " + s.ResultLst.Count + " arquivo(s), de um total de " + s.FileLst.Count + " arquivos. Tempo decorrido ";
            if (s.ElapsedSpan.Seconds < 1)
                statusStrip.Items[0].Text += s.ElapsedSpan.Milliseconds + " milissegundos.";
            else
                if (s.ElapsedSpan.Minutes < 1)
                    statusStrip.Items[0].Text += s.ElapsedSpan.Seconds + " segundo(s) e " + s.ElapsedSpan.Milliseconds + " milissegundos.";
                else
                    statusStrip.Items[0].Text += (s.ElapsedSpan.Seconds / 60) + " minutos(s) e " + (s.ElapsedSpan.Seconds%60) + " segundo(s).";
            MessageBox.Show("Terminado");
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog.SelectedPath = txtDir.Text;
            }
            catch { }
            dg.RowCount = 1;
            folderBrowserDialog.ShowDialog();
            txtDir.Text = folderBrowserDialog.SelectedPath;
            File.WriteAllText("Localiza.cfg",txtDir.Text);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (File.Exists("Localiza.cfg"))
                txtDir.Text = File.ReadAllText("Localiza.cfg");
            txtTermo.Focus();
        }

        private void lblExampleDirBusca_DoubleClick(object sender, EventArgs e)
        {
            txtDir.Text = @"C:\vob_view\34613e-Processo\01-Sistema\06-Implementacao\01-Aplicacao";
        }


        private void llManualExpressoesRegulares_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "http://aurelio.net/regex/guia/";

            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch
                (
                 System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch
            {
                try
                {
                    System.Diagnostics.Process proc = new
                    System.Diagnostics.Process();
                    proc.StartInfo.FileName = "iexplore";
                    proc.StartInfo.Arguments = "target";
                    proc.Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro ao abrir endereço", "Não foi possível abrir a página " + target + ex.Message);
                }
            }



        }

        private void cbxAlsoSearchInFilenames_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAlsoSearchInFilenames.Checked)
                cbxOnlySearchInFileNames.Checked = false;
        }

        private void cbxOnlySearchInFileNames_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxOnlySearchInFileNames.Checked)
                cbxAlsoSearchInFilenames.Checked = false;
        }

                

        private void cbxSearchWholeWord_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSearchWholeWord.Checked)
            {
                if (!hasBeenAlertedAboutRegularExpressionsInsideWholeWordPattern)
                {
                    hasBeenAlertedAboutRegularExpressionsInsideWholeWordPattern = true;
                    MessageBox.Show(@"Ao selecionar esta opção os caracteres especiais " + Search.RegExSpecialChars + " serão removidos do padrão", "Aviso");
                }
                cbxUseRegularExpressions.Checked = false;
                txtTermo.Text = Search.RemoveRegExSpecialChars(txtTermo.Text);
                
            }
        }

        private void cbxUseRegularExpressions_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUseRegularExpressions.Checked)
                cbxSearchWholeWord.Checked = false;
        }


        private void dg_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {

            string file = dg.CurrentRow.Cells[2].Value.ToString();
            string lineCell;
            string programfiles = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
            string notepadpp = programfiles + @"\Notepad++\notepad++.exe";

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (e.ColumnIndex == 1) {
                file = dg[2, e.RowIndex].Value.ToString();
                lineCell = dg[1, e.RowIndex].Value.ToString();
                if (file != "" && lineCell != "") {

                    Search s = new Search(txtTermo.Text, txtDir.Text, cbxCaseSensitive.Checked, cbxSearchBinaryFiles.Checked, cbxSearchWholeWord.Checked, cbxUseRegularExpressions.Checked, cbxAlsoSearchInFilenames.Checked, cbxOnlySearchInFileNames.Checked, cbxDecodeHtml.Checked);
                    s.PreProcess();
                    Search.Result r = s.LocateAllLines(file);
                    FrmViewText f = new FrmViewText();
                    f.rtb.Text = r.Results;
                    f.Show(this);
                }
            } else {
                try {
                    Fcn.CommandLineExecuteInBackground(notepadpp, file, "");
                } catch {
                    Fcn.CommandLineExecuteInBackground("Notepad.exe", file, "");
                }
            }
        }

        private void dg_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {

            
        }



    }
}
