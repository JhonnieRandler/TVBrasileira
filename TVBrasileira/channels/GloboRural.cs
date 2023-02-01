using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.channels
{
    public class GloboRural : Channel
    {
        private static readonly Rectangle LivinOffTheLandArea = new(517, 361, 84, 28);
        private static IRawTextureData _globoRuralTexture;

        public GloboRural(IModHelper helper, IMonitor monitor) : base(helper, monitor)
        {
            _globoRuralTexture = Helper.ModContent.Load<IRawTextureData>("assets/globoRural.png");
            Helper.Events.Content.AssetRequested += ChangeDialogues;
            Helper.Events.Content.AssetRequested += ChangeImages;
        }
        
        private void ChangeDialogues(object sender, AssetRequestedEventArgs e)
        {
            if (!IsChannelEnabled()) return;
            
            var assetName = e.NameWithoutLocale;
            if (assetName.IsEquivalentTo("Strings/StringsFromCSFiles") || assetName.IsEquivalentTo("Data/TV/TipChannel"))
                e.Edit(asset => AssignChannelStrings(asset, assetName));
        }

        private void ChangeImages(object sender, AssetRequestedEventArgs e)
        {
            if (!IsChannelEnabled()) return;
            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    editor.PatchImage(_globoRuralTexture, targetArea: LivinOffTheLandArea);
                });
            }
        }
    }
}