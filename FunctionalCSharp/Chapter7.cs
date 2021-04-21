using LaYumba.Functional;
using NUnit.Framework;
using System;

namespace Exercises.Chapter7
{
    public static class Exercises
    {
        // 1. Partial application with a binary arithmethic function:
        // Write a function `Remainder`, that calculates the remainder of 
        // integer division(and works for negative input values!).
        public static int Remainder(int demoninator, int numerator)
        {
            Math.DivRem(numerator, demoninator, out int result);
            return result;
        }

        public static Func<int, int, int> Remainder2 = (dividend, divisor)
         => dividend - ((dividend / divisor) * divisor);



        // Notice how the expected order of parameters is not the
        // one that is most likely to be required by partial application
        // (you are more likely to partially apply the divisor).

        // Write an `ApplyR` function, that gives the rightmost parameter to
        // a given binary function (try to write it without looking at the implementation for `Apply`).
        // Write the signature of `ApplyR` in arrow notation, both in curried and non-curried form
        static Func<T1, R> ApplyR<T1,T2,R>(this Func<T1, T2, R> func, T2 t2)
            => t1 => func(t1, t2);


        // Use `ApplyR` to create a function that returns the
        // remainder of dividing any number by 5. 
        static Func<int, int> RemainderBy5 = Remainder2.ApplyR(5);

        // Write an overload of `ApplyR` that gives the rightmost argument to a ternary function
        static Func<T1, T2, R> ApplyR<T1, T2, T3, R>(this Func<T1,T2, T3, R> func, T3 t3)
            => (t1,t2) => func(t1, t2, t3);


        // 2. Let's move on to ternary functions. Define a class `PhoneNumber` with 3
        // fields: number type(home, mobile, ...), country code('it', 'uk', ...), and number.
        // `CountryCode` should be a custom type with implicit conversion to and from string.

        // Now define a ternary function that creates a new number, given values for these fields.
        // What's the signature of your factory function? 

        // Use partial application to create a binary function that creates a UK number, 
        // and then again to create a unary function that creates a UK mobile


        // 3. Functions everywhere. You may still have a feeling that objects are ultimately 
        // more powerful than functions. Surely, a logger object should expose methods 
        // for related operations such as Debug, Info, Error? 
        // To see that this is not necessarily so, challenge yourself to write 
        // a very simple logging mechanism without defining any classes or structs. 
        // You should still be able to inject a Log value into a consumer class/function, 
        // exposing operations like Debug, Info, and Error, like so:

        //static void ConsumeLog(Log log) 
        //   => log.Info("look! no objects!");

        enum Level { Debug, Info, Error }
    }
}
