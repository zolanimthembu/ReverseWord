using System;
using System.Collections.Generic;
using System.Linq;

namespace LightstoneAssessment
{
    public interface IInputProcessor
    {
        IEnumerable<string> ReadTestCases();
    }

    public class ConsoleInputProcessor : IInputProcessor
    {
        public IEnumerable<string> ReadTestCases()
        {
            Console.WriteLine("Enter number of your lines to reverse");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
                throw new ArgumentException("Invalid number of test cases.");

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter line {i + 1} to reverse:");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    throw new ArgumentException("Test case cannot be empty.");
                yield return line;
            }
        }
    }

    public interface IWordReverser
    {
        string ReverseWords(string input);
    }

    public class WordReverser : IWordReverser
    {
        public string ReverseWords(string input)
        {
            var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(' ', words.Reverse());
        }
    }

    public class ProblemSolver
    {
        private readonly IInputProcessor _inputProcessor;
        private readonly IWordReverser _wordReverser;

        public ProblemSolver(IInputProcessor inputProcessor, IWordReverser wordReverser)
        {
            _inputProcessor = inputProcessor;
            _wordReverser = wordReverser;
        }

        public void Run()
        {
            var testCases = _inputProcessor.ReadTestCases().ToList();

            for (int i = 0; i < testCases.Count; i++)
            {
                string result = _wordReverser.ReverseWords(testCases[i]);
                Console.WriteLine($"Case {i + 1}: {result}");
            }
        }
    }

    public static class UnitTests
    {
        public static void RunTests()
        {
            IWordReverser reverser = new WordReverser();

            // Test 1
            string input1 = "this is a test";
            string expected1 = "test a is this";
            AssertEqual(expected1, reverser.ReverseWords(input1), "Test 1");

            // Test 2
            string input2 = "foobar";
            string expected2 = "foobar";
            AssertEqual(expected2, reverser.ReverseWords(input2), "Test 2");

            // Test 3
            string input3 = "all your base";
            string expected3 = "base your all";
            AssertEqual(expected3, reverser.ReverseWords(input3), "Test 3");
        }

        private static void AssertEqual(string expected, string actual, string testName)
        {
            if (expected == actual)
                Console.WriteLine($"{testName} passed");
            else
                Console.WriteLine($"{testName} failed (Expected: '{expected}', Got: '{actual}')");
        }
    }

    // Main entry point
    class Program
    {
        static void Main(string[] args)
        {
            // Run unit tests (can be commented out in production)
            UnitTests.RunTests();

            // Run the actual program
            IInputProcessor inputProcessor = new ConsoleInputProcessor();
            IWordReverser wordReverser = new WordReverser();
            var solver = new ProblemSolver(inputProcessor, wordReverser);

            solver.Run();
        }
    }
}
