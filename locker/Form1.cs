using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace locker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists("secretfolder1"))
            {
                DirectoryInfo di = Directory.CreateDirectory("secretfolder1");
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
           textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Start();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Location = Cursor.Position;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] passwords = File.ReadAllLines("resetpass.txt");
            if (textBox1.Text == passwords[passwords.Length - 1])
            {
                checkBox1.Visible = false;
                button1.Visible = false;
                button3.Visible = false;
                button4.Visible = true;
                linkLabel1.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = true;
                label1.Visible = false;
                button5.Visible = true;
                textBox2.PasswordChar = '\0';
                label3.Visible = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = openFileDialog1.FileName;
                }
            }
            else
            {
                label2.Text = "enter password to add new";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
                if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("enter proper file path", "file path error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (DialogResult.Equals(DialogResult.OK))
                    {
                        textBox2.Text = String.Empty;
                    }
                }
                else
                {
                    if (!File.Exists("paths.txt"))
                    {
                        File.Create("paths.txt");
                    }
                    else
                    {
                        string[] names = File.ReadAllLines("paths.txt");
                        if (names.Contains(textBox2.Text))
                        {
                            MessageBox.Show("no duplication allowed.change name instead \n and try again", "no duplicates allowed!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            try
                            {
                                StreamWriter file0 = File.AppendText("paths.txt");
                                file0.WriteLine(textBox2.Text);
                                file0.Close();
                                //add file process here
                                File.Copy(textBox2.Text, @"secretfolder1\\" + Path.GetFileName(textBox2.Text));
                                File.Delete(textBox2.Text);
                                textBox2.Text = string.Empty;
                            }
                            catch
                            {
                                MessageBox.Show("There seems to be an error in the path you entered\n please check.", "invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                button3.Visible = true;
                button4.Visible = false;
                button1.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = false;
                label1.Visible = true;
                linkLabel1.Visible = true;
                button5.Visible = false;
                textBox2.PasswordChar = '*';
                label3.Visible = false;
                checkBox1.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(textBox1.Text=="lockeradminlocker")
            {
                textBox2.Text=String.Empty;
                textBox1.Visible = false;
                textBox2.Visible = true;
                button3.Visible = false;
                button1.Text = "change";
                textBox1.Text=String.Empty;
            }
            else
            {
                label2.Visible = true;
                label2.Text = "check the password entered";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text!=string.Empty)
            {
                StreamWriter file3 = File.AppendText("resetpass.txt");
                file3.WriteLine(textBox2.Text);
                file3.Close();
                label2.Text = "successfully entered";
                textBox1.Visible = true;
                textBox2.Visible = false;
                button3.Visible = true;
                button1.Text = "Get access";
            }
            if (File.Exists("resetpass.txt"))
            {
                string[] passwords = File.ReadAllLines("resetpass.txt");
                if (textBox1.Text == passwords[passwords.Length - 1])
                {
                    System.Diagnostics.Process.Start("explorer.exe", "secretfolder1");
                    this.Close();
                }
                else
                {
                    label2.Visible = true;
                    label2.Text = "check the password entered";
                }
            }
            else
            {
                MessageBox.Show("you seem to be a new user\n please set password ", "new user?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }
    }
}
