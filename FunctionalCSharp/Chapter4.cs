using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalCSharp
{
    using static LanguageExt.Prelude;

    static class Chapter4
    {
        // 1 Implement Map for ISet<T> and IDictionary<K, T>. (Tip: start by writing down
        // the signature in arrow notation.)
        static ISet<R> Map<T,R>(this ISet<T> ts, Func<T,R> f)
        {
            var rs = new System.Collections.Generic.HashSet<R>();
            foreach(var t in ts)
            {
                rs.Add(f(t));
            }

            return rs;

        }

        static IDictionary<K,R> Map<K, T,R>(this IDictionary<K,T> dict, Func<T,R> f)
        {
            var rd = new System.Collections.Generic.Dictionary<K,  R>();
            foreach  (var keypair in dict) 
            {
                rd[keypair.Key] = f(keypair.Value);
            }
            return rd;
        }

        // 2 Implement Map for Option and IEnumerable in terms of Bind and Return.
        public static Option<R> Map<T,R>(this Option<T> option, Func<T,R> f) => option.Bind(t => Some(f(t)));

        public static IEnumerable<R> Map<T,R>(this IEnumerable<T> ts, Func<T,R> f) => ts.Bind(t => List(f(t)));
        
       
        public static Option<T> Lookup<K,T>(this IDictionary<K,T> dict, K key)
        {
            T value;
            return dict.TryGetValue(key, out value)
                ? Some(value) : None;
        }

        // 3 Use Bind and an Option-returning Lookup function (such as the one we defined
        // in chapter 3) to implement GetWorkPermit, shown below. 

        // Then enrich the implementation so that `GetWorkPermit`
        // returns `None` if the work permit has expired.

        static Option<WorkPermit> GetWorkPermit(Dictionary<string, Employee> employees, string employeeId)
        {
            return employees.Lookup(employeeId).Bind(e => e.WorkPermit);
        }

        static Option<WorkPermit> GetNoneExpiredWorkPermit(Dictionary<string, Employee> employees, string employeeId)
        {
            return employees.Lookup(employeeId)
            .Bind(e => e.WorkPermit)
            .Where(e => e.Expiry > DateTime.Now.Date);
        }

        // 4 Use Bind to implement AverageYearsWorkedAtTheCompany, shown below (only
        // employees who have left should be included).

        static double AverageYearsWorkedAtTheCompany(List<Employee> employees)
        {
            // your implementation here...
            return employees.Bind(employee => employee.LeftOn.Map(leftOn => YearsBetween(employee.JoinedOn, leftOn))).Average();
        }

        static double YearsBetween(DateTime startDate, DateTime endDate) => (endDate - startDate).Days / 365d;
          
    }

    public struct WorkPermit
    {
        public string Number { get; set; }
        public DateTime Expiry { get; set; }
    }

    public class Employee
    {
        public string Id { get; set; }
        public Option<WorkPermit> WorkPermit { get; set; }

        public DateTime JoinedOn { get; }
        public Option<DateTime> LeftOn { get; }
    }
}
