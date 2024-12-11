global using System.Reflection;
global using System.Text.RegularExpressions;

using System.Runtime.CompilerServices;


namespace AoC;

public class Program
{
    static void Main(string[] args)
    {
        //new _2015._2015().Start();

        new _2024._2024().Start();


        //Auto close + time to read results
        Task.Run(() => { Thread.Sleep(1000 * 60); Environment.Exit(0); });
        Console.ReadKey();
    }
}
