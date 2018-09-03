﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combination
{
    class Program
    {
        static void Main(string[] args)
        {


            IEnumerable<string> list = new List<string> {  "varoitus", "koulualue", "rajoitus", "tyhjä", "tyhjäpieni"  };
            var alist = GetCombinations(list,3);
            foreach (var lista in alist)
            {
                Console.WriteLine(String.Join(" ", lista));
            }

            Console.WriteLine("\n --------------- \n");
            List<string[]> lists = new List<string[]>()
            {
                new[]{"tyhjä", "varoitus"},
                new[]{"tyhjäpieni","koulualue"},
                new[]{"tyhjä", "rajoitus"},
            };

            var cp = lists.CartesianProduct();

            foreach (var line in cp)
            {
                Console.WriteLine(String.Join(" ", line));
            }
            Console.ReadKey();
        }

        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetCombinations(list, length - 1)
                .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }



    }




    public static partial class MyExtensions
        {
            //http://blogs.msdn.com/b/ericlippert/archive/2010/06/28/computing-a-cartesian-product-with-linq.aspx
            public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
            {
                // base case: 
                IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
                foreach (var sequence in sequences)
                {
                    var s = sequence; // don't close over the loop variable 
                                      // recursive case: use SelectMany to build the new product out of the old one 
                    result =
                        from seq in result
                        from item in s
                        select seq.Concat(new[] { item });
                }
                return result;
            }
        }
}