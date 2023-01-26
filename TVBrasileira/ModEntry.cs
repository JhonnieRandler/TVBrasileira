using StardewModdingAPI;
using TVBrasileira.Framework;
using TVBrasileira.Framework.GenericModConfigMenu;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            var configMenu = new CreateMenu(helper, ModManifest, Monitor);
            var ednaldoPereira = new EdnaldoPereira(helper);
            var palmirinha = new Palmirinha(helper);
            var globoRural = new GloboRural(helper);
        }
    }
}