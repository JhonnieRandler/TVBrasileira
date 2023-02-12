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
        protected List<string> TargetImageAssets;

        protected Channel(IModHelper helper, IMonitor monitor)
        {
            Helper = helper;
            Monitor = monitor;
        }

        ///  <summary>
        /// Determines whether the current channel is enabled based on its name.
        /// </summary>
        /// <returns>A Boolean value indicating whether the channel is enabled.</returns>
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

        /// <summary>
        /// Check if the requested asset is equivalent to a target dialogue asset
        /// If equivalent, edit the asset to set custom dialogues
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments that contain information about the requested asset.</param>
        protected void CheckTargetDialogues(object sender, AssetRequestedEventArgs e)
        {
            var requestedAssetName = e.NameWithoutLocale;
            foreach (var targetAsset in TargetDialogueAssets)
            {
                if (!requestedAssetName.IsEquivalentTo(targetAsset)) continue;
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    SetCustomDialogues(editor, requestedAssetName);
                });
                break;
            }
        }

        /// <summary>
        /// Sets custom dialogues for the specified asset.
        /// </summary>
        /// <param name="editor">The editor for the asset data as a dictionary.</param>
        /// <param name="assetName">The name of the asset.</param>
        protected abstract void SetCustomDialogues(IAssetDataForDictionary<string, string> editor, IAssetName assetName);
        
        /// <summary>
        /// Check if the requested asset is equivalent to a target image asset
        /// If equivalent, edit the asset to set custom images
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments that contain information about the requested asset.</param>
        protected void CheckTargetImages(object sender, AssetRequestedEventArgs e)
        {
            var requestedAssetName = e.NameWithoutLocale;
            foreach (var targetAsset in TargetImageAssets)
            {
                if (!requestedAssetName.IsEquivalentTo(targetAsset)) continue;
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    SetCustomImages(editor, requestedAssetName);
                });
                break;
            }
        }
        
        /// <summary>
        /// Sets custom images for the specified asset.
        /// </summary>
        /// <param name="editor">The editor for the asset data as an image.</param>
        /// <param name="assetName">The name of the asset.</param>
        protected abstract void SetCustomImages(IAssetDataForImage editor, IAssetName assetName);
        
        /// <summary>
        /// Invalidates the cache for the target assets in the `TargetDialogueAssets` list.
        /// </summary>
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