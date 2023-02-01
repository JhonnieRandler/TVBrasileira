using StardewModdingAPI;
using TVBrasileira.frameworks;

namespace TVBrasileira.channels
{
    public abstract class Channel
    {
        protected readonly IModHelper Helper;
        protected readonly IMonitor Monitor;

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

        /// <summary>
        /// Assigns values to the string keys in the editor dictionary based on the class that calls the method.
        /// </summary>
        /// <param name="asset">The asset data to be edited.</param>
        /// <param name="assetName">The asset to be edited</param>
        protected void AssignChannelStrings(IAssetData asset, IAssetName assetName)
        {
            var editor = asset.AsDictionary<string, string>();
            switch (GetType().Name)
            {
                case "Palmirinha":
                    editor.Data["TV.cs.13114"] = I18n.TitlePalmirinha();
                    editor.Data["TV.cs.13117"] = I18n.RerunPalmirinha();
                    editor.Data["TV.cs.13127"] = I18n.IntroPalmirinha();
                    editor.Data["TV.cs.13151"] = I18n.LearnedPalmirinha();
                    editor.Data["TV.cs.13153"] = I18n.OutroPalmirinha();
                    break;
                case "EdnaldoPereira":
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
                    break;
                case "GloboRural" when assetName.IsEquivalentTo("Strings/StringsFromCSFiles"):
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
                    break;
                case "GloboRural" when assetName.IsEquivalentTo("Data/TV/TipChannel"):
                    editor.Data["TV.cs.13111"] = I18n.TitleGloboRural();
                    editor.Data["TV.cs.13124"] = I18n.IntroGloboRural();
                    break;
            }
        }
    }
}