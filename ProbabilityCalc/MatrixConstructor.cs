using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ProbabilityCalc
{
    public class MyComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x.Length != y.Length) return false;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i]) return false;
            }
            return true;
        }

        public int GetHashCode(int[] arr)
        {
            int hc = arr.Length;
            for (int i = 0; i < arr.Length; i++)
            {
                hc = unchecked(hc * 117 + arr[i]);
            }
            return hc;
        }
    }
    static class MatrixConstructor
    {
        private static List<int> rows;
        private static List<int> cols;
        private static List<double> vals;
        public static Matrix<double> CreateMatrix(RewardPool rewardPool)
        {
            rows = new List<int>();
            cols = new List<int>();
            vals = new List<double>();
            var table = new Dictionary<int[], int>(new MyComparer());
            var goal = rewardPool.RewardItems.Select(t => t.Amount).ToArray();
            var probs = rewardPool.RewardItems.Select(t =>(double)t.Rarity).ToArray();
            int len = goal.Length;
            var list = new List<int[]>();
            int index = 0;
            list.Add(goal);
            table.Add(goal, index++);
            NewValue(table[goal], table[goal], 1);
            while (list[0].Sum() != 0)
            {
                var newlist = new HashSet<int[]>(new MyComparer());
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < len; j++)
                    {
                        if (list[i][j] > 0)
                        {
                            int[] arr = new int[len];
                            Array.Copy(list[i], arr, len);
                            arr[j]--;
                            if (!newlist.Contains(arr))
                            {
                                newlist.Add(arr);
                                table.Add(arr, index++);
                                NewValue(table[arr], table[arr], 1 - probs[j]);
                                NewValue(table[arr], table[list[i]], probs[j]);
                            }
                            else
                            {
                                AddToValue(table[arr], table[arr], -probs[j]);
                                NewValue(table[arr], table[list[i]], probs[j]);
                            }
                        }
                    }
                }
                list = newlist.ToList();
            }
            return GetMatrix();
        }
        private static  void NewValue(int row, int col, double value)
        {
            rows.Add(row);
            cols.Add(col);
            vals.Add(value);
        }
        private static void AddToValue(int row, int col, double value)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i] == row && cols[i] == col)
                {
                    vals[i] += value;
                }
            }
        }
        private static Matrix<double> GetMatrix()
        {
            int max = Math.Max(rows.Max(), cols.Max());
            var result = Matrix.Build.Sparse(max+1, max+1);
            for (int i = 0; i < vals.Count; i++)
            {
                result[rows[i], cols[i]] = (double)vals[i];
            }
            return result;
        }
    }
}
