using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework
{
    public class Palmirinha
    {
        private readonly IModHelper _helper;
        private ModConfig _config;

        public Palmirinha(IModHelper helper)
        {
            _helper = helper;
            _helper.Events.Content.AssetRequested += ChangeDialogs;
            _helper.Events.Content.AssetRequested += ChangeImages;
        }
        
        private void ChangeDialogs(object sender, AssetRequestedEventArgs e)
        {
            _config = _helper.ReadConfig<ModConfig>();
            if (!_config.PalmirinhaToggle) return;
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

        private void ChangeImages(object sender, AssetRequestedEventArgs e)
        {
            _config = _helper.ReadConfig<ModConfig>();
            if (!_config.PalmirinhaToggle) return;
            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    IRawTextureData palmirinhaTexture =
                        _helper.ModContent.Load<IRawTextureData>("assets/palmirinha.png");
                    editor.PatchImage(palmirinhaTexture, targetArea: new Rectangle(602, 361, 84, 28));
                });
            }
        }
    }
}