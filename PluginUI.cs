using System;
using ImGuiNET;
using System.Numerics;
using Dalamud.Game.ClientState;

namespace Stormtalons
{
    public class PluginUI
    {
        public bool IsVisible { get; set; }
        private Configuration config;
        private ImGuiScene.TextureWrap stormtalonImage;

        public PluginUI(Configuration config, ClientState clientState, ImGuiScene.TextureWrap stormtalonImage)
        {
            this.config = config;
            this.stormtalonImage = stormtalonImage;
        }
        public void Draw()
        {
            if (!IsVisible)
                return;
            var mobHealth = TargetInfo.Health;
            var flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoTitleBar;
            if (config.IsClickthrough)
            {
                flags |= ImGuiWindowFlags.NoInputs;
            }
            decimal remainingStormtalons = decimal.Round(mobHealth / 1115109m, 2, MidpointRounding.AwayFromZero);
            string roundedStringtalons = String.Format("{0:0.00}", remainingStormtalons);
            ImGui.SetNextWindowSizeConstraints(new Vector2(150, 0), new Vector2(900, 900));
            ImGui.SetNextWindowBgAlpha(config.Opacity);
            ImGui.Begin("Stormtalons", flags);
            if (config.ShowStormtalonImage)
            {
                ImGui.Image(this.stormtalonImage.ImGuiHandle, new Vector2(this.stormtalonImage.Width, this.stormtalonImage.Height));
            }
            ImGui.Text(roundedStringtalons + " Stormtalons");
            ImGui.End();
        }
    }
}
