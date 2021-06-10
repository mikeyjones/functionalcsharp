using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
    public static class Chapter1
    {

         // 1. Write a function that negates a given predicate: whenever the given predicate
         // evaluates to `true`, the resulting function evaluates to `false`, and vice versa.
         static Func<T, bool> Negative<T>(this Func<T, bool> predicate) => t => !predicate(t);

         // 2. Write a method that uses quicksort to sort a `List<int>` (return a new list,
         // rather than sorting it in place).

         // 3. Generalize your implementation to take a `List<T>`, and additionally a 
         // `Comparison<T>` delegate.

         // 4. In this chapter, you've seen a `Using` function that takes an `IDisposable`
         // and a function of type `Func<TDisp, R>`. Write an overload of `Using` that
         // takes a `Func<IDisposable>` as first
         // parameter, instead of the `IDisposable`. (This can be used to fix warnings
         // given by some code analysis tools about instantiating an `IDisposable` and
         // not disposing it.)

    }
}
