using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GarterBelt
{
    class Program
    {
        [STAThread]
        static void Main(string[] arguments)
        {
            // Debug console initializer
            ConsoleManager.Init();
#if !DEBUG
			ConsoleManager.Hide();
#endif

            // Resolve merged libraries from resource
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

            // Make application entrypoint
            var application = new Application
            {
                StartupUri = new Uri("MainWindow.xaml", UriKind.RelativeOrAbsolute)
            };

            // Initialize wpf resources
            application.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml",
                UriKind.RelativeOrAbsolute)
            });
            application.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
                UriKind.RelativeOrAbsolute)
            });
            application.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Indigo.xaml",
                UriKind.RelativeOrAbsolute)
            });
            application.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml",
                UriKind.RelativeOrAbsolute)
            });

            // go
            application.Run();
        }
    }
}
