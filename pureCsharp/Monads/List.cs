using System;
using System.Collections.Generic;
using System.Text;

namespace pureCsharp.Monads
{
    public class ListMonad<T> where T : IEnumerable<T>
    {
    }

    public static class ListExtensions
    {
        public static IEnumerable<B> Bind<A, B>(this IEnumerable<A> monadicValue, Func<A, IEnumerable<B>> func)
        {
            foreach(var v in monadicValue)
            {
                foreach(var b in func(v))
                {
                    yield return b;
                }
            }
        }
        public static IEnumerable<A> ReturnList<A>(this A value)
        {
            yield return value;
        }
    }
}
