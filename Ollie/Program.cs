/*
 * Description
Ollie is new to the gym and is figuring out the maximum weights she can lift.
The maximum capacity of the barbell is given as maxCapacity.
There are n barbell plates in the gym.The weight of thei-thbarbell piece is weights [i].
Now Ollie has to select as many plates as she can but the total weight of the selected
plates should not exceed maxCapacity. 
What is the maximum weight of plates Ollie can add to the barbell?

For example, given barbell plates of weights of 1, 3 and 5 lbs and a barbell of maximum
capacity 7 lbs - the right plates to insert 
would be 1 and 5 lbs (1 + 5 = 6), thus making the right answer 6.

1 <= n <= 42
1 <= maxCapacity <= 10^6
1 <= weights[i] <= 10^6

 

Example
Example 1

Input：
[1,3,5]
7
Output：
6
*/

using System.Collections;

namespace Ollie
{
    public class Weights
    {
        private List<int> _barBellPlates;
        private int _maxWeight { get; init; }
        private int currentMaxWeight;

        public Weights(List<int> weightList, int maxWeight)
        {
            _barBellPlates = weightList;
            _maxWeight = maxWeight;
        }

        public int Calculate()
        {
            PrepareWeightPlates();

            if(_barBellPlates.Count==1)
                return _barBellPlates.First();

            foreach (var permutation in GeneratePowerSet())
            {
                int sum = permutation.Sum();

                if (sum < _maxWeight && sum > currentMaxWeight)
                    currentMaxWeight = sum;

                else if (sum == _maxWeight)
                    return sum;
            }

            return currentMaxWeight;
        }

        private IEnumerable<IEnumerable<int>> GeneratePowerSet()
        {
            int count = _barBellPlates.Count;
            long power = (long)Math.Pow(2, count);
            for (long i = 0; i<=power;++i)
            {
                var set = new List<int>();
                int[] table = { (int)i, 0 };
                BitArray bits = new(table);

                for (int j =0;j< count; ++j)
                {
                    if (bits.Get(j))
                    { 
                        set.Add(_barBellPlates[j]); 
                    }
                }
                yield return set;
            }
        }

        private void PrepareWeightPlates()
        {
            _barBellPlates.RemoveAll(x => x > _maxWeight);
        }
    }


    public class Program
    {
        public static int Main(string[] args)
        {
            //int capacity = 7;
            //List<int> weightList = new() { 1, 3, 5 };

            int capacity = 8;
            List<int> weightList = new() { 1, 3, 4, 7, 10, 11 };

            Weights weights = new Weights(weightList, capacity);
            return weights.Calculate();
        }
    }
}