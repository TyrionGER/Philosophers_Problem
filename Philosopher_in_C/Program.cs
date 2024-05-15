class Program
{
    static void Main(string[] args)
    {
        Mutex[] forks = new Mutex[5];
        for (int i = 0; i < 5; i++)
        {
            forks[i] = new Mutex();
        }

        Philosopher[] philosophers = new Philosopher[5];
        for (int i = 0; i < 5; i++)
        {
            philosophers[i] = new Philosopher(i, forks[i], forks[(i + 1) % 5], forks[(i - 1 + 5) % 5], forks[(i + 2) % 5]);
        }

        foreach (Philosopher philosopher in philosophers)
        {
            Thread thread = new Thread(philosopher.Eat);
            thread.Start();
        }

        Console.ReadLine();
    }
}