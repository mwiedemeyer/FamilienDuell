using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FamilienDuell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static GameController gameController;
        internal static ControllerWindow ControllerWindow { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ControllerWindow = new ControllerWindow();
            ControllerWindow.Show();
        }

        public static GameController GameController
        {
            get
            {
                if (gameController == null)
                    gameController = new GameController(new SNESBuzzer());
                return gameController;
            }
        }
    }
}
