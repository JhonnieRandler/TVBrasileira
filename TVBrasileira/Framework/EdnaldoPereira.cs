using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework
{
    public class EdnaldoPereira
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private ModConfig _config;
        
        public EdnaldoPereira(IModHelper helper, IMonitor monitor)
        {
            this._helper = helper;
            this._monitor = monitor;
            this._helper.Events.Content.AssetRequested += this.Logger;
            this._helper.Events.Content.AssetRequested += this.ChangeDialogs;
            this._helper.Events.Content.AssetRequested += this.ChangeImages;
        }
        
        private void Logger(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors"))
            {
                _monitor.Log("Asset Requested (LooseSprites/Cursors) event raised", LogLevel.Trace);
            }
        }
        
        private void ChangeDialogs(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Strings/StringsFromCSFiles"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    editor.Data["TV.cs.13105"] = I18n.TitleEdnaldo();
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
                    this._config = this._helper.ReadConfig<ModConfig>();
                    if (_config.EdnaldoPereiraToggle)
                    {
                        editor.Data["TV.cs.13136"] = I18n.IntroEdnaldo();
                    }
                    else {
                        editor.Data["TV.cs.13136"] = I18n.DisabledEdnaldo();
                    }
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
                    IRawTextureData ednaldoPereiraTexture =
                        this._helper.ModContent.Load<IRawTextureData>("assets/ednaldoPereira.png");
                    editor.PatchImage(ednaldoPereiraTexture, targetArea: new Rectangle(413, 305, 126, 28));
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors2"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    IRawTextureData eduAnttunesTextures =
                        this._helper.ModContent.Load<IRawTextureData>("assets/eduAnttunes.png");
                    editor.PatchImage(eduAnttunesTextures, targetArea: new Rectangle(148, 62, 42, 28));
                    _monitor.Log("Ednaldo's cursor", LogLevel.Trace);
                });
            }
        }
    }
}