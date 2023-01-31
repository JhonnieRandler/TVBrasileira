using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using TVBrasileira.frameworks;

namespace TVBrasileira.channels
{
    public class MarciaSensitiva
    {
        private readonly IModHelper _helper;
        private bool _isChannelEnabled;
        private bool _playerDivorceTonight;
        private bool _pierresBirthday;
        public MarciaSensitiva(IModHelper helper)
        {
            _helper = helper;
            _helper.Events.Content.AssetRequested += ChangeDialogs;
            _helper.Events.GameLoop.UpdateTicked += DivorceCheck;
            _helper.Events.GameLoop.DayStarted += PierresBirthdayCheck;
        }

        private void ChangeDialogs(object sender, AssetRequestedEventArgs e)
        {
            _isChannelEnabled = _helper.ReadConfig<ModConfig>().SensitiveMarciaToggle;
            if (!_isChannelEnabled) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Strings/StringsFromCSFiles"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    
                    editor.Data["TV.cs.13107"] = I18n.TitleSensitiveMarcia();
                    if (_playerDivorceTonight)
                    {
                        string voaCara = "VOA, CARA, VOA!#Meu marido foi embora. VAI COM DEUS, MEU CHAPA! PRÓXIMO!";
                        string essaSemana = "Essa semana, pessoal: Salmo 66, vamo tirar os capeta de dendicasa";
                        string arrasada =
                            "Quer me ver arrasada é a mulherada casada: Ain, meu marido não presta.#VAI EMBORA. Fala do coitado, xinga o marido pra mim. Bem feito que tenha amante.";
                        editor.Data["TV.cs.13128"] = editor.Data["TV.cs.13133"] = voaCara;
                        editor.Data["TV.cs.13130"] = editor.Data["TV.cs.13134"] = essaSemana;
                        editor.Data["TV.cs.13132"] = editor.Data["TV.cs.13135"] = arrasada;
                    } else if (_pierresBirthday)
                    {
                        string fuckPierre = "Eu ainda tenho uma pessoa... UMA PESSOA... nessa terra. Um dono de um armazém. Que eu não consigo. Que se eu encontrar com ele vai ser uns tapa na cara que não vai ter graça. E se eu encontrar ele no céu então eu vou jogar ele pro inferno.";
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
                });
            }
        }

        private void DivorceCheck(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;
            if (Game1.player.divorceTonight.Value == _playerDivorceTonight)
                return;
            _playerDivorceTonight = !_playerDivorceTonight;
            InvalidateAssets();
        }
        
        private void PierresBirthdayCheck(object sender, DayStartedEventArgs e)
        {
            if (Game1.currentSeason == "spring" && Game1.dayOfMonth == 26)
            {
                _pierresBirthday = true;
                InvalidateAssets();
            }
            else
            {
                if(!_pierresBirthday)
                    return;
                _pierresBirthday = false;
                InvalidateAssets();
            }
        }

        private void InvalidateAssets()
        {
            string currentLocale = _helper.GameContent.CurrentLocale != "" ? 
                "." + _helper.GameContent.CurrentLocale : "";
            _helper.GameContent.InvalidateCache("Strings/StringsFromCSFiles");
            _helper.GameContent.InvalidateCache("Strings/StringsFromCSFiles" + currentLocale);
        }
    }
}