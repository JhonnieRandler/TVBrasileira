using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework.GenericModConfigMenu
{
    public class CreateMenu
    {
        private readonly IModHelper _helper;
        private readonly IManifest _modManifest;
        private readonly IMonitor _monitor;
        private  ModConfig _config;
        
        public CreateMenu(IModHelper helper, IManifest modManifest, IMonitor monitor) {
            this._monitor = monitor;
            this._helper = helper;
            this._config = helper.ReadConfig<ModConfig>();
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
                save: () => CommitConfig(),
                titleScreenOnly: false
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
        
        private void CommitConfig()
        {
            String language = this._helper.GameContent.CurrentLocale != "" ? "." + this._helper.GameContent.CurrentLocale : "";
            this._helper.WriteConfig(_config);
            _helper.GameContent.InvalidateCache("LooseSprites/Cursors" + language);
            _helper.GameContent.InvalidateCache("LooseSprites/Cursors2");
            _helper.GameContent.InvalidateCache("Strings/StringsFromCSFiles" + language);
            _helper.GameContent.InvalidateCache("Data/TV/TipChannel" + language);
        }
    }
    
}