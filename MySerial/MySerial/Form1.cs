using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace MySerial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
            serialPort1.WriteTimeout = 500;
            serialPort1.ReadTimeout = 500;
        }
        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBox1.Text == "" || comboBox2.Text == "")
                {
                    textBox2.Text = "Please select port settings";
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Open();
                    progressBar1.Value = 100;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    textBox1.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = true;
                }

            }

            catch (UnauthorizedAccessException)
            {

                textBox2.Text = "UnauthorizedAccess";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = true;
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int bufsize;
            
            try
            {
                serialPort1.Write(textBox1.Text);
                //textBox1.Text = "";
                bufsize = serialPort1.ReadBufferSize;
                textBox3.Text = bufsize.ToString();

                textBox3.Text = serialPort1.BytesToRead.ToString();
            }
            catch (InvalidOperationException)
            {
                textBox1.Text = "Port Not Open";

            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = new byte[10];
                //balance[0] ='01';
                // textBox2.Text = serialPort1.ReadLine();
                serialPort1.Read(buffer,0,2);
                string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                textBox2.Text = converted;

            }
            catch(TimeoutException)
            {
                InitializeComponent();
                textBox2.Text = "Timeout Eception";

            }
            




        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
