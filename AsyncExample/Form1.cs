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
using System.Threading;

namespace AsyncExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int processFile()
        {
            int count = 0;
            using (StreamReader reader = new StreamReader("c:\\Data\\Data.txt"))
            {
                string content = reader.ReadToEnd();

                count = content.Length;
                Thread.Sleep(5000);
            }
            
            return count;
        }
        //private async void btnProcessFile_Click(object sender, EventArgs e)
        //{
        //    Task<int> task = new Task<int>(processFile);
        //    task.Start();
        //    lblCount.Text = "Please wait, file is processing.";
        //    int count = await task;
        //    lblCount.Text = count.ToString() + " charracters in file";
        //}

        //above async await can be replaced and similar functionality can be achived using thread like below.

        int charcount = 0;
        private void btnProcessFile_Click(object sender, EventArgs e)
        {
           
            Thread thread = new Thread(()=> {
                
                charcount = processFile();
                Action action = new Action(Setcharratercount); ///it's delegate

                this.BeginInvoke(action); //calling a main thread to invoke setcharcount method to set count value on label

            });
            thread.Start();
            lblCount.Text = "Please wait, file is processing.";

           
           
        }

        private void Setcharratercount()
        {
            lblCount.Text = charcount.ToString() + " charracters in file";
        }
    }
}
