using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using Microsoft.Xna.Framework;
using TVBrasileira.Framework;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            var ednaldoPereira = new EdnaldoPereira(helper);
            var palmirinha = new Palmirinha(helper);
            var globoRural = new GloboRural(helper);
        }
    }
}