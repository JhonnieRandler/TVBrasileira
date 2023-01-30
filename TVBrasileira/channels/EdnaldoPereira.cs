using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using TVBrasileira.Framework;

namespace TVBrasileira.Channels
{
    public class EdnaldoPereira
    {
        private readonly IModHelper _helper;
        private bool _isChannelEnabled;
        private static readonly Rectangle WeatherReportArea = new(413, 305, 126, 28);
        private static readonly Rectangle IslandReportArea = new(148, 62, 42, 28);

        private static IRawTextureData _ednaldoPereiraTexture;
        private static IRawTextureData _ednaldoIslandTexture;

        private string _farmerName;
        
        public EdnaldoPereira(IModHelper helper)
        {
            _helper = helper;
            _ednaldoPereiraTexture =  _helper.ModContent.Load<IRawTextureData>("assets/ednaldoPereira.png");
            _ednaldoIslandTexture = _helper.ModContent.Load<IRawTextureData>("assets/ednaldoIsland.png");
            _helper.Events.Content.AssetRequested += ChangeDialogs;
            _helper.Events.Content.AssetRequested += ChangeImages;
            _helper.Events.GameLoop.SaveLoaded += OnSaveLoad;
        }

        private void ChangeDialogs(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Strings/StringsFromCSFiles"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    editor.Data["TV.cs.13105"] = I18n.TitleEdnaldo();
                    editor.Data["TV.cs.13136"] = I18n.DisabledEdnaldo(_farmerName);
                    editor.Data["TV.cs.13175"] = I18n.FestivalEdnaldo();
                    editor.Data["TV.cs.13180"] = I18n.SnowEdnaldo();
                    editor.Data["TV.cs.13181"] = I18n.AltSnowEdnaldo();
                    editor.Data["TV.cs.13182"] = I18n.SunnyEdnaldo();
                    editor.Data["TV.cs.13183"] = I18n.AltSunnyEdnaldo();
                    editor.Data["TV.cs.13184"] = I18n.RainEdnaldo();
                    editor.Data["TV.cs.13185"] = I18n.StormEdnaldo();
                    editor.Data["TV.cs.13187"] = I18n.CloudyEdnaldo();
                    editor.Data["TV.cs.13189"] = I18n.WindCloudyEdnaldo();
                    editor.Data["TV.cs.13190"] = I18n.BlizzardEdnaldo();
                    editor.Data["TV_IslandWeatherIntro"] = I18n.IslandEdnaldo();
                    _isChannelEnabled = _helper.ReadConfig<ModConfig>().EdnaldoPereiraToggle;
                    if (_isChannelEnabled)
                        editor.Data["TV.cs.13136"] = I18n.IntroEdnaldo();
                });
            }
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

            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors2"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    editor.PatchImage(_ednaldoIslandTexture, targetArea: IslandReportArea);
                });
            }
        }

        private void OnSaveLoad(object sender, SaveLoadedEventArgs e)
        {
            _farmerName = Game1.player.Name;
            
            string currentLocale = _helper.GameContent.CurrentLocale != "" ? 
                "." + _helper.GameContent.CurrentLocale : "";
            
            _helper.GameContent.InvalidateCache("Strings/StringsFromCSFiles");
            _helper.GameContent.InvalidateCache("Strings/StringsFromCSFiles" + currentLocale);
        }
    }
}