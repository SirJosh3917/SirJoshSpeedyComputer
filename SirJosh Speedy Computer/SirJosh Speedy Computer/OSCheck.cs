using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SirJoshSpeedyComputer {
	public enum OS {
		NotApplicable = 0,
		Windows7 = 1,
		Windows10 = 2
	}

	public static class OSCheck {
		public static bool VerMajor(int ver) {
			return Environment.OSVersion.Version.Major == ver;
		}

		public static bool VerMinor(int ver) {
			return Environment.OSVersion.Version.Minor == ver;
		}

		public static bool IsWindowsNT { get { return Environment.OSVersion.Platform == PlatformID.Win32NT; } set { } }

		public static bool IsOSMajMin(int major, int minor) {
			return IsWindowsNT && VerMajor(major) && VerMinor(minor);
		}

		public static bool IsWindows7() {
			return IsOSMajMin(6, 1);
		}

		public static bool IsWindows10() {
			return IsOSMajMin(10, 0) || IsOSMajMin(6, 2);
		}

		public static bool OSSupported {
			get {
				return IsWindows7() || IsWindows10();
			}
			set { }
		}

		public static OS GetOS() {
			if (OSSupported) {
				if (IsWindows7()) {
					return OS.Windows7;
				}

				if (IsWindows10()) {
					return OS.Windows10;
				}
			}

			return OS.NotApplicable;
		}

		public static bool ShowOSSupportMessage() {
			if (!OSCheck.OSSupported) {
				System.Windows.Forms.MessageBox.Show("Your OS is not supported.", "OS Not Supported.");
				return false;
			}
			return true;
		}
	}
}
