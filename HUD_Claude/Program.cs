using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HUD_Claude
{
    internal static class Program
    {
        //dotnet pack --configuration Release /p:PackageVersion=1.2.0
        //dotnet nuget push bin\Release\YourProjectName.1.2.0.nupkg --source https://api.nuget.org/v3/index.json --api-key YourApiKey

        // nuget API Key oy2iyzzpxpbo5j4bje3ah7c6bnnpswjunpuer7hgowyayu

        //remove before pushing
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()
            {

            });
        }
    }
}
