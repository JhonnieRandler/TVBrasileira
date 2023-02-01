using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace TVBrasileira.channels
{
    public class EdnaldoPereira : Channel
    {
        private static readonly Rectangle WeatherReportArea = new(413, 305, 126, 28);
        private static readonly Rectangle IslandReportArea = new(148, 62, 42, 28);

        private static IRawTextureData _ednaldoPereiraTexture;
        private static IRawTextureData _ednaldoIslandTexture;

        private string _farmerName;
        
        public EdnaldoPereira(IModHelper helper, IMonitor monitor) : base(helper, monitor)
        {
            _ednaldoPereiraTexture = Helper.ModContent.Load<IRawTextureData>("assets/ednaldoPereira.png");
            _ednaldoIslandTexture = Helper.ModContent.Load<IRawTextureData>("assets/ednaldoIsland.png");
            Helper.Events.Content.AssetRequested += ChangeDialogues;
            Helper.Events.Content.AssetRequested += ChangeImages;
            Helper.Events.GameLoop.SaveLoaded += OnSaveLoad;
            Helper.Events.GameLoop.UpdateTicked += FarmerNameChanged;
        }

        private void ChangeDialogues(object sender, AssetRequestedEventArgs e)
        {
            var assetName = e.NameWithoutLocale;
            if (!assetName.IsEquivalentTo("Strings/StringsFromCSFiles")) return;
            e.Edit(asset =>
            {
                var editor = asset.AsDictionary<string, string>();
                editor.Data["TV.cs.13136"] = IsChannelEnabled() ?
                    I18n.DisabledEdnaldo(_farmerName) : I18n.IntroEdnaldo();
                
                AssignChannelStrings(asset, assetName);
            });
        }

        private void ChangeImages(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    editor.PatchImage(_ednaldoPereiraTexture, targetArea: WeatherReportArea);
                });
            }

            if (!e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors2")) return;
            e.Edit(asset =>
            {
                var editor = asset.AsImage();
                editor.PatchImage(_ednaldoIslandTexture, targetArea: IslandReportArea);
            });
        }

        private void OnSaveLoad(object sender, SaveLoadedEventArgs e)
        {
            _farmerName = Game1.player.Name;
            InvalidateAssets();
        }

        private void FarmerNameChanged(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;
            if (_farmerName == Game1.player.Name)
                return;
            _farmerName = Game1.player.Name;
            InvalidateAssets();
        }

        private void InvalidateAssets()
        {
            string currentLocale = Helper.GameContent.CurrentLocale != "" ? 
                "." + Helper.GameContent.CurrentLocale : "";
            
            Helper.GameContent.InvalidateCache("Strings/StringsFromCSFiles");
            Helper.GameContent.InvalidateCache("Strings/StringsFromCSFiles" + currentLocale);
        }
    }
}