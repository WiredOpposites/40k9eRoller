namespace _40k9eRoller.Shared
{
    public class RollRequest
    {
        public int HitRolls { get; set; }
        public int ToHit { get; set; }
        public int ToWound { get; set; }
        public bool Reroll1 { get; set; }
        public bool FullReroll { get; set; }
        public bool Explodes { get; set; }
    }
}
