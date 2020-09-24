using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace GarterBelt
{
    class ShortCut
    {
        private static readonly string saveDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetEntryAssembly().GetName().Name);
        private static readonly string savePath = Path.Combine(saveDir, "shortcuts");

        public static void SaveShortCuts(List<Tuple<int, Keys>> shortcuts)
        {
            if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
            var json = new JavaScriptSerializer().Serialize(shortcuts);
            File.WriteAllText(savePath, json);
        }

        public static List<Tuple<int, Keys>> LoadShortcuts()
        {
            if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
            List<Tuple<int, Keys>> shortcuts = null;
            try
            {
                var json = File.ReadAllText(savePath);
                shortcuts = new JavaScriptSerializer().Deserialize<List<Tuple<int, Keys>>>(json);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            return shortcuts != null ? shortcuts : DefaultShortcuts();
        }

        private static List<Tuple<int, Keys>> DefaultShortcuts()
        {
            var shortcuts = new List<Tuple<int, Keys>>();
            shortcuts.Add(new Tuple<int, Keys>((int)ModifierKeys.Shift, Keys.F2));
            shortcuts.Add(new Tuple<int, Keys>((int)ModifierKeys.Shift, Keys.F1));
            shortcuts.Add(new Tuple<int, Keys>((int)ModifierKeys.Control, Keys.F2));
            shortcuts.Add(new Tuple<int, Keys>((int)ModifierKeys.Control, Keys.F1));
            return shortcuts;
        }
    }
}
