namespace TVBrasileira.Framework
{
    public class ModConfig
    {
        public bool Palmirinha { get; set; }
        public bool GloboRural { get; set; }
        public bool EdnaldoPereira { get; set; }

        public ModConfig()
        {
            this.EdnaldoPereira = true;
            this.Palmirinha = true;
            this.GloboRural = true;
        }
    }
}