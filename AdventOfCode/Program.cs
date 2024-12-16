using System.Reflection;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) 
            { 
                Usage(); 
                return;
            }

            Run(args);
        }

        private static void Usage()
        {
            Console.WriteLine("Welcome to AdventOfCode. Please enter the following parameters in the specified format to run the challenge. ");
            Console.WriteLine("year day");
            Console.WriteLine("year day param1");
            Console.WriteLine("year day param1,param2");
        }

        private static void Run(string[] args)
        {
            try
            {
                // find and run the intended challenge dynamically
                var year = args[0];
                var day = args[1];
                var parameters = args.Length > 2 ? args[2..] : Array.Empty<string>();

                var typeName = $"AdventOfCode._{year}.Day{day}";

                var type = Type.GetType(typeName);
                
                if (type == null)
                {
                    Console.WriteLine($"Could not find type {typeName}");
                    return;
                }

                if (Activator.CreateInstance(type) is not IAdventChallenge challenge)
                {
                    Console.WriteLine("Challenge does not implement IAdventChallenge");
                    return;
                }

                // TODO: should make abstract instead of interface so we're not
                // having to define each challenge's Run function twice
                if (parameters.Length > 0)
                {
                    challenge.Run(parameters);
                }
                else
                {
                    challenge.Run();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured");
                Console.WriteLine(ex);
            }
        }
    }
}
