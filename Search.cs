using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using ShowLib;
using System.Text.RegularExpressions;

namespace Localiza
{
    public class Search
    {
        public string Pattern;
        public string Dir;
        public bool CaseSensitive;
        public bool SearchBinaryFiles;
        public bool RegularExpression;
        public bool SearchInFilenames;
        public bool SearchOnlyInFilenames;
        public List<Result> ResultLst = new List<Result>();
        public List<String> FilesToSearch = new List<String>();
        public TimeSpan ElapsedSpan;
        public bool SearchWholeWord;
        private List<string> BinaryExtensions;
        private List<string> AllFiles=null;
        public static string RegExSpecialChars= @"^,?,*,+,\,.,-,|,(,),$,{,},[,],<,>,=,*";


        public Search(string pattern, string dir, bool caseSensitive, bool searchBinaryFiles, bool serchWholeWord,
                                        bool regularExpression, bool searchInFileNames, bool searchOnlyInFilenames)
        {
            this.Pattern= pattern;
            this.Dir = dir;
            this.CaseSensitive = caseSensitive;
            this.SearchBinaryFiles = searchBinaryFiles;
            this.Pattern = pattern;
            this.SearchWholeWord = serchWholeWord;
            this.RegularExpression = regularExpression;
            this.SearchInFilenames = searchInFileNames;
            this.SearchOnlyInFilenames = searchOnlyInFilenames;
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
            if (!CaseSensitive)
                Pattern = Pattern.ToLower();
            if (this.SearchWholeWord)
                Pattern = "\\b" + this.Pattern + "\\b";
            if (!this.SearchBinaryFiles)
            {
                FilesToSearch = new List<string>();
                fillBinaryExtensions();
            }
            if (this.SearchWholeWord)
                Pattern=RemoveRegExSpecialChars(Pattern);
        }

        public void StartSearch(){
            PreProcess();
            AllFiles = Fcn.DirectoryGetFilesRecursive(Dir);
            if (!SearchOnlyInFilenames)
            {
                SearchInFileContent();
                LocateLines();
            }
            if(SearchOnlyInFilenames || SearchInFilenames)
                SearchInFilename();
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
                        if (MatchContent(line, ResultLst[i].Filename, ResultLst[i].Encoding))
                            ResultLst[i].AddTextBlock(counter, line);
                        counter++;
                    }
                    file.Close();
                }
            }
        }


        public void SearchInFileContent()
        {
            long startTime = DateTime.Now.Ticks;
            string content;            
            Encoding enc;
            bool binaryExtensionFound;

            if (!SearchBinaryFiles) {
                foreach (string s in AllFiles) {
                    binaryExtensionFound = false;
                    foreach (string extension in BinaryExtensions) {
                        if (s.ToLower().EndsWith(extension)) {
                            binaryExtensionFound = true;
                            break;
                        }

                    }
                    if (!binaryExtensionFound)
                        if (Fcn.IsText(out enc, s, 100)) {
                            FilesToSearch.Add(s);
                        }
                }
            } else
                FilesToSearch = AllFiles;

            foreach (string filename in FilesToSearch)
            {
                try
                {
                    enc = Fcn.GetFileEncoding(filename);
                }
                catch
                {
                    enc = Encoding.Default;
                }
                content = File.ReadAllText(filename, enc);
                if(MatchContent(content, filename, enc))
                    ResultAdd(filename, enc);
            }
            ElapsedSpan = new TimeSpan(DateTime.Now.Ticks - startTime);
        }


        private bool MatchContent(string content, string filename, Encoding enc) {

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
                    } else {
                        if (Regex.IsMatch(content, Pattern, RegexOptions.IgnoreCase))
                            return true;
                    }
                }else{
                        //Precisa colocar isso, para permitir que seja pesquisado um padrão que tenha caracteres de uma expressão regular
                        if (content.Contains(Pattern))
                            return true;;                   
                }
                return false;
        }

        private void SearchInFilename()
        {


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
