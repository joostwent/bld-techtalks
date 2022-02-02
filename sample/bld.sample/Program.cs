using bld.sample.Context;
using bld.sample.Model;
using System;

namespace bld.sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Booting");
            bool keepLooping = true;
            Console.CancelKeyPress += (_, ea) =>
            {
                keepLooping = false;
                Console.WriteLine("Terminating...");
            };
            var dbContext = new TickContext();
            dbContext.Database.EnsureCreated();
            while (keepLooping)
            {
                var tick = new Tick();
                dbContext.Ticks.Add(tick);
                dbContext.SaveChanges();
                Console.WriteLine($"Writting tick {tick.TickTime:u}");
                System.Threading.Thread.Sleep(7500);
            }
            Console.WriteLine("Terminated");
        }
    }
}