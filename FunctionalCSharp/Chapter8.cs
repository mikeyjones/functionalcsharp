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



        //=> eitherF.Match(
        //    leftF => Left(leftF),
        //    rightF => eitherT.Match(
        //        leftT => Left(leftT),
        //        rightT => Right(rightF(rightT))
        //    ));


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

            Func<string, (int, bool)> ParseAge = (x) => GetAge2(x);

            Either<string, Func<string,(int,bool)>> GetAge(string ageStr)
            {
                //return Right(ParseAge(ageStr));
         

            }

            

            (int, bool) GetAge2(string ageStr)
            {
                var result = int.TryParse(ageStr, out int value);
                return (value, result);
            }

            //var temp = (string age) => GetAge(age);

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

            

            GetAge("13").Apply(AgeGreaterThan10);
        }
    }
}
