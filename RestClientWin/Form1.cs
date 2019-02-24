using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestClient;

namespace RestClientWin
{
    public partial class Form1 : Form
    {
        Stopwatch sw = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void FrtPutFullButton_Click(object sender, EventArgs e)
        {
            //RestClient.SendWithGrpc.FrtSend("F");
            //RestClient.SendWithGrpc.Frt2Send().Wait();
            //RestClient.SendWithGrpc send = new RestClient.SendWithGrpc();
            //send.Frt2Send().Wait();

            sw.Start();
            var nor = SendWithGrpc.FrtSend("F");
            sw.Stop();
            label1.Text = $"{nor:n0} records sent in {sw.ElapsedMilliseconds:n0} ms";

        }

        private void OpmPutFullButton_Click(object sender, EventArgs e)
        {
            sw.Start();
            var nor = SendWithGrpc.OpmSend("F");
            sw.Stop();
            label2.Text = $"{nor:n0} records sent in {sw.ElapsedMilliseconds:n0} ms";
        }

        private void OphPutFullButton_Click(object sender, EventArgs e)
        {
            sw.Start();
            var nor = SendWithGrpc.OphSend("F");
            sw.Stop();
            label3.Text = $"{nor:n0} records sent in {sw.ElapsedMilliseconds:n0} ms";
        }
    }
}
