using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace SirJoshSpeedyComputer.Forms {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			FCT.KillProcesses(sender, e);
		}

		private void updateToolStripMenuItem_Click(object sender, EventArgs e) {
			FCT.Update(sender, e);
		}
	}
}
