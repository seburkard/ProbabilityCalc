using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilityCalc
{
    class SimulationCalculator :  ICalculator
    {
        public static int Tries { get; set; }
        private RewardPool rewardPool;
        private int totalSlots;
        public SimulationCalculator(RewardPool rewardPool)
        {
            this.rewardPool = rewardPool;
            Fraction product = new Fraction(1, 1);
            foreach(var rewardItem in rewardPool.RewardItems)
            {
                product *= rewardItem.Rarity;
            }
            totalSlots =(int) product.Denominator;
        }
        public double CalculateAverage()
        {
            Random rand = new Random();
            int[] GoalAmounts = rewardPool.RewardItems.Select(t => t.Amount).ToArray();

            //Construct the rolltable corresponding to the rarity of items
            int[] rolls = new int[totalSlots];
            int a = 0, b = 0;
            foreach (var item in rewardPool.RewardItems)
            {
                b =(int) (a + item.Rarity.Numerator * totalSlots / item.Rarity.Denominator);
                for (int i = a; i < b; i++)
                {
                    rolls[i] = item.Index;
                }
                a = b;
            }
            for (int i = a; i < totalSlots; i++)
            {
                rolls[i] = -1;
            }
            int length = GoalAmounts.Length;
            int[] results = new int[Tries];
            //Simulate rolls till all the items are collected <=> needed is empty
            for (int i = 0; i < Tries; i++)
            {
                int[] needed = new int[length];
                Array.Copy(GoalAmounts, needed, length);
                while (!AllEmpty(needed, length))
                {
                    int next = rand.Next(0, totalSlots);
                    if (next < a)
                    {
                        needed[rolls[next]]--;
                    }
                    results[i]++;
                }
            }
            return results.Average();
        }
        private static bool AllEmpty(int[] a, int l)
        {
            for (int i = 0; i < l; i++)
            {
                if (a[i] > 0) return false;
            }
            return true;
        }
    }
}
