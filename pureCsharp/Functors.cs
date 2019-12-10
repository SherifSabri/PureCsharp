using pureCsharp.Monads;
using System;
using System.Collections.Generic;
using System.Text;

namespace pureCsharp
{
    public static class Functors
    {
        /// <summary>
        /// fmap implementation for List Monad === map
        /// (a -> b) -> f a -> f b
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="aList"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<B> FMap<A, B>(this IEnumerable<A> aList, Func<A, B> func)
            where A : IEnumerable<A>
        {
            foreach(var a in aList)
            {
                yield return func(a);
            }
        }

        /// <summary>
        /// fmap implementation for Maybe Monad
        /// (a -> b) -> f a -> f b
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="maybeA"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Maybe<B> FMap<A, B>(this Maybe<A> maybeA, Func<A, B> func)
            where A : Maybe<A>
        {
            return func(maybeA.JustValue).ReturnMaybe<B>();
        }

        /// <summary>
        /// fmap implementation for Either Monad
        /// (a -> b) -> f a -> f b
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="eitherA"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Either<B> FMap<A, B>(this Either<A> eitherA, Func<A, B> func)
        {
            return func(eitherA.Right).ReturnEither<B>();
        }
    }
}
