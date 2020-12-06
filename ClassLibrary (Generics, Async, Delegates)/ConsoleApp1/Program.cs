using LibraryTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private delegate TResult Ayylmao<in T, out TResult>(T val);

        static async Task Main(string[] args)
        {
            int multicase = 1;

            switch (multicase)
            {
                case int n when (n >= 7):
                    Console.WriteLine($"I am 7 or above: {n}");
                    break;

                case int n when (n >= 4 && n <= 6):
                    Console.WriteLine($"I am between 4 and 6: {n}");
                    break;

                case int n when (n <= 3):
                    Console.WriteLine($"I am 3 or less: {n}");
                    break;
            }


            int[] xdar = { 1, 2, 4, 5, 6, 4, 2, 2 };
            int[] xd2 = new int[500];

            for (int i = 0; i < xdar.Length - 1; i++)
                xd2[i] = i;

            xdar.ToList().Add(5);
            xdar.ToArray();

            xdar.Max();
            //KEEP FORGETTING ABOUT THIS USING ARRAY INSTEAD
            Array.IndexOf(xdar, xdar.Max());


            //xdar.GroupBy(x => x.ToString());
            

            //Predicate
            Array.Find(xdar, (x) => x == 5);
            //Notice the method is called from Array and not xd (The array itself)


            //Long lambda wtf
            var enumb = from lol in xdar where lol == 5 select lol;

            List<string> xdlisteste1 = new List<string>();
            var xdlist = new List<int>() { 1, 3, 5, 6, 7, 8 };
            int[] chararraytest = { 1, 3, 5 };
            int[] charteste2 = new int[2];
            int[] chararray = new int[3] { 1, 3, 5 };
         
            await Task.Run(() => { Parallel.ForEach<int>(xdlist, val => Console.WriteLine($"{val} * {val}")); });

            xdlist.ForEach(val =>
            {
                Console.WriteLine($"{val} * {val}");
                Console.WriteLine("lmao");
            });

            //foreach (var item in xdlist)
            //{

            //}

            Console.WriteLine("Hello World!");
            string a = "";

            Console.WriteLine(Tools.EmptyCheck(a));

            Console.WriteLine(Tools.GenerateRandomID(12));


            //throw new Tools.NumeroBIException()


            for (int i = 0, c = i + 1; i < c; i++, c++)
            {
                //Could have just used one for instead
            }


            Func<int, double, string> xdlmao2lol = (int lmao, double xd) =>
              {
                  return "lol";
              };

            //var foo = 2; 
            //foo = foo / 2.5; 
            //foo = Math.Sqrt(foo); 
            //string bar = foo.ToString("c");


            dynamic foo2 = 2; // still returns a decimal
            foo2 = foo2 / 2.5; // The runtime takes care of this for us
            foo2 = Math.Sqrt(foo2); // Again, the DLR works its magic
            string bar2 = foo2.ToString();
            Console.WriteLine(bar2);

            Predicate<int> isequal;
            Func<int, int, int> returns;
            Action<double> lmao;
            lmao = x => Console.WriteLine(x);
            isequal = x => x == 4;
            returns = (x, y) => x * y;

            //HAS TO RETURN TASK

            //customdelegatetest = asynchronous method (takes in x) => { returns task of func<Bool> }
            xd<int, Task<bool>> customdelegateTest = async (int x) => 
            { 
                return await Task.Run(() => x == 2); 
            };
            
            //test name = x => Console.WriteLine($"{x}");
            Action<int> actioncopy = async (int x) => { await Task.Run(() => testfunc(x)); };


            try
            {
                //throw new Tools.NumeroBIException("test");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message} - {ex.StackTrace}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Console.WriteLine($"{inner.Message} - {inner.StackTrace}");
                    inner = inner.InnerException;
                }
                //throw;
            }


            #region hide
            int a2 = 2;
            int b = 5;
            Console.WriteLine($"a - {a2} - b - {b}");
            Tools.Swap<int>(ref a2, ref b);

            //Type can be infered by compiler
            Tools.Swap(ref a2, ref b);
            Console.WriteLine($"a - {a2} - b - {b}");

            #endregion

            Action EmptyAction = () => Console.WriteLine("Useless");

            Func<double, double, double> boggers = (a, b) => Math.Pow(a, b);
            //anonymous method/function being passed in instead of a defined method/function
            testdelegate(x => x * 4);


            testdelegate(testfunc);

            test xd = testfunc;

            //just a pointer
            xd(2);


            //public delegate TResult Ayylmao<in T, out TResult>(T val);
            //delegate TResult Ayylmao<in T, out TResult>(T val);
            Ayylmao<double, bool> xd123 = (double num) => num > 5;

            xd123(25.5);


  

            //this is not a delegate, you're trying to say that string x equals a lambda
            //just because it returns a string doesnt make it not a lambda

            //string x = (int i, double b) => { return "xd"; };

            Func<int, double, string> test520 = (int i, double b) => { return "xd"; };
            string x34 = test520(2,2.5);

    }

        public delegate TResult xd<T, TResult>(T a);
        public delegate bool testbool(int a, int b);
        public delegate bool testboolv2<T, T2>(T a, T2 b);
        private delegate TResult Ayylmao2<in T, out TResult>(T val);
        Ayylmao<double, bool> xd123 = (double num) => num > 5;


        public delegate int test(int a);
        // public delegate TResult xd<T, T2, T3, TResult>(T a, T2 b, T3 c);


        //Param is a delegate - the arguments are methods/functions now
        //lambdas are anonymous functions
        public static bool testdelegate(test xd)
        {
            xd(2);
            Console.WriteLine($"{xd(2)}");
            return true;
        }

        public static int testfunc(int a)
        {
            return a *= 2;
        }



        public delegate string nametry<T, U>(T value, U value2);

        nametry<double, int> lmaotaelgf = funtest;

        public static string funtest<T, U>(T value1, U value2)
        {
            return "re";
        }

       

        sealed class Xdtest
        {
            public readonly int _lmao = 2;
            //public xdTestinherit lmao { get; set; }

            public Xdtest()
            {
            }
            //public void fucntioner(xdTestinherit test)
            //{
            //    lmao = test;
            //}
        }

        //class xdTestinherit : Xdtest
        //{
        //    public int MyProperty { get; set; }
        //    public xdTestinherit()
        //    {
        //        base.fucntioner(this);
        //    }

        //    public xdTestinherit(xdTestinherit xd)
        //    {

        //    }
        //}

    }
}
