using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using Microsoft.Xna.Framework;
using TVBrasileira.Framework;

namespace TVBrasileira
{
    public class ModEntry : Mod
    {
        private ModConfig Config;
        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            I18n.Init(helper.Translation);
            var ednaldoPereira = new EdnaldoPereira(helper, this.Config.EdnaldoPereira);
            var palmirinha = new Palmirinha(helper, this.Config.Palmirinha);
            var globoRural = new GloboRural(helper, this.Config.GloboRural);
        }
    }
}