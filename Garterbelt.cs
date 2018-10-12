using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace GarterBelt
{
    public class ObservableGarterbelt : ObservableCollection<Garterbelt>
    {
    }

    public class Garterbelt
    {
        public string Name { get; set; }
        public List<GarterProcess> Processes { get; set; } = new List<GarterProcess>();

        public Garterbelt() {}

        public Garterbelt(Process p) : this()
        {
            Name = p.ProcessName;
            AttatchGarterbelt(p);
        }

        public void AttatchGarterbelt(Process p)
        {
            Processes.Add(new GarterProcess(p));
        }

        public override string ToString()
        {
            return Name;
        }

        public bool ContainProcess(Process p)
        {
            foreach (var x in Processes)
            {
                if (x.ProcessId == p.Id)
                {
                    return true;
                }
            }
            return false;
        }

        #region Object Serialize
        
        public static void SerializeObject(List<Garterbelt> list, string path)
        {
            var json = new JavaScriptSerializer().Serialize(list);
            File.WriteAllText(path, json);
            Console.WriteLine(json);
        }

        public static List<Garterbelt> DeserializeObject(string path)
        {
            List<Garterbelt> list = null;
            var json = File.ReadAllText(path);
            Console.WriteLine(json);
            try
            {
                list = new JavaScriptSerializer().Deserialize<List<Garterbelt>>(json);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            return list;
        }
        #endregion
    }

    public class GarterProcess
    {
        public int ProcessId { get; set; }
        public int MainWindowHandle { get; set; }

        public GarterProcess() { }

        public GarterProcess(int ProcessId, int MainWindowHandle)
        {
            this.ProcessId = ProcessId;
            this.MainWindowHandle = MainWindowHandle;
        }

        public GarterProcess(Process proc) : this(proc.Id, proc.MainWindowHandle.ToInt32()) { }
    }
}
