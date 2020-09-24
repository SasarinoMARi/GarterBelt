using GarterBelt.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace GarterBelt
{
    class Program
    {

        [STAThread]
        static void Main(string[] arguments)
        {
            ResolveMergedLibraries();
            if (arguments.Length > 0) startsWithArgs(arguments);
            else startWithoutArgs();
        }

        private static void startsWithArgs(string[] args)
        {
            // 프로세스 관리 파트 따로 클래스로 빼서 처리
            // 작용한 프로세스는 gui처리와 마찬가지로 킵
            // 이후 gui 실행시 이어 작업할 수 있도록 

            // arguments
            // -p [string]      : * fine process with [string]
            // -hide            : hide window
            // -show            : show window
            // -opa [int]       : set opacity to [int]
            // -tom [bool]      : set topmost with [bool]

            Garterbelt garter = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-p":
                        if (i + 1 >= args.Length) break;
                        garter = FetishManager.Instance.FindFetish(args[++i]);
                        break;
                    case "-hide":
                        garter.Hide();
                        break;
                    case "-show":
                        garter.Show();
                        break;
                    case "-opa":
                        if (i + 1 >= args.Length) break;
                        int opa = -1;
                        int.TryParse(args[++i], out opa);
                        if (opa != -1) garter.SetOpacity(byte.Parse(opa.ToString()));
                        break;
                    case "-tom":
                        if (i + 1 >= args.Length) break;
                        garter.SetTopmost("true" == args[++i].ToLower());
                        break;

                }
            }
        }

        private static void startWithoutArgs()
        {
            //System.Windows.Forms.Application.Run(new GarterGUIContext());
            var window = new MainWindow();
            window.Show();
            new System.Windows.Application().Run(window);
        }

        private static void ResolveMergedLibraries()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(new Program().GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
        }

        private static void InitializeConsole()
        {
            // Not fucking need
            ConsoleManager.Init();
#if !DEBUG
            ConsoleManager.Hide();
#endif
        }
    }

    public class GarterGUIContext : ApplicationContext
    {
        public GarterGUIContext()
        {
            var f = new GUI.GarterGUI();
            f.FormClosed += delegate { ExitThread(); };
            f.Show();
        }
    }
}
