using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.channels
{
    public class Palmirinha : Channel
    {
        private static readonly Rectangle QueenOfSauceArea = new(602, 361, 84, 28);
        private static IRawTextureData _palmirinhaTexture;
        
        public Palmirinha(IModHelper helper, IMonitor monitor) : base(helper, monitor)
        {
            _palmirinhaTexture = Helper.ModContent.Load<IRawTextureData>("assets/palmirinha.png");
            Helper.Events.Content.AssetRequested += ChangeDialogues;
            Helper.Events.Content.AssetRequested += ChangeImages;
        }
        
        private void ChangeDialogues(object sender, AssetRequestedEventArgs e)
        {
            if (!IsChannelEnabled()) return;
            
            var assetName = e.NameWithoutLocale;
            if (!assetName.IsEquivalentTo("Strings/StringsFromCSFiles")) return;
            e.Edit(asset => AssignChannelStrings(asset, assetName));
        }

        private void ChangeImages(object sender, AssetRequestedEventArgs e)
        {
            if (!IsChannelEnabled()) return;
            if (!e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors")) return;
            e.Edit(asset =>
            {
                var editor = asset.AsImage();
                editor.PatchImage(_palmirinhaTexture, targetArea: QueenOfSauceArea);
            });
        }
    }
}