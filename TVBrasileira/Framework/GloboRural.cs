using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace TVBrasileira.Framework
{
    public class GloboRural
    {
        private IModHelper _helper;
        
        public GloboRural(IModHelper helper)
        {
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
                    editor.Data["TV.cs.13111"] = I18n.TitleGloboRural();
                    editor.Data["TV.cs.13124"] = I18n.IntroGloboRural();
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Data/TV/TipChannel"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    editor.Data["1"] = I18n._1();
                    editor.Data["8"] = I18n._8();
                    editor.Data["15"] = I18n._15();
                    editor.Data["22"] = I18n._22();
                    editor.Data["29"] = I18n._29();
                    editor.Data["36"] = I18n._36();
                    editor.Data["43"] = I18n._43();
                    editor.Data["50"] = I18n._50();
                    editor.Data["57"] = I18n._57();
                    editor.Data["64"] = I18n._64();
                    editor.Data["71"] = I18n._71();
                    editor.Data["78"] = I18n._78();
                    editor.Data["85"] = I18n._85();
                    editor.Data["92"] = I18n._92();
                    editor.Data["99"] = I18n._99();
                    editor.Data["106"] = I18n._106();
                    editor.Data["113"] = I18n._113();
                    editor.Data["120"] = I18n._120();
                    editor.Data["127"] = I18n._127();
                    editor.Data["134"] = I18n._134();
                    editor.Data["141"] = I18n._141();
                    editor.Data["148"] = I18n._148();
                    editor.Data["155"] = I18n._155();
                    editor.Data["162"] = I18n._162();
                    editor.Data["169"] = I18n._169();
                    editor.Data["176"] = I18n._176();
                    editor.Data["183"] = I18n._183();
                    editor.Data["190"] = I18n._190();
                    editor.Data["197"] = I18n._197();
                    editor.Data["204"] = I18n._204();
                    editor.Data["211"] = I18n._211();
                    editor.Data["218"] = I18n._218();
                    editor.Data["221"] = I18n._221();
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
                    IRawTextureData globoruralpng =
                        _helper.ModContent.Load<IRawTextureData>("assets/globorural.png");
                    editor.PatchImage(globoruralpng, targetArea: new Rectangle(517, 361, 84, 28));
                });
            }
        }
    }
}