using System;
using System.IO;
using System.Text;

namespace main

{
    interface IWritableObject
    {
        void Write(SaveManager info);
    }

    class SaveManager
    {
        FileInfo file;

        public SaveManager(string filename)
        {
            file = new FileInfo(filename);
            file.CreateText().Close();
        }

        public void WriteInfo(string line)
        {
            var output = new StreamWriter("info.txt", true, Encoding.GetEncoding(1251));
            output.WriteLine(line);
            output.Close();
        }

        public void WriteObject(IWritableObject obj)
        {
            obj.Write(this);
        }
    }
}
