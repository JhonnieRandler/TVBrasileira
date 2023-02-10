using System.Collections.Generic;
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
            
            TargetDialogueAssets = new List<string> { "Strings/StringsFromCSFiles" };
            
            Helper.Events.Content.AssetRequested += ChangeDialogues;
            Helper.Events.Content.AssetRequested += ChangeImages;
        }
        
        protected override void SetCustomDialogues(IAssetDataForDictionary<string, string> editor, IAssetName assetName)
        {
            if (!IsChannelEnabled()) return;
            editor.Data["TV.cs.13114"] = I18n.TitlePalmirinha();
            editor.Data["TV.cs.13117"] = I18n.RerunPalmirinha();
            editor.Data["TV.cs.13127"] = I18n.IntroPalmirinha();
            editor.Data["TV.cs.13151"] = I18n.LearnedPalmirinha();
            editor.Data["TV.cs.13153"] = I18n.OutroPalmirinha();
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