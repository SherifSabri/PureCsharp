using System;
using System.Collections.Generic;
using System.Text;

namespace pureCsharp.Monads
{
    public class Either<T>
    {
        /// <summary>
        /// The Correct Value
        /// </summary>
        public T Right { get; private set; }

        /// <summary>
        /// The Error Message
        /// </summary>
        public string Left { get; private set; }

        private Either()
        {

        }

        public Either(T value)
        {
            Right = value;
            Left = null;
        }

        public Either(string error, T value)
        {
            Right = value;
            Left = error;
        }

        public override string ToString()
        {
            if (Right != null)
            {
                return "Right " + Right.ToString();
            }
            else
            {
                throw new EitherExceptionError(Left);
            }
        }

    }

    public static class EitherExtension
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
        public static Either<B> Bind<A, B>(this Either<A> monadicValue, Func<A, Either<B>> func)
        {
            if(monadicValue != null)
            {
                try
                {
                    return func(monadicValue.Right);
                }
                catch(Exception e)
                {
                    throw new EitherExceptionError(e.Message);
                }
            }
            else
            {
                throw new EitherExceptionError(monadicValue.Left);
            }
        }

        /// <summary>
        /// The monad return operator
        /// a -> m a
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Either<A> ReturnEither<A>(this A value)
        {
            return new Either<A>(value);
        }
    }

    class EitherExceptionError : Exception
    {
        public EitherExceptionError(string error) 
            : base(string.Format($"Error {error}"))
        {
        }
    }
}
