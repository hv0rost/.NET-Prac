using System;
using System.IO;

namespace main.Properties

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
            file.CreateText();
        }

        public void WriteInfo(string line)
        {

        }

        public void WriteObject(IWritableObject obj)
        {
            obj.Write(this);
        }
    }
}
