using StardewModdingAPI;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            var ednaldoPereira = new EdnaldoPereira();
            helper.Events.Content.AssetRequested += ednaldoPereira.alterarDialogos;
        }
    }
}