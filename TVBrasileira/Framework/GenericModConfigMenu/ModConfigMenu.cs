using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework.GenericModConfigMenu
{
    public class ModConfigMenu
    {
        private readonly IModHelper _helper;
        private readonly IManifest _modManifest;
        private readonly IMonitor _monitor;
        private  ModConfig _config;

        public ModConfigMenu(IModHelper helper, ModConfig config, IManifest modManifest, IMonitor monitor)
        {
            this._monitor = monitor;
            this._helper = helper;
            this._config = config;
            this._modManifest = modManifest;
            this._helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var configMenuApi =
                this._helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenuApi is null)
            {
                _monitor.Log(I18n.DisabledGmcm(), LogLevel.Info);
                return;
            }

            configMenuApi.Register(
                mod: this._modManifest,
                reset: () => this._config = new ModConfig(),
                save: () => this._helper.WriteConfig(_config)
            );
            
            configMenuApi.AddBoolOption(
                mod: this._modManifest,
                name: () => I18n.TitleEdnaldo(),
                tooltip: () => I18n.TooltipEdnaldo(),
                getValue: () => this._config.EdnaldoPereiraToggle,
                setValue: value => this._config.EdnaldoPereiraToggle = value
            );
            
            configMenuApi.AddBoolOption(
                mod: this._modManifest,
                name: () => I18n.TitlePalmirinha(),
                tooltip: () => I18n.TooltipPalmirinha(),
                getValue: () => this._config.PalmirinhaToggle,
                setValue: value => this._config.PalmirinhaToggle = value
            );
            
            configMenuApi.AddBoolOption(
                mod: this._modManifest,
                name: () => I18n.TitleGloboRural(),
                tooltip: () => I18n.TooltipGloboRural(),
                getValue: () => this._config.GloboRuralToggle,
                setValue: value => this._config.GloboRuralToggle = value
            );
        }
    }
}