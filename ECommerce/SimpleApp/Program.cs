using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleApp
{

    public class Program
    {
        enum Alphabets { A = 1, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z }
        private static int myClassVariable = 5;
        static void Main(string[] args)
        {
            while (true)
            {
                var quotients = new List<int>();

                Console.WriteLine("Enter a number");

                var input = Console.ReadLine();
                
                int number;
                if (int.TryParse(input, out number))
                {
                    ConvertToExcelRowNo(ref quotients, number);

                    quotients.ForEach(elm =>
                    Console.Write(Enum.GetName(typeof(Alphabets), elm)));

                    Console.ReadLine();
                }

            }
            

            //playlist(songs, 2, "a");
            //idFoundInArray();
            //leftShiftArray();
            // playLinq();
            //A myObject = new A();
            // myObject.ReadProperty();
            //tryOtherExample();
        }
        //public Enum Albhabets = { "A", "B"};


        static void ConvertToExcelRowNo(ref List<int> quotients, int number)
        {            
            while (number > 26)
            {
                quotients.Add(number / 26);
                while (quotients.Last() > 26)
                {
                    ConvertToExcelRowNo(ref quotients, quotients.Last());
                }
                number = number % 26;
            }
            quotients.Add(number);
        }

        static void basicTaskExample()
        {

            var t1 = new Task<int>(() =>
            {
                return M1(myClassVariable);
            });

            var t2 = new Task<int>(() =>
            {
                return M2();
            });

            t1.Start();
            t2.Start();

            Console.WriteLine("Check point 1");
            Task.WaitAll(t1, t2);
            Console.WriteLine("Check point 2");

            Console.WriteLine($"{t1.Result} - {t2.Result}");
            Console.ReadLine();
        }
        static void BasicAction()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Method=alpha, Thread={0}", Thread.CurrentThread.ManagedThreadId);
        }
        static int M1(int a)
        {
            for (int i = 0; a > 0 && i < 5; i++)
            {
                Thread.Sleep(200);
            }

            Console.WriteLine("method 1");

            return 100;
        }

        static int M2()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(300);
            }

            Console.WriteLine("method 2");

            return 200;
        }

        static void tryOtherExample()
        {
            Task[] tasks = new Task[2];
            Stopwatch sw = null;
            double timeSpan2 = 0;
            double timeSpan1 = 0;
            double synchronousTimeSpan = 0;
            for (int i = 0; i < 50; i++)
            {
                sw = Stopwatch.StartNew();
                Parallel.Invoke(() => Thread.Sleep(100), () => Thread.Sleep(100));

                sw.Stop();
                timeSpan1 += sw.Elapsed.TotalMilliseconds;

            }
            Console.WriteLine("Parallel invoke results: " + timeSpan1 / 50);


            for (int i = 0; i < 50; i++)
            {
                sw = Stopwatch.StartNew();

                tasks[0] = Task.Factory.StartNew(() => Thread.Sleep(100));
                tasks[1] = Task.Factory.StartNew(() => Thread.Sleep(100));

                Task.WaitAll(tasks);
                sw.Stop();
                timeSpan2 += sw.Elapsed.TotalMilliseconds;

            }
            Console.WriteLine("Task wait all results: " + timeSpan2);
            for (int i = 0; i < 50; i++)
            {
                sw = Stopwatch.StartNew();
                Thread.Sleep(200);
                sw.Stop();
                synchronousTimeSpan += sw.Elapsed.TotalMilliseconds;

            }
            Console.WriteLine("synchronousTimeSpan all results: " + synchronousTimeSpan);
            Console.ReadLine();
        }

        static void simpleParallelExample()
        {
            try
            {
                Parallel.Invoke(
                    BasicAction,	// Param #0 - static method
                    () =>			// Param #1 - lambda expression
                    {
                        Console.WriteLine("Method=beta, Thread={0}", Thread.CurrentThread.ManagedThreadId);
                    },
                    delegate ()		// Param #2 - in-line delegate
                    {
                        Console.WriteLine("Method=gamma, Thread={0}", Thread.CurrentThread.ManagedThreadId);
                    }
                );
                Console.ReadLine();
            }
            // No exception is expected in this example, but if one is still thrown from a task,
            // it will be wrapped in AggregateException and propagated to the main thread.
            catch (AggregateException e)
            {
                Console.WriteLine("An action has thrown an exception. THIS WAS UNEXPECTED.\n{0}", e.InnerException.ToString());
            }
        }

        static void playLinq()
        {
            var persons = new List<Person> {
                new Person
                {
                    FirstName = "Aashish",
                    LastName = "Kumar"
                },
                new Person
                {
                    FirstName = "Rahul",
                    LastName = "Abhishek"
                }
            };

            var pets = new List<Pet> {
                new Pet
                {
                    Name ="Catoo",
                    Owner = new Person{FirstName = "Aashish",LastName ="Kumar" }
                },
                new Pet
                {
                    Name ="Mithhoo",
                    Owner = new Person{FirstName = "Aashish",LastName ="Kumar" }
                },
                new Pet
                {
                    Name ="Pussy",
                    Owner = new Person { FirstName = "Halua", LastName ="Abhishek"}
                }
            };

            var query = from person in persons
                        join pet in pets on
                        new { FirstName = person.FirstName, LastName = person.LastName }
                        equals new { FirstName = pet.Owner.FirstName, LastName = pet.Owner.LastName } into gj
                        from subpet in gj.DefaultIfEmpty()
                        select new { person.FirstName, PetName = (subpet == null ? person.HasNoPets : subpet.Name) };

            Debug.WriteLine(query);
            Console.ReadLine();

        }

        private static void leftShiftArray()
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int size = Convert.ToInt32(tokens_n[0]);// size of array
            int shiftBy = Convert.ToInt32(tokens_n[1]);// shift by k cells left 
            string[] a_temp = Console.ReadLine().Split(' ');
            int[] array = Array.ConvertAll(a_temp, Int32.Parse);

            int[] temp = new int[shiftBy];
            Array.Copy(array, temp, shiftBy);
            for (int startingIndex = 0, runningIndex = shiftBy; runningIndex < size; runningIndex++, startingIndex++)
            {
                array[startingIndex] = array[runningIndex];
            }
            Array.Copy(temp, 0, array, size - shiftBy, shiftBy);

            string[] shiftedArray = Array.ConvertAll(array, x => x.ToString());

            Console.WriteLine(string.Join(" ", shiftedArray));
            Console.ReadLine();
        }

        private static void idFoundInArray()
        {

            string value;
            //bool toDelete = false;

            // Check whether the environment variable exists.
            value = Environment.GetEnvironmentVariable("Test1");
            // If necessary, create it.
            if (value == null)
            {
                Environment.SetEnvironmentVariable("Test1", "Value1");
                //toDelete = true;               
            }
            // Display the value.
            Console.WriteLine("Test1: {0}\n", value);


            string fileName = System.Environment.GetEnvironmentVariable("Test1");
            TextWriter tw = new StreamWriter(@fileName, true);
            string res;

            int _arr_size = 0;
            _arr_size = Convert.ToInt32(Console.ReadLine());
            int[] _arr = new int[_arr_size];
            int _arr_item;
            for (int _arr_i = 0; _arr_i < _arr_size; _arr_i++)
            {
                _arr_item = Convert.ToInt32(Console.ReadLine());
                _arr[_arr_i] = _arr_item;
            }

            int _k;
            _k = Convert.ToInt32(Console.ReadLine());
            res = findNumber(_arr, _k);
            tw.WriteLine(res);
            Console.WriteLine(res);
            Console.Read();
            Environment.SetEnvironmentVariable("Test1", null);
            tw.Flush();
            tw.Close();

        }
        private static string findNumber(int[] arr, int n)
        {



            bool isFound = Array.IndexOf(arr, n) > -1;
            return isFound ? "Yes" : "No";
        }
        static int playlist(string[] songs, int k, string q)
        {


            int result = -1;
            for (int i = 0; i <= k; i++)
            {
                if (songs[k - i].Equals(q) || songs[k + i].Equals(q))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

    }
    public class A
    {
        public int MyPropertyA { get; set; } = 1;
        public virtual void ReadProperty()
        {
            Console.WriteLine("Property value is (A):" + MyPropertyA);
        }
    }
    public class B : A
    {
        public int MyPropertyB { get; set; } = 2;
        public override void ReadProperty()
        {
            Console.WriteLine("Property value is (B):" + MyPropertyB);
        }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HasNoPets { get; set; } = "has no pets";
    }

    public class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }
}