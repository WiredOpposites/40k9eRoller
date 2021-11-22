using _40k9eRoller.Shared;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;

namespace _9eRollerTest
{
    internal static class HelperMethods
    {
        public static Dictionary<int, int> GroupResults(IEnumerable<TermResult> result)
        {
            Dictionary<int, int> groupedResults = new();
            for (int i = 1; i < 7; i++)
            {
                groupedResults[i] = result.Where(x => x.Value == i).Count();
            }
            return groupedResults;
        }
        public static Dictionary<int, int> GroupResults(IEnumerable<TermResult> result1, IEnumerable<TermResult> result2)
        {
            Dictionary<int, int> groupedResults = new();
            for (int i = 1; i < 7; i++)
            {
                groupedResults[i] = result1.Where(x => x.Value == i).Count() + result2.Where(x => x.Value == i).Count();

            }
            return groupedResults;
        }
        public static void PrintD6Results(IDictionary<int, int> groupedResults)
        {
            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine(i + ": " + groupedResults[i]);
            }
        }

        public static Dictionary<int, int> ReRoll(DiceResult results, Dictionary<int, int> groupedResults, out string rerollResultString, 
            bool full = false, int bs = 2)
        {
           Dictionary<int, int> newGroupedResult = new Dictionary<int, int>();
           if (!full)
           {
                int rr1s = groupedResults[1];
                if (rr1s > 0)
                { 
                    var d1Str = results.RollsDisplayText.Replace("1, ", "");
                    var d2Str = d1Str.Replace(", 1", "");
                    var dropped1s = results.Results.Where(x => x.Value != 1);

                    Console.WriteLine("Re-rolling 1's...");
                    IDice rr1Dice = new Dice();
                    rr1Dice.Dice(6, rr1s);
                    var rr1Result = rr1Dice.Roll(new RandomDieRoller());
                    rerollResultString = d2Str + ", " + rr1Result.RollsDisplayText;
                    newGroupedResult = GroupResults(dropped1s, rr1Result.Results);
                }
                else
                {
                    rerollResultString = results.RollsDisplayText;
                    return groupedResults;
                }
           }
           else
           {
                int numOfRerolls = 0;
                int i = 1;
                string droppedString = results.RollsDisplayText.Replace(i + ", ", "");
                droppedString = droppedString.Replace(", " + i, "");

                while (i < bs)
                {
                    numOfRerolls += groupedResults[i];
                    if (i != 1)
                    {
                        droppedString = droppedString.Replace(i + ", ", "");
                        droppedString = droppedString.Replace(", " + i, "");
                    }
                    i++;
                }
                if (numOfRerolls > 0)
                {
                    var droppedFails = results.Results.Where(x => x.Value >= bs);
                    
                    IDice rrDice = new Dice();
                    rrDice.Dice(6, numOfRerolls);
                    var rrResult = rrDice.Roll(new RandomDieRoller());
                    rerollResultString = droppedString + ", " + rrResult.RollsDisplayText;
                    newGroupedResult = GroupResults(droppedFails, rrResult.Results);
                }
                else
                {
                    rerollResultString = results.RollsDisplayText;
                    return groupedResults;
                }
            }
           return newGroupedResult;

        }

        public static HitsAndMisses CalcuateHitsAndMisses(Dictionary<int, int> groupedResults, int bs, bool explodes = false)
        {
            var ham = new HitsAndMisses();

            for (int m = 1; m < bs; m++)
                ham.Misses += groupedResults[m];
            for (int h = bs; h < 7; h++)
                ham.Hits += groupedResults[h];
            if (explodes)
                ham.Hits += groupedResults[6];
            return ham;
        }
    }
}
