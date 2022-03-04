using System.Text;
using Cocona;

namespace EasyCli;

public class Program
{
    public static void Main(string[] args)
    {
        // Create app builder and build application class from Cocona library
        // For more information about builder pattern see
        // https://refactoring.guru/design-patterns/builder/csharp/example
        var builder = CoconaApp.CreateBuilder();
        var app = builder.Build();

        // Configure main command running lambda expression when cli executed without any commands/subcommands
        // For more information about lambda expressions see
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
        app.AddCommand(() => Console.WriteLine("Welcome to EasyCli! use -h | --help for more information about usage"));

        // Configure "calc" subcommands with Cocona library using lambda statement
        // For more information about lambda statements see
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
        app.AddSubCommand("calc", commandsBuilder =>
        {
            // Configure "add" addition subcommand using lamdba statement
            commandsBuilder.AddCommand("add", (
                // Define parameters as argument with name and description (default is option) using attribute
                // For more information about attributes see
                // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/
                // For more information about Cocona arguments and options see
                // https://github.com/mayuki/Cocona#options
                // https://github.com/mayuki/Cocona#arguments
                [Argument(Name = "Augend", Description = "Augend")]
                double x,
                [Argument(Name = "Addend", Description = "Addend")]
                double y) =>
            {
                // Use class from Calculator.cs file to calculate output
                var calculator = new Calculator();
                var output = calculator.Add(x, y);

                Console.WriteLine($"{x.ToExpressionNumber()} + {y.ToExpressionNumber()} = {output.ToExpressionNumber()}");
            }).WithDescription("Addition of two numbers"); // Set subcommand description

            // Configure "sum" addition subcommand using lamdba statement
            commandsBuilder.AddCommand("sum", (
                // Define parameters as argument with name and description (default is option) using attribute
                [Argument(Name = "numbers", Description = "Numbers to sum")]
                double[] numbers) =>
            {
                // Use class from Calculator.cs file to calculate output
                var calculator = new Calculator();
                var output = calculator.Add(numbers);

                // Use StringBuilder class for better optimized string concatenation (from 5 strings and up)
                // For more information about StringBuilder see
                // https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder
                var stringBuilder = new StringBuilder();
                // Append first number to string builder
                stringBuilder.Append($"{numbers[0]}");
                for (var i = 1; i < numbers.Length; i++)
                {
                    // Append addition expression to string builder
                    stringBuilder.Append($" + {numbers[i].ToExpressionNumber()}");
                }
                // Append final output to string builder
                stringBuilder.Append($" = {output.ToExpressionNumber()}");

                Console.WriteLine(stringBuilder.ToString());
            }).WithDescription("Sum all passed numbers"); // Set subcommand description

            // Configure "sub" subtraction subcommand using lamdba statement
            commandsBuilder.AddCommand("sub", (
                // Define parameters as argument with name and description (default is option) using attribute
                [Argument(Name = "Minuend", Description = "Minuend")]
                double x,
                [Argument(Name = "Subtrahend", Description = "Subtrahend")]
                double y) =>
            {
                // Use class from Calculator.cs file to calculate output
                var calculator = new Calculator();
                var output = calculator.Subtract(x, y);

                Console.WriteLine($"{x.ToExpressionNumber()} - {y.ToExpressionNumber()} = {output.ToExpressionNumber()}");
            }).WithDescription("Subtraction of two numbers"); // Set subcommand description

            // Configure "mul" multiplication subcommand using lamdba statement
            commandsBuilder.AddCommand("mul", (
                // Define parameters as argument with name and description (default is option) using attribute
                [Argument(Name = "Multiplier", Description = "Multiplier")]
                double x,
                [Argument(Name = "Multiplicand", Description = "Multiplicand")]
                double y) =>
            {
                // Use class from Calculator.cs file to calculate output
                var calculator = new Calculator();
                var output = calculator.Multiply(x, y);

                Console.WriteLine($"{x.ToExpressionNumber()} * {y.ToExpressionNumber()} = {output.ToExpressionNumber()}");
            }).WithDescription("Multiplication of two numbers"); // Set subcommand description

            // Configure "div" division subcommand using lamdba statement
            commandsBuilder.AddCommand("div", (
                // Define parameters as argument with name and description (default is option) using attribute
                [Argument(Name = "Dividend", Description = "Dividend")]
                double x,
                [Argument(Name = "Divisor", Description = "Divisor")]
                double y) =>
            {
                // Use try/catch that catches DivideByZeroException (if user tries to divide by 0)
                try
                {
                    // Use class from Calculator.cs file to calculate output
                    var calculator = new Calculator();
                    var output = calculator.Divide(x, y);

                    Console.WriteLine($"{x.ToExpressionNumber()} / {y.ToExpressionNumber()} = {output.ToExpressionNumber()}");
                }
                catch (DivideByZeroException e)
                {
                    // Show exception message as error
                    Console.WriteLine($"[ERR] {e.Message}");
                }
            }).WithDescription("Division of two numbers"); // Set subcommand description
        }).WithDescription("Compute simple calculations"); // Set command description

        // Run application configured with Cocona library
        app.Run();
    }
}