using System;
using System.Diagnostics;

namespace ASD_Lab3
{
   static class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ShowWelcome();

            while (true)
            {
                DataMenu();

            }
        }

        static void ShowWelcome()
        {
            Console.WriteLine("Лабораторна робота #3 : Пошук підрядка в рядку");
            Console.WriteLine("Студент: Прокопишин Ярослав Олександрович ІПЗ-11(02)");
            Console.WriteLine("Завдання: Реалізувати алгоритми пошуку підрядка в рядку: простий");
            Console.WriteLine("Пошук та алгоритм КМП. Порівняти їх ефективність");
        }

        static void DataMenu()
        {

            Console.WriteLine("\n======== МЕНЮ ========");
            Console.WriteLine("1. Використати базовий рядок та підрядок");
            Console.WriteLine("2. Ввести рядок та підрядок з клавіатури");
            Console.WriteLine("3. Згенерувати");
            Console.WriteLine("0. Вихід\n");
            Console.WriteLine("Ваш вибір: \n");

            string choice;
            choice = Console.ReadLine() ?? "";
           
            string text = "";
            string pattern = "";

            switch (choice)
            {
                case "1":
                   Console.WriteLine("Ти вибрав 1\n");
                    text = "abcabeabcabcabd";
                    pattern = "abcabd";
                    Console.WriteLine("Текст: " + text);
                    Console.WriteLine("Підрядок: " + pattern);
                    AlgorithmMenu(text, pattern);
                   
                    break;
                        
                case "2":
                   Console.WriteLine("Ти вибрав 2\n");
                    Console.Write("Введіть текст: ");
                    text = Console.ReadLine() ?? "";

                    Console.Write("\nВведіть підрядок: ");
                    pattern = Console.ReadLine() ?? "";
                    Console.WriteLine("Текст: " + text);
                    Console.WriteLine("Підрядок: " + pattern);
                    AlgorithmMenu(text, pattern);
                    break;

            
                case "3":
                    Console.WriteLine("\nТи вибрав 3\n");

                    int textLength = 20;
                    int patternLength = 5;

                    text = "";
                    pattern = "";

                    for (int i = 0; i < textLength; i++)
                    {
                        text += (char)('a' + rand.Next(0, 3));
                    }

                    for (int i = 0; i < patternLength; i++)
                    {
                        pattern += (char)('a' + rand.Next(0, 3));
                    }

                    Console.WriteLine("Згенерований текст: " + text);
                    Console.WriteLine("Згенерований підрядок: " + pattern);

                    AlgorithmMenu(text, pattern);
                    break;

                case "0":
                    Console.WriteLine("Вихід з програми");
                    Environment.Exit(0);
                    break;

                default:
                     Console.WriteLine("Неправильний вибір");
                    break;
            }
        }

        static void AlgorithmMenu(string text, string pattern)
        {
            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ АЛГОРИТМІВ ===");
                Console.WriteLine("Текст: " + text);
                Console.WriteLine("Підрядок: " + pattern);
                Console.WriteLine("1. Простий пошук");
                Console.WriteLine("2. КМП");
                Console.WriteLine("3. Обидва алгоритми");
                Console.WriteLine("0. Назад\n");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Простий пошук");
                        int result = SimpleSearch(text, pattern);

                        if (result != -1)
                        {
                            Console.WriteLine("Підрядок знайдено на позиції: " + result);
                        }
                        else
                        {
                            Console.WriteLine("Підрядок не знайдено.");
                        }
                        break;


                    case "2":
                        Console.WriteLine("КМП");
                        result = KmpSearch(text, pattern); 

                        if (result != -1)
                        {
                            Console.WriteLine("Підрядок знайдено на позиції: " + result);
                        }
                        else
                        {
                            Console.WriteLine("Підрядок не знайдено.");
                        }
                        break;


                    case "3":
                        Console.WriteLine("Обидва алгоритми");

                        Stopwatch sw = new Stopwatch();

                        sw.Start();
                        int simpleResult = SimpleSearch(text, pattern);
                        sw.Stop();
                        long simpleTime = sw.ElapsedTicks;

                        sw.Reset();

                        sw.Start();
                        int kmpResult = KmpSearch(text, pattern);
                        sw.Stop();
                        long kmpTime = sw.ElapsedTicks;

                        Console.WriteLine("\nРезультат простого пошуку:");
                        if (simpleResult != -1)
                        {
                            Console.WriteLine("Підрядок знайдено на позиції: " + simpleResult);
                        }
                        else
                        {
                            Console.WriteLine("Підрядок не знайдено.");
                        }
                        Console.WriteLine("Час виконання SimpleSearch: " + simpleTime);

                        Console.WriteLine("\nРезультат КМП:");
                        if (kmpResult != -1)
                        {
                            Console.WriteLine("Підрядок знайдено на позиції: " + kmpResult);
                        }
                        else
                        {
                            Console.WriteLine("Підрядок не знайдено.");
                        }
                        Console.WriteLine("Час виконання KMP: " + kmpTime);

                        break;


                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неправильний вибір");
                        break;
                }
            }
        }

        static int SimpleSearch(string text, string pattern)
        {
            if (pattern.Length == 0)
            {
                return 0;
            }

            int textLength = text.Length;
            int patternLength = pattern.Length;

            for (int i = 0; i <= textLength - patternLength; i++)
            {
                int j = 0;

                while (j < patternLength)
                {
                    if (text[i + j] != pattern[j])
                        break;

                    j++;
                }

                if (j == patternLength)
                {
                    return i;
                }
            }

            return -1;
        }



        static int[] BuildPi(string pattern)
        {
            int[] pi = new int[pattern.Length];
            int j = 0;

            for (int i = 1; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[i] != pattern[j])
                {
                    j = pi[j - 1];
                }
                
                if (pattern[i] == pattern[j])
                {
                    j++;
                }

                pi[i] = j;
            }
            return pi;
        }
        static int KmpSearch(string text, string pattern)
        {
            if (pattern.Length == 0)
            {
                return 0;
            }

            int[] pi = BuildPi(pattern);

            int j = 0;

            for (int i = 0; i < text.Length; i++)
            {

                while (j > 0 && text[i] != pattern[j])
                {
                    j = pi[j - 1];
                }


                if (text[i] == pattern[j])
                {
                    j++;
                }

                if (j == pattern.Length)
                {
                    return i - pattern.Length + 1;
                }

            }
            return -1;
        }
    }
}
