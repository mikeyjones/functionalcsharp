using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;
using Examples.Chapter3;

namespace FunctionalCSharp
{
    static class Exercises
    {
        // 1. Write a `ToOption` extension method to convert an `Either` into an
        // `Option`. Then write a `ToEither` method to convert an `Option` into an
        // `Either`, with a suitable parameter that can be invoked to obtain the
        // appropriate `Left` value, if the `Option` is `None`. (Tip: start by writing
        // the function signatures in arrow notation)
        static Option<R> ToOption<L,R>(this Either<L,R> either) => either.Match(left => None, right => Some(right));

        static Either<L,R> ToEither<L,R>(this Option<R> option, Func<L> left)
            => option.Match<Either<L, R>>(() => left(), right => right);



        // 2. Take a workflow where 2 or more functions that return an `Option`
        // are chained using `Bind`.
        static Option<Age> ParseAge(string s)
            => Int.Parse(s).Bind(Age.Of);

        static Either<string, Age> TryParseAge(string age, string error)
            => ParseAge(age).ToEither(() => error);
   

        // Then change the first one of the functions to return an `Either`.

        // This should cause compilation to fail. Since `Either` can be
        // converted into an `Option` as we have done in the previous exercise,
        // write extension overloads for `Bind`, so that
        // functions returning `Either` and `Option` can be chained with `Bind`,
        // yielding an `Option`.


        // 3. Write a function `Safely` of type ((() → R), (Exception → L)) → Either<L, R> that will
        // run the given function in a `try/catch`, returning an appropriately
        // populated `Either`.
        static Either<L,R> Safely<L,R>(Func<R> func, Func<Exception, L> func2)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                return Left(func2(e));
            }
        }

        static void test()
        {
            var test12 = Safely(() => "hello", ex => ex.Message);
        }

        // 4. Write a function `Try` of type (() → T) → Exceptional<T> that will
        // run the given function in a `try/catch`, returning an appropriately
        // populated `Exceptional`.
        static Exceptional<T> Try<T>(Func<T> func)
        {
            try { return func(); }
            catch (Exception e) { return e;  }
        }
    }
}
