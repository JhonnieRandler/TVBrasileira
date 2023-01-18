using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework.GenericModConfigMenu
{
    public class ConfigureGMCM
    {
        private IModHelper _helper;
        private ModConfig _config;
        private IManifest _modManifest;

        public ConfigureGMCM(IModHelper helper, ModConfig config, IManifest modManifest)
        {
            this._helper = helper;
            this._config = config;
            this._modManifest = modManifest;
            this._helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        }
        
        public void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var configMenu =
                this._helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;
            
            configMenu.Register(
                mod: this._modManifest,
                reset: () => this._config = new ModConfig(),
                save: () => this._helper.WriteConfig(_config)
            );
            
            configMenu.AddBoolOption(
                mod: this._modManifest,
                name: () => I18n.TitleEdnaldo(),
                tooltip: () => I18n.TooltipEdnaldo(),
                getValue: () => this._config.EdnaldoPereiraToggle,
                setValue: value => this._config.EdnaldoPereiraToggle = value
            );
            
            configMenu.AddBoolOption(
                mod: this._modManifest,
                name: () => I18n.TitlePalmirinha(),
                tooltip: () => I18n.TooltipPalmirinha(),
                getValue: () => this._config.PalmirinhaToggle,
                setValue: value => this._config.PalmirinhaToggle = value
            );
            
            configMenu.AddBoolOption(
                mod: this._modManifest,
                name: () => I18n.TitleGloboRural(),
                tooltip: () => I18n.TooltipGloboRural(),
                getValue: () => this._config.GloboRuralToggle,
                setValue: value => this._config.GloboRuralToggle = value
            );
        }
    }
}