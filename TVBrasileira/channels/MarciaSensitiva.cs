using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace TVBrasileira.channels
{
    public class MarciaSensitiva : Channel
    {
        private bool _playerDivorceTonight;
        private bool _isPierreBirthday;
        private bool _pierreBirthdayWasTrueYesterday;
        public MarciaSensitiva(IModHelper helper, IMonitor monitor) : base(helper, monitor)
        {
            TargetDialogueAssets = new List<string> { "Strings/StringsFromCSFiles" };
            TargetImageAssets = new List<string> { "LooseSprites/Cursors" };
            
            Helper.Events.GameLoop.UpdateTicked += CheckDivorce;
            Helper.Events.GameLoop.DayStarted += CheckPierreBirthday;
            Helper.Events.Content.AssetRequested += CheckTargetDialogues;
            Helper.Events.Content.AssetRequested += CheckTargetImages;
        }

        protected override void SetCustomDialogues(IAssetDataForDictionary<string, string> editor, IAssetName assetName)
        {
            if (!IsChannelEnabled()) return;
            editor.Data["TV.cs.13107"] = I18n.TitleMarciaSensitiva();
            if (_playerDivorceTonight)
            {
                string voaCara = "VOA, CARA, VOA!#Meu marido foi embora. VAI COM DEUS, MEU CHAPA! PRÓXIMO!";
                string essaSemana = "Essa semana, pessoal: Salmo 66, vamo tirar os capeta de dendicasa";
                string arrasada =
                    "Quer me ver arrasada é a mulherada casada: Ain, meu marido não presta.#VAI EMBORA. Fala do coitado, xinga o marido pra mim. Bem feito que tenha amante.";
                editor.Data["TV.cs.13128"] = editor.Data["TV.cs.13133"] = voaCara;
                editor.Data["TV.cs.13130"] = editor.Data["TV.cs.13134"] = essaSemana;
                editor.Data["TV.cs.13132"] = editor.Data["TV.cs.13135"] = arrasada;
            } else if (_isPierreBirthday)
            {
                string fuckPierre = "Eu ainda tenho uma pessoa... UMA PESSOA... nessa terra. Um dono de um armazém... Que eu não consigo. Que se eu encontrar com ele vai ser uns tapa na cara que não vai ter graça. E se eu encontrar ele no céu então eu vou jogar ele pro inferno.";
                editor.Data["TV.cs.13128"] = fuckPierre;
                editor.Data["TV.cs.13130"] = fuckPierre;
                editor.Data["TV.cs.13132"] = fuckPierre;
                editor.Data["TV.cs.13133"] = fuckPierre;
                editor.Data["TV.cs.13134"] = fuckPierre;
                editor.Data["TV.cs.13135"] = fuckPierre;
            }
            else
            {
                string marcia = "Olá, sou a Márcia Sensitiva";
                editor.Data["TV.cs.13128"] = marcia;
                editor.Data["TV.cs.13130"] = marcia;
                editor.Data["TV.cs.13132"] = marcia;
                editor.Data["TV.cs.13133"] = marcia;
                editor.Data["TV.cs.13134"] = marcia;
                editor.Data["TV.cs.13135"] = marcia;
            }
        }

        protected override void SetCustomImages(IAssetDataForImage editor, IAssetName assetName)
        {
            throw new System.NotImplementedException();
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
    }
}