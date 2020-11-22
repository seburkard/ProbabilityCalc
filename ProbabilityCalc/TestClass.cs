using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilityCalc
{
    static class TestClass
    {
        public static string[] TestSet1 = new string[]
        {
            "3,mc/sim",
            "1,5,3",
            "1,20,1",
            "1,3,1"
        };
        public static string[] TestSet2 = new string[]
        {
            "6,mc/sim 1000000",
            "1,3,1",
            "1,5,1",
            "1,7,1",
            "1,11,1",
            "1,13,1",
            "1,17,1"
        };
        public static string[] TestSet3 = new string[]
        {
            "6,mc/sim 10000",
            "1,3,100",
            "1,5,1",
            "1,7,1",
            "1,11111,200",
            "1,13,1",
            "1,17,1"
        };
        public static string[] TestSet4 = new string[]
        {
            "3,mc/sim 1000/sim 10000/sim 100000",
            "1,3,5",
            "1,5,3",
            "1,50,2",
            "1,100,1"
        };
    }
}
