using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SirJoshSpeedyComputer {
	public static class Updater {
		public static WebClient downloader = new WebClient() { Proxy = null };
		public const string BaseURL = "http://www.sirjosh3917.com/apps/speedycomputer/";
		public const string UpdateExe = "updatedSpeedyComputer.exe";

		public static void FetchAvailability(string baseurl) {
			try {
				if (downloader.DownloadData(baseurl + "needdownload.php?id=" + (UpdateConfig.UpdateString))[0] == 97) {
					downloader.DownloadFile(baseurl + "exedownload.php?id=" + (UpdateConfig.UpdateString), UpdateExe);
					if (File.ReadAllText(UpdateExe) != "NUA") {
						MessageBox.Show("An update has been downloaded. To proceed, please close this box.");

						//start new cmd.exe process to delete the old and rename the new
						ProcessStartInfo Info = new ProcessStartInfo();
						Info.Arguments = "/C choice /C Y /N /D Y /T 2 & Del \"" + 
System.AppDomain.CurrentDomain.FriendlyName + "\"& Rename " + UpdateExe + " SirJoshSpeedyComputer.exe & pause";
						Info.WindowStyle = ProcessWindowStyle.Hidden;
						Info.CreateNoWindow = true;
						Info.FileName = "cmd.exe";
						Process.Start(Info);
						Application.Exit();
					} else {
						File.Delete(UpdateExe);
					}
				} else {
					MessageBox.Show("No updates available.");
				}
			} catch {
				//Just plain quit if any errors
				MessageBox.Show("Error occured while updating.");
			}
		}

		private static char[] _byte2char(byte[] l) {
			string chars = "";
			foreach(var i in l) {
				chars += Convert.ToChar(i).ToString();
			}
			return chars.ToCharArray();
		}
	}
}
