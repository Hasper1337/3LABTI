using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3LABTI
{
    /// <summary>
    /// Класс для арифметического кодирования и декодирования
    /// </summary>
    public class ArithmeticCoding
    {
        private Dictionary<char, decimal> probabilities;
        private Dictionary<char, (decimal low, decimal high)> ranges;

        public class EncodingStep
        {
            public int StepNumber { get; set; }
            public string CurrentChain { get; set; }
            public char CurrentSymbol { get; set; }
            public decimal LowBound { get; set; }
            public decimal HighBound { get; set; }
            public decimal SymbolProbability { get; set; }

            public override string ToString()
            {
                return $"Шаг {StepNumber}: '{CurrentSymbol}' | Цепочка: \"{CurrentChain}\" | [{LowBound}, {HighBound})";
            }
        }

        public class DecodingStep
        {
            public int StepNumber { get; set; }
            public string DecodedChain { get; set; }
            public char DecodedSymbol { get; set; }
            public decimal CodeValue { get; set; }
            public decimal LowBound { get; set; }
            public decimal HighBound { get; set; }

            public override string ToString()
            {
                return $"Шаг {StepNumber}: '{DecodedSymbol}' | Цепочка: \"{DecodedChain}\" | Код: {CodeValue} | [{LowBound}, {HighBound})";
            }
        }

        /// <summary>
        /// Получить таблицу вероятностей
        /// </summary>
        public Dictionary<char, decimal> GetProbabilities()
        {
            return new Dictionary<char, decimal>(probabilities);
        }

        /// <summary>
        /// Получить таблицу диапазонов
        /// </summary>
        public Dictionary<char, (decimal low, decimal high)> GetRanges()
        {
            return new Dictionary<char, (decimal, decimal)>(ranges);
        }

        /// <summary>
        /// Инициализация с автоматическим подсчетом вероятностей
        /// </summary>
        public void Initialize(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Входная строка не может быть пустой!");

            // Подсчет частот символов
            var frequencies = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (frequencies.ContainsKey(c))
                    frequencies[c]++;
                else
                    frequencies[c] = 1;
            }

            // Вычисление вероятностей
            probabilities = new Dictionary<char, decimal>();
            int totalChars = input.Length;

            foreach (var kvp in frequencies)
            {
                probabilities[kvp.Key] = (decimal)kvp.Value / totalChars;
            }

            // Построение диапазонов ПО УБЫВАНИЮ вероятностей
            BuildRanges();
        }

        /// <summary>
        /// Инициализация с заданными вероятностями
        /// </summary>
        public void Initialize(Dictionary<char, decimal> customProbabilities)
        {
            if (customProbabilities == null || customProbabilities.Count == 0)
                throw new ArgumentException("Вероятности не могут быть пустыми!");

            decimal sum = customProbabilities.Values.Sum();
            if (Math.Abs(sum - 1.0m) > 0.0001m)
            {
                // Нормализация
                probabilities = new Dictionary<char, decimal>();
                foreach (var kvp in customProbabilities)
                {
                    probabilities[kvp.Key] = kvp.Value / sum;
                }
            }
            else
            {
                probabilities = new Dictionary<char, decimal>(customProbabilities);
            }

            BuildRanges();
        }

        /// <summary>
        /// Построение диапазонов для каждого символа ПО УБЫВАНИЮ вероятностей
        /// </summary>
        private void BuildRanges()
        {
            ranges = new Dictionary<char, (decimal, decimal)>();
            decimal cumulative = 0.0m;

            // Сортируем ПО УБЫВАНИЮ вероятностей (от большей к меньшей)
            foreach (var kvp in probabilities.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                decimal low = cumulative;
                decimal high = cumulative + kvp.Value;
                ranges[kvp.Key] = (low, high);
                cumulative = high;
            }
        }

        /// <summary>
        /// Кодирование строки с отображением всех шагов
        /// </summary>
        public (decimal code, List<EncodingStep> steps) Encode(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Входная строка не может быть пустой!");

            List<EncodingStep> steps = new List<EncodingStep>();
            decimal low = 0.0m;
            decimal high = 1.0m;
            StringBuilder currentChain = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char symbol = input[i];
                currentChain.Append(symbol);

                if (!ranges.ContainsKey(symbol))
                {
                    throw new ArgumentException($"Символ '{symbol}' не найден в таблице вероятностей!");
                }

                var symbolRange = ranges[symbol];
                decimal range = high - low;

                decimal newLow = low + range * symbolRange.low;
                decimal newHigh = low + range * symbolRange.high;

                var step = new EncodingStep
                {
                    StepNumber = i + 1,
                    CurrentChain = currentChain.ToString(),
                    CurrentSymbol = symbol,
                    LowBound = newLow,
                    HighBound = newHigh,
                    SymbolProbability = probabilities[symbol]
                };

                steps.Add(step);

                low = newLow;
                high = newHigh;
            }

            // Финальный код - среднее значение интервала
            decimal finalCode = (low + high) / 2.0m;

            return (finalCode, steps);
        }

        /// <summary>
        /// Декодирование с отображением всех шагов
        /// </summary>
        public (string decoded, List<DecodingStep> steps) Decode(decimal code, int length)
        {
            if (length <= 0)
                throw new ArgumentException("Длина должна быть больше нуля!");

            if (code < 0 || code >= 1)
                throw new ArgumentException("Код должен быть в диапазоне [0, 1)!");

            List<DecodingStep> steps = new List<DecodingStep>();
            StringBuilder decoded = new StringBuilder();
            decimal currentCode = code; // Текущее значение кода (будет изменяться!)

            for (int i = 0; i < length; i++)
            {
                // На каждом шаге работаем с полным интервалом [0.0, 1.0)
                // так как код нормализован
                char decodedSymbol = '\0';
                decimal symbolLow = 0, symbolHigh = 0;

                // Ищем символ, в чей диапазон попадает НОРМАЛИЗОВАННЫЙ код
                foreach (var kvp in ranges)
                {
                    char symbol = kvp.Key;
                    var range = kvp.Value;

                    // Используем диапазоны напрямую из таблицы ranges
                    // так как currentCode уже нормализован к [0, 1)
                    if (currentCode >= range.low && currentCode < range.high)
                    {
                        decodedSymbol = symbol;
                        symbolLow = range.low;
                        symbolHigh = range.high;
                        break;
                    }
                }

                if (decodedSymbol == '\0')
                {
                    // Подробная диагностика ошибки
                    string errorMsg = $"Не удалось декодировать символ на шаге {i + 1}.\n";
                    errorMsg += $"Текущий код: {currentCode}\n";
                    errorMsg += $"Доступные диапазоны:\n";
                    foreach (var kvp in ranges.OrderBy(x => x.Value.low))
                    {
                        errorMsg += $"  '{kvp.Key}': [{kvp.Value.low}, {kvp.Value.high})\n";
                    }
                    throw new InvalidOperationException(errorMsg);
                }

                decoded.Append(decodedSymbol);

                var step = new DecodingStep
                {
                    StepNumber = i + 1,
                    DecodedChain = decoded.ToString(),
                    DecodedSymbol = decodedSymbol,
                    CodeValue = currentCode,  // Текущее нормализованное значение кода
                    LowBound = symbolLow,
                    HighBound = symbolHigh
                };

                steps.Add(step);

                // ⭐ КЛЮЧЕВАЯ ФОРМУЛА: Нормализуем код для следующего шага
                // code = (code - RangeLow) / (RangeHigh - RangeLow)
                if (i < length - 1) // Не пересчитываем на последнем шаге
                {
                    currentCode = (currentCode - symbolLow) / (symbolHigh - symbolLow);
                }
            }

            return (decoded.ToString(), steps);
        }

        /// <summary>
        /// Проверка инициализации
        /// </summary>
        public bool IsInitialized()
        {
            return probabilities != null && probabilities.Count > 0;
        }
    }
}