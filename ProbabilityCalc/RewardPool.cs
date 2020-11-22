using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilityCalc
{
    class RewardPool
    {
        public List<RewardItem> RewardItems { get; private set; }
        public RewardPool()
        {
            Init();
        }
        public RewardPool(string[] itemStrings):this()
        {
            RewardItems.Capacity = itemStrings.Length;
            for(int i = 0; i < itemStrings.Length; i++)
            {
                RewardItems.Add(new RewardItem(itemStrings[i], i));
            }
        }
        public void PrintItems()
        {
            foreach(var rewardItem in RewardItems)
            {
                Console.WriteLine("Need {0} 'Item {1}' of rarity {2}", rewardItem.Amount,rewardItem.Index, rewardItem.Rarity);
            }
        }
        private void Init()
        {
            RewardItems = new List<RewardItem>();
        }
        public void AddItem(RewardItem item)
        {
            RewardItems.Add(item);
        }
    }
}
