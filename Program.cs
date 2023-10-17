using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Modul03
{
    internal class Program
    {
        static void Print_Array(int[] scores)
        {
            foreach (int score in scores)
            {
                Console.Write(score+"\t");
            }
            Console.WriteLine();
        }
        static void Print_Array(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static void Max_value(int[] scores)
        {
            int max= scores[0];
            int max_index = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                if(scores[i] > max)
                {
                    max = scores[i];
                    max_index = i;
                }
            }
            Console.WriteLine("Максимум значение:{0} \tЕго индекс: {1}",max, max_index);
        }
        static void Max_value2(int[] scores)
        {
            int max = scores.Max();
            
            int max_index = scores.Select((value, index) => new { Value = value, Index = index })
                                 .First(item => item.Value == max).Index;
            Console.WriteLine("Максимум значение:{0} \tЕго индекс: {1}", max, max_index);
        }
        static void Max_value3(int[] scores)
        {
            int max = scores.Max();
            int max_index = Array.IndexOf(scores, scores.Max());
            Console.WriteLine("Максимум значение:{0} \tЕго индекс: {1}",
                max, max_index);
        }
        static int Max_gerade_value(int[] scores)
        {
            var gerade_scores = scores.Where(num => num % 2 == 0);
            int gerade_max = gerade_scores.DefaultIfEmpty(0).Max();
            
            int gerade_max_index = Array.IndexOf(scores, gerade_max);
            if (gerade_max_index != -1)
            {
                Console.WriteLine("Максимум четное значение:{0} \tЕго индекс: {1}",
                gerade_max, gerade_max_index);
            }
            else Console.WriteLine("Нет четных значении");
            return gerade_max_index;
        }
        static int Max_gerade_value2(int[] scores)
        {
            int gerade_max_index = Array.FindIndex(scores, num => num % 2 == 0);
            if (gerade_max_index != -1)
            {
                int gerade_max = scores[gerade_max_index];
                Console.WriteLine("Максимум четное значение:{0} \tЕго индекс: {1}",
                gerade_max, gerade_max_index);
            }
            else Console.WriteLine("Нет четных значении");
            return gerade_max_index;
        }
        static int Max_gerade_value3(int[] scores)
        {
            int gerade_max = int.MinValue;
            int gerade_max_index = -1;
            for (int i = 0; i < scores.Length; i++)
            {
                if(scores[i] > gerade_max && scores[i] % 2 == 0)
                {
                    gerade_max = scores[i];
                    gerade_max_index = i;   
                }
            }
            if (gerade_max_index != -1)
            {
                Console.WriteLine("Максимум четное значение:{0} \tЕго индекс: {1}",
                gerade_max, gerade_max_index);
            }
            else Console.WriteLine("Нет четных значении");
            return gerade_max_index;
        }
        static int[] Del_index(int[] scores,int del_index)
        {
            if (del_index >= 0 && del_index < scores.Length)
            {
                int[] newArray = new int[scores.Length - 1];
                int newArrayIndex = 0;
                for (int i = 0; i < scores.Length; i++)
                {
                    if (i != del_index)
                    {
                        newArray[newArrayIndex] = scores[i];
                        newArrayIndex++;
                    }
                }
                scores = newArray;
                Console.WriteLine("Массив изменен");
            }
            else Console.WriteLine("Недопустимый индекс для удаления.");
            return scores;
        }
        static int[] Del_value(int[] scores, int del_value)
        {
            int del_num = scores.Count(num => num == del_value);
            if (del_num>0)
            {
                int[] newArray = new int[scores.Length - del_num];
                int newArrayIndex = 0;
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i] != del_value)
                    {
                        newArray[newArrayIndex] = scores[i];
                        newArrayIndex++;
                    }
                }
                scores = newArray;
                Console.WriteLine("Массив изменен");
            }
            else Console.WriteLine("Недопустимый значение для удаления.");
            return scores;
        }
        static int[] Add_index(int[] scores, int add_index,int add_value)
        {
            if (add_index >= 0 && add_index <= scores.Length)
            {
                int[] newArray = new int[scores.Length +1];
                for (int newArrayIndex = 0, i = 0; newArrayIndex < newArray.Length; newArrayIndex++)
                {
                    if (newArrayIndex == add_index)
                    {
                        newArray[newArrayIndex] = add_value;
                        continue;
                    }
                        newArray[newArrayIndex] = scores[i];
                        i++;
                }
                scores = newArray;
                Console.WriteLine("Массив изменен");
            }
            else Console.WriteLine("Недопустимый индекс для удаления.");
            return scores;
        }
        static int[] Del_value_two(int[] scores)
        {
            foreach(int score in scores)
            {
                int del_num = scores.Count(num => num == score);
                if (del_num == 2)
                {
                    scores = Del_value(scores, score);
                }
            }
            return scores;
        }
        static void del_a(string input)
        {
            string[] words = input.Split(' ');
            words = words.Where(word => !word.Contains('a')).ToArray();
            words = words.Where(word => !word.Contains('а')).ToArray();
            string result = string.Join(" ", words);
            Console.WriteLine(result);


        }
        static void del_last_word(string input)
        {
            // Разделяем строку на слова
            var words = input.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            // Если в строке нет слов, возвращаем исходную строку
            if (words.Length == 0)
                Console.WriteLine("no text");

            // Взятие последнего слова
            var lastWord = words[words.Length - 1];

            // Удаляем слова, которые содержат хотя бы одну букву из последнего слова
            var resultWords = words.Where(word => !word.Any(ch => lastWord.Contains(ch))).ToList();

            // Объединяем слова обратно в строку
            Console.WriteLine(string.Join(" ", resultWords));
        }
        static void Tipa_polindrom(string input)
        {
            var pattern = @"\b(\w)\w*\1\b";
            Console.WriteLine( Regex.Replace(input, pattern, "[$0]"));
        }
        static void Tipa_polindrom2(string input)
        {
            string[] words = input.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i][0] == words[i][words[i].Length - 1])
                {
                    words[i] = "[" + words[i] + "]";
                }
            }
            Console.WriteLine(string.Join(" ", words));
        }
        static void Rows_diagonal_elements(ref int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Если элемент главной диагонали четный
                if (matrix[i, i] % 2 == 0)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = 0;  // Обнуляем элементы строки
                    }
                }
            }
            Print_Array(matrix);
        }
        static void Cols_diagonal_elements(ref int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Если элемент главной диагонали четный
                if (matrix[i, i] % 2 == 0)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[j,i] = 0;  // Обнуляем элементы строки
                    }
                }
            }
            Print_Array(matrix);
        }
        static void Columns_Duplicates(int[,] matrix)
        {
            List<int> columns_remove = new List<int>();

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                HashSet<int> unique = new HashSet<int>();
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (unique.Contains(matrix[i, j]))
                    {
                        columns_remove.Add(j);
                        break;
                    }
                    unique.Add(matrix[i, j]);
                }
            }

            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1) - columns_remove.Count];
            int newColumnIndex = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (!columns_remove.Contains(j))
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        newMatrix[i, newColumnIndex] = matrix[i, j];
                    }
                    newColumnIndex++;
                }
            }
            Print_Array(newMatrix) ;
        }
        static void EndDot()
        {
            Console.WriteLine("Введите символы. Для завершения введите точку.");
            char ch;
            do
            {
                ch = (char)Console.Read();  // Считываем символ
            }
            while (ch != '.');  // Продолжаем, пока не встретим точку

            Console.WriteLine("\nесть точка.");
        }
        static void Whitespace()
        {
            Console.WriteLine("Введите символы. Для завершения введите точку.");
            string input = "";
            char ch;
            do
            {
                ch = (char)Console.Read();  
                input += ch;  
            }
            while (ch != '.');  
            int spaceCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')  
                {
                    spaceCount++;
                }
            }
            Console.WriteLine($"\nКоличество пробелов: {spaceCount}");
        }
        static void Happy_Ticket()
        {
            Console.WriteLine("Введите номер трамвайного билета (6-значное число):");
            string input = Console.ReadLine();
            if (input.Length != 6 || !int.TryParse(input, out _))
            {
                Console.WriteLine("Номер билета должен быть 6-значным числом.");
                Happy_Ticket();
                return;
            }
            int firstHalf = int.Parse(input.Substring(0, 3));  // Первые 3 цифры
            int secondHalf = int.Parse(input.Substring(3, 3));    // Последние 3 цифры
            if (SumOfDigits(firstHalf) == SumOfDigits(secondHalf))Console.WriteLine("Счастливый билет!");
            else Console.WriteLine("Обычный билет.");
        }
        static int SumOfDigits(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }
        static void Sum_min_max()
        {
            int[,] massiv = new int[5, 5];
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    massiv[i, j] = rand.Next(-100, 101);
                    Console.Write(massiv[i, j] + "\t");
                }
                Console.WriteLine();
            }
            int minRow = 0, minCol = 0, maxRow = 0, maxCol = 0;
            int minValue = int.MaxValue;
            int maxValue = int.MinValue;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (massiv[i, j] < minValue)
                    {
                        minValue = massiv[i, j];
                        minRow = i;
                        minCol = j;
                    }
                    if (massiv[i, j] > maxValue)
                    {
                        maxValue = massiv[i, j];
                        maxRow = i;
                        maxCol = j;
                    }
                }
            }
            int sum = 0;
            bool startSum = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((i == minRow && j == minCol) || (i == maxRow && j == maxCol))
                    {
                        if (startSum)
                            break;
                        else
                            startSum = true; 
                    }
                    else if (startSum)
                    {
                        sum += massiv[i, j];
                    }
                }
            }
            Console.WriteLine($"Сумма элементов между минимальным ({minValue}) и максимальным ({maxValue}) элементами: {sum}");
        }
        static void identical_char()
        {
            Console.WriteLine("Введите текст:");
            string text = Console.ReadLine();
            int current = 1; 
            int max = 1; 
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == text[i - 1])
                {
                    current++;
                    if (current > max)
                    {
                        max = current;
                    }
                }
                else
                {
                    current = 1; 
                }
            }

            Console.WriteLine($"Наибольшее количество идущих подряд одинаковых символов: {max}");
        }
        static void How_digits()
        {
            Console.WriteLine("Введите строку длиной 20 символов:");
            string input = Console.ReadLine();

            if (input.Length != 20)
            {
                Console.WriteLine("Строка должна содержать 20 символов.");
                How_digits();
                return;
            }
            else
            {
                int digitCount = 0;

                for (int i = 0; i < input.Length; i++)
                {
                    if (char.IsDigit(input[i]))
                    {
                        digitCount++;
                    }
                }

                Console.WriteLine($"Количество цифр в строке: {digitCount}");
            }
        }
        static void Main(string[] args)
        {
            int[] scores = { 95, 101, 101, 95, 120,100 };

            //Напечатать весь массив целых чисел
            Print_Array(scores);

            //Найти индекс максимального значения в массиве(воспользоваться функцией)
            Max_value(scores);
            Max_value2(scores);
            Max_value3(scores);

            //Найти индекс максимального четного значения в массиве
            Max_gerade_value(scores);
            Max_gerade_value2(scores);
            Max_gerade_value3(scores);

            //Удалить элемент из массива по индексу.
            scores=Del_index(scores, Max_gerade_value3(scores));
            Print_Array(scores);

            //Удаление элементов из массива по значению
            scores=Del_value(scores, 95);
            Print_Array(scores);
            //Вставить элемент в массив по индексу
            scores = Add_index(scores,3, 195);
            Print_Array(scores);
            //Удалить те элементы массива, которые встречаются в нем ровно два раза
            scores = Del_value_two(scores);
            Print_Array(scores);
            //Удалить из строки слова, в которых есть буква 'a'
            Console.WriteLine("Введите текст");
            string input = Console.ReadLine();
            del_a(input);
            //Удалить из строки слова, в которых есть хоть одна буква последнего слова
            del_last_word(input);

            //В строке все слова, которые начинаются и заканчиваются одной буквой, выделить квадратными скобками
            Tipa_polindrom(input);
            Tipa_polindrom2(input);
            //Обнулить элементы тех строк, на пересечении которых с главной диагональю стоит четный элемент
            int[,] matrix =
            {
                { 2, 1, 3 },
                { 4, 2, 6 },
                { 7, 8, 9 }
            };
            Rows_diagonal_elements(ref matrix);

            //Обнулить элементы тех столбцов, на пересечении которых с главной диагональю стоит четный элемент
            Cols_diagonal_elements(ref matrix);
            //Удалить те столбцы, в которых встречается хотя бы два одинаковых элемента
            Columns_Duplicates(matrix);
            //Написать программу, которая считывает символы с клавиатуры, пока не будет введена точка.
            EndDot();
            //Программа должна сосчитать количество введенных пользователем пробелов. (Подсказка.IF, Length)
            Whitespace();
            //Ввести с клавиатуры номер трамвайного билета(6 - значное число) и проверить является ли данный билет
            //счастливым(если на билете напечатано шестизначное число, и сумма первых трёх цифр равна сумме последних трёх,
            //то этот билет считается счастливым).
            Happy_Ticket();
            //Дан двумерный массив размерностью 5×5, заполненный случайными числами из диапазона от –100 до 100.
            //Определить сумму элементов массива, расположенных между минимальным и максимальным элементами.
            Sum_min_max();
            //Дан текст. Найти наибольшее количество идущих подряд одинаковых символов
            identical_char();
            //Составить программу, которая подсчитывает, сколько содержится цифр в строке длиной 20 символов.
            How_digits();
            
            Console.ReadKey();
        }
    }
    

}
