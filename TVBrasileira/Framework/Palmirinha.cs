using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework
{
    public class Palmirinha
    {
        private IModHelper _helper;
        private bool _config;
        
        public Palmirinha(IModHelper helper, bool config)
        {
            _config = config;
            _helper = helper;
            _helper.Events.Content.AssetRequested += AlterarDialogos;
            _helper.Events.Content.AssetRequested += AlterarImagens;
        }
        
        public void AlterarDialogos(object sender, AssetRequestedEventArgs e)
        {
            if (_config == true)
            {
                if (e.NameWithoutLocale.IsEquivalentTo("Strings/StringsFromCSFiles"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsDictionary<string, string>();
                        editor.Data["TV.cs.13114"] = I18n.TitlePalmirinha();
                        editor.Data["TV.cs.13117"] = I18n.RerunPalmirinha();
                        editor.Data["TV.cs.13127"] = I18n.IntroPalmirinha();
                        editor.Data["TV.cs.13151"] = I18n.LearnedPalmirinha();
                        editor.Data["TV.cs.13153"] = I18n.OutroPalmirinha();
                    });
                }
            }
        }

        public void AlterarImagens(object sender, AssetRequestedEventArgs e)
        {
            if (_config == true)
            {
                if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsImage();
                        IRawTextureData palmirinhapng =
                            _helper.ModContent.Load<IRawTextureData>("assets/palmirinha.png");
                        editor.PatchImage(palmirinhapng, targetArea: new Rectangle(602, 361, 84, 28));
                    });
                }
            }
        }
    }
}