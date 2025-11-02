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
        private Dictionary<char, double> probabilities;
        private Dictionary<char, (double low, double high)> ranges;

        public class EncodingStep
        {
            public int StepNumber { get; set; }
            public string CurrentChain { get; set; }
            public char CurrentSymbol { get; set; }
            public double LowBound { get; set; }
            public double HighBound { get; set; }
            public double SymbolProbability { get; set; }

            public override string ToString()
            {
                return $"Шаг {StepNumber}: '{CurrentSymbol}' | Цепочка: \"{CurrentChain}\" | [{LowBound:F10}, {HighBound:F10})";
            }
        }

        public class DecodingStep
        {
            public int StepNumber { get; set; }
            public string DecodedChain { get; set; }
            public char DecodedSymbol { get; set; }
            public double CodeValue { get; set; }
            public double LowBound { get; set; }
            public double HighBound { get; set; }

            public override string ToString()
            {
                return $"Шаг {StepNumber}: '{DecodedSymbol}' | Цепочка: \"{DecodedChain}\" | [{LowBound:F10}, {HighBound:F10})";
            }
        }

        /// <summary>
        /// Получить таблицу вероятностей
        /// </summary>
        public Dictionary<char, double> GetProbabilities()
        {
            return new Dictionary<char, double>(probabilities);
        }

        /// <summary>
        /// Получить таблицу диапазонов
        /// </summary>
        public Dictionary<char, (double low, double high)> GetRanges()
        {
            return new Dictionary<char, (double, double)>(ranges);
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
            probabilities = new Dictionary<char, double>();
            int totalChars = input.Length;

            foreach (var kvp in frequencies.OrderBy(x => x.Key))
            {
                probabilities[kvp.Key] = (double)kvp.Value / totalChars;
            }

            // Построение диапазонов
            BuildRanges();
        }

        /// <summary>
        /// Инициализация с заданными вероятностями
        /// </summary>
        public void Initialize(Dictionary<char, double> customProbabilities)
        {
            if (customProbabilities == null || customProbabilities.Count == 0)
                throw new ArgumentException("Вероятности не могут быть пустыми!");

            double sum = customProbabilities.Values.Sum();
            if (Math.Abs(sum - 1.0) > 0.0001)
            {
                // Нормализация
                probabilities = new Dictionary<char, double>();
                foreach (var kvp in customProbabilities)
                {
                    probabilities[kvp.Key] = kvp.Value / sum;
                }
            }
            else
            {
                probabilities = new Dictionary<char, double>(customProbabilities);
            }

            BuildRanges();
        }

        /// <summary>
        /// Построение диапазонов для каждого символа
        /// </summary>
        private void BuildRanges()
        {
            ranges = new Dictionary<char, (double, double)>();
            double cumulative = 0.0;

            foreach (var kvp in probabilities.OrderBy(x => x.Key))
            {
                double low = cumulative;
                double high = cumulative + kvp.Value;
                ranges[kvp.Key] = (low, high);
                cumulative = high;
            }
        }

        /// <summary>
        /// Кодирование строки с отображением всех шагов
        /// </summary>
        public (double code, List<EncodingStep> steps) Encode(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Входная строка не может быть пустой!");

            List<EncodingStep> steps = new List<EncodingStep>();
            double low = 0.0;
            double high = 1.0;
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
                double range = high - low;

                double newLow = low + range * symbolRange.low;
                double newHigh = low + range * symbolRange.high;

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

            double finalCode = (low + high) / 2.0;

            return (finalCode, steps);
        }

        /// <summary>
        /// Декодирование с отображением всех шагов
        /// </summary>
        public (string decoded, List<DecodingStep> steps) Decode(double code, int length)
        {
            if (length <= 0)
                throw new ArgumentException("Длина должна быть больше нуля!");

            List<DecodingStep> steps = new List<DecodingStep>();
            StringBuilder decoded = new StringBuilder();
            double currentCode = code; 

            for (int i = 0; i < length; i++)
            {

                char decodedSymbol = '\0';
                double symbolLow = 0, symbolHigh = 0;


                foreach (var kvp in ranges.OrderBy(x => x.Key))
                {
                    char symbol = kvp.Key;
                    var range = kvp.Value;


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
                    throw new InvalidOperationException($"Не удалось декодировать символ на шаге {i + 1}. Текущий код: {currentCode:F15}");
                }

                decoded.Append(decodedSymbol);

                var step = new DecodingStep
                {
                    StepNumber = i + 1,
                    DecodedChain = decoded.ToString(),
                    DecodedSymbol = decodedSymbol,
                    CodeValue = currentCode,
                    LowBound = symbolLow,
                    HighBound = symbolHigh
                };

                steps.Add(step);


                if (i < length - 1)
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