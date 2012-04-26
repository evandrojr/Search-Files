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
using System.Text.RegularExpressions;
using System.Web;
using System.Diagnostics;

namespace Localiza
{
    public class Search
    {
        public string Pattern;
        public string PatternWithSpecialChars;
        public string Dir;
        public bool CaseSensitive;
        public bool SearchBinaryFiles;
        public bool RegularExpression;
        public bool SearchInFilenames;
        public bool SearchOnlyInFilenames;
        public bool DecodeHtml;
        public List<Result> ResultLst = new List<Result>();
        public List<FileInformation> FileLst;
        public TimeSpan ElapsedSpan;
        public bool SearchWholeWord;
        private List<string> BinaryExtensions;
        public static string RegExSpecialChars= @"^,?,*,+,\,.,-,|,(,),$,{,},[,],<,>,=,*";


        public Search(){}

        public Search(string pattern, string dir, bool caseSensitive, bool searchBinaryFiles, bool serchWholeWord,
                                        bool regularExpression, bool searchInFileNames, bool searchOnlyInFilenames, bool decodeHml)
        {
            this.Pattern= pattern;
            this.PatternWithSpecialChars = pattern;
            this.Dir = dir;
            this.CaseSensitive = caseSensitive;
            this.SearchBinaryFiles = searchBinaryFiles;
            this.Pattern = pattern;
            this.SearchWholeWord = serchWholeWord;
            this.RegularExpression = regularExpression;
            this.SearchInFilenames = searchInFileNames;
            this.SearchOnlyInFilenames = searchOnlyInFilenames;
            this.DecodeHtml = decodeHml;

        }

        public struct FileInformation
        {
            public string Path;
            public Encoding Encoding;
            public bool Text;
            public long size;
        }


        /// <summary>Gera uma lista lista dos arquivos contidos num determinado diretório</summary>
        /// <summary>Pega informação sobre tipo e codificação do arquivo</summary>
        /// <param name="b">Caminho do diretório desejado</param>
        /// <returns>Stringlist com os caminhos dos arquivos existentes no diretório</returns>
        public List<FileInformation> DirectoryGetFilesInfoRecusive(string b) {

            bool binaryExtensionFound=false;

            // 1.
            // Store results in the file results list.
            List<FileInformation> result = new List<FileInformation>();

            // 2.
            // Store a stack of our directories.
            Stack<string> stack = new Stack<string>();

            // 3.
            // Add initial directory.
            stack.Push(b);

            // 4.
            // Continue while there are directories to process
            while (stack.Count > 0) {
                // A.
                // Get top directory
                string dir = stack.Pop();

                try {
                    // B
                    // Add all files at this directory to the result List.
                    //Era o original
                    //result.AddRange(Directory.GetFiles(dir, "*.*"));
                    foreach (string s in Directory.GetFiles(dir, "*.*")) {

                        //if (s.Contains("1.PDF"))
                        //    binaryExtensionFound = binaryExtensionFound;

                        FileInformation fi = new FileInformation();
                        fi.Path = s;
                        FileInfo info = new FileInfo(s);
                        fi.size = info.Length;
                        //1st check it the file has a common type of binary extension
                        binaryExtensionFound = false;
                        foreach (string extension in BinaryExtensions) {
                            if (s.ToLower().EndsWith(extension)) {
                                binaryExtensionFound = true;
                                break;
                            }
                        }
                        if (binaryExtensionFound) {
                            fi.Encoding = null;
                            fi.Text = false;
                        } else {
                            fi.Text = true;
                            Fcn.TryToDetectEncoding(out fi.Encoding, s, 500);
                        }
                        result.Add(fi);
                    }

                    // C
                    // Add all directories at this directory.
                    foreach (string dn in Directory.GetDirectories(dir)) {
                        stack.Push(dn);
                    }
                } catch {
                    // D
                    // Could not open the directory
                }
            }
            return result;
        }


        public void ResultAdd(string filename, Encoding encoding){
            Result r = new Result(filename, encoding);
            ResultLst.Add(r);
        }

        private void fillBinaryExtensions(){
            BinaryExtensions = new List<string>();
            BinaryExtensions.Add(".exe");
            BinaryExtensions.Add(".db");
            BinaryExtensions.Add(".dll");
            BinaryExtensions.Add(".jpeg");
            BinaryExtensions.Add(".jar");
            BinaryExtensions.Add(".zip");
            BinaryExtensions.Add(".jpg");
            BinaryExtensions.Add(".pdf");
            BinaryExtensions.Add(".db");
            BinaryExtensions.Add(".p7z");
            BinaryExtensions.Add(".p7b");
            BinaryExtensions.Add(".suo");
            BinaryExtensions.Add(".pfx");
        }

        public static string RemoveRegExSpecialChars(string j)
        {
            foreach (string s in RegExSpecialChars.Split(','))
                j=j.Replace(s, "");
            return j;
        }

        public void PreProcess()
        {
            fillBinaryExtensions();
            if (!CaseSensitive) {
                Pattern = Pattern.ToLower();
                PatternWithSpecialChars = PatternWithSpecialChars.ToLower();
            }
            if (SearchWholeWord)
                Pattern = RemoveRegExSpecialChars(Pattern);
            if (SearchWholeWord)
                Pattern = "\\b" + this.Pattern + "\\b";
        }

        public void StartSearch(){
            long startTime = DateTime.Now.Ticks;
            PreProcess();
            FileLst = DirectoryGetFilesInfoRecusive(Dir);
            SearchForContentAndFilename();
            Locate1stLine();
            ElapsedSpan = new TimeSpan(DateTime.Now.Ticks - startTime);
        }


        public Result LocateAllLines(string filename)
        {
            int counter=0;
            string line=null;
            Encoding enc;

            if(false==Fcn.TryToDetectEncoding(out enc, filename, 1000))
                    enc = Encoding.Default;
            Result result = new Result(filename, enc);

            using (StreamReader file = new StreamReader(filename, enc)) {
                counter = 1;
                while ((line = file.ReadLine()) != null)
                {
                    if (MatchContent(line, enc)) {
                        result.AddTextBlock(counter, line);

                    }
                    counter++;
                }
                file.Close();
            }

            return result;
            
        }


        public void Locate1stLine() {
            int counter = 0;
            string line = null;
            Encoding enc;


            for (int i = 0; i < ResultLst.Count; ++i) {
                enc = ResultLst[i].Encoding;
                if (enc == null)
                    enc = Encoding.Default;
                using (StreamReader file = new StreamReader(ResultLst[i].Filename, enc)) {
                    counter = 1;
                    while ((line = file.ReadLine()) != null) {
                        if (MatchContent(line, ResultLst[i].Encoding)) {
                            ResultLst[i].AddTextBlock(counter, line);
                            break;
                        }
                        counter++;
                    }
                    file.Close();
                }
            }
        }

        public void SearchForContentAndFilename()
        {
           
            string content="";
            int filesCount=1;
            Process proc; 
            foreach (FileInformation fileInfo in FileLst)
            {

                if (filesCount % 200 == 0) {
                    proc = Process.GetCurrentProcess();
                    if (proc.PeakWorkingSet64 / 1024 > 50000)
                        GC.Collect();
                }
                ++filesCount;
                //search in filename
                if (SearchInFilenames || SearchOnlyInFilenames) {
                    if (Fcn.FileName(fileInfo.Path).ToLower().Contains(PatternWithSpecialChars)) {
                       ResultAdd(fileInfo.Path, fileInfo.Encoding);
                        continue; //Does not need to add the same result twice!
                    }
               }
                if (!SearchOnlyInFilenames && fileInfo.size < 30 * 1048576) { 
                        //Seach text files    
                        if (fileInfo.Text) {
                                if (fileInfo.Encoding != null)
                                    content = File.ReadAllText(fileInfo.Path, fileInfo.Encoding);
                                else
                                    content = File.ReadAllText(fileInfo.Path, Encoding.Default);
                                if (MatchContent(content, fileInfo.Encoding)) {
                                    ResultAdd(fileInfo.Path, fileInfo.Encoding);
                                }
                        } else {
                            if (SearchBinaryFiles) {
                                //Search binary file
                                content = File.ReadAllText(fileInfo.Path, Encoding.Default);
                                if (MatchContent(content, Encoding.Default))
                                    ResultAdd(fileInfo.Path, fileInfo.Encoding);
                            }
                        }

                }
            }
        }



        private bool MatchContent(string content, Encoding enc) {

                if (RegularExpression || SearchWholeWord)
                {
                    if (CaseSensitive) {
                        if (Regex.IsMatch(content, Pattern))
                            return true;
                        else {
                            if (DecodeHtml)
                                if (HttpUtility.HtmlDecode(content).Contains(Pattern))
                                    return true;
                        }
                    } else {
                        if (Regex.IsMatch(content, Pattern, RegexOptions.IgnoreCase))
                            return true;
                        else {
                            if (DecodeHtml)
                                if (HttpUtility.HtmlDecode(content).Contains(Pattern))
                                    return true;
                        }
                    }
                }else{
                        if(!CaseSensitive)
                            content = content.ToLower();
                        //Precisa colocar isso, para permitir que seja pesquisado um padrão que tenha caracteres de uma expressão regular
                        if (content.Contains(Pattern))
                            return true;
                        else {
                            if(DecodeHtml)
                                if (HttpUtility.HtmlDecode(content).Contains(Pattern))
                                    return true;
                        }
                }
                return false;
        }

        public class Result
        {
            public string Filename;
            public List<TextBlock> TextBlockLst = new List<TextBlock>();
            public Encoding Encoding=null;

            public string Results {
                get {
                    string ret="";
                    foreach (TextBlock tb in TextBlockLst) {
                        ret += "Linha " + tb.LineNumber.ToString().PadLeft(3,'0') + ":  " + tb.Text + Environment.NewLine;
                    }
                    return ret;
                }
            }

            public Result(string filename, Encoding encoding)
            {
                Filename = filename;
                Encoding = encoding;
            }

            public void AddTextBlock(int lineNumber, string text)
            {
                TextBlock t = new TextBlock(lineNumber, text);
                TextBlockLst.Add(t);
            }

            public class TextBlock{
                public int LineNumber;
                public string Text;

                public TextBlock(int lineNumber, string text){
                   LineNumber=lineNumber;
                   Text=text;
                }

            }
        }
    }


    


}
