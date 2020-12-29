using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StatsdClient;
using log4net;
using System.Reflection;
using log4net.Config;

namespace DdStardewValley
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            XmlConfigurator.Configure();
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
            _log.Info(testLogLine);

            DogStatsd.Dispose(); // Flush all metrics not yet sent
        }
    }
}