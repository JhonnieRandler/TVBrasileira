using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using Microsoft.Xna.Framework;
using TVBrasileira.Framework;
using TVBrasileira.Framework.GenericModConfigMenu;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        private ModConfig Config;
        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            I18n.Init(helper.Translation);
            var ednaldoPereira = new EdnaldoPereira(helper, this.Config.EdnaldoPereiraToggle);
            var palmirinha = new Palmirinha(helper, this.Config.PalmirinhaToggle);
            var globoRural = new GloboRural(helper, this.Config.GloboRuralToggle);
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            //get Generic Mod Config Menu's API if it's installed
            //fixme: although the config is being updated in real-time, the game assets aren't
            var configMenu =
                this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;
            
            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
                );
            
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("title-ednaldo"),
                tooltip: () => this.Helper.Translation.Get("tooltip-ednaldo"),
                getValue: () => this.Config.EdnaldoPereiraToggle,
                setValue: value => this.Config.EdnaldoPereiraToggle = value
            );
            
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("title-palmirinha"),
                tooltip: () => this.Helper.Translation.Get("tooltip-palmirinha"),
                getValue: () => this.Config.PalmirinhaToggle,
                setValue: value => this.Config.PalmirinhaToggle = value
            );
            
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("title-globo-rural"),
                tooltip: () => this.Helper.Translation.Get("tooltip-globo-rural"),
                getValue: () => this.Config.GloboRuralToggle,
                setValue: value => this.Config.GloboRuralToggle = value
            );
        }
    }
}