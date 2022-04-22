using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace Stormtalons
{
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; }
        public bool IsClickthrough { get; set; } = false;
        public float Opacity { get; set; } = 1.0f;
        public bool ShowStormtalonImage { get; set; } = true;

        // the below exist just to make saving less cumbersome

        [NonSerialized]
        private DalamudPluginInterface pluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.pluginInterface.SavePluginConfig(this);
        }
    }
}
