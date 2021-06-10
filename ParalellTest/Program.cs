using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ParallelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var limit = 10_000_000;
            var numbers = Enumerable.Range(0, limit).ToList();

            var watch = Stopwatch.StartNew();
            var primenumbersFromForeach = GetPrimeList(numbers);
            watch.Stop();

            Console.WriteLine($"Classical foreach loop | Total prime numbers: {primenumbersFromForeach.Count} | Time: {watch.ElapsedMilliseconds} ");

            var watch2 = Stopwatch.StartNew();
            var primenumbersFromForeach2 = GetPrimeList2(numbers);
            watch.Stop();

            Console.WriteLine($"Classical foreach loop2 | Total prime numbers: {primenumbersFromForeach2.Count} | Time: {watch2.ElapsedMilliseconds} ");

            var watchForParallel = Stopwatch.StartNew();
            var primenumbersFromParallelForeach = GetPrimeListWithParallel(numbers);
            watch.Stop();

            Console.WriteLine($"Parallel.Foreach loop | Total prime numbers: {primenumbersFromParallelForeach.Count} | Time: {watchForParallel.ElapsedMilliseconds} ");

            var watchForParallel4 = Stopwatch.StartNew();
            var primenumbersFromParallelForeach4 = GetPrimeListWithParallel4(numbers);
            watch.Stop();

            Console.WriteLine($"Parallel.Foreach loop4 | Total prime numbers: {primenumbersFromParallelForeach4.Count} | Time: {watchForParallel4.ElapsedMilliseconds} ");

            var watchForParallel8 = Stopwatch.StartNew();
            var primenumbersFromParallelForeach8 = GetPrimeListWithParallel8(numbers);
            watch.Stop();

            Console.WriteLine($"Parallel.Foreach loop8 | Total prime numbers: {primenumbersFromParallelForeach8.Count} | Time: {watchForParallel8.ElapsedMilliseconds} ");

            var watchForParallel16 = Stopwatch.StartNew();
            var primenumbersFromParallelForeach16 = GetPrimeListWithParallel16(numbers);
            watch.Stop();

            Console.WriteLine($"Parallel.Foreach loop16 | Total prime numbers: {primenumbersFromParallelForeach16.Count} | Time: {watchForParallel16.ElapsedMilliseconds} ");

            var watchForParallel32 = Stopwatch.StartNew();
            var primenumbersFromParallelForeach32 = GetPrimeListWithParallel32(numbers);
            watch.Stop();

            Console.WriteLine($"Parallel.Foreach loop32 | Total prime numbers: {primenumbersFromParallelForeach32.Count} | Time: {watchForParallel32.ElapsedMilliseconds} ");

        }
        private static IList<int> GetPrimeList(IList<int> numbers) => numbers.Where(IsPrime).ToList();
        private static IList<int> GetPrimeList2(IList<int> numbers)
        {
            var primenumbers = new List<int>();
            foreach (var n in numbers)
            {
                if (IsPrime(n))
                {
                    primenumbers.Add(n);
                }
            }

            return primenumbers.ToList();
        }
        private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primenumbers = new ConcurrentBag<int>();
            Parallel.ForEach(numbers, number =>
            {
                if (IsPrime(number))
                {
                    primenumbers.Add(number);
                }
            });
            return primenumbers.ToList();
        }
        private static IList<int> GetPrimeListWithParallel4(IList<int> numbers)
        {
            var primenumbers = new ConcurrentBag<int>();
            Parallel.ForEach(
                numbers,
                new ParallelOptions { MaxDegreeOfParallelism = 4},
                number =>
            {
                if (IsPrime(number))
                {
                    primenumbers.Add(number);
                }
            });
            return primenumbers.ToList();
        }

        private static IList<int> GetPrimeListWithParallel8(IList<int> numbers)
        {
            var primenumbers = new ConcurrentBag<int>();
            Parallel.ForEach(
                numbers,
                new ParallelOptions { MaxDegreeOfParallelism = 8 },
                number =>
                {
                    if (IsPrime(number))
                    {
                        primenumbers.Add(number);
                    }
                });
            return primenumbers.ToList();
        }

        private static IList<int> GetPrimeListWithParallel16(IList<int> numbers)
        {
            var primenumbers = new ConcurrentBag<int>();
            Parallel.ForEach(
                numbers,
                new ParallelOptions { MaxDegreeOfParallelism = 16 },
                number =>
                {
                    if (IsPrime(number))
                    {
                        primenumbers.Add(number);
                    }
                });
            return primenumbers.ToList();
        }

        private static IList<int> GetPrimeListWithParallel32(IList<int> numbers)
        {
            var primenumbers = new ConcurrentBag<int>();
            Parallel.ForEach(
                numbers,
                new ParallelOptions { MaxDegreeOfParallelism = 32 },
                number =>
                {
                    if (IsPrime(number))
                    {
                        primenumbers.Add(number);
                    }
                });
            return primenumbers.ToList();
        }

        /// <summary>
        /// IsPrime returns true if number is Prime, else false.(https://en.wikipedia.org/wiki/Prime_number)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
