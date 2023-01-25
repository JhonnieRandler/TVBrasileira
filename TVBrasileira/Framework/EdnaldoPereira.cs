using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework
{
    public class EdnaldoPereira
    {
        private readonly IModHelper helper;
        private ModConfig config;
        
        public EdnaldoPereira(IModHelper helper)
        {
            this.helper = helper;
            this.helper.Events.Content.AssetRequested += this.ChangeDialogs;
            this.helper.Events.Content.AssetRequested += this.ChangeImages;
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
                    this.config = this.helper.ReadConfig<ModConfig>();
                    if (this.config.EdnaldoPereiraToggle)
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
                        this.helper.ModContent.Load<IRawTextureData>("assets/ednaldoPereira.png");
                    editor.PatchImage(ednaldoPereiraTexture, targetArea: new Rectangle(413, 305, 126, 28));
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors2"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    IRawTextureData eduAnttunesTextures =
                        this.helper.ModContent.Load<IRawTextureData>("assets/eduAnttunes.png");
                    editor.PatchImage(eduAnttunesTextures, targetArea: new Rectangle(148, 62, 42, 28));
                });
            }
        }
    }
}