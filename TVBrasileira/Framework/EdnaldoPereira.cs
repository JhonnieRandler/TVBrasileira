using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework
{
    public class EdnaldoPereira
    {
        private IModHelper _helper;
        private bool _config;
        
        public EdnaldoPereira(IModHelper helper, bool config)
        {
            _config = config;
            _helper = helper;
            _helper.Events.Content.AssetRequested += AlterarDialogos;
            _helper.Events.Content.AssetRequested += AlterarImagens;
        }
        
        public void AlterarDialogos(object sender, AssetRequestedEventArgs e)
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
                    if (_config == false)
                    {
                        editor.Data["TV.cs.13136"] = I18n.DisabledEdnaldo();
                    }
                    else {
                        editor.Data["TV.cs.13136"] = I18n.IntroEdnaldo();
                    }
                });
            }
        }

        public void AlterarImagens(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    IRawTextureData ednaldopereirapng =
                        _helper.ModContent.Load<IRawTextureData>("assets/ednaldopereira.png");
                    editor.PatchImage(ednaldopereirapng, targetArea: new Rectangle(413, 305, 126, 28));
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("LooseSprites/Cursors2"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    IRawTextureData eduanttunespng =
                        _helper.ModContent.Load<IRawTextureData>("assets/eduanttunes.png");
                    editor.PatchImage(eduanttunespng, targetArea: new Rectangle(148, 62, 42, 28));
                });
            }
        }
    }
}