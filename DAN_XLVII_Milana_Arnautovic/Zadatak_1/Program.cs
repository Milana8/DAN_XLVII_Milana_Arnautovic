using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static Random random = new Random();
        static List<Thread> cars = new List<Thread>();
        static int a = random.Next(1, 16);
        static CountdownEvent countdown = new CountdownEvent(1);

        /// <summary>
        /// Direction comparison method
        /// </summary>
        /// <param name="dir1"></param>
        /// <param name="dir2"></param>
        /// <returns></returns>
        static bool CheckDirection(string dir1, string dir2)
        {
            if ((dir1.Contains("north") && (dir2.Contains("north")) || (dir1.Contains("south") && (dir2.Contains("south")))))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method of crossing the bridge
        /// </summary>
        /// <param name="thread"></param>
        public static void Bridge(Thread thread)
        {
            Console.WriteLine(thread.Name + " the car started to cross the bridge");
            Thread.Sleep(500);
            Console.WriteLine(thread.Name + " has crossed the bridge.");


        }
        /// <summary>
        ///Method for printing total cars
        /// </summary>
        public static void TotalCars()
        {
            Console.WriteLine("The total number of cars crossing the bridge is " + a + ".\n");
        }


        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Thread total = new Thread(new ThreadStart(TotalCars));
            total.Start();
            total.Join();

            string direction1 = "south";
            string direction2 = "north";
            string[] directions = { direction1, direction2 };

            for (int i = 0; i < a; i++)
            {
                int d = random.Next(0, 2);
                string carNum = (i + 1).ToString();

                Thread thread = new Thread(() => Bridge(Thread.CurrentThread))
                {

                    Name = string.Format("Car " + carNum + " going in the direction: " + directions[d] + ".")
                };
                cars.Add(thread);
            }
            foreach (Thread item in cars)
            {
                Console.WriteLine(item.Name);
            }

            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].Start();
                if ((i < cars.Count - 1) && CheckDirection(cars[i].Name, cars[i + 1].Name) == false) //different direction of movement

                {
                    //the car is waiting for the bridge to clear
                     Console.WriteLine(cars[i + 1].Name + " the car is waiting for the bridge to be free.", cars[i + 1].Name);
                    
                    cars[i].Join();

                }

                else if (cars[i] == cars[cars.Count - 1])
                {

                    cars[i].Join();
                    countdown.Signal();
                }
            }

            if (countdown.IsSet)
            {
                sw.Stop();
                Console.WriteLine("The application is running: " + sw.Elapsed.TotalMilliseconds + " milliseconds");
            }
            Console.ReadLine();
        }
    }
}





