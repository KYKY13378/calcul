using System;

class Program
{
    static double memoryValue = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Калькулятор");
        Console.WriteLine("Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, M+, M-, MR, C (очистить), E (выход)");

        double? lastResult = null; 

        while (true)
        {
            try
            {
                Console.WriteLine("-----------------------------------");
                Console.Write("Введите операцию или число: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "e")
                {
                    break;
                }
                
                if (double.TryParse(input, out double number))
                {
                    lastResult = number;
                    Console.WriteLine($"Текущее число: {lastResult}");
                    continue;
                }

                switch (input)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "%":
                        if (lastResult == null)
                        {
                            Console.WriteLine("Ошибка: Введите число сначала.");
                            continue;
                        }
                        Console.Write("Введите второе число: ");
                        string input2 = Console.ReadLine();
                        double num2 = Convert.ToDouble(input2);

                        lastResult = PerformOperation(lastResult.Value, num2, input);
                        break;
                    case "1/x":
                        if (lastResult == null)
                        {
                            Console.WriteLine("Ошибка: Введите число сначала.");
                            continue;
                        }
                        if (lastResult.Value == 0)
                        {
                            Console.WriteLine("Ошибка: Деление на ноль.");
                        }
                        else
                        {
                            lastResult = 1 / lastResult.Value;
                        }
                        break;
                    case "x^2":
                        if (lastResult == null)
                        {
                            Console.WriteLine("Ошибка: Введите число сначала.");
                            continue;
                        }
                        lastResult = Math.Pow(lastResult.Value, 2);
                        break;
                    case "sqrt":
                        if (lastResult == null)
                        {
                            Console.WriteLine("Ошибка: Введите число сначала.");
                            continue;
                        }
                        if (lastResult.Value < 0)
                        {
                            Console.WriteLine("Ошибка: Нельзя извлечь корень из отрицательного числа.");
                        }
                        else
                        {
                            lastResult = Math.Sqrt(lastResult.Value);
                        }
                        break;
                    case "M+":
                        if (lastResult != null)
                        {
                            memoryValue += lastResult.Value;
                            Console.WriteLine($"Прибавлено к памяти. Текущее значение памяти: {memoryValue}");
                        }
                        break;
                    case "M-":
                        if (lastResult != null)
                        {
                            memoryValue -= lastResult.Value;
                            Console.WriteLine($"Вычтено из памяти. Текущее значение памяти: {memoryValue}");
                        }
                        break;
                    case "MR":
                        lastResult = memoryValue;

Драник, [15.09.2025 17:10]
Console.WriteLine($"Значение из памяти: {lastResult}");
                        break;
                    case "C":
                        lastResult = null;
                        Console.WriteLine("Результат сброшен.");
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Попробуйте еще раз.");
                        break;
                }

                if (lastResult != null)
                {
                    Console.WriteLine($"Результат: {lastResult}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Число слишком большое или слишком маленькое.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }

    static double PerformOperation(double num1, double num2, string op)
    {
        switch (op)
        {
            case "+":
                return num1 + num2;
            case "-":
                return num1 - num2;
            case "*":
                return num1 * num2;
            case "/":
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Деление на ноль.");
                }
                return num1 / num2;
            case "%":
                return num1 % num2;
            default:
                throw new InvalidOperationException("Неизвестная операция.");
        }
    }
}
