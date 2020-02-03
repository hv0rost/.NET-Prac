﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    class Logger
    {
        TextWriter output;
        public Logger(TextWriter output)
        {
            this.output = output;
        }
        public virtual void WriteLine(string message)
        {
            output.WriteLine(message);
        }
    }

    class DatedLogger : Logger
    {
        public DatedLogger(TextWriter output) : base(output) { }
        public override void WriteLine(string message)
        {
            base.WriteLine($"{DateTime.Now} {message}");
        }
    }

    class LoadLogger
    {
        LoadManager man;
        Logger log;

        public LoadLogger(LoadManager man, Logger log)
        {
            this.man = man;
            this.log = log;
            man.ObjectDidLoad += OnObjectLoad;
            man.DidStartLoad += OnDidStartLoad;
            man.DidEndLoad += OnDidEndLoad;
        }
        protected virtual void OnObjectLoad(object sender, IReadableObject obj)
        {
            log.WriteLine($"Объект успешно загружен: {obj}");
        }
        protected virtual void OnDidStartLoad(object sender, string file)
        {
            log.WriteLine($"Начата загрузка из файла: {file}");
        }
        protected virtual void OnDidEndLoad(object sender, string file)
        {
            log.WriteLine($"Завершена загрузка из файла: {file}");
        }
        ~LoadLogger()
        {
            man.ObjectDidLoad -= OnObjectLoad;
            man.DidStartLoad -= OnDidStartLoad;
            man.DidEndLoad -= OnDidStartLoad;
        }
    }
}
