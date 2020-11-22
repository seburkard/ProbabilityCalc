using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilityCalc
{
    class AnalyticCalculator : ICalculator
    {
        private RewardPool rewardPool;
        public AnalyticCalculator(RewardPool rewardPool)
        {
            this.rewardPool = rewardPool;
        }
        public double CalculateAverage()
        {
            int[] GoalAmounts = rewardPool.RewardItems.Select(t => t.Amount).ToArray();
            var list = new List<int[]>();
            list.Add(GoalAmounts);
            Layer.rarities = rewardPool.RewardItems.Select(t =>(double)t.Rarity).ToArray();
            Layer.Length = GoalAmounts.Length;
            Layer.weights = new Dictionary<int[], double>(new MyComparer());
            Layer.weights.Add(GoalAmounts, 0);
            var layer = new Layer(list);
            while (layer.GetSum() != 0)
            {
                layer = layer.NextLayer();
            }
            return (double) Layer.weights[new int[Layer.Length]];
        }

    }
    class Layer
    {
        public static double[] rarities;
        public static int Length;
        public static Dictionary<int[], double> weights;
        List<int[]> arrays;
        public Layer(List<int[]> arrays)
        {
            this.arrays = arrays;
        }
        public int GetSum()
        {
            return arrays[0].Sum();
        }
        public Layer NextLayer()
        {
            var hashset = new HashSet<int[]>(new MyComparer());
            for (int i = 0; i < arrays.Count; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (arrays[i][j] > 0)
                    {
                        int[] arr = new int[Length];
                        Array.Copy(arrays[i], arr, Length);
                        arr[j]--;
                        if (!hashset.Contains(arr))
                        {
                            hashset.Add(arr);
                            weights.Add(arr, GetWeight(arr));
                        }
                    }
                }
            }
            return new Layer(hashset.ToList());
        }
        public double GetWeight(int[] arr)
        {
            double c = 1;
            double p =1;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i]++;
                if (weights.ContainsKey(arr))
                {
                    p -= rarities[i];
                    c += rarities[i] * weights[arr];
                }
                arr[i]--;
            }
            return c / (1 - p);
        }
    }
}
