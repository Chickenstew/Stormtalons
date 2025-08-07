using System;
using Dalamud.Plugin;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using System.IO;
using Dalamud.Plugin.Services;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.IoC;


namespace Stormtalons
{
    public class Plugin : IDalamudPlugin
    {
        private IDalamudPluginInterface pluginInterface;
        private Configuration config;
        private PluginUI ui;
        private ConfigUI cui;
        private IGameObject previousTarget;
        private IClientState _clientState;
        private ICondition _condition;
        private ITargetManager _targetManager;
        private IFramework _framework;
        private ICommandManager _commands;
        [PluginService] internal static ITextureProvider? TextureProvider { get; private set; } = null;

        public string Name => "Stormtalons";


        public Plugin(
            IDalamudPluginInterface pluginInterface,
            IClientState clientState,
            ICommandManager commands,
            ICondition condition,
            IFramework framework,
            ITargetManager targets,
            ITextureProvider textureProvider)
        {
            this.pluginInterface = pluginInterface;
            this._clientState = clientState;
            this._condition = condition;
            this._framework = framework;
            this._commands = commands;
            this._targetManager = targets;
            TextureProvider = textureProvider;


            this.config = (Configuration)this.pluginInterface.GetPluginConfig() ?? new Configuration();
            this.config.Initialize(this.pluginInterface);

            var imagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "stormtalon.png");
            var stormtalonImage = textureProvider.GetFromFile(imagePath);

            this.ui = new PluginUI(config, clientState, stormtalonImage);
            this.cui = new ConfigUI(config, config.IsClickthrough, config.Opacity, config.RemainingStormtalonDisplay, config.ShowStormtalonImage,
                                    config.DecayStormtalonImage, config.DecayStormtalonCounter, config.ChosenColour);
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

        public void GetData(IFramework framework)
        {
            IGameObject target = _targetManager.Target;
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