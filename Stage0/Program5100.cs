using System;

partial class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Welcome5100();
        Welcome2542();

        Console.ReadKey();


    }
    static partial void Welcome2542();

    private static void Welcome5100()
    {
        Console.Write("Enter your name: ");
        string name = "";
        name = Console.ReadLine();
        Console.WriteLine(name + ",welcome to my first console");
    }
}