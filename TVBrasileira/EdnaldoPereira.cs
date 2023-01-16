using StardewModdingAPI.Events;

namespace TVBrasileira
{
    public class EdnaldoPereira
    {
        public void alterarDialogos(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Strings/StringsFromCSFiles"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsDictionary<string, string>();
                    editor.Data["TV.cs.13105"] = I18n.EdnaldoPereira();
                    editor.Data["TV.cs.13136"] = I18n.InicioEdnaldo();
                    editor.Data["TV.cs.13175"] = I18n.Festival();
                    editor.Data["TV.cs.13180"] = I18n.Neve();
                    editor.Data["TV.cs.13181"] = I18n.BastanteNeve();
                    editor.Data["TV.cs.13182"] = I18n.DiaEnsolarado();
                    editor.Data["TV.cs.13183"] = I18n.DiaEnsolarado2();
                    editor.Data["TV.cs.13184"] = I18n.Chuva();
                    editor.Data["TV.cs.13185"] = I18n.Tempestade();
                    editor.Data["TV.cs.13187"] = I18n.Nublado();
                    editor.Data["TV.cs.13189"] = I18n.NubladoVento();
                    editor.Data["TV.cs.13190"] = I18n.Neve2();
                });
            }
        }
    }
}