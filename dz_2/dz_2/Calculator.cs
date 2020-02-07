using System;
using System.Collections.Generic;
using System.IO;

namespace dz_2
{
    public delegate void ErrorNotificationType(string message);
    public delegate double MathOperation(double a, double b);
    public class Calculator
    {

        public static event ErrorNotificationType ErrorNotification;
        public static Dictionary<string, MathOperation> operations;

        /// <summary>
        /// Метод работает с файлами: записывает вычисленные значения в нужный файл
        /// и проверяет корректность посчитанных значений в исходном
        /// </summary>
        public static void WorkWithFiles()
        {

            // Считыванием содержимое файлов expressions и expressions_checker
            string[] str = File.ReadAllLines("expressions.txt");
            string[] str1 = File.ReadAllLines("expressions_checker.txt");
            // Создаем массив строк, в который на каждой итерации цикла
            // будем записывать содержимое одной отдельной строки файла expressions
            string[] st;
            // Создаем массив посчитанных значений
            double[] results = new double[str.Length];
            string re;
            int countErrors = 0;

            File.WriteAllText("answers.txt", string.Empty);
            File.WriteAllText("results.txt", string.Empty);

            string s = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                try
                {// Элемент массива строк st - подстрока одной конкретной строки исходного файла
                    st = str[i].Split(' ');
                    // Пробрасываем исключения: попытку деления на 0 и неверный оператор
                    if (double.Parse(st[2]) == 0 && st[1] == "/")
                    {
                        s = "bruh";
                        throw new Exception("bruh");
                    }
                    if (!(operations.ContainsKey(st[1])))
                    {
                        s = "неверный оператор";
                        throw new Exception("неверный оператор");
                    }
                    // Считаем значение и записываем в массив
                    results[i] = operations[st[1].ToLower()](double.Parse(st[0]), double.Parse(st[2]));


                    // Приводим к нужному формату
                    s = $"{Math.Round(results[i], 3, MidpointRounding.ToEven):f3}";
                    // Записываем в файл
                    File.AppendAllText("answers.txt", s + Environment.NewLine);
                }
                // Ловим исключение и вызываем событие
                catch (OverflowException)
                {
                    s = double.PositiveInfinity.ToString();
                    ErrorNotification(double.PositiveInfinity.ToString());
                }
                // Ловим исключения и вызываем событие
                catch (Exception e)
                {
                    ErrorNotification(e.Message);
                }
                // Проверяем верность значений файла expressions_checker
                if (str1[i] == s)
                {
                    re = "OK";
                    File.AppendAllText("results.txt", re + Environment.NewLine);
                }
                else
                {
                    re = "Error";
                    // Считаем количество ошибок
                    countErrors++;
                    File.AppendAllText("results.txt", re + Environment.NewLine);
                }

            }
            // Записываем количество ошибок в файл
            File.AppendAllText("results.txt", countErrors.ToString());

        }
        /// <summary>
        /// Метод подписывает на событие методы и создаем словарик с добавлением операций в него
        /// </summary>
        public static void CalculatorMethod()
        {
            // Подписываем на событие два метода
            ErrorNotification += Program.ConsoleErrorHandler;
            ErrorNotification += Program.ResultErrorHandler;
            // Cоздаем словарик и добавляем в него 5 операций
            operations = new Dictionary<string, MathOperation>();
            operations.Add("+", (x, y) => x + y);
            operations.Add("-", (x, y) => x - y);
            operations.Add("*", (x, y) => x * y);
            operations.Add("/", (x, y) => x / y);
            operations.Add("^", (x, y) => Math.Pow(x, y));
        }

    }
}
