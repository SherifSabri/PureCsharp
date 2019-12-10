using pureCsharp.Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static pureCsharp.HigherOrderFunctions;

namespace driver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing...");
            var ints = new List<int> { 1, 2, 3, 4, 5 };
            var ints1 = new List<int> { };
            var ints2 = new List<int> { 8, 12, 3, 24, 4 };
            var ints3 = new List<int> { };
            var strings = ints.Map((x) => x.ToString());
            var doubleStrings = ints.Map(x => x * x).Filter(x => x % 2 == 0).Map(x => x.ToString());
            //ints.ForEach(i => Console.WriteLine(i));
            //foreach(var v in doubleStrings)
            //{
            //    Console.WriteLine(v);
            //}

            var ll = ints2.TakeWhileTrue(x => x % 2 == 0);
            foreach (var i in ll)
            {
                Console.WriteLine(i);
            }
            Func<int, int> doubleX = x => x + x;
            Func<int, int> squareX = x => x * x;
            Func<int, int> tripleX = x => 3*x;

            Console.WriteLine(doubleX.Apply(squareX).Apply(tripleX)(3));   //prints 3 * 3 = 9 * 9 = 81 + 81 = 162
                                                                           //var l1= ints.Foldl((x, y) => x+y, 0);
                                                                           //var l21= ints2.Foldr((x, y) => x / y, 2);

            //var add3 = CurryOne<int>(x => x + 3);
            //var add2 = CurryOne<string>((x) => x + " Curried!");
            //Console.WriteLine(add3(2));
            //Console.WriteLine(add2("2 "));

            var justResult = 5.ReturnMaybe().Bind(x => (x + 5).ReturnMaybe()).Bind(x => (x * 5).ReturnMaybe());     // prints Just 50
            var justResultDifferent = new Maybe<int>(5).Bind(x => (x + 5).ReturnMaybe()).Bind(x => (x * 5).ReturnMaybe());     // prints Just 50
            var nothingResultDifferent = Maybe<int>.Nothing.Bind(x => (x + 5).ReturnMaybe()).Bind(x => (x * 5).ReturnMaybe());     // prints Nothing
            var nothingResult = 5.ReturnMaybe().Bind(x => (x + 5).ReturnMaybe()).Bind(x => (Maybe<int>.Nothing).ReturnMaybe());     // prints Nothing

            Console.WriteLine(6.ReturnMaybe().ToString());
            Console.WriteLine(justResult);
            Console.WriteLine(nothingResultDifferent);
            Console.WriteLine(justResultDifferent);
            Console.WriteLine(nothingResult);



            var rightResult = 5.ReturnEither().Bind(x => (x + 5).ReturnEither()).Bind(x => (x * 5).ReturnEither());     // prints Right 50
            var rightResultDifferent = new Either<int>(5).Bind(x => (x + 5).ReturnEither()).Bind(x => (x * 5).ReturnEither());     // prints Right 50
            var leftResultDifferent = new Either<int?>("someError", 5).Bind(x => (x / 0).ReturnEither()).Bind(x => (x * 5).ReturnEither());     // error
            var leftResult = 5.ReturnEither().Bind(x => (x + 5).ReturnEither()).Bind(x => new Either<int?>(null).ReturnEither());     // error

            Console.WriteLine(6.ReturnEither().ToString());
            Console.WriteLine(rightResult);
            Console.WriteLine(rightResultDifferent);
            Console.WriteLine(leftResultDifferent);
            Console.WriteLine(leftResult);


        }

    }
}
