using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using TVBrasileira.frameworks;

namespace TVBrasileira.channels
{
    public class Palmirinha
    {
        private readonly IModHelper _helper;
        private bool _isChannelEnabled;
        private static readonly Rectangle QueenOfSauceArea = new(602, 361, 84, 28);

        private static IRawTextureData _palmirinhaTexture;
        
        public Palmirinha(IModHelper helper)
        {
            _helper = helper;
            _palmirinhaTexture = _helper.ModContent.Load<IRawTextureData>("assets/palmirinha.png");
            _helper.Events.Content.AssetRequested += ChangeDialogs;
            _helper.Events.Content.AssetRequested += ChangeImages;
        }
        
        private void ChangeDialogs(object sender, AssetRequestedEventArgs e)
        {
            _isChannelEnabled = _helper.ReadConfig<ModConfig>().PalmirinhaToggle;
            if (!_isChannelEnabled) return;
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
            _isChannelEnabled = _helper.ReadConfig<ModConfig>().PalmirinhaToggle;
            if (!_isChannelEnabled) return;
            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    editor.PatchImage(_palmirinhaTexture, targetArea: QueenOfSauceArea);
                });
            }
        }
    }
}