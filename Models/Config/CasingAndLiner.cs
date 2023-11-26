namespace Models.Config
{
    public partial class CasingAndLiner
    {
        public int NumberOfGenerators { get; set; } = 4;
        public long GeneratorPowerRating { get; set; } = 1200;
        public long MudPump1 { get; set; } = 1600;
        public long MudPump2 { get; set; } = 1600;
        public long CementPump { get; set; } = 400;
        public long RotaryTable { get; set; } = 800;
        public long Drawworks { get; set; } = 2000;
        public long TopDrive { get; set; } = 1000;
    }
}