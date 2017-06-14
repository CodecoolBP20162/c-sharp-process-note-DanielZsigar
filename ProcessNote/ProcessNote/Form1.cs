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
        
        // on load fill the itemBox1 with processes (ID and Name)
        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllProcesses();
         }

        // on click reload the process in the listBox1.
        private void processesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetAllProcesses();
        }

        // on click reload the threads in the listBox1.
        private void threadsOfProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetThreads();
        }
        
        // get all the processes and make their id and name visible in the listBox1.
        private void GetAllProcesses()
        {
            listBox1.Items.Clear();
            foreach (Process p in Process.GetProcesses())
            {
                listBox1.Items.Add(p.Id + " " + p.ProcessName);
            }
        }

        // get all the threads and make their id and name visible in the listBox1.
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

        

       // get and return the ID of the selected item from the itemBox1
        private int returnSelectedItemID()
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            String[] elements = Regex.Split(selectedItem, " ");
            int id = Int32.Parse(elements[0]);
            return id;
        }

        
        // on click the selected item's properties become visible in the labels (id,name etc.)
        private void listBox1_Click(object sender, EventArgs e)
        {
            int id = returnSelectedItemID();

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

        // on click saves the text in the area into a .txt file and clears the area
        private void button1_Click(object sender, EventArgs e)
        {
                string path = @"c:\Users\Dániel\Documents\MyTest.txt";
                string comment = richTextBox1.Text;
                int id = returnSelectedItemID();
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

