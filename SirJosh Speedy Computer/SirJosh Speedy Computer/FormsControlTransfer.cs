using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SirJoshSpeedyComputer;
using System.Windows.Forms;

namespace SirJoshSpeedyComputer.Forms {
	public static class FCT {
		#region Buttons
		public static void KillProcesses(object sender, EventArgs e) {
			if(OSCheck.ShowOSSupportMessage())
				ProccessKiller.AutoKillProcesses();
		}

		public static void Update(object sender, EventArgs e) {
			Updater.FetchAvailability(Updater.BaseURL);
		}
		#endregion
	}
}