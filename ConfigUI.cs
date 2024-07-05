using ImGuiNET;
using System.Numerics;

namespace Stormtalons
{
    public class ConfigUI
    {

        public bool IsVisible { get; set; }
        private Configuration config;
        private bool isClickthrough;
        private float opacity;
        private bool remainingStormtalonDisplay;
        private bool showStormtalonImage;
        private bool decayStormtalonImage;
        private bool decayStormtalonCounter;
        private Vector4 chosenColour;


        public ConfigUI(Configuration config, bool isClickthrough, float opacity, bool remainingStormtalonDisplay, bool showStormtalonImage,
                                        bool decayStormtalonImage, bool decayStormtalonCounter, Vector4 chosenColour)
        {
            this.config = config;
            this.opacity = opacity;
            this.isClickthrough = isClickthrough;
            this.showStormtalonImage = showStormtalonImage;
            this.decayStormtalonImage = decayStormtalonImage;
            this.decayStormtalonCounter = decayStormtalonCounter;
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
            if (ImGui.Checkbox("Enable remaining Stormtalons Display", ref remainingStormtalonDisplay))
            {
                config.RemainingStormtalonDisplay = remainingStormtalonDisplay;
            }
            if (ImGui.Checkbox("Show Stormtalon in all his glory.", ref showStormtalonImage))
            {
                config.ShowStormtalonImage = showStormtalonImage;
            }
            if (config.ShowStormtalonImage)
            {
                if (ImGui.Checkbox("Enable Decaying Stormtalons", ref decayStormtalonImage))
                {
                    config.DecayStormtalonImage = decayStormtalonImage;
                }
                if (config.DecayStormtalonImage)
                {
                    if (ImGui.Checkbox("Enable Decaying Stormtalon Counter", ref decayStormtalonCounter))
                    {
                        config.DecayStormtalonCounter = decayStormtalonCounter;
                    }
                }
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