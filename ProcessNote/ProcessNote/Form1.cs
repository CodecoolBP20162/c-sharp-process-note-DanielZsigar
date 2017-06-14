using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

namespace ProcessNote
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        
        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllProcesses();
            

        }
 

        private void threadsOfProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            GetThreads();

        }
        
        private void processesToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            GetAllProcesses();
        }

   

        private void GetAllProcesses()
        {
            listBox1.Items.Clear();
            foreach (Process p in Process.GetProcesses())
            {
                listBox1.Items.Add(p.Id + " " + p.ProcessName);
            }
            
        }

        private void GetThreads()
        {
            listBox1.Items.Clear();
            foreach (Process p in Process.GetProcesses())
            {
                listBox1.Items.Add(p.Id +" " + p.Threads);
            }

            try { foreach(Process p in Process.GetProcesses())
                {
                    listBox1.Items.Add(p.ProcessName + " " + p.Threads);
                    
                }
            }
            catch (Win32Exception e)
            {
                MessageBox.Show("Access denied, error message: ");
                MessageBox.Show(e.ToString());
            }
        
        }

        private void processesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetAllProcesses();
        }

       
        private int selecetedId()
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            String[] elements = Regex.Split(selectedItem, " ");
            int id = Int32.Parse(elements[0]);
            return id;
        }

        
        
        private void listBox1_Click(object sender, EventArgs e)
        {
            int id = selecetedId();

            try
            {
                Process p = Process.GetProcessById(id);

                label1.Text = "ID: "+ p.Id.ToString();
                label2.Text = "Name: "+ p.ProcessName.ToString();
                //label3.Text = process_cpu_usage.ToString();
                label4.Text = "Memory usage: "+p.PagedMemorySize64.ToString();
                label5.Text = "Running time: "+p.TotalProcessorTime.ToString();
                label6.Text = "Start time: "+p.StartTime.ToString();

            }
            catch
            {
                label5.Text = "Access denied!";

            }
        }


        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }
                
            

        private void button1_Click(object sender, EventArgs e)
        {
                string path = @"c:\Users\Dániel\Documents\MyTest.txt";
                string comment = richTextBox1.Text;
                int id = selecetedId();
                Process p = Process.GetProcessById(id);

                string text = "Process ID: " + p.Id + Environment.NewLine + comment + comment + Environment.NewLine + Environment.NewLine;




                if (!File.Exists(path))
                {

                    File.WriteAllText(path, text);
                }
                else
                {

                    File.AppendAllText(path, text);

                }
                richTextBox1.Clear();
            }
    }
}

