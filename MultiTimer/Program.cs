using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiTimer
{
    class Program
    {
        static Dictionary<char, TimedStream> Mappings = new Dictionary<char, TimedStream>();
        private static char LastCharEntered = 'a';

        static void Main(string[] args)
        {
            ParseFileToPopulateMappings();

            TimerLoop();

            WriteOutputFile();
        }

        private static void WriteOutputFile()
        {
            // Man I'm not even bothering to write this out to a file...
            Console.Clear();
            foreach (var entry in Mappings)
            {
                Console.WriteLine(entry.Key + " - " + (entry.Value.Name + ":").PadRight(20) + "\t" + entry.Value.Timer.Elapsed);
            }

            Console.WriteLine("   Will exit now - copy what you need!");

            Console.ReadLine();
        }

        private static void TimerLoop()
        {
            while (LastCharEntered != 'x')
            {
                Console.WriteLine();

                foreach (var entry in Mappings)
                {
                    Console.WriteLine(entry.Key + " - " + (entry.Value.Name + ":").PadRight(20) + "\t" + entry.Value.Timer.Elapsed);
                }

                Console.WriteLine("... or press `x` to exit and write out results.");

                LastCharEntered = Console.ReadKey().KeyChar;

                foreach (var entry in Mappings)
                {
                    if (entry.Key != LastCharEntered)
                    {
                        entry.Value.Timer.Stop();
                    }
                    else
                    {
                        entry.Value.Timer.Start();
                    }
                }
            }
        }

        private static void ParseFileToPopulateMappings()
        {
            // Cheating - not actually parsing a file at all! I'm lazy today.
            Mappings.Add('1', new TimedStream("Elizabeth Warren"));
            Mappings.Add('2', new TimedStream("Beto O'Rourke"));
            Mappings.Add('3', new TimedStream("Cory Booker"));
            Mappings.Add('4', new TimedStream("Julian Castro"));
            Mappings.Add('5', new TimedStream("Tulsi Gabbard"));
            Mappings.Add('6', new TimedStream("Jay Inslee"));
            Mappings.Add('7', new TimedStream("Amy Klobuchar"));
            Mappings.Add('8', new TimedStream("Bill de Blasio"));
            Mappings.Add('9', new TimedStream("John Delaney"));
            Mappings.Add('0', new TimedStream("Tim Ryan"));
            Mappings.Add('m', new TimedStream("Moderator"));
        }
    }

    internal class TimedStream
    {
        public string Name { get; set; }
        public Stopwatch Timer { get; set; }

        public TimedStream(string name)
        {
            Name = name;
            Timer = new Stopwatch();
        }
    }
}
