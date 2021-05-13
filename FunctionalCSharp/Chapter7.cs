using LaYumba.Functional;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

        public enum NumberType { Mobile, Home, Work }

        public class CountryCode
        {
            private string Value { get; }

            public CountryCode(string value)
            {
                Value = value;
            }

            public static implicit operator string(CountryCode c) => c.Value;
            public static implicit operator CountryCode(string s) => new CountryCode(s);

            public override string ToString() => Value;
        }

        public record PhoneNumber
        {
            public NumberType Type { get; init; }
            public CountryCode CountryCode { get; init; }
            public string Number { get; init; }


        }

        // Now define a ternary function that creates a new number, given values for these fields.
        // What's the signature of your factory function? 
        // NewPhoneNumber -> CountryCode -> NumberType -> string -> PhoneNumber
        public static Func<CountryCode, NumberType, string, PhoneNumber> NewPhoneNumber =
            (countryCode, numberType, number)
                => new PhoneNumber {CountryCode = countryCode, Type = numberType, Number = number};

        // Use partial application to create a binary function that creates a UK number, 
        // and then again to create a unary function that creates a UK mobile
        public static Func<NumberType, string, PhoneNumber> NewUkPhoneNumber =
            NewPhoneNumber.Apply((CountryCode) "UK");

        public static Func<string, PhoneNumber> NewUKMobileNumber =
            NewUkPhoneNumber.Apply(NumberType.Mobile);


        // 3. Functions everywhere. You may still have a feeling that objects are ultimately 
        // more powerful than functions. Surely, a logger object should expose methods 
        // for related operations such as Debug, Info, Error? 
        // To see that this is not necessarily so, challenge yourself to write 
        // a very simple logging mechanism without defining any classes or structs. 
        // You should still be able to inject a Log value into a consumer class/function, 
        // exposing operations like Debug, Info, and Error, like so:

        //static void ConsumeLog(Log log) 
        //   => log.Info("look! no objects!");

        class Unit
        {
        }

        static class ConsoleWriter
        {
            public static Unit WriteLine(string line)
            {
                Console.WriteLine(line);
                return new Unit(); 
            }

            public static Unit WriteLineToFile(string filename, string line)
            {
                System.IO.File.WriteAllLines(filename, new List<string> { line });
                return new Unit();
            }
        }

        enum Level { Debug, Info, Error }

        delegate Unit Log(Level level, string message);

        static Log ConsoleLogger = (Level level, string message) =>
            ConsoleWriter.WriteLine($"{DateTime.Now}[{level}] - {message}");

        static Log ToDelegate(this Func<Level, string, Unit> f)
        {
            Log myDeleg = (x,y) => f(x,y);
            return myDeleg;
        }


        private static Func<string, Level, string, Unit> FileLogger = (string filename, Level level, string message) =>
            ConsoleWriter.WriteLineToFile(filename,$"{DateTime.Now}[{level}] - {message}");

        private static Log FileLogger2 =
            FileLogger.Apply("log.txt").ToDelegate();
            //FileLogger.Apply("log.txt");

        //FileLogger2(Level.Debug, "rgrgr");

        //FileLogger (Level.Debug) ("rwgwrgrwgwr")

        static Unit Debug(this Log log, string message) => log(Level.Debug, message);
        static Unit Info(this Log log, string message) => log(Level.Info, message);
        static Unit Error(this Log log, string message) => log(Level.Error, message);

        public static void _main()
            => ConsumeLog(ConsoleLogger);

        static void ConsumeLog(Log log)
            => log.Info("this is an info message");



        //static Func<Log, string, PhoneNumber> NewLoggedUkPhoneNumber = (logger, number) =>

        //ConsoleLogger.Info("")





    }
}
