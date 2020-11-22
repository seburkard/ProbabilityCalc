using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilityCalc
{
    class RewardItem
    {
        public int Index { get; set; }
        public Fraction Rarity { get; set; }
        public int Amount { get; set; }
        //input of the form index,numerator,denominator,amount
        public RewardItem(string itemstring, int index)
        {
            var nums = Array.ConvertAll(itemstring.Split(','), t => Convert.ToInt32(t));
            Index = index;
            Rarity = new Fraction(nums[0], nums[1]);
            Amount = nums[2];
        }
    }
}
