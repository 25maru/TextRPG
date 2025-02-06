using System;

// 박지원님
public class Dungeon
{
    public string Name { get; }

    public int Time { get; set; }

    public List<Monster> Monsters { get; set; }

    public Dungeon(string name)
    {
        Name = name;
        Monsters = new List<Monster>();
    }

    public void SetTimer(int startTime)
    {
        Time = startTime;

        while (Time >= 0)
        {
            Time--;

            if (Time <= 0)
                Time = 0;

            TimeSpan timeSpan = TimeSpan.FromSeconds(Time);
            string formattedTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

            Console.Write($"\r남은 시간: {formattedTime}");

            Thread.Sleep(1000);
        }
    }
}
