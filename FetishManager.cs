using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GarterBelt
{
    class FetishManager
    {
        public List<Garterbelt> get(string processName = null)
        {
            var garters = LoadFetish();
            if (processName == null) return garters;

            processName = processName.ToLower();
            var processRunning = Process.GetProcesses();
            foreach (Process p in processRunning)
            {
                if (p.MainWindowHandle.ToInt32() == 0) continue;
                if (p.ProcessName.ToLower().Contains(processName))
                {
                    if (garters.Any(x => x.ContainProcess(p)))
                    {
                        continue;
                    }
                    else
                    {
                        var query = garters.Where(x => x.Name == p.ProcessName);
                        if (query.Count() > 0)
                        {
                            query.First().AttatchGarterbelt(p);
                        }
                        else
                        {
                            var g = new Garterbelt(p);
                            garters.Add(g);
                        }
                    }
                }
            }

            SaveFetish(garters);
            return garters;
        }

        public Garterbelt find(string processName)
        {
            foreach (var garterbelt in get(processName))
            {
                if (garterbelt.Name.ToLower().Contains(
                    processName.ToLower())) return garterbelt;
            }
            return null;
        }

        public void remove(Garterbelt garter)
        {
            var garters = LoadFetish();
            garters.RemoveAll(g => g.Name == garter.Name);
            SaveFetish(garters);
        }

        public void clear() {
            SaveFetish(new List<Garterbelt>());
        }

        #region File I/O 

        private static readonly string saveDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetEntryAssembly().GetName().Name);
        private static readonly string savePath = Path.Combine(saveDir, "handles");

        private List<Garterbelt> LoadFetish()
        {
            var garters = new List<Garterbelt>();
            if (!File.Exists(savePath)) return garters;

            var saved = Garterbelt.DeserializeObject(savePath);
            if (saved == null || saved.Count == 0) return garters;

            foreach (var garter in saved)
            {
                if (!garter.IsValidate()) continue;
                //if (garters.Any(x => x.ContainProcess(garter) == garter.ProcessId && x.Name == garter.Name)) continue;
                garters.Add(garter);
            }
            return garters;
        }

        private void SaveFetish(List<Garterbelt> garters)
        {
            Garterbelt.SerializeObject(garters, savePath);
        }

        #endregion

        #region Class instance

        private FetishManager()
        {
            if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
        }

        public static readonly FetishManager Instance = new FetishManager();

        #endregion
    }
}