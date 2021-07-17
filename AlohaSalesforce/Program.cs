using AlohaSalesforce.Commands;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AlohaSalesforce
{
    public class Program
    {

        private static readonly Dictionary<string, Command> commands = new Dictionary<string, Command>()
        {
            { "LIST", new ListCommand() },
            { "DEPEND", new DependCommand() },
            { "INSTALL", new InstallCommand() },
            { "REMOVE", new RemoveCommand() }
        };

        public static void Main()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    input = Regex.Replace(input.Trim(), " +", " ");
                    if (input.Length > 80)
                    {
                        throw new ArgumentOutOfRangeException("Instruction longer than expected");
                    }
                    var instruction = input.Split(' ');
                    if (instruction[0] == "END")
                    {
                        Console.WriteLine(input);
                        break;
                    }
                    var command = commands.GetValueOrDefault(instruction[0]);
                    if (command is null)
                    {
                        throw new InvalidOperationException("Unknown command");
                    }
                    Console.WriteLine(command.Execute(instruction));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
