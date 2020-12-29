using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StatsdClient;
using log4net;
using System.Security.Permissions;

namespace DdStardewValley
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        private static ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static log4net.Repository.ILoggerRepository logRepo = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
        
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after a save is loaded </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            //log4net.Config.XmlConfigurator.Configure(logRepo, new System.IO.FileInfo("log4net.config"));
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            //FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, "~/dev/dd-stardew-valley");
            //f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, "~/dev/dd-stardew-valley/bin/Debug/myapp.log");
            this.Monitor.Log("configured logger", LogLevel.Debug);
            this.Monitor.Log("Save loaded", LogLevel.Debug);
            var dogstatsdConfig = new StatsdConfig
            {
                StatsdServerName = "127.0.0.1",
                StatsdPort = 8125,
            };

            DogStatsd.Configure(dogstatsdConfig);
            DogStatsd.Counter("dev.stardew.save_loaded", 2, tags: new[] { "player:" + Game1.player.name });
            this.Monitor.Log("Metric sent", LogLevel.Debug);
            String testLogLine = String.Format("Save successfully loaded from player {0}", Game1.player.name);
            _log.Debug(testLogLine);
            this.Monitor.Log("Sent log line", LogLevel.Debug);

            DogStatsd.Dispose(); // Flush all metrics not yet sent
        }
    }
}