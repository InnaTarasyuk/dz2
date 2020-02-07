using System;
using System.Collections.Generic;

namespace DZ_1
{
    public class Calculator
    {
        public static Dictionary<string, MathOperation> operations;
        /// <summary>
        /// Метод добавляет в словарь необходимые операции, которые реализуют вычисление значений
        /// </summary>
        public static void CalculatorMethod()
        {
            operations = new Dictionary<string, MathOperation>();
            MathOperation plus = (x, y) => x + y;
            operations.Add("+", plus);
            operations.Add("-", (x, y) => x - y);
            operations.Add("*", (x, y) => x * y);
            operations.Add("/", (x, y) => x / y);
            operations.Add("^", (x, y) =>
            {
                x = Math.Pow(x, y);
                return x;
            });
        }
    }
}
