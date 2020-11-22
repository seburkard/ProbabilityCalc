using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ProbabilityCalc
{
    class Program
    {
        const int defaultNSimulations = 10000;
        static void Main(string[] args)
        {
            /*
            var parameters = Console.ReadLine().Split(',');
            int t = Convert.ToInt32(parameters[0]);
            var lines = new string[t];
            for(int i = 0; i < t; i++)
            {
                lines[i] = Console.ReadLine();
            }
            FindAverage(parameters[1], lines);
            */
            FindAverage(TestClass.TestSet1, true);
        }
        static void FindAverage(string[] inputs, bool printItems=false)
        {
            string[] lines = new string[inputs.Length - 1];
            Array.Copy(inputs, 1, lines, 0, lines.Length);
            FindAverage(inputs[0].Split(',')[1], lines, printItems);
        }
        static void FindAverage(string parameters, string[] lines, bool printItems)
        {
            if (parameters.Split('/').Length > 1)
            {
                foreach(var par in parameters.Split('/'))
                {
                    FindAverage(par, lines, printItems);
                }
                return;
            }
            var rewardPool = new RewardPool(lines);
            if (printItems)
            {
                rewardPool.PrintItems();
            }
            ICalculator calculator; 
            var sw = new Stopwatch();
            string calcText = "";
            string outputText="";
            if (parameters.StartsWith("mc"))
            {
                calculator = new AnalyticCalculator(rewardPool);
                calcText = "Trying to solve markov-chain";
                outputText = "Solving markov-chain took {0} ms";
            }
            else { 
                if(parameters.Split(' ').Length == 2)
                {
                    SimulationCalculator.Tries = Convert.ToInt32(parameters.Split(' ')[1]);
                }
                else
                {
                    SimulationCalculator.Tries = defaultNSimulations;
                }
                calcText = "Simulating until "+SimulationCalculator.Tries+" completions";
                calculator = new SimulationCalculator(rewardPool);
                outputText = "Simulation of " + SimulationCalculator.Tries + " completions took {0} ms";
            }
            Console.WriteLine(calcText);
            sw.Start();
            var result = calculator.CalculateAverage();
            sw.Stop();
            Console.WriteLine("On average needed {0:F2} tries", result);
            Console.WriteLine(outputText, sw.ElapsedMilliseconds);
        }
    }
}
