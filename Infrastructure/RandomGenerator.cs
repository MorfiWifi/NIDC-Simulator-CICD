using System;
using System.Linq;

namespace Infrastructure
{
    public static class RandomGenerator
    {
        private static readonly string numbers = "0123456789";
        private static readonly string lowercases = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string uppercases = lowercases.ToUpper();

        public static string GetRandomString(int length, bool number = true, bool lowercase = false, bool uppercase = false)
        {
            var pool = $"{(number ? numbers : "")}{(lowercase ? lowercases : "")}{(uppercase ? uppercases : "")}";

            var generator = new Random();

            return new string(Enumerable.Repeat(pool, length).Select(s => s[generator.Next(s.Length)]).ToArray());

        }
    }
}
