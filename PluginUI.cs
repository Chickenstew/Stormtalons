using System;
using ImGuiNET;
using System.Numerics;
using Dalamud.Game.ClientState;
using Dalamud.Interface.GameFonts;

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
            ImGui.SetNextWindowSizeConstraints(new Vector2(200, 0), new Vector2(900, 900));
            ImGui.SetNextWindowBgAlpha(config.Opacity);
            ImGui.Begin("Stormtalons", flags);
            if (config.ShowStormtalonImage)
            {
                if (config.DecayStormtalonImage)
                {
                    float imgAdjuster = (float)(remainingStormtalons - Math.Truncate(remainingStormtalons));
                    string roundedRemaingStringtalons = String.Format("{0:0}", remainingStormtalons);
                    Vector2 uv0 = new Vector2(0.0f, 0.0f);
                    Vector2 uv1 = new Vector2(imgAdjuster, 1.0f);
                    ImGui.Image(this.stormtalonImage.ImGuiHandle, new Vector2(imgAdjuster * this.stormtalonImage.Width, this.stormtalonImage.Height), uv0, uv1);
                    if (config.DecayStormtalonCounter)
                    {
                        ImGui.SameLine(160);
                        ImGui.SetCursorPosY(25);
                        ImGui.SetWindowFontScale(2);
                        ImGui.PushStyleColor(ImGuiCol.Text, ImGui.GetColorU32(config.ChosenColour));
                        ImGui.Text($"x{roundedRemaingStringtalons}");
                        ImGui.PopStyleColor();
                        ImGui.SetWindowFontScale(1);
                    }
                } else
                {
                    ImGui.Image(this.stormtalonImage.ImGuiHandle, new Vector2(this.stormtalonImage.Width, this.stormtalonImage.Height));
                }
            }
            if (config.RemainingStormtalonDisplay)
            {
                ImGui.PushStyleColor(ImGuiCol.Text, ImGui.GetColorU32(config.ChosenColour));
                ImGui.Text(roundedStringtalons + " Stormtalons");
                ImGui.PopStyleColor();
            }
            ImGui.End();
        }
    }
}
