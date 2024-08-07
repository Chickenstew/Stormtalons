﻿using Dalamud.Configuration;
using Dalamud.Plugin;
using System.Numerics;
using System;

namespace Stormtalons
{
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; }
        public bool IsClickthrough { get; set; } = false;
        public float Opacity { get; set; } = 1.0f;
        public bool RemainingStormtalonDisplay { get; set; } = true;
        public bool ShowStormtalonImage { get; set; } = true;
        public bool DecayStormtalonImage { get; set; } = true;
        public bool DecayStormtalonCounter { get; set; } = true;
        public Vector4 ChosenColour { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        // the below exist just to make saving less cumbersome

        [NonSerialized]
        private IDalamudPluginInterface pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.pluginInterface.SavePluginConfig(this);
        }
    }
}