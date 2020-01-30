using System;
using System.IO;
using System.Text;

namespace main
{
    interface IReadableObject { }
    interface ILoader
    {
        IReadableObject Load(string[] text);
    }
    interface ILoadManager
    {
        IReadableObject Read(ILoader text);
    }

    class LoadManager: ILoadManager
    {
        string file;
        StreamReader input;

        public LoadManager(string filename)
        {
            file = filename;
            input = null;
        }

        public void BeginRead()
        {
            if (input != null)
                throw new IOException("Load Error");

            input = new StreamReader(file);
        }
        public bool IsLoading
        {
            get { return input != null && !input.EndOfStream; }
        }
        public void EndRead()
        {
            if (input == null)
                throw new IOException("Load Error");

            input.Close();
        }

        public IReadableObject Read(ILoader loader)
        {
            StringBuilder str = new StringBuilder();
            string line = input.ReadLine();
            while (!line.StartsWith("=") && !input.EndOfStream)
            {
                str.AppendLine(line);
                line = input.ReadLine();
            }
            string[] text = str.ToString().Split('\n');

            return loader.Load(text);
        }
    }
}