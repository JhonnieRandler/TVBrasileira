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
        /// Changes the dialogues for the target assets in the `TargetDialogueAssets` list.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments that contain information about the requested asset.</param>
        protected void ChangeDialogues(object sender, AssetRequestedEventArgs e)
        {
            var currentAssetName = e.NameWithoutLocale;
            
            if (TargetDialogueAssets.Contains(currentAssetName.ToString())){
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    SetCustomDialogues(editor, currentAssetName);
                });
            }
        }

        /// <summary>
        /// Sets custom dialogues for the specified asset.
        /// </summary>
        /// <param name="editor">The editor for the asset data as a dictionary.</param>
        /// <param name="assetName">The name of the asset.</param>
        protected abstract void SetCustomDialogues(IAssetDataForDictionary<string, string> editor, IAssetName assetName);
        
        /// <summary>
        /// Changes the images for the target assets in the `TargetImageAssets` list.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments that contain information about the requested asset.</param>
        protected void ChangeImages(object sender, AssetRequestedEventArgs e)
        {
            var currentAssetName = e.NameWithoutLocale;
            foreach (var targetAsset in TargetImageAssets)
            {
                if (!currentAssetName.IsEquivalentTo(targetAsset)) return;
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    SetCustomImages(editor, currentAssetName);
                });
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