using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.button1_Click);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Txt Files|*.txt";
            openFileDialog1.Title = "Select a txt File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = System.IO.File.ReadAllText(textBox1.Text);
            string[] b = text.Split('\n');
            string searchSSID = "HDA Codec Subsystem ID Verb-table";
            string searchVerbTable = ";===== Pin Widget Verb-table =====";
            List<string> newFile = new List<string>();
            for (int i=0 ; i<b.Length ; i++)
            {
                if (b[i].IndexOf(searchSSID) != -1)
                {
                    newFile.Add(b[i + 1].Replace(";", "//"));
                    for (int x = 0 ; x < 7;x++ )
                    {
                        newFile.Add(b[i + x].Replace("dd ", "0x").Replace("h", ""));
                    }                    
                }

                if (b[i].IndexOf(searchVerbTable) != -1)
                {
                    try
                    {
                        while (i < b.Length)
                        {
                            newFile.Add(b[i + 2].Replace("dd ", "0x").Replace("DD ", "0x").Replace("h", "").Replace(";", "//"));
                            i++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                }
            }
            System.IO.File.WriteAllLines(@"c:\text.txt", newFile);

            System.Console.WriteLine("Contents of WriteText.txt = {0}", b.Length);
        }
    }
}
