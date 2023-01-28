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
        private Vector4 chosenColour;

        public ConfigUI(float opacity, bool isClickthrough, Configuration config, bool showStormtalonImage, Vector4 chosenColour)
        {
            this.config = config;
            this.opacity = opacity;
            this.isClickthrough = isClickthrough;
            this.showStormtalonImage = showStormtalonImage;
            this.chosenColour = chosenColour;
        }

        public void Draw()
        {
            if (!IsVisible)
                return;
            var flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize;
            var colourSelection = 0;
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
            if (ImGui.ColorEdit4($"Text Colour ##{colourSelection}", ref chosenColour, ImGuiColorEditFlags.NoInputs))
            {
                config.ChosenColour = chosenColour;
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