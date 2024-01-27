using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Work6_6
{
    public class DataStorage
    {
        private static string fileName = "employees.dat";
        private static readonly DataStorage _instance = new DataStorage();
        private FileStream fileHandle;
        private StreamWriter streamWrite; 
        private StreamReader streamRead;
        public static DataStorage getInstance()
        {
            return _instance;
        }
        private DataStorage()
        {
            fileHandle = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamWrite = new StreamWriter(fileHandle);
            streamRead = new StreamReader(fileHandle);
        }

        ~DataStorage()
        {
            streamWrite.Close();
            streamRead.Close();
            fileHandle.Close();
        }

        public void ClearData()
        {
            fileHandle.SetLength(0);
        }
        
        public void ResetFilePointer()
        {
            fileHandle.Seek(0, SeekOrigin.Begin);
        }
        public void AddData(string data)
        {
            fileHandle.Seek(0, SeekOrigin.End);
            streamWrite.WriteLine(data);
            streamWrite.Flush();
            ResetFilePointer();
        }

        public string GetNextLine()
        {
            return streamRead.ReadLine();
        }

        public string GetLastLine()
        {
            Regex checkLetter = new Regex(@"[\w\d\p{P} ]");
            StringBuilder lastLine = new StringBuilder();
            for(int i = 0; i < fileHandle.Length; i++)
            {
                fileHandle.Seek(-i, SeekOrigin.End);
                int readByte = fileHandle.ReadByte();
                if (checkLetter.IsMatch(((char) readByte).ToString()))
                    lastLine.Insert(0, (char)readByte);
                else if (lastLine.Length > 0) 
                    break;
            }
            ResetFilePointer();
            return lastLine.ToString();
        }
    }
}