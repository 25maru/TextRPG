using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;

namespace Tool
{
    public static class Utils
    {
        public static StringWriter logWriter = new StringWriter();
        public static TextWriter originalConsoleOut = Console.Out;
        public static DualConsoleWriter dualConsoleWriter = new DualConsoleWriter(originalConsoleOut, logWriter);

        // 마지막으로 저장된 로그 길이 (중복 방지를 위해 사용)
        private static int lastLogLength = 0;

        /// <summary>
        /// 이전 장면과 구분할 수 있는 선 추가
        /// 장면의 이름과 설명 출력 가능
        /// </summary>
        /// <param name="title">장면의 이름 (없을 경우 "")</param>
        /// <param name="description">장면의 설명 (없을 경우 "")</param>
        public static void ShowHeader(string title, string description, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            // Thread.Sleep(500);

            // 로그 저장 및 화면 갱신
            // SaveAndClearConsole();

            Console.WriteLine("\n========================================================================================================================");
            Thread.Sleep(500);

            Console.ForegroundColor = color;
            if (title != "") Console.WriteLine($"\n{title}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (description != "") Console.WriteLine($"{description}\n");
            Console.ResetColor();
        }

        /// <summary>
        /// 텍스트에 색상을 간편하게 적용
        /// </summary>
        /// <param name="text">텍스트</param>
        /// <param name="color">색상</param>
        public static void ColorText(string text, ConsoleColor color, bool breakLine = true)
        {
            Console.ForegroundColor = color;
            if (breakLine) Console.WriteLine(text);
            else Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// 선택지 스타일을 간편하게 구현
        /// </summary>
        /// <param name="index">선택지의 번호</param>
        /// <param name="name">선택지 이름</param>
        public static void OptionText(int index, string name, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            Console.ForegroundColor = color;
            Console.Write(index);
            Console.ResetColor();
            Console.WriteLine($". {name}");
        }

        /// <summary>
        /// 아이템 스타일을 간편하게 구현
        /// </summary>
        /// <param name="text">텍스트</param>
        /// <param name="textColor">텍스트 색상</param>
        /// <param name="prefix">접두사</param>
        /// <param name="prefixColor">접두사 색상</param>
        /// <param name="suffix">접미사</param>
        /// <param name="suffixColor">접미사 색상</param>
        public static void ItemText(string prefix, string text, string suffix = "", ConsoleColor prefixColor = ConsoleColor.DarkGray, ConsoleColor color = ConsoleColor.Gray, ConsoleColor suffixColor = ConsoleColor.DarkGray)
        {
            if (prefix != "")
            {
                Console.ForegroundColor = prefixColor;
                Console.Write(prefix);
            }

            if (suffix != "")
            {
                Console.ForegroundColor = color;
                Console.Write(text);

                Console.ForegroundColor = suffixColor;
                Console.Write(suffix);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = color;
                Console.Write(text);
                Console.ResetColor();
            }
        }
        
        /// <summary>
         /// 아이템 스타일을 간편하게 구현
         /// </summary>
         /// <param name="text">텍스트</param>
         /// <param name="textColor">텍스트 색상</param>
         /// <param name="prefix">접두사</param>
         /// <param name="prefixColor">접두사 색상</param>
         /// <param name="suffix">접미사</param>
         /// <param name="suffixColor">접미사 색상</param>
        public static void ItemTextLine(string prefix, string text, string suffix = "", ConsoleColor prefixColor = ConsoleColor.DarkGray, ConsoleColor color = ConsoleColor.Gray, ConsoleColor suffixColor = ConsoleColor.DarkGray)
        {
            if (prefix != "")
            {
                Console.ForegroundColor = prefixColor;
                Console.Write(prefix);
            }

            if (suffix != "")
            {
                Console.ForegroundColor = color;
                Console.Write(text);

                Console.ForegroundColor = suffixColor;
                Console.WriteLine(suffix);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// 플레이어 스타일을 간편하게 구현
        /// </summary>
        /// <param name="player">플레이어</param>
        public static void PlayerText(Character player)
        {
            Console.Write("Lv.");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(player.Level);
            Console.ResetColor();

            Console.Write($"  {player.Name} ");
            string playerClass = Utils.FormatString($"({player.Class})", 13 - Utils.GetDisplayWidth(player.Name));

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(playerClass);
            Console.ResetColor();

            Console.Write($"(공격력 ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(player.TotalAttack);
            Console.ResetColor();

            Console.Write($" / 체력 ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(player.Health);
            Console.ResetColor();

            Console.WriteLine(")");
        }

        /// <summary>
        /// 몬스터 스타일을 간편하게 구현
        /// </summary>
        /// <param name="level">몬스터 레벨</param>
        /// <param name="name">몬스터 이름</param>
        /// <param name="attack">몬스터 공격력</param>
        /// <param name="health">몬스터 체력</param>
        /// <param name="isDead">몬스터 생사 여부</param>
        public static void MonsterText(int level, string name, float attack, int health, bool isDead)
        {
            name = FormatString(name, 14);

            if (!isDead)
            {
                Console.Write("Lv.");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(level);
                Console.ResetColor();

                if (level <= 9)
                    Console.Write($"  {name}(공격력 ");
                else
                    Console.Write($" {name}(공격력 ");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(attack);
                Console.ResetColor();

                Console.Write($" / 체력 ");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(health);
                Console.ResetColor();

                Console.WriteLine(")");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Lv.{level} {name} (공격력 {attack} / 체력 {health})");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// 회색으로 표시될 안내 메시지에 사용
        /// </summary>
        /// <param name="text">표시할 텍스트</param>
        public static void InfoText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// 빨간색으로 표시될 경고 메시지에 사용
        /// </summary>
        /// <param name="text">표시할 텍스트</param>
        public static void ErrorText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// 인풋 값의 허용 범위를 정하고, 이외의 입력에는 "잘못된 입력" 경고
        /// (min, max 값도 범위에 포함)
        /// </summary>
        /// <param name="min">최소 허용 범위</param>
        /// <param name="max">최대 허용 범위</param>
        /// <returns></returns>
        public static int GetInput(int min, int max)
        {
            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                // 원래 콘솔 출력으로 돌아와 입력을 받음
                Console.SetOut(originalConsoleOut);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(">> ");

                // 현재 커서 위치 저장
                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;
                
                // 2줄 여백 추가
                Console.WriteLine("\n\n");

                int cursorOffset = cursorTop switch
                {
                    27 => 1,
                    28 => 2,
                    29 => 3,
                    _ => 0,
                };

                // 입력을 위해 커서를 원래 위치로 복귀
                Console.SetCursorPosition(cursorLeft, cursorTop - cursorOffset);

                if (int.TryParse(Console.ReadLine(), out int input) && input >= min && input <= max)
                {
                    Console.ResetColor();

                    // 다시 콘솔 출력을 DualConsoleWriter로 변경
                    Console.SetOut(dualConsoleWriter);

                    // 입력된 내용 로그에 저장
                    logWriter.WriteLine($">> {input}");

                    return input;
                }

                // 다시 콘솔 출력을 DualConsoleWriter로 변경
                Console.SetOut(dualConsoleWriter);

                // 입력된 내용 로그에 저장
                logWriter.WriteLine($">> {input}");

                ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
            }
        }

        /// <summary>
        /// 플레이어의 입력을 간편하게 받아서 저장
        /// </summary>
        public static string GetInput()
        {
            // 원래 콘솔 출력으로 돌아와 입력을 받음
            Console.SetOut(originalConsoleOut);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");

            // 현재 커서 위치 저장
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            // 2줄 여백 추가
            Console.WriteLine("\n\n");

            int cursorOffset = cursorTop switch
            {
                27 => 1,
                28 => 2,
                29 => 3,
                _ => 0,
            };

            // 입력을 위해 커서를 원래 위치로 복귀
            Console.SetCursorPosition(cursorLeft, cursorTop - cursorOffset);

            string input = Console.ReadLine();
            Console.ResetColor();

            // 다시 콘솔 출력을 DualConsoleWriter로 변경
            Console.SetOut(dualConsoleWriter);

            // 입력된 내용 로그에 저장
            logWriter.WriteLine($">> {input}");

            return input;
        }

        /// <summary>
        /// 콘솔에 출력될 글자가 실제로 몇칸인지 측정
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetDisplayWidth(string text)
        {
            int width = 0;
            foreach (char c in text)
            {
                // 한글은 2칸, 나머지는 1칸
                width += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
            }
            return width;
        }

        /// <summary>
        /// 텍스트의 오른쪽에 공백을 추가해서 특정 길이에 맞춤
        /// </summary>
        /// <param name="text">적용할 텍스트</param>
        /// <param name="totalWidth">총 길이</param>
        /// <returns></returns>
        public static string FormatString(string text, int totalWidth)
        {
            int padding = totalWidth - GetDisplayWidth(text);
            return text + new string(' ', padding > 0 ? padding : 0);
        }

        public static void SaveAndClearConsole()
        {
            // 현재까지 출력된 모든 문자열 저장
            string output = logWriter.ToString();

            // 콘솔 클리어
            Console.SetOut(originalConsoleOut);
            Console.Clear();

            // DarkGray 색상으로 저장된 내용 출력
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(output);
            Console.ResetColor();

            // 콘솔 출력을 다시 DualConsoleWriter로 변경 (기존 logWriter 유지)
            Console.SetOut(dualConsoleWriter);
        }
    }
}
