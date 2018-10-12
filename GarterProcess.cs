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
    public class Garterbelts : ObservableCollection<GarterProcess>
    {
    }

    public class GarterProcess
    {
        public string Name { get; set; }
        public int ProcessId { get; set; }
        public int MainWindowHandle { get; set; }

        public GarterProcess()
        {
        }

        public GarterProcess(Process p)
        {
            Name = p.ProcessName;
            ProcessId = p.Id;
            MainWindowHandle = p.MainWindowHandle.ToInt32();
        }

        public override string ToString()
        {
            return Name;
        }

        #region Object Serialize
        
        public static void SerializeObject(List<GarterProcess> list, string path)
        {
            var json = new JavaScriptSerializer().Serialize(list);
            File.WriteAllText(path, json);
            Console.WriteLine(json);
        }

        public static List<GarterProcess> DeserializeObject(string path)
        {
            List<GarterProcess> list = null;
            var json = File.ReadAllText(path);
            Console.WriteLine(json);
            try
            {
                list = new JavaScriptSerializer().Deserialize<List<GarterProcess>>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return list;
        }
        #endregion
    }
}
