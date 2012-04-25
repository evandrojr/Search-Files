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
using Ude.Core;
using System.Web;
using Ude;

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
        public List<FileInfo> FileLst;
        public TimeSpan ElapsedSpan;
        public bool SearchWholeWord;
        private List<string> BinaryExtensions;
        public static string RegExSpecialChars= @"^,?,*,+,\,.,-,|,(,),$,{,},[,],<,>,=,*";


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

        public struct FileInfo
        {
            public string Path;
            public Encoding Encoding;
            public bool Text;
        }


        /// <summary>Gera uma lista lista dos arquivos contidos num determinado diretório</summary>
        /// <summary>Pega informação sobre tipo e codificação do arquivo</summary>
        /// <param name="b">Caminho do diretório desejado</param>
        /// <returns>Stringlist com os caminhos dos arquivos existentes no diretório</returns>
        public List<FileInfo> DirectoryGetFilesInfoRecusive(string b) {

            bool binaryExtensionFound=false;

            // 1.
            // Store results in the file results list.
            List<FileInfo> result = new List<FileInfo>();

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
                        FileInfo fi = new FileInfo();
                        fi.Path = s;
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
                            fi.Text = Fcn.IsText(out fi.Encoding, s, 1000);
                            result.Add(fi);
                        }
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
        }

        public static string RemoveRegExSpecialChars(string j)
        {
            foreach (string s in RegExSpecialChars.Split(','))
                j=j.Replace(s, "");
            return j;
        }

        private void PreProcess()
        {
            fillBinaryExtensions();
            if (!CaseSensitive) {
                Pattern = Pattern.ToLower();
                PatternWithSpecialChars = PatternWithSpecialChars.ToLower();
            }
            if (SearchWholeWord)
                Pattern = "\\b" + this.Pattern + "\\b";
            if (SearchWholeWord)
                Pattern=RemoveRegExSpecialChars(Pattern);
        }

        public void StartSearch(){
            long startTime = DateTime.Now.Ticks;
            PreProcess();
            FileLst = DirectoryGetFilesInfoRecusive(Dir);
            SearchForContentAndFilename();
            LocateLines();
            ElapsedSpan = new TimeSpan(DateTime.Now.Ticks - startTime);
        }


        public void LocateLines()
        {
            int counter=0;
            string line=null;

            for (int i = 0; i < ResultLst.Count; ++i)
            {
                using(StreamReader file = new StreamReader(ResultLst[i].Filename, ResultLst[i].Encoding)){
                    counter = 1;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (MatchContent(line, ResultLst[i].Encoding))
                            ResultLst[i].AddTextBlock(counter, line);
                        counter++;
                    }
                    file.Close();
                }
            }
        }


        public void SearchForContentAndFilename()
        {
           
            string content;          

            foreach (FileInfo fileInfo in FileLst)
            {
               //search in filename
                if (SearchInFilenames || SearchOnlyInFilenames) {
                    if (Fcn.FileName(fileInfo.Path).ToLower().Contains(PatternWithSpecialChars)) {
                       ResultAdd(fileInfo.Path, fileInfo.Encoding);
                        continue; //Does not need to add the same result twice!
                    }
               }
               if (!SearchOnlyInFilenames) { 
                    if (fileInfo.Encoding != null || SearchBinaryFiles) {
                        //Search text
                        if (fileInfo.Encoding != null) {
                            content = File.ReadAllText(fileInfo.Path, fileInfo.Encoding);
                            if (MatchContent(content, fileInfo.Encoding)) {
                                ResultAdd(fileInfo.Path, fileInfo.Encoding);
                            }
                        } else {
                            //Search binary file
                            content = File.ReadAllText(fileInfo.Path, Encoding.Default);
                            if (MatchContent(content, Encoding.Default))
                                ResultAdd(fileInfo.Path, fileInfo.Encoding);
                        }
                    }
                }
            }
        }

        //Very very slow! Better to not use it!
        private Encoding EncodingDetect(string file) {

            using (FileStream fs = File.OpenRead(file)) {
            ICharsetDetector cdet = new CharsetDetector();
            cdet.Feed(fs);
            cdet.DataEnd();
                if (cdet.Charset != null)         {
                    switch (cdet.Charset) {
                        case "UTF-8":
                            return Encoding.UTF8;
                        case "ASCII":
                            return Encoding.Default;
                        case "UTF-16LE":
                            return Encoding.Unicode;
                        case "UTF-16BE":
                            return Encoding.BigEndianUnicode;
                        case "UTF-32BE:":
                            return Encoding.UTF32;
                        case "UTF-32LE:":
                            return Encoding.UTF32;
                        case "X-ISO-10646-UCS-4-3412":
                            return Encoding.UTF32;
                        case "X-ISO-10646-UCS-4-2413":
                            return Encoding.UTF32;
                        default:
                            return Encoding.Default;
                    }
                } else {
                    return null;
                }
            }
        }

        private bool MatchContent(string content, Encoding enc) {

                //Se não marcar case sensitive ou não marcar expressao regular ele entra
                //Se marcar caseSensitive e nao marcar expressao regular F || V = V entra
                //Só não entra se for CaseSensitive || ExpressaoRegular, expressão regular F || F
                if (!CaseSensitive || !RegularExpression) 
                    content = content.ToLower();
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
                        ret += "Linha: " + tb.LineNumber + " " + tb.Text + Environment.NewLine;
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
