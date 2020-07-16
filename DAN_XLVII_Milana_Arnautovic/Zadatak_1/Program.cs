using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        public static Random random = new Random();
        public static List<string> cars = new List<string>();
        public static int a = random.Next(1, 16);

        public static void Bridge()
        {

        }

        public static void Directions()
        {
            string direction1 = "south";
            string direction2 = "north";
            string[] directions = { direction1, direction2 };
            int d = random.Next(0, 2);
            Console.WriteLine(Thread.CurrentThread.Name + " going in the direction: " + directions[d] + ".");


        }
        public static void TotalCars()
        {
            Console.WriteLine("The total number of cars crossing the bridge is " + a + ".\n");
        }
        
        static void Main(string[] args)
        {
            Thread total = new Thread(new ThreadStart(TotalCars));
            total.Start();
            total.Join();

            for (int i = 0; i < a; i++)
            {
                Thread thread = new Thread(Directions) //Creating threads
                {

                    Name = String.Format("Car_{0}", i + 1) //Naming threads
                };
                thread.Start();//Starting threads
                thread.Join();

            }
           
            Thread direc = new Thread(new ThreadStart(Bridge));
            direc.Start();
            direc.Join();


            

            Console.ReadLine();

        }
    }
}
