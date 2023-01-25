using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework.GenericModConfigMenu
{
    public class CreateMenu
    {
        private readonly IModHelper helper;
        private readonly IManifest modManifest;
        private readonly IMonitor monitor;
        private  ModConfig config;
        
        private readonly List<string> _patchedAssets = new()
        {
            "LooseSprites/Cursors", 
            "LooseSprites/Cursors2",
            "Strings/StringsFromCSFiles", 
            "Data/TV/TipChannel",
        };
        
        public CreateMenu(IModHelper helper, IManifest modManifest, IMonitor monitor) {
            this.monitor = monitor;
            this.helper = helper;
            this.config = helper.ReadConfig<ModConfig>();
            this.modManifest = modManifest;
            this.helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        }
        
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var configMenuApi =
                this.helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenuApi is null)
            {
                this.monitor.Log(I18n.DisabledGmcm(), LogLevel.Info);
                return;
            }

            configMenuApi.Register(
                mod: this.modManifest,
                reset: () => this.config = new ModConfig(),
                save: CommitConfig,
                titleScreenOnly: false
            );
            
            configMenuApi.AddBoolOption(
                mod: this.modManifest,
                name: () => I18n.TitleEdnaldo(),
                tooltip: () => I18n.TooltipEdnaldo(),
                getValue: () => this.config.EdnaldoPereiraToggle,
                setValue: value => this.config.EdnaldoPereiraToggle = value
            );
            
            configMenuApi.AddBoolOption(
                mod: this.modManifest,
                name: () => I18n.TitlePalmirinha(),
                tooltip: () => I18n.TooltipPalmirinha(),
                getValue: () => this.config.PalmirinhaToggle,
                setValue: value => this.config.PalmirinhaToggle = value
            );
            
            configMenuApi.AddBoolOption(
                mod: this.modManifest,
                name: () => I18n.TitleGloboRural(),
                tooltip: () => I18n.TooltipGloboRural(),
                getValue: () => this.config.GloboRuralToggle,
                setValue: value => this.config.GloboRuralToggle = value
            );
        }
        
        private void CommitConfig()
        {
            this.helper.WriteConfig(this.config);
            
            string locale;
            
            if (this.helper.GameContent.CurrentLocale != "")
            {
                locale = "." + this.helper.GameContent.CurrentLocale;
            }
            else
            {
                locale = "";
            }
            
            foreach (var path in _patchedAssets)
            {
                this.helper.GameContent.InvalidateCache(path);
                this.helper.GameContent.InvalidateCache(path + locale);
            }
        }
    }
    
}