using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Examples.Chapter3;

namespace Examples.Chapter5
{
    public static class Exercises
    {
        // 1. Without looking at any code or documentation (or intllisense), write the function signatures of
        // `OrderByDescending`, `Take` and `Average`, which we used to implement `AverageEarningsOfRichestQuartile`:
        static decimal AverageEarningsOfRichestQuartile(List<Person> population)
           => population
              .OrderByDescending(p => p.Earnings)
              .Take(population.Count / 4)
              .Select(p => p.Earnings)
              .Average();

        // OrderByDescending: IEummerable<Person> -> (Person -> decimal) -> IEummerable<Person>
        // Take: IEummerable<Person> -> (int) -> IEummerable<Person>
        // Average: IEummerable<decimal> -> decimal

        // 2 Check your answer with the MSDN documentation: https://docs.microsoft.com/
        // en-us/dotnet/api/system.linq.enumerable. How is Average different?

        // 3 Implement a general purpose Compose function that takes two unary functions
        // and returns the composition of the two.
        public static Func<T1,R> Compose<T1,T2, R> (this Func<T2,R> g, Func<T1,T2> f)
        {
            return x => g(f(x));
        }


    }

    [TestFixture]
    public class Tests
    {
        

        [Test]
        public void something()
        {
            Func<int, int> add1()  => x => x + 1;
            Func<int, int> add10() => x => x + 10;

            var func1 = add1();
            var func2 = add10();

            var result = func1.Compose(func2);

            var result1 = func1(1);

            Assert.True(true);

            //.Compose(add10(1));
        }
    }
}
