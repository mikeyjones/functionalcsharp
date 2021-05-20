using System;
using LaYumba.Functional;
using NUnit.Framework;

namespace FunctionalCSharp.Chapter8
{
    using static LaYumba.Functional.F;

    public static class Exercise
    {
        // 1. Implement Apply for Either and Exceptional.
        public static Either<L, RR> Apply<L, R, RR>(this Either<L, Func<R, Either<L, RR>>> eitherF,
            Either<L, R> eitherT)
            => eitherT.Bind(t => eitherF.Bind<L, Func<R, Either<L, RR>>, RR>(f => f(t)));

        public static Either<L, RR> Apply2<L, R, RR>(this Either<L, RR> eitherF,
            Either<L, R> eitherT)
            => new Either<L, RR>();

        public static Either<L, R> Apply3<L, R, RR>(this Either<L, RR> eitherF,
            Func<RR, Either<L, R>> f)
        {
            return eitherF.Match(
                Left: l => Left(l),
                Right: right => f(right));
        }

        public static Exceptional<T> Apply<T, TT>(this Exceptional<TT> eitherF,
            Func<TT, Exceptional<T>> f)
        {
            return eitherF.Match(
                Exception: l => l,
                Success:
                right =>
                {
                    return f(right);
                });
        }

        public static Either<L, RR> SelectMany<L, R, RR>(this Either<L, R> eitherL, Func<R, Either<L,RR>> project)
        {
            return eitherL.Match(
                Left: l => Left(l),
                Right: r => project(r));
        }


        // 2. Implement the query pattern for Either and Exceptional. Try to write down the signatures for Select and SelectMany without looking at the examples
        //    for the implementation, just follow the types-fi it type checks, it's probably right!

        // 3. Come up with a scenario in which various Either-returning operations are chained with Bind. (If you're short of ideas, you can use the favorite-dish
        //    example for chapter 6.) Rewrite the code using a LINQ expression.
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void HowDoesThisWork()
        {

            Either<string, int> GetAge(string ageStr)
            {
                if (int.TryParse(ageStr, out var value))
                {
                    return Right(value);
                }
                else
                {
                    return Left("Some error");
                }
         

            }

            Either<string, int> AgeGreaterThan10(int age)
            {
                if (age > 9)
                {
                    return Right(age);
                }
                else
                {
                    return Left("age has to be 10 or higher");
                }
            }

            Func<int, Either<string, int>> AgeGreaterThan10_2 =
                (age) => age > 9 ? Right(age) : Left("age has to be 10 or higher");




            var result = GetAge("9").Apply3(AgeGreaterThan10_2);

            Assert.True(true);
        }


        [Test]
        public void HowDoesThisAlsoWork()
        {

            Exceptional<int> GetAge(string ageStr)
            {
                if (int.TryParse(ageStr, out var value))
                {
                    return value;
                }
                else
                {
                    return new Exception("Some error");
                }


            }


            Func<int, Exceptional<int>> AgeGreaterThan10 =
                (age) => age > 9 ? age : new Exception("age has to be 10 or higher");




            var result = GetAge("9").Apply(AgeGreaterThan10);

            Assert.True(true);
        }

        [Test]
        public void HowDoesThisAlsoWorkAswell()
        {

            Either<string, int> GetAge(string ageStr)
            {
                if (int.TryParse(ageStr, out var value))
                {
                    return value;
                }
                else
                {
                    return "Some error";
                }


            }


            Func<int, Either<string, int>> AgeGreaterThan10 =
                (age) => age > 9 ? age : "age has to be 10 or higher";




            var result = GetAge("9").SelectMany((age) =>
            {
                return (age > 9) ? Right(age) : Left("age has to be 10 or higher");
            });

            Assert.True(true);
        }
    }
}
