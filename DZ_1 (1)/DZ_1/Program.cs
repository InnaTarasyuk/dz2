using System;
using System.IO;

namespace dz_2
{


    public class Program
    {
        /// <summary>
        /// Метод выводит на консоль сообщение об исключительной ситуации, дополняя его информацией 
        /// о времени поимки исключения
        /// </summary>
        /// <param name="message"> Сообщение об исключительной ситуации </param>
        public static void ConsoleErrorHandler(string message)
        {
            Console.WriteLine($"Время поимки исключения: {DateTime.Now} + {message}");
        }
        /// <summary>
        /// Метод записывает в файл информацию об ошибках
        /// </summary>
        /// <param name="message"> Сообщение об ошибке </param>
        public static void ResultErrorHandler(string message)
        {
            File.AppendAllText("answers.txt", message + Environment.NewLine);
        }



        static void Main(string[] args)
        {

            Calculator.CalculatorMethod();

            Calculator.WorkWithFiles();
        }

    }
}

