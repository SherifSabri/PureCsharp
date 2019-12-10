using System;
using System.Collections.Generic;
using System.Text;

namespace pureCsharp.Monads
{
    public class Maybe<T>
    {
        private T value;

        /// <summary>
        /// The Empty/null Maybe Monad
        /// </summary>
        public static readonly Maybe<T> Nothing = new Maybe<T>();
        public bool nothing { get; private set; }

        private Maybe()
        {
            nothing = true;
        }
        public Maybe(T value)
        {
            if (value != null)
            {
                this.value = value;
            }
            else
            {
                nothing = true;
            }
        }

        /// <summary>
        /// The underlying contained value of the Maybe Monad
        /// </summary>
        public T JustValue
        {
            get
            {
                if (nothing)
                {
                    throw new Exception("Illegal Operation, No Just value");
                }
                else
                {
                    return value;
                }
            }
        }

        public override string ToString()
        {
            if (nothing || JustValue.ToString() == "Nothing")
            {
                return "Nothing";
            }
            else
            {
                return "Just " + JustValue.ToString();
            }
        }

    }

    public static class MaybeExtension
    {
        /// <summary>
        /// The monad bind (>>=) operator
        ///  m a -> (a -> m b) -> m b
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="monadicValue"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Maybe<B> Bind<A, B>(this Maybe<A> monadicValue, Func<A, Maybe<B>> func)
        {
            if (monadicValue != null && !monadicValue.nothing)
            {
                return func(monadicValue.JustValue);
            }
            else
            {
                return Maybe<B>.Nothing;
            }
        }

        /// <summary>
        /// The monad return operator
        /// a -> m a
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<A> ReturnMaybe<A>(this A value)
        {
            return new Maybe<A>(value);
        }

    }    
}
