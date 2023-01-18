using StardewModdingAPI;
using TVBrasileira.Framework;
using TVBrasileira.Framework.GenericModConfigMenu;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        private ModConfig _config;
        private ConfigureGMCM _configMenu;
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            this._config = helper.ReadConfig<ModConfig>();
            this._configMenu = new ConfigureGMCM(helper, this._config, this.ModManifest);
            var ednaldoPereira = new EdnaldoPereira(helper, this._config.EdnaldoPereiraToggle);
            var palmirinha = new Palmirinha(helper, this._config.PalmirinhaToggle);
            var globoRural = new GloboRural(helper, this._config.GloboRuralToggle);
        }
    }
}