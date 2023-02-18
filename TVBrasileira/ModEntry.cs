using StardewModdingAPI;
using TVBrasileira.frameworks;
using TVBrasileira.channels;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            var configMenu = new CreateMenu(helper, ModManifest, Monitor);
            var ednaldoPereira = new EdnaldoPereira(helper, Monitor);
            var palmirinha = new Palmirinha(helper, Monitor);
            var globoRural = new GloboRural(helper, Monitor);
            var marciaSensitiva = new MarciaSensitiva(helper, Monitor);
        }
    }
}