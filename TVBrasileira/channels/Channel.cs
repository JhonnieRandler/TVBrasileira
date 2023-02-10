using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using TVBrasileira.frameworks;

namespace TVBrasileira.channels
{
    public abstract class Channel
    {
        protected readonly IModHelper Helper;
        protected readonly IMonitor Monitor;

        protected List<string> TargetDialogueAssets;

        protected Channel(IModHelper helper, IMonitor monitor)
        {
            Helper = helper;
            Monitor = monitor;
        }

        /// <member name="M:Channel.IsChannelEnabled">
        ///  <summary>
        /// Determines whether the current channel is enabled based on its name.
        /// </summary>
        /// <returns>A Boolean value indicating whether the channel is enabled.</returns>
        /// </member>
        protected bool IsChannelEnabled()
        {
            return GetType().Name switch
            {
                "Palmirinha" => Helper.ReadConfig<ModConfig>().PalmirinhaToggle,
                "EdnaldoPereira" => Helper.ReadConfig<ModConfig>().EdnaldoPereiraToggle,
                "GloboRural" => Helper.ReadConfig<ModConfig>().GloboRuralToggle,
                _ => false
            };
        }

        protected void ChangeDialogues(object sender, AssetRequestedEventArgs e)
        {
            var currentAssetName = e.NameWithoutLocale;
            foreach (var targetAsset in TargetDialogueAssets)
            {
                if (!currentAssetName.IsEquivalentTo(targetAsset)) return;
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    SetCustomDialogues(editor, currentAssetName);
                });
            }
        }

        protected abstract void SetCustomDialogues(IAssetDataForDictionary<string, string> editor, IAssetName assetName);
        
        protected void InvalidateDialogues()
        {
            foreach (var targetAsset in TargetDialogueAssets)
            {
                string currentLocale =
                    Helper.GameContent.CurrentLocale != "" ? "." + Helper.GameContent.CurrentLocale : "";

                Helper.GameContent.InvalidateCache(targetAsset);
                Helper.GameContent.InvalidateCache(targetAsset + currentLocale);
            }
        }
    }
}