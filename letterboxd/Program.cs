//movie rating app based on the actual app Letterboxd.
//One way to improve this to be fully fuctional would be to add a storage system 
//for ratings and adding a prompt to call previous ratings stored in a JSON file

namespace Letterboxd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("               Letterboxd               ");
            Console.WriteLine("========================================\n");

            while (true)
            {
                Console.WriteLine(" 1 - Rate a movie");
                Console.WriteLine(" 0 - Quit\n");

                Console.Write("Choose (0-1): ");
                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input?.Trim())
                {
                    case "1": RateMovie(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Enter 0 or 1.\n");
                        break;
                }
            }
        }

        static void RateMovie()
        {
            string title = Prompt("Movie title");
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title can't be blank. Back to menu.\n");
                return;
            }

            int year = PromptYear();
            string genre = PromptGenre();
            decimal? rating = PromptRating();
            if (rating is null) return;

            Console.Write("Review (or Enter to skip): ");
            string note = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine("\n======================================");
            Console.WriteLine($"{Truncate(title, 35),-35}");
            if (year > 0)
                Console.WriteLine($"{year,-35}");
            if (!string.IsNullOrWhiteSpace(genre))
                Console.WriteLine($"{genre,-35}");
            Console.WriteLine($"{rating}/10");
            if (!string.IsNullOrWhiteSpace(note))
                Console.WriteLine($"\"{Truncate(note, 100),-100}");
            Console.WriteLine("======================================\n");
        }

        static string Prompt(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        static int PromptYear()
        {
            Console.Write("Year (or Enter to skip): ");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(input)) return 0;
            if (int.TryParse(input, out int y) && y >= 1890 && y <= DateTime.Now.Year + 2)
                return y;
            Console.WriteLine("Invalid year — skipping.");
            return 0;
        }

        static string PromptGenre()
        {
            Console.Write("Genre (or Enter to skip): ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        static int? PromptRating()
        {
            Console.Write("Rating (1-10): ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (int.TryParse(input, out int r) && r >= 1 && r <= 10)
                return r;

            if (string.IsNullOrWhiteSpace(input))
                Console.WriteLine("No rating entered. Back to menu.\n");
            else
                Console.WriteLine("Invalid input. Enter a whole number from 1 to 10. Back to menu.\n");

            return null;
        }

        static string Truncate(string s, int max) =>
            s.Length <= max ? s : s[..(max - 1)] + "…";
    }
}
