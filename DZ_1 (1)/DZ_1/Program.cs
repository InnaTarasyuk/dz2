using System;
using System.IO;

namespace DZ_1
{
    public delegate double MathOperation(double a, double b);
    public class Program
    {

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
            for (int i = 0; i < str.Length; i++)
            { // Элемент массива строк st - подстрока одной конкретной строки исходного файла 
                st = str[i].Split();
                // Считаем значение и записываем в массив
                results[i] += Calculate(st[1].ToLower(), double.Parse(st[0]), double.Parse(st[2]));
                // Приводим к нужному формату
                string s = String.Format("{0:f3}", results[i]);
                // Записываем в файл
                File.AppendAllText("answers.txt", s + Environment.NewLine);
                // Проверяем верность значений файла expressions_checker
                if (double.Parse(str1[i]) == double.Parse(s))
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

        static void Main(string[] args)
        {
            Calculator.CalculatorMethod();
            WorkWithFiles();
        }
        /// <summary>
        /// При условии верности ключа(наличии его в словаре) возвращает вычисленное значение
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="a"> Первое число, с которым нужно выполнить операцию </param>
        /// <param name="b"> Второе число, с которым нужно выполнить операцию </param>
        /// <returns> Вычисленное значение </returns>
        public static double Calculate(string operation, double a, double b)
        {
            if (Calculator.operations.ContainsKey(operation))
            {
                return Calculator.operations[operation](a, b);
            }
            else
            {
                return 0;
            }
        }
    }
}
