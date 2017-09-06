using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SirJoshSpeedyComputer {
	public static class ProccessKiller {

		#region Default Processes
		#region Windows 7
		public static string[] Windows7DefaultProcess { get {
				return @"SirJoshSpeedyComputer'taskmgr'WmiPrvSE'winlogon'svchost'mscorsvw'dllhost'VSSVC'dwm'explorer'sppsvc'wininit'spoolsv'lsm'audiodg'smss'lsass'TrustedInstaller'csrss'services'System'Idle".Split('\'');
			}
		}
		#endregion
		#region Windows 10
		public static string[] Windows10DefaultProcess {
			get {
				return @"SirJoshSpeedyComputer'taskmgr'svchost'csrss'ShellExperienceHost'SearchFilterHost'sihost'lsass'SearchProtocolHost'SearchIndexer'audiodg'wininit'services'dllhost'SearchUI'TiWorker'MpCmdRun'explorer'NisSrv'sppsvc'RuntimeBroker'winlogon'VSSVC'TrustedInstaller'dwm'spoolsv'taskhostw'MsMpEng'WmiPrvSE'smss'System'Idle'sihost".Split('\'');
			}
		}
		#endregion
		public static string[] NotApplicableProcessList = new string[1] { "n/a" };

		public static string[] GetDefaultProcessesListAccordingToOs(OS ver) {
			switch (ver) {
				case OS.Windows7:
				return Windows7DefaultProcess;

				case OS.Windows10:
				return Windows10DefaultProcess;

				case OS.NotApplicable:
				default:
				return NotApplicableProcessList;
			}
		}

		public static string[] AutoGetDefaultList() {
			return GetDefaultProcessesListAccordingToOs(OSCheck.GetOS());
		}
		#endregion

		public static Process[] GetProcesses() {
			return Process.GetProcesses();
		}

		public static bool IsOkToKill(string processname, string[] processlist) {
			for (int i = 0; i < processlist.Length; i++)
				if (processname == processlist[i])
					return false;
			return true;
		}

		public static void KillProcesses(Process[] processes, string[] killcheck) {
			if (killcheck == NotApplicableProcessList)
				return;

			List<string> procsKilled = new List<string>();

			for (int i = 0; i < processes.Length; i++) {
				bool kill = true;
				for (int j = 0; j < killcheck.Length; j++)
					if (processes[i].ProcessName == killcheck[j]) { kill = false; break; } else kill = true;
				if (kill) {
					try {
						procsKilled.Add(processes[i].ProcessName);
						processes[i].Kill();
					} catch { }
				}
			}

			if (System.Windows.Forms.MessageBox.Show("Would you like a report of all the processes killed?", "Report", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes) {
				StringBuilder b = new StringBuilder();
				for(int i = 0; i < procsKilled.Count; i++) {
					b.Append(procsKilled[i]);
					b.Append("\r\n");
				}

				System.Windows.Forms.MessageBox.Show(b.ToString(), "Report", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1);
			}
		}

		public static void AutoKillProcesses() {
			KillProcesses(GetProcesses(), AutoGetDefaultList());
		}
	}
}
