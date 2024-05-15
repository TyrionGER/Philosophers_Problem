public class Philosopher
{
    private int id;
    private Mutex leftFork;
    private Mutex rightFork;
    private Mutex leftNeighborRightFork;
    private Mutex rightNeighborLeftFork;

    public Philosopher(int id, Mutex leftFork, Mutex rightFork, Mutex leftNeighborRightFork, Mutex rightNeighborLeftFork)
    {
        this.id = id;
        this.leftFork = leftFork;
        this.rightFork = rightFork;
        this.leftNeighborRightFork = leftNeighborRightFork;
        this.rightNeighborLeftFork = rightNeighborLeftFork;
    }

    public void Eat()
    {
        while (true)
        {
            Think();
            PickUpForks();
            EatMeal();
        }
    }

    private void Think()
    {
        Console.WriteLine($"Philosopher {id} is thinking.");
        Thread.Sleep(1000);
    }

    private void PickUpForks()
    {
        leftFork.WaitOne();
        while (rightNeighborLeftFork.WaitOne(0) == false)
        {
            leftFork.ReleaseMutex();
            Thread.Sleep(10);
            leftFork.WaitOne();
        }
        while (leftNeighborRightFork.WaitOne(0) == false)
        {
            rightNeighborLeftFork.ReleaseMutex();
            leftFork.ReleaseMutex();
            Thread.Sleep(10);
            leftFork.WaitOne();
            rightNeighborLeftFork.WaitOne();
        }
        rightFork.WaitOne();
        Console.WriteLine($"Philosopher {id} picked up both forks.");
    }

    private void EatMeal()
    {
        Console.WriteLine($"Philosopher {id} is eating.");
        Thread.Sleep(1000);
        
        Console.WriteLine($"Philosopher {id} put down both forks.");
        leftFork.ReleaseMutex();
        rightFork.ReleaseMutex();
        leftNeighborRightFork.ReleaseMutex();
        rightNeighborLeftFork.ReleaseMutex();
    }
}