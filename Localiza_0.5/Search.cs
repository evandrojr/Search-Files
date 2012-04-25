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
        public List<Result> ResultLst;
        public List<String> FilesToSearch;
        public TimeSpan ElapsedSpan;
        public bool SearchWholeWord;
        private List<string> BinaryExtensions;


        public Search(string pattern, string dir, bool caseSensitive, bool searchBinaryFiles, bool serchWholeWord)
        {
            this.Pattern= pattern;
            this.Dir = dir;
            this.CaseSensitive = caseSensitive;
            this.SearchBinaryFiles = searchBinaryFiles;
            if (!caseSensitive)
                this.Pattern = this.Pattern.ToLower();
            ResultLst = new List<Result>();
            this.SearchWholeWord = serchWholeWord;
            if(this.SearchWholeWord)
                Pattern = "\\b" + this.Pattern + "\\b";
            if (!this.SearchBinaryFiles)
            {
                FilesToSearch = new List<string>();
                fillBinaryExtensions();
            }
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
        }

        public bool StartSearch(){

            int countFiles=0;
            long startTime = DateTime.Now.Ticks;
            List<string> AllFiles;
            string content;
            AllFiles = Fcn.DirectoryGetFilesRecursive(Dir);
            Encoding enc;
            Result found;
            bool binaryExtensionFound;

            if (!SearchBinaryFiles)
                foreach (string s in AllFiles)
                {
                    binaryExtensionFound = false;
                    foreach (string extension in BinaryExtensions)
                    {
                        if (s.ToLower().EndsWith(extension))
                        {
                            binaryExtensionFound = true;
                            break;
                        }
                          
                    }
                    if(!binaryExtensionFound)
                        if (Fcn.IsText(out enc, s, 100))
                        {
                            FilesToSearch.Add(s);
                        }
                }
            else
                FilesToSearch = AllFiles;

            foreach (string s in FilesToSearch)
            {
                if (countFiles % 250 == 0 && countFiles > 0)
                    GC.Collect();
                ++countFiles;
                try
                {
                    enc = Fcn.GetFileEncoding(s);
                }
                catch
                {
                    enc = Encoding.Default;
                }
                content = File.ReadAllText(s, enc);
                if (!CaseSensitive)
                    content = content.ToLower();

                if (SearchWholeWord)
                {
                    if (Regex.IsMatch(content, Pattern))
                    {
                        found = new Result();
                        found.Filename = s;
                        ResultLst.Add(found);
                    }
                }else{
                    if (content.Contains(Pattern))
                    {
                        found = new Result();
                        found.Filename = s;
                        ResultLst.Add(found);
                    }
                }
            }
            ElapsedSpan = new TimeSpan(DateTime.Now.Ticks - startTime);
            return true;
        }

        public class Result
        {
            public string Filename;
            public List<int> Lines = new List<int>();
        }

    }


    


}
