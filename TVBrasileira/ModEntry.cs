using StardewModdingAPI;
using TVBrasileira.Framework;
using TVBrasileira.Framework.GenericModConfigMenu;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        private ModConfig _config;
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            this._config = helper.ReadConfig<ModConfig>();
            var configMenu = new ModConfigMenu(helper, this._config, this.ModManifest, Monitor);
            var ednaldoPereira = new EdnaldoPereira(helper, Monitor);
            var palmirinha = new Palmirinha(helper);
            var globoRural = new GloboRural(helper);
        }
    }
}