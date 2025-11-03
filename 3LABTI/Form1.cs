using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _3LABTI
{
    public partial class Form1 : Form
    {
        private ArithmeticCoding arithmeticCoding;
        private decimal encodedValue;
        private int messageLength;

        public Form1()
        {
            InitializeComponent();
            arithmeticCoding = new ArithmeticCoding();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Настройка DataGridView для вероятностей
            probabilityGrid.AllowUserToAddRows = false;
            probabilityGrid.AllowUserToDeleteRows = false;
            probabilityGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            probabilityGrid.MultiSelect = false;
            probabilityGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Настройка DataGridView для шагов кодирования
            encodingStepsGrid.AllowUserToAddRows = false;
            encodingStepsGrid.AllowUserToDeleteRows = false;
            encodingStepsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            encodingStepsGrid.MultiSelect = false;
            encodingStepsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            encodingStepsGrid.ReadOnly = true;

            // Настройка DataGridView для шагов декодирования
            decodingStepsGrid.AllowUserToAddRows = false;
            decodingStepsGrid.AllowUserToDeleteRows = false;
            decodingStepsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            decodingStepsGrid.MultiSelect = false;
            decodingStepsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            decodingStepsGrid.ReadOnly = true;

            // Настройка RichTextBox
            outputBox.ReadOnly = true;
            outputBox.Font = new Font("Consolas", 9);

            // Разрешаем редактирование поля кода
            encodedCodeBox.ReadOnly = false;
        }

        private void codingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string input = msgForCoding.Text;

                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Введите сообщение для кодирования!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Очистка предыдущих результатов
                outputBox.Clear();
                encodingStepsGrid.Rows.Clear();
                decodingStepsGrid.Rows.Clear();
                probabilityGrid.Rows.Clear();

                // Инициализация и кодирование
                arithmeticCoding.Initialize(input);
                var (code, steps) = arithmeticCoding.Encode(input);

                encodedValue = code;
                messageLength = input.Length;

                // Отображение результатов
                DisplayProbabilities();
                DisplayEncodingSteps(steps);
                DisplayEncodingResult(input, code, steps);

                // Активация кнопки декодирования
                decodingBtn.Enabled = true;
                encodedCodeBox.Text = code.ToString();
                lengthBox.Text = input.Length.ToString();

                MessageBox.Show("Кодирование выполнено успешно!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при кодировании: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void decodingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка инициализации
                if (!arithmeticCoding.IsInitialized())
                {
                    MessageBox.Show("Сначала выполните кодирование для построения таблицы вероятностей!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(encodedCodeBox.Text, out decimal code))
                {
                    MessageBox.Show("Некорректный код для декодирования! Введите число в формате 0.123456",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(lengthBox.Text, out int length) || length <= 0)
                {
                    MessageBox.Show("Некорректная длина сообщения!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Декодирование
                var (decoded, steps) = arithmeticCoding.Decode(code, length);

                // Отображение результатов
                DisplayDecodingSteps(steps);
                DisplayDecodingResult(decoded, code, steps);
                decodedMsgBox.Text = decoded;

                MessageBox.Show("Декодирование выполнено успешно!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при декодировании:\n\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayProbabilities()
        {
            var probabilities = arithmeticCoding.GetProbabilities();
            var ranges = arithmeticCoding.GetRanges();

            probabilityGrid.Columns.Clear();
            probabilityGrid.Columns.Add("Symbol", "Символ");
            probabilityGrid.Columns.Add("Probability", "Вероятность");
            probabilityGrid.Columns.Add("Percentage", "Процент");
            probabilityGrid.Columns.Add("Range", "Диапазон [low, high)");

            // Сортируем ПО УБЫВАНИЮ вероятностей (как в таблице ranges)
            foreach (var kvp in probabilities.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                char symbol = kvp.Key;
                decimal prob = kvp.Value;
                var range = ranges[symbol];

                probabilityGrid.Rows.Add(
                    symbol.ToString(),
                    prob.ToString(),
                    (prob * 100).ToString() + "%",
                    $"[{range.low}, {range.high})"
                );
            }
        }

        private void DisplayEncodingSteps(List<ArithmeticCoding.EncodingStep> steps)
        {
            encodingStepsGrid.Columns.Clear();
            encodingStepsGrid.Columns.Add("Step", "Шаг");
            encodingStepsGrid.Columns.Add("Symbol", "Символ");
            encodingStepsGrid.Columns.Add("Chain", "Цепочка");
            encodingStepsGrid.Columns.Add("Low", "Нижняя граница");
            encodingStepsGrid.Columns.Add("High", "Верхняя граница");
            encodingStepsGrid.Columns.Add("Interval", "Интервал");

            foreach (var step in steps)
            {
                encodingStepsGrid.Rows.Add(
                    step.StepNumber,
                    step.CurrentSymbol,
                    step.CurrentChain,
                    step.LowBound.ToString(),
                    step.HighBound.ToString(),
                    $"[{step.LowBound}, {step.HighBound})"
                );
            }

            // Подсветка последней строки
            if (encodingStepsGrid.Rows.Count > 0)
            {
                encodingStepsGrid.Rows[encodingStepsGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        private void DisplayDecodingSteps(List<ArithmeticCoding.DecodingStep> steps)
        {
            decodingStepsGrid.Columns.Clear();
            decodingStepsGrid.Columns.Add("Step", "Шаг");
            decodingStepsGrid.Columns.Add("Symbol", "Символ");
            decodingStepsGrid.Columns.Add("Chain", "Цепочка");
            decodingStepsGrid.Columns.Add("Code", "Код");
            decodingStepsGrid.Columns.Add("Low", "Нижняя граница");
            decodingStepsGrid.Columns.Add("High", "Верхняя граница");

            foreach (var step in steps)
            {
                decodingStepsGrid.Rows.Add(
                    step.StepNumber,
                    step.DecodedSymbol,
                    step.DecodedChain,
                    step.CodeValue.ToString(),
                    step.LowBound.ToString(),
                    step.HighBound.ToString()
                );
            }

            // Подсветка последней строки
            if (decodingStepsGrid.Rows.Count > 0)
            {
                decodingStepsGrid.Rows[decodingStepsGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }

        private void DisplayEncodingResult(string input, decimal code, List<ArithmeticCoding.EncodingStep> steps)
        {
            outputBox.Clear();
            outputBox.AppendText($"Исходное сообщение: \"{input}\"\n");
            outputBox.AppendText($"Длина сообщения: {input.Length} символов\n\n");

            outputBox.AppendText("ПОШАГОВОЕ КОДИРОВАНИЕ:\n");

            foreach (var step in steps)
            {
                outputBox.AppendText($"Шаг {step.StepNumber}: Символ '{step.CurrentSymbol}'\n");
                outputBox.AppendText($"  Цепочка: \"{step.CurrentChain}\"\n");
                outputBox.AppendText($"  Интервал: [{step.LowBound}, {step.HighBound})\n");
                outputBox.AppendText($"  Длина интервала: {step.HighBound - step.LowBound}\n\n");
            }

            var lastStep = steps[steps.Count - 1];
            outputBox.AppendText($"ФИНАЛЬНЫЙ ИНТЕРВАЛ: [{lastStep.LowBound}, {lastStep.HighBound})\n");
            outputBox.AppendText($"ЗАКОДИРОВАННОЕ ЗНАЧЕНИЕ: {code}\n");
            outputBox.AppendText(new string('═', 70));
        }

        private void DisplayDecodingResult(string decoded, decimal code, List<ArithmeticCoding.DecodingStep> steps)
        {
            outputBox.AppendText($"\nКод для декодирования: {code}\n");
            outputBox.AppendText($"Ожидаемая длина: {steps.Count} символов\n\n");

            outputBox.AppendText("ПОШАГОВОЕ ДЕКОДИРОВАНИЕ:\n");

            foreach (var step in steps)
            {
                outputBox.AppendText($"Шаг {step.StepNumber}: Декодирован символ '{step.DecodedSymbol}'\n");
                outputBox.AppendText($"  Цепочка: \"{step.DecodedChain}\"\n");
                outputBox.AppendText($"  Текущий код: {step.CodeValue}\n");
                outputBox.AppendText($"  Интервал: [{step.LowBound}, {step.HighBound})\n");
                outputBox.AppendText($"  Код попадает в интервал: {(step.CodeValue >= step.LowBound && step.CodeValue < step.HighBound ? "ДА" : "НЕТ")}\n");

                // Показываем формулу пересчета (кроме последнего шага)
                if (step.StepNumber < steps.Count)
                {
                    decimal nextCode = (step.CodeValue - step.LowBound) / (step.HighBound - step.LowBound);
                    outputBox.AppendText($"  Пересчет кода: ({step.CodeValue} - {step.LowBound}) / ({step.HighBound} - {step.LowBound}) = {nextCode}\n");
                }
                outputBox.AppendText("\n");
            }
            outputBox.AppendText($"ДЕКОДИРОВАННОЕ СООБЩЕНИЕ: \"{decoded}\"\n");
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            msgForCoding.Clear();
            encodedCodeBox.Clear();
            lengthBox.Clear();
            decodedMsgBox.Clear();
            outputBox.Clear();
            probabilityGrid.Rows.Clear();
            encodingStepsGrid.Rows.Clear();
            decodingStepsGrid.Rows.Clear();
            decodingBtn.Enabled = false;
        }

        private void msgForCoding_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}