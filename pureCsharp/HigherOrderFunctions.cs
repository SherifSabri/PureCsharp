using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pureCsharp
{
    public static class HigherOrderFunctions
    {
        public static IEnumerable<B> Map<A, B>(this IEnumerable<A> aList, Func<A, B> func) 
        {
            foreach(var a in aList)
            {
                yield return func(a);
            }
        }

        public static IEnumerable<A> Filter<A>(this IEnumerable<A> aList, Func<A, bool> predicate)
        {
            foreach (var a in aList)
            {
                if (predicate(a))
                {
                    yield return a;
                }
            }
        }

        public static IEnumerable<A> TakeWhileTrue<A>(this IEnumerable<A> aList, Func<A, bool> predicate)
        {
            foreach (var a in aList)
            {
                if (!predicate(a))
                {
                    break;
                }
                else
                {
                    yield return a;
                }
            }
        } 

        public static A Foldl<A, B>(this IEnumerable<B> bList, Func<A, B, A> func, A acc)
        {
            if (bList == null || !bList.Any())
                return acc;
            return Foldl(bList.Tail(), func, func(acc, bList.Head()));
        }

        public static B Foldr<A, B>(this IEnumerable<A> aList, Func<A, B, B> func, B acc)
        {
            if (aList == null || !aList.Any())
                return acc;
            return func(aList.Head(), Foldr(aList.Tail(), func, acc));
        }

        private static T Head<T>(this IEnumerable<T> _list)
        {
            return _list.First();
        }  
        
        private static IEnumerable<T> Tail<T>(this IEnumerable<T> _list)
        {
            return _list.Skip(1);
        }

        public static Func<T, T> CurryOne<T>(Func<T, T> func)
        {
            return (y => func(y));
        }

        /// <summary>
        /// Function Application/Composition
        /// f(g(x)) == f.Apply(g)(x)
        /// </summary>
        /// <returns></returns>
        public static Func<T, TResult> Apply<T, U, TResult>(this Func<U, TResult> funcF, Func<T, U> FuncG)
        {
            return (x => funcF(FuncG(x)));
        }
    }
}
