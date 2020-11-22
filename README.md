# ProbabilityCalc
Tool to solve a generalized version of the coupon collector problem. The problem is the following: Each box of cereal may contain one coupon. Now the question is: How many boxes do you need on average to get a given amount of a set of coupons if you know the rarity of each type of coupon. Example: If you want 2 coupons with odds 1/5, 1 of odds 1/3 and 1 of odds 1/20, you will on average need to open around 25.27 boxes.

It implements two algorithms:

Simple simulation that draws boxes till it completes the coupon set.
Markov-Chain approach that calculates probabilities between two states and then calculates the average from that. They are useful in different scenarios as simulation scales better with many different items and markov chain scales better with very low odds.
