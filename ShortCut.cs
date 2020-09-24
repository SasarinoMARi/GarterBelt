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

        public static void SaveShortCuts(List<ShortCutObject> shortcuts)
        {
            if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
            var json = new JavaScriptSerializer().Serialize(shortcuts);
            File.WriteAllText(savePath, json);
        }

        public static List<ShortCutObject> LoadShortcuts()
        {
            if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
            List<ShortCutObject> shortcuts = null;
            try
            {
                var json = File.ReadAllText(savePath);
                shortcuts = new JavaScriptSerializer().Deserialize<List<ShortCutObject>>(json);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            return shortcuts != null ? shortcuts : DefaultShortcuts();
        }

        private static List<ShortCutObject> DefaultShortcuts()
        {
            var shortcuts = new List<ShortCutObject>();
            shortcuts.Add(new ShortCutObject((uint)ModifierKeys.Shift, (uint)Keys.F2));
            shortcuts.Add(new ShortCutObject((uint)ModifierKeys.Shift, (uint)Keys.F1));
            shortcuts.Add(new ShortCutObject((uint)ModifierKeys.Control, (uint)Keys.F2));
            shortcuts.Add(new ShortCutObject((uint)ModifierKeys.Control, (uint)Keys.F1));
            return shortcuts;
        }

        public class ShortCutObject {
            public readonly uint modifierKey;
            public readonly uint key;
            
            public ShortCutObject() {

            }


            public ShortCutObject(uint modifierKey, uint key) {
                this.modifierKey = modifierKey;
                this.key = key;
            }
        }
    }
}
