using ImGuiNET;
using System.Numerics;

namespace Stormtalons
{
    public class ConfigUI
    {

        public bool IsVisible { get; set; }
        private float opacity;
        private bool isClickthrough;
        private Configuration config;
        private bool showStormtalonImage;

        public ConfigUI(float opacity, bool isClickthrough, Configuration config, bool showStormtalonImage)
        {
            this.config = config;
            this.opacity = opacity;
            this.isClickthrough = isClickthrough;
            this.showStormtalonImage = showStormtalonImage;
        }

        public void Draw()
        {
            if (!IsVisible)
                return;
            var flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize;
            ImGui.SetNextWindowSizeConstraints(new Vector2(250, 100), new Vector2(400, 300));
            ImGui.Begin("config", flags);
            if (ImGui.SliderFloat("Opacity", ref opacity, 0.0f, 1.0f))
            {
                config.Opacity = opacity;
            }
            if (ImGui.Checkbox("Enable clickthrough", ref isClickthrough))
            {
                config.IsClickthrough = isClickthrough;
            }
            if (ImGui.Checkbox("Show Stormtalon in all his glory.", ref showStormtalonImage))
            {
                config.ShowStormtalonImage = showStormtalonImage;
            }
            ImGui.NewLine();
            if (ImGui.Button("Save"))
            {
                IsVisible = false;
                config.Save();
            }
            ImGui.End();
        }
    }
}