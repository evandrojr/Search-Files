using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Security;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO; 
using Ude;

namespace ShowLib
{
    public class Fcn
    {



        /// <summary>Gera uma lista lista dos arquivos contidos num determinado diretório</summary>
        /// <param name="b">Caminho do diretório desejado</param>
        /// <returns>Stringlist com os caminhos dos arquivos existentes no diretório</returns>
        public static List<string> DirectoryGetFilesRecursive(string b)
        {
            // 1.
            // Store results in the file results list.
            List<string> result = new List<string>();

            // 2.
            // Store a stack of our directories.
            Stack<string> stack = new Stack<string>();

            // 3.
            // Add initial directory.
            stack.Push(b);

            // 4.
            // Continue while there are directories to process
            while (stack.Count > 0)
            {
                // A.
                // Get top directory
                string dir = stack.Pop();

                try
                {
                    // B
                    // Add all files at this directory to the result List.
                    result.AddRange(Directory.GetFiles(dir, "*.*"));

                    // C
                    // Add all directories at this directory.
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        stack.Push(dn);
                    }
                }
                catch
                {
                    // D
                    // Could not open the directory
                }
            }
            return result;
        }




        /// <summary>
        /// Extrai o nome de um arquivo do caminho informado
        /// </summary>
        /// <param name="pathAndFileName">Caminho do arquivo</param>
        /// <returns>Nome do arquivo sem o caminho completo</returns>
        public static string FileName(string pathAndFileName)
        {
            return pathAndFileName.Substring(pathAndFileName.LastIndexOf('\\') + 1);
        }

        /// <summary>
        /// Extrai o nome de um arquivo do caminho informado
        /// </summary>
        /// <param name="pathAndFileName">Caminho do arquivo ou nome do arquivo</param>
        /// <returns>Nome do arquivo sem o caminho completo</returns>
        public static string Extension(string pathAndFileName)
        {
            //if(
            int LastIndexOf = pathAndFileName.LastIndexOf('.');
            if (LastIndexOf > -1)
                return pathAndFileName.Substring(LastIndexOf);
            else
                return "";
        }

        /// <summary>
        /// Retira a extnsão do nome do arquivo
        /// </summary>
        /// <param name="FileName">Nome do arquivo</param>
        /// <returns>Nome do arquivo sem extensão</returns>
        public static string FileBaseName(string FileName)
        {
            int idx = FileName.IndexOf(@".");
            return FileName.Substring(0, idx);
        }

        /// <summary>
        /// Extrai o caminho do arquivo informado. Deve ser informado o caminho completo do arquivo
        /// </summary>
        /// <param name="pathAndFileName">Caminho completo do arquivo</param>
        /// <returns>Caminho do aquivo</returns>
        public static string FilePath(string pathAndFileName)
        {
            string file = FileName(pathAndFileName);
            if (file == pathAndFileName)    
                return "";
            return pathAndFileName.Remove(pathAndFileName.Length - file.Length - 1, file.Length + 1);
        }

        public static string MD5Hash(string input){

            FileStream fs = null;
            StringBuilder sb = null;
            try
            {
                sb = new StringBuilder();
                fs = new FileStream(input, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hash = md5.ComputeHash(fs);
                foreach (byte hex in hash)
                    sb.Append(hex.ToString("X2"));
            }
            catch
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
            return sb.ToString(); 
       }


        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(string srcFile)
        {
            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;
            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            FileStream file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer, 0, 5);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = Encoding.UTF7;
            return enc;
        }




        /// <summary>
        /// Try to detect the encoding
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
        public static bool TryToDetectEncoding(out Encoding encoding, string fileName, int windowSize)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                var rawData = new byte[windowSize];
                var text = new char[windowSize];
                var EncodingDetected = true;

                // Read raw bytes
                var rawLength = fileStream.Read(rawData, 0, rawData.Length);
                fileStream.Seek(0, SeekOrigin.Begin);

                // Detect encoding correctly (from Rick Strahl's blog)
                // http://www.west-wind.com/weblog/posts/2007/Nov/28/Detecting-Text-Encoding-for-StreamReader
                if (rawData[0] == 0xef && rawData[1] == 0xbb && rawData[2] == 0xbf){
                    encoding = Encoding.UTF8;
                } else if (rawData[0] == 0xff && rawData[1] == 0xfe){
                    encoding = Encoding.Unicode; // utf-16le
                } else if (rawData[0] == 0xfe && rawData[1] == 0xff) {
                    encoding = Encoding.BigEndianUnicode; // utf-16be
                } else if (rawData[0] == 0 && rawData[1] == 0 && rawData[2] == 0xfe && rawData[3] == 0xff) {
                    encoding = Encoding.UTF32;
                } else if (rawData[0] == 0x2b && rawData[1] == 0x2f && rawData[2] == 0x76) {
                    encoding = Encoding.UTF7;
                } else {
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

                        EncodingDetected = true;
                        // Compare only bytes read
                        for (var i = 0; i < rawLength; i++)
                        {
                            if (rawData[i] != memoryBuffer[i]) {
                                EncodingDetected = false;
                                encoding = null;
                                break;
                            }
                        }
                    }
                }

                //Try again using UTF8 without BOM :)
                if (!EncodingDetected) {
                    
                    using (var memoryStream = new MemoryStream()) {
                        using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8)) {
                            // Write the text to a buffer
                            streamWriter.Write(text);
                            streamWriter.Flush();

                            // Get the buffer from the memory stream for comparision
                            var memoryBuffer = memoryStream.GetBuffer();
                            EncodingDetected = true;
                            // Compare only bytes read
                            for (var i = 0; i < rawLength; i++) {
                                if (rawData[i] != memoryBuffer[i+3]) {
                                    EncodingDetected = false;
                                    encoding = null;
                                    break;
                                }
                            }
                        }
                    }
                    if (EncodingDetected)
                        encoding = Encoding.UTF8;
                }
                return EncodingDetected;
            }
        }


        //Very very slow! Better to not use it!
        public static Encoding EncodingDetect(string file) {

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


        /// <summary>
        /// Descompacta para o mesmo diretório que estiver o arquivo compactado. Deve ser passado o diretório
        /// onde estar o executável do RAR
        /// </summary>
        /// <param name="file">File to be decompressed</param>
        /// <param name="rarExecutableDir">Directory where lies the rar.exe</param>
        /// <returns></returns>
        public static bool RarFileDecompress(string file, string rarExecutableDir)
        {
            string application = rarExecutableDir + "rar.exe";
            string parameters = " e -y ";
            string arquivoSemPath, standardErrorMessage, standardOutputMessage;
            string filePath;

            if (!File.Exists(application))
            {
                throw new Exception("Fcn.RarFileDecompress(string file, string rarExecutableDir) com rar.exe errado");
            }

            arquivoSemPath = Fcn.FileName(file);
            filePath = Fcn.FilePath(file);
            int  errorCode = CommandLineExecute(application, parameters + " \"" + file + "\"", out standardOutputMessage, out standardErrorMessage, filePath);
            if (errorCode == 0)
                return true;
            else
                return false;
        }

        /// <summary>Executes shell commands</summary>
        /// <returns>Inteiro demonstrando o estado da saída do programa. Varia de acordo com o programa utilizado. Consulte documentação do programa que está chamando.</returns>
        /// <param name="program">Programa a ser chamado pela linha de comando</param>
        /// <param name="arguments">Conjunto de argumentos</param><param name="standardErrorMessage">Mensagem de erro</param>
        /// <param name="standardOutputMessage">Mensagem de saída</param><param name="workingDirectory">Diretório de trabalho</param>        
        public static int CommandLineExecute(string program, string arguments, out string standardOutputMessage, out string standardErrorMessage, string workingDirectory)
        {
            int exitCode;
            standardErrorMessage = "";
            standardOutputMessage = "";

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = program;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();

            standardErrorMessage = proc.StandardError.ReadToEnd();
            standardOutputMessage = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();
            exitCode = proc.ExitCode;
            proc.Close();

            return exitCode;
        }


        /// <summary>Executes shell commands in background</summary>
        public static void CommandLineExecuteInBackground(string program, string arguments, string workingDirectory)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = program;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
            proc.Close();
        }

        //TODO: Verificar a diferença entre este e o outro método
        /// <summary>
        /// Executes shell commands
        /// </summary>
        /// <returns>Inteiro demonstrando o estado da saída do programa. Varia de acordo com o programa utilizado. Consulte documentação do programa que está chamando.</returns>
        /// <param name="program">Programa a ser chamado pela linha de comando</param>
        /// <param name="arguments">Conjunto de argumentos</param><param name="standardErrorMessage">Mensagem de erro</param>
        /// <param name="standardOutputMessage">Mensagem de saída</param><param name="workingDirectory">Diretório de trabalho</param>
        public static int CommandLineVerboseExecute(string program, string arguments, out string standardOutputMessage, out string standardErrorMessage, string workingDirectory)
        {
            int exitCode;
            standardErrorMessage = "";
            standardOutputMessage = "";

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = program;
            proc.StartInfo.Arguments = arguments;
            
            proc.Start();
            
            proc.WaitForExit();
            exitCode = proc.ExitCode;
            proc.Close();
            
            return exitCode;
        }

        /// <summary>
        /// Executes shell commands deprecated
        /// </summary>
        /// <returns>Inteiro demonstrando o estado da saída do programa. Varia de acordo com o programa utilizado. Consulte documentação do programa que está chamando.</returns>
        /// <param name="program">Programa a ser chamado pela linha de comando</param>
        /// <param name="arguments">Conjunto de argumentos</param>
        /// <param name="workingDirectory">Diretório de trabalho</param>
        public static string CommandLineExecute(string program, string arguments, string workingDirectory)
        {
            //string msg = "";
            int exitCode;
            string outputMessage = "";

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = program;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError  = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.Verb = "runas";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();

            outputMessage = proc.StandardError.ReadToEnd();

            proc.WaitForExit();
            exitCode = proc.ExitCode;
            proc.Close();

            if (exitCode != 0)
            {
                outputMessage = "#Erro " + exitCode + " - " + outputMessage;
            }

            return outputMessage;
        }


        /// <summary>
        /// Executes shell commands deprecated
        /// </summary>
        /// <returns></returns>
        public static string CommandLineVerboseExecute(string program, string arguments, string workingDirectory)
        {
            //string msg = "";
            int exitCode;
            string outputMessage = "";

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = program;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();

            outputMessage = proc.StandardError.ReadToEnd();

            proc.WaitForExit();
            exitCode = proc.ExitCode;
            outputMessage = proc.StandardError.ReadToEnd();            
            proc.Close();

            if (exitCode != 0)
            {
                throw new Exception("#Erro " + exitCode + " - " + outputMessage);
            }
            return outputMessage;
        }

        /// <summary>
        /// Checks wheater the application is running with administrator rights
        /// </summary>
        /// <returns>
        /// True if the user has admin permission
        /// False if it has no
        /// </returns>
        public static Boolean AdministratorPermissionsCheck(String SystemDir, String PrgFilesDir)
        {
            try
            {
                File.Create(SystemDir + "a.txt",1,FileOptions.DeleteOnClose);                
                File.Create(PrgFilesDir + "a.txt", 1, FileOptions.DeleteOnClose);
                return true;
            }
            catch
            {
                return false;
            }          
        }

        public static string TimestampToPath(DateTime dt)
        {
            return dt.Year + "-" + dt.Month + "-" + dt.Day + "_" + dt.Hour + "_" + dt.Minute + 
                "_" + dt.Second;
        }
     
        /// <summary>
        /// Executes shell commands
        /// </summary>
        /// <returns></returns>
        public static void CommandExecute(
            string program, 
            string arguments, 
            string workingDirectory
            )
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = program;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
        }

        public static string CommandExecute(
            string program, 
            string arguments, 
            string workingDirectory, 
            int timeoutMinutes
            )
        {
            //string msg = "";

            StringBuilder output = new StringBuilder();

            ProcessStartInfo psi = new ProcessStartInfo();
            // Setup the ProcessStartInfo instance as needed.
            psi.UseShellExecute = true;
            psi.FileName = program;
            psi.Arguments = arguments;
            //psi.RedirectStandardOutput = true;
            //psi.RedirectStandardError = true;
            using (Process p = Process.Start(psi)) {
                output.AppendLine(p.StandardOutput.ReadToEnd()); 
                TimeSpan waitTime = TimeSpan.FromMinutes(2);
                if (!p.WaitForExit((int)waitTime.TotalMilliseconds))
                {
                    throw new Exception("I failed miserably.");
                }
            }
            return output.ToString();
       }

        public static void DirectoryCopy(string srcDir, string trgDir)
        {
            string[] allFiles = Directory.GetFiles(srcDir, "*.*", SearchOption.AllDirectories);
            foreach (string srcFile in allFiles)
            {
                string targetFile = trgDir + srcFile.Substring(srcDir.Length);
                string targetDir = Path.GetDirectoryName(targetFile);
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                File.Copy(srcFile, targetFile);
            }
        }

        /// <summary>
        /// Find if your application is already running (needs you application process name)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool MyApplicationIsAlreadyIsRunning(string name)
        {
            //here we're going to get a list of all running processes on
            //the computer
            int count = 0;
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes by using the StartsWith Method,
                //this prevents us from incluing the .EXE for the process we're looking for.
                //. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running
                if (clsProcess.ProcessName.ToLower() == name.ToLower())
                {
                    ++count;                    
                }
                if(count > 1)
                    return true;
            }
            //process not found, return false
            return false;
        }


        /// <summary>
        /// Find if there is a process running that starts with the same name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ProcessFind(string name)
        {
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes by using the StartsWith Method,
                //this prevents us from incluing the .EXE for the process we're looking for.
                //. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running
                if (clsProcess.ProcessName.ToLower() == name.ToLower())
                {
                    return true;
                }
            }
            //process not found, return false
            return false;
        }


        /// <summary>
        /// Find if there is a process running that starts with the same name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ProcessFindBeginingWith(string name)
        {
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes by using the StartsWith Method,
                //this prevents us from incluing the .EXE for the process we're looking for.
                //. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running
                if (clsProcess.ProcessName.ToLower().StartsWith(name.ToLower()))
                {
                    return true;   
                }
            }
            //process not found, return false
            return false;
        }

        /// <summary>
        /// Kill all the processes that matches the begining of the name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ProcessAllInstancesFindAndKillBeginingWith(string name)
        {
            bool ret = false;
            
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes by using the StartsWith Method,
                //this prevents us from incluing the .EXE for the process we're looking for.
                //. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running
                if (clsProcess.ProcessName.ToLower().StartsWith(name.ToLower()))
                {

                    //NAO APLICA-SE JÁ mudei o código

                    //since we found the proccess we now need to use the
                    //Kill Method to kill the process. Remember, if you have
                    //the process running more than once, say IE open 4
                    //times the loop thr way it is now will close all 4,
                    //if you want it to just close the first one it finds
                    //then add a return; after the Kill
                    clsProcess.Kill();
                    //process killed, return true
                    ret = true;
                }
            }
            //process not found, return false
            return ret;
        }

        /// <summary>
        /// Kill all the processes that matches the begining of the name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ProcessAllInstancesFindAndKill(string name)
        {
            bool ret = false;

            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes by using the StartsWith Method,
                //this prevents us from incluing the .EXE for the process we're looking for.
                //. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running
                if (clsProcess.ProcessName.ToLower() == name.ToLower())
                {

                    //NAO APLICA-SE JÁ mudei o código

                    //since we found the proccess we now need to use the
                    //Kill Method to kill the process. Remember, if you have
                    //the process running more than once, say IE open 4
                    //times the loop thr way it is now will close all 4,
                    //if you want it to just close the first one it finds
                    //then add a return; after the Kill
                    clsProcess.Kill();
                    //process killed, return true
                    ret = true;
                }
            }
            //process not found, return false
            return ret;
        }

    }
}