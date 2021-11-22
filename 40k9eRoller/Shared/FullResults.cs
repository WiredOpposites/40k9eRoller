using D20Tek.DiceNotation;

namespace _40k9eRoller.Shared
{
    public class FullResults
    {
        public DiceResult HitResult { get; set; }
        public DiceResult ReRollHitResult { get; set; }
        public DiceResult WoundResult { get; set; }
        public string HitRollResultString { get; set; }
        public string ReRollResultString { get; set; }
        public string WoundResultString { get; set; }
        public IDictionary<int, int> GroupedResults { get; set; }
        public IDictionary<int, int> ReRollGroupedResults { get; set; }
        public IDictionary<int, int> WoundGroupedResults { get; set; }

    }
}
