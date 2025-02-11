using System;
using System.Threading;

public class StartAnim
{
    // 기차 문자열 배열 (1줄씩 따로)
    private static string[] train = new string[]
    {
        "   ____     __     ___       _             _  _           ",
        "  |__ /    / /    | __|   __| |   __ _    | || |   ___    ",
        "   |_ \\   / _ \\   |__ \\  / _` |  / _` |    \\_, |  (_-<    ",
        "  |___/   \\___/   |___/  \\__,_|  \\__,_|   _|__/   /__/_   ",
        "_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|  ",
        "\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'  "
    };

    private static string[] title = new string[]
    {
        "   _____ _____ _____ ____                                 ",
        "  |__  // ___// ___// __ \\___ ____  _______               ",
        "   /_ </ __ \\/___ \\/ / / / __` / / / / ___/               ",
        " ___/ / /_/ /___/ / /_/ / /_/ / /_/ /__ \\                 ",
        "/____/\\____/_____/_____/\\___,_\\__, /____/                 ",
        "                             /____/                       "
    };

    /// <summary>
    /// 기차 등장 애니메이션 플레이
    /// </summary>
    public static void Play()
    {
        int screenWidth = Console.WindowWidth;
        int trainWidth = train[0].Length;

        // 기차가 오른쪽 바깥에서 등장
        double startX = screenWidth + trainWidth;
        double targetX = 0;
        double x = startX;

        // 단일 기차의 길이
        int engineLength = 8;  // 기관차 길이 (글자 개수 기준)

        // 속도 조절을 위한 값 설정 (minSpeed: 느린 속도, maxSpeed: 빠른 속도)
        double minSpeed = 0.2;  // 가장 느릴 때
        double maxSpeed = 2.5;  // 가장 빠를 때

        int yOffset = 1; // 기차 위쪽 공백 추가

        Console.Clear();
        Console.CursorVisible = false; // 커서 숨기기

        while (x >= targetX)
        {
            // 기차 머리부터 점진적으로 출력
            for (int i = 0; i < train.Length; i++)
            {
                if (x < screenWidth + trainWidth)
                {
                    int startPos = Math.Max(0, trainWidth - (int)(startX - x));
                    int visibleLength = Math.Min(trainWidth - startPos, screenWidth - Math.Max(0, (int)x));

                    if (visibleLength > 0)
                    {
                        int posX = Math.Max(0, (int)x);
                        Console.SetCursorPosition(posX, i + yOffset);   

                        // 색상을 글자별로 적용하기 위해 한 글자씩 출력
                        for (int j = 0; j < visibleLength; j++)
                        {
                            if (i < 4)
                            {
                                if (j < engineLength * 2)
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                else if (j < engineLength * 3)
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                else
                                    Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;

                                if (j % 8 == 0)
                                    Console.ForegroundColor = ConsoleColor.White;
                            }

                            Console.Write(train[i][startPos + j]); // 한 글자씩 출력
                        }

                        Console.ResetColor(); // 색상 초기화
                    }
                }
            }

            // Ease In-Out 속도 조절
            double progress = (x - targetX) / (startX - targetX); // 0 ~ 1
            double easeSpeed = 1 - Math.Pow(1 - progress, 3); // Ease Out Cubic 공식

            double moveSpeed = minSpeed + (maxSpeed - minSpeed) * easeSpeed; // 가변 속도 적용
            x -= moveSpeed; // x 위치 이동

            Thread.Sleep(10); // 고정된 짧은 간격으로 업데이트 (부드러움 유지)
        }

        // 가로 방향 페이드 효과 + 트랜지션 (네모 기호) 적용
        int maxSteps = Math.Max(screenWidth, title.Length);

        for (int step = 0; step <= maxSteps; step++)
        {
            for (int i = 0; i < title.Length; i++)
            {
                int posX = step - i; // 대각선 이동 효과 적용 (↘)
                if (posX >= 0 && posX < title[i].Length - 15) // 화면 범위 내에서만 출력
                {
                    Console.SetCursorPosition(posX, i + 1);
                    Console.Write("▒"); // 트랜지션 효과 (페이드 아웃)
                }

                // ✨ 네모가 지나간 자리에서 새로운 텍스트가 등장
                if (posX > 0 && posX <= train[i].Length)
                {
                    Console.SetCursorPosition(posX - 1, i + 1);

                    if (i == 0)
                    {
                        if (posX < 15)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (posX < 21)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == 1)
                    {
                        if (posX < 15)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (posX < 21)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == 2)
                    {
                        if (posX < 14)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (posX < 20)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == 3)
                    {
                        if (posX < 14)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (posX < 20)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == 4)
                    {
                        if (posX < 13)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (posX < 19)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(title[i][posX - 1]);
                }

                Console.ResetColor(); // 색상 초기화
            }

            Thread.Sleep(10);
        }

        for (int i = 0; i < title.Length; i++)
        {
            Console.SetCursorPosition(screenWidth - 1, i);
            Console.Write(" "); // 마지막 네모를 공백으로 덮어 마무리
        }

        Console.CursorVisible = true;

        Console.SetCursorPosition(0, train.Length + 2); // 커서를 아래로 이동
    }
}
