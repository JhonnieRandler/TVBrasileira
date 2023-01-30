namespace TVBrasileira.Framework
{
    public class ModConfig
    {
        public bool PalmirinhaToggle { get; set; }
        public bool GloboRuralToggle { get; set; }
        public bool EdnaldoPereiraToggle { get; set; }

        public ModConfig()
        {
            EdnaldoPereiraToggle = true;
            PalmirinhaToggle = true;
            GloboRuralToggle = true;
        }
    }
}