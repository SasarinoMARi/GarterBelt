using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarterBelt
{
	public class Garterbelts : ObservableCollection<GarterProcesses> { }

	public class GarterProcesses
	{
		public string Name { get; private set; }
		public int ProcessId { get; private set; }
		public int MainWindowHandle { get; private set; }

		public GarterProcesses(Process p)
		{
			this.Name = p.ProcessName;
			this.ProcessId = p.Id;
			this.MainWindowHandle = p.MainWindowHandle.ToInt32();
		}

		private GarterProcesses(string name, string procId, string hWnd)
		{
			this.Name = name;
			this.ProcessId = int.Parse(procId);
			this.MainWindowHandle = int.Parse(hWnd);
		}

		public static GarterProcesses LoadFromLine(string s) {
			var cols = s.Split(' ');
			if (cols.Length != 3) return null;
			return new GarterProcesses(cols[0], cols[1], cols[2]);
		}

		public override string ToString()
		{
			return this.Name + ' ' + this.ProcessId + ' ' + this.MainWindowHandle;
		}
	}
}
