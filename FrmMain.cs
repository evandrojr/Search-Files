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
        //Definidas aqui para ver se fica mais rápido!
        Encoding enc;
        string fileName;
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
                if(s.ResultLst[y] != null)
                    dg[6, y].Value = s.ResultLst[y].Encoding.EncodingName;
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
                    statusStrip.Items[0].Text += s.ElapsedSpan.Minutes + " minutos(s) e " + (s.ElapsedSpan.Seconds%60) + " segundo(s).";
            MessageBox.Show("Terminado");
        }


        /// <summary>
        /// Detect if a file is text and detect the encoding.
        /// </summary>
        /// <param name="encoding">
        /// The detected encoding.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="windowSize">
        /// The number of characters to use for testing.
        /// </param>
        /// <returns>
        /// true if the file is text.
        /// </returns>
        public static bool IsText(out Encoding encoding, string fileName, int windowSize)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                var rawData = new byte[windowSize];
                var text = new char[windowSize];
                var isText = true;

                // Read raw bytes
                var rawLength = fileStream.Read(rawData, 0, rawData.Length);
                fileStream.Seek(0, SeekOrigin.Begin);

                // Detect encoding correctly (from Rick Strahl's blog)
                // http://www.west-wind.com/weblog/posts/2007/Nov/28/Detecting-Text-Encoding-for-StreamReader
                if (rawData[0] == 0xef && rawData[1] == 0xbb && rawData[2] == 0xbf)
                {
                    encoding = Encoding.UTF8;
                }
                else if (rawData[0] == 0xfe && rawData[1] == 0xff)
                {
                    encoding = Encoding.Unicode;
                }
                else if (rawData[0] == 0 && rawData[1] == 0 && rawData[2] == 0xfe && rawData[3] == 0xff)
                {
                    encoding = Encoding.UTF32;
                }
                else if (rawData[0] == 0x2b && rawData[1] == 0x2f && rawData[2] == 0x76)
                {
                    encoding = Encoding.UTF7;
                }
                else
                {
                    encoding = Encoding.Default;
                }

                // Read text and detect the encoding
                using (var streamReader = new StreamReader(fileStream))
                {
                    streamReader.Read(text, 0, text.Length);
                }

                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream, encoding))
                    {
                        // Write the text to a buffer
                        streamWriter.Write(text);
                        streamWriter.Flush();

                        // Get the buffer from the memory stream for comparision
                        var memoryBuffer = memoryStream.GetBuffer();

                        // Compare only bytes read
                        for (var i = 0; i < rawLength && isText; i++)
                        {
                            isText = rawData[i] == memoryBuffer[i];
                        }
                    }
                }

                return isText;
            }
        }


        private void checkFilesCaseSensitive(string pattern, string path)
        {
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            
            for(int i = 0; i < files.Length; i++)
            {
                fileName = files[i].Substring(files[i].LastIndexOf('\\') + 1);
                if (IsText(out enc, files[i], 50) &&  fileName != ".copyarea.db")
                    if (files[i].Contains(pattern))
                        //rtbMatches.Text += fileName + "".PadRight(Math.Abs(60 - fileName.Length), ' ') + "\t" + files[i] + "\n";
                        dg.Rows.Add(fileName, files[i]);
            }
            for (int i = 0; i < directories.Length; i++)
            {
                checkFilesCaseSensitive(pattern, directories[i]);
            }
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

        private void dg_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            string file;
            try {
                file = dg.CurrentRow.Cells[2].Value.ToString();
                Fcn.CommandLineExecuteInBackground(@"%ProgramFiles%\Notepad++\Notepad++.exe", file, "");
            } catch {
                file = dg.CurrentRow.Cells[2].Value.ToString();
                Fcn.CommandLineExecuteInBackground("Notepad.exe", file, "");
            }
        }

    }
}
