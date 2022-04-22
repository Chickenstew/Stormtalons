using System;
using Dalamud.Game.ClientState;
using Dalamud.Plugin;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Dalamud.Game;
using System.IO;
using System.Reflection;

namespace Stormtalons
{
    public class Plugin : IDalamudPlugin
    {
        private DalamudPluginInterface pluginInterface;
        private Configuration config;
        private PluginUI ui;
        private ConfigUI cui;
        private GameObject previousTarget;
        private ClientState _clientState;
        private Condition _condition;
        private TargetManager _targetManager;
        private Framework _framework;
        private CommandManager _commands;

        public string Name => "Stormtalons";


        public Plugin(
            DalamudPluginInterface pluginInterface,
            ClientState clientState,
            CommandManager commands,
            Condition condition,
            Framework framework,
            TargetManager targets)
        {
            this.pluginInterface = pluginInterface;
            this._clientState = clientState;
            this._condition = condition;
            this._framework = framework;
            this._commands = commands;
            this._targetManager = targets;

            this.config = (Configuration)this.pluginInterface.GetPluginConfig() ?? new Configuration();
            this.config.Initialize(this.pluginInterface);

            var imagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "stormtalon.png");
            var stormtalonImage = this.pluginInterface.UiBuilder.LoadImage(imagePath);

            this.ui = new PluginUI(config, clientState, stormtalonImage);
            this.cui = new ConfigUI(config.Opacity, config.IsClickthrough, config, config.ShowStormtalonImage);
            this.pluginInterface.UiBuilder.Draw += this.ui.Draw;
            this.pluginInterface.UiBuilder.Draw += this.cui.Draw;

            this._commands.AddHandler("/stormtalon", new CommandInfo(OpenConfig)
            {
                HelpMessage = "Stormtalon config"
            });

            this._framework.Update += this.GetData;
        }

        public void OpenConfig(string command, string args)
        {
            cui.IsVisible = true;
        }

        public void GetData(Framework framework)
        {
            GameObject target = _targetManager.Target;
            TargetInfo t = new TargetInfo();
            if (!t.IsValidTarget(target))
            {
                ui.IsVisible = false;
                return;
            }
            else
            {
                previousTarget = target;
                ui.IsVisible = true;
            }

        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            this._commands.RemoveHandler("/stormtalon");

            this.pluginInterface.SavePluginConfig(this.config);

            this.pluginInterface.UiBuilder.Draw -= this.ui.Draw;
            this.pluginInterface.UiBuilder.Draw -= this.cui.Draw;

            this._framework.Update -= this.GetData;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
