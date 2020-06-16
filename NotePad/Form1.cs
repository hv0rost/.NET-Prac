using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace WordPadMin
{
    public partial class Form1 : Form
    {
       // private FileData data = new FileData();
    
        public Form1()
        {
            InitializeComponent();
            InitHandlers();
            FileChanged += UpdateFilePath;
            saveDialog.Filter = "Text files(*.txt)| *.txt | RTF(*.rtf) | *.rtf | FB2(*.fb2) | *.fb2 | DOCX(*.docx) | *.docx|All files(*.*)|*.*";
            openDialog.Filter = "Text files(*.txt)| *.txt | RTF(*.rtf) | *.rtf | FB2(*.fb2) | *.fb2 | DOCX(*.docx) | *.docx|All files(*.*)|*.*";
        }

        private void UpdateFilePath(object sender, string args)
        {
            this.Text = args;
        }

        private void InitHandlers()
        {
         ContentUpdate += OnContentUpdate;
        }

        private void OnContentUpdate(object sender, string args)
        {
            richTextBox1.Text = args;
        }

        private FileInfo currentFile = null;
        private FileInfo CurrentFile
        {
            get { return currentFile; }
            set
            {
                currentFile = value;
                FileChanged.Invoke(this, currentFile.FullName);
            }
        }
        private string content;
        private SaveFileDialog saveDialog = new SaveFileDialog();
        private OpenFileDialog openDialog = new OpenFileDialog();
        public RichTextBox richText = new RichTextBox();

        //private EventHandler<string> ContentLoaded;
        public EventHandler<string> ContentUpdate;
        public EventHandler<string> FileChanged;

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                ContentUpdate.Invoke(this, content);
            }
        }

           private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {   openDialog.Filter = "Text files(*.txt)| *.txt | RTF(*.rtf) | *.rtf | FB2(*.fb2) | *.fb2 | DOCX(*.docx) | *.docx|All files(*.*)|*.*";
            openDialog.AddExtension = true;
            openDialog.RestoreDirectory = true;
            openDialog.FilterIndex = 5;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ResetText();
                CurrentFile = new FileInfo(openDialog.FileName);
                
               StreamReader  sr = currentFile.OpenText();
               
                string line = sr.ReadLine();
                sr.Close();
                sr = currentFile.OpenText();
                if (line.StartsWith("{\\rtf"))
                {
                    richTextBox1.Rtf = sr.ReadToEnd();
                }
                else
                {
                    Content = sr.ReadToEnd();
                }

                content = richTextBox1.Rtf;
                sr.Close();
            }
        }

        private void SaveAs(object sender, EventArgs e)
        {
            saveDialog.Filter = "All files(*.*)|*.*| RTF(*.rtf) | *.rtf | FB2(*.fb2) | *.fb2 | DOCX(*.docx) | *.docx| Text files(*.txt) | *.txt";
            saveDialog.AddExtension = true;
            saveDialog.FilterIndex = 5;
            saveDialog.RestoreDirectory = true;
                                        
            Stream stream;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveDialog.OpenFile()) != null)
                {
                    stream.Close();
                    CurrentFile = new FileInfo(saveDialog.FileName);
                    Save(sender,e);
                }
            }     
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите очистить файл?", "Очистка файла", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                Content = "";
            }
            else
            {
                return;
            }
        }

  
        private void Save(object sender, EventArgs e)
        {
           if (currentFile == null || !currentFile.Exists)
            {
                SaveAs(sender,e);
                return;
            }
            StreamWriter writer = currentFile.CreateText();
            if (currentFile.Extension.ToUpper() == ".RTF")
            {
                writer.Write(richTextBox1.Rtf);
           
            }
            else
            {
                writer.Write(richTextBox1.Text);
              
            }
            writer.Close();
            
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            clearToolStripMenuItem_Click(sender, e);
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            Save(sender, e);
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            SaveAs(sender, e);
        }

        private void PrintPageHandler(object sender, EventArgs e)
        {

            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += PrintPageHandler;

            PrintDialog printDialog = new PrintDialog();


            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print();
        }

        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(Content, new Font("Microsoft Sans Serif", 14), Brushes.Black, 0, 0);
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void ToolStripButton7_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void ШрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                richTextBox1.Font = fd.Font;
        }

    

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ActiveControl.Text.Trim() !=  "") 
            {
                DialogResult result = MessageBox.Show("Вы хотите сохранить изменение в файле?", "Блокнот С#", 
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Save(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }

}





