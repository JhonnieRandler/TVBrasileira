using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace TVBrasileira.channels
{
    public class MarciaSensitiva : Channel
    {
        private static readonly Rectangle FortuneTellerArea = new(540, 305, 126, 28);
        
        private static IRawTextureData _marciaSensitivaTexture;
        
        private bool _playerDivorceTonight;
        private bool _isPierreBirthday;
        private bool _pierreBirthdayWasTrueYesterday;
        private bool _isReveillon;
        private bool _reveillonWasTrueYesterday;
        
        public MarciaSensitiva(IModHelper helper, IMonitor monitor) : base(helper, monitor)
        {
            _marciaSensitivaTexture = Helper.ModContent.Load<IRawTextureData>("assets/marciaSensitiva.png");
            
            TargetDialogueAssets = new List<string> { "Strings/StringsFromCSFiles" };
            TargetImageAssets = new List<string> { "LooseSprites/Cursors" };
            
            Helper.Events.GameLoop.UpdateTicked += CheckDivorce;
            Helper.Events.GameLoop.DayStarted += CheckPierreBirthday;
            Helper.Events.GameLoop.DayStarted += CheckReveillon;
            Helper.Events.Content.AssetRequested += CheckTargetDialogues;
            Helper.Events.Content.AssetRequested += CheckTargetImages;
        }

        protected override void SetCustomDialogues(IAssetDataForDictionary<string, string> editor, IAssetName assetName)
        {
            if (!IsChannelEnabled()) return;
            editor.Data["TV.cs.13107"] = I18n.TitleMarciaSensitiva();
            
            editor.Data["TV.cs.13128"] = I18n.MessyHouse();
            editor.Data["TV.cs.13130"] = I18n.Creature();
            editor.Data["TV.cs.13132"] = I18n.LittleDead();
            editor.Data["TV.cs.13133"] = I18n.SideralEngineers();
            editor.Data["TV.cs.13134"] = I18n.Karma();
            editor.Data["TV.cs.13135"] = I18n.SundayNight();

            editor.Data["TV.cs.13191"] = I18n.AngrySpirits();
            editor.Data["TV.cs.13192"] = I18n.UpsetSpirits();
            editor.Data["TV.cs.13193"] = I18n.DullSpirits();
            editor.Data["TV.cs.13195"] = I18n.RetrogradeMercury();
            editor.Data["TV.cs.13197"] = I18n.VeryLuckyDay();
            editor.Data["TV.cs.13198"] = I18n.LuckyDay();
            editor.Data["TV.cs.13199"] = I18n.GoodMoodSpirits();
            editor.Data["TV.cs.13200"] = I18n.NeutralSpirits();
            editor.Data["TV.cs.13201"] = I18n.AbsolutelyNeutralSpirits();
            
            if (_playerDivorceTonight)
            {
                editor.Data["TV.cs.13132"] = I18n.GrayAura();
                editor.Data["TV.cs.13133"] = I18n.GoAway();
                editor.Data["TV.cs.13134"] = I18n.Marriage();
                editor.Data["TV.cs.13135"] = I18n.Psalm66();
            } else if (_isPierreBirthday)
            {
                //editor.Data["TV.cs.13128"] = I18n.Disgrace();
                //editor.Data["TV.cs.13130"] = I18n.Twice();
                editor.Data["TV.cs.13132"] = I18n.BadPeople();
                editor.Data["TV.cs.13133"] = I18n.ScrewYouPierre();
                editor.Data["TV.cs.13134"] = I18n.WorldOfDarkness();
                editor.Data["TV.cs.13135"] = I18n.Envied();
            }
            else if (_isReveillon)
            {
                editor.Data["TV.cs.13132"] = I18n.EndOfYear();
                editor.Data["TV.cs.13133"] = I18n.Reveillon();
                editor.Data["TV.cs.13134"] = I18n.WakeUp();
                editor.Data["TV.cs.13135"] = I18n.NewYear();
            }
        }

        protected override void SetCustomImages(IAssetDataForImage editor, IAssetName assetName)
        {
            if (!IsChannelEnabled()) return;
            editor.PatchImage(_marciaSensitivaTexture, targetArea: FortuneTellerArea);
        }
        
        private void CheckDivorce(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;
            if (Game1.player.divorceTonight.Value == _playerDivorceTonight)
                return;
            _playerDivorceTonight = !_playerDivorceTonight;
            InvalidateDialogues();
        }
        
        private void CheckPierreBirthday(object sender, DayStartedEventArgs e)
        {
            _isPierreBirthday = Game1.currentSeason == "spring" && Game1.dayOfMonth == 26;
            
            var shouldInvalidateAssets = _isPierreBirthday || _pierreBirthdayWasTrueYesterday;
            if (shouldInvalidateAssets)
                InvalidateDialogues();

            _pierreBirthdayWasTrueYesterday = _isPierreBirthday;
        }

        private void CheckReveillon(object sender, DayStartedEventArgs e)
        {
            _isReveillon = Game1.currentSeason == "winter" && Game1.dayOfMonth == 28;
            
            var shouldInvalidateAssets = _isReveillon || _reveillonWasTrueYesterday;
            if (shouldInvalidateAssets)
                InvalidateDialogues();

            _reveillonWasTrueYesterday = _isReveillon;
        }
    }
}