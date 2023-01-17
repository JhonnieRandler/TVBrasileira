using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using Microsoft.Xna.Framework;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            var ednaldoPereira = new EdnaldoPereira(helper);
            helper.Events.Content.AssetRequested += ednaldoPereira.alterarDialogos;
            helper.Events.Content.AssetRequested += ednaldoPereira.alterarImagens;
            
            IDictionary<string, string> data = helper.GameContent.Load<Dictionary<string, string>>("Strings/StringsFromCSFiles");
            helper.Data.WriteJsonFile("Strings/StringsFromCSFiles", data);
        }
    }
}