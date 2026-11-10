using System.Diagnostics;

class Program
{
   static long CalculateSumOfSquares(int n)
    {
        long sum = 0;

        for (int i = 1; i <= n; i++)
        {
            sum += (long)i * i;
        }

        return sum;
    }

    static int CountPrimeNumbers(int n)
    {
        int count = 0;

        for (int i = 2; i <= n; i++)
        {
            bool isPrime = true;
            for (int j = 2; j <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
            {
                count++;
            }
        }

        return count;
    }

    static Task<long> CalculateSumOfSquaresAsync(int n)
    {
        return Task.Run(() => CalculateSumOfSquares(n));
    }

    static Task<int> CountPrimeNumbersAsync(int n)
    {
        return Task.Run(() => CountPrimeNumbers(n));
    }

    static async Task Main()
    {
        try
        {
            Console.Write("Enter n: ");
            int n = int.Parse(Console.ReadLine());

            if (n < 1 || n > 1000000)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n must be between 1 and 1000000");
            }

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("\n--- Synchronous execution ---");
            stopwatch.Start();

            long sum = CalculateSumOfSquares(n);
            int primeCount = CountPrimeNumbers(n);

            stopwatch.Stop();

            Console.WriteLine($"Sum of squares: {sum}");
            Console.WriteLine($"Count of prime numbers: {primeCount}");
            Console.WriteLine($"Execution time: {stopwatch.Elapsed.TotalMilliseconds:f4} ms");

            Console.WriteLine("\n--- Asynchronous execution ---");
            stopwatch.Restart();

            Task<long> sumTask = CalculateSumOfSquaresAsync(n);
            Task<int> primeCountTask = CountPrimeNumbersAsync(n);

            await Task.WhenAll(sumTask, primeCountTask);

            long asyncSum = sumTask.Result;
            int asyncPrimeCount = primeCountTask.Result;

            stopwatch.Stop();

            Console.WriteLine($"Sum of squares: {asyncSum}");
            Console.WriteLine($"Count of prime numbers: {asyncPrimeCount}");
            Console.WriteLine($"Execution time: {stopwatch.Elapsed.TotalMilliseconds:f4} ms");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please enter a valid integer.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}