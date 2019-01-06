using System;
using System.Windows.Forms;

namespace Screensaver
{
    /// <summary>
    /// This program demonstrates a simple scaffold for a Microsoft Windows Screensaver. It should be compiled with the same architecture of the OS (remove "Prefer 32 bit" from the project's build configurations), otherwise it can't run from C:/Windows/System32.
    /// The executable should be renamed with a .scr extension and saved in C:/WIndows/System32.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command line arguments passed to the application. A screensaver needs to respond to three flags:
        /// - /p: Preview mode. The OS also passes a pointer to the handle in which the preview should be shown.
        /// - /s: Screensaver mode. It should run on fullscreen and be closed on any user input.
        /// - /c Configuration mode. Should display a configuration form.</param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form f;

            // If compiled in Debug configuration, ignores the command line arguments.
#if DEBUG
            f = new Form1(Screen.PrimaryScreen.Bounds); // Per schermi multipli ha senso un foreach su tutti gli schermi di Screen.AllScreens e duplicare il form su tutti
            f.Show();
            Application.Run(f);
#else
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "/p":
                        if (args.Length > 1)
                        {
                            f = new Form1(new IntPtr(long.Parse(args[1])));
                            f.Show();
                            Application.Run(f);
                        }
                        break;
                    case "/s":
                        f = new Form1(Screen.PrimaryScreen.Bounds); // It should check for multiple screens and stretch or clone the form in all of them...
                        f.Show();
                        Application.Run(f);
                        break;
                    case "/c":
                    default:
                        f = new Form2();
                        f.Show();
                        Application.Run(f);
                        break;
                }
            }
            else
            {
                f = new Form1(Screen.PrimaryScreen.Bounds);
                f.Show();
                Application.Run(f);
            }
#endif
        }
    }
}
