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
        private double encodedValue;
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
                encodedCodeBox.Text = code.ToString("F15");

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
                if (!double.TryParse(encodedCodeBox.Text, out double code))
                {
                    MessageBox.Show("Некорректный код для декодирования!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show($"Ошибка при декодировании: {ex.Message}", "Ошибка",
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

            foreach (var kvp in probabilities.OrderBy(x => x.Key))
            {
                char symbol = kvp.Key;
                double prob = kvp.Value;
                var range = ranges[symbol];

                probabilityGrid.Rows.Add(
                    symbol.ToString(),
                    prob.ToString("F6"),
                    (prob * 100).ToString("F2") + "%",
                    $"[{range.low:F6}, {range.high:F6})"
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
                double interval = step.HighBound - step.LowBound;
                encodingStepsGrid.Rows.Add(
                    step.StepNumber,
                    step.CurrentSymbol,
                    step.CurrentChain,
                    step.LowBound.ToString("F10"),
                    step.HighBound.ToString("F10"),
                    $"[{step.LowBound:F10}, {step.HighBound:F10})"
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
                    step.CodeValue.ToString("F10"),
                    step.LowBound.ToString("F10"),
                    step.HighBound.ToString("F10")
                );
            }

            // Подсветка последней строки
            if (decodingStepsGrid.Rows.Count > 0)
            {
                decodingStepsGrid.Rows[decodingStepsGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }

        private void DisplayEncodingResult(string input, double code, List<ArithmeticCoding.EncodingStep> steps)
        {
            outputBox.Clear();
            outputBox.AppendText("╔════════════════════════════════════════════════════════════════════╗\n");
            outputBox.AppendText("║                  РЕЗУЛЬТАТ КОДИРОВАНИЯ                             ║\n");
            outputBox.AppendText("╚════════════════════════════════════════════════════════════════════╝\n\n");

            outputBox.AppendText($"Исходное сообщение: \"{input}\"\n");
            outputBox.AppendText($"Длина сообщения: {input.Length} символов\n\n");

            outputBox.AppendText("ПОШАГОВОЕ КОДИРОВАНИЕ:\n");
            outputBox.AppendText(new string('─', 70) + "\n");

            foreach (var step in steps)
            {
                outputBox.AppendText($"Шаг {step.StepNumber}: Символ '{step.CurrentSymbol}'\n");
                outputBox.AppendText($"  Цепочка: \"{step.CurrentChain}\"\n");
                outputBox.AppendText($"  Интервал: [{step.LowBound:F10}, {step.HighBound:F10})\n");
                outputBox.AppendText($"  Длина интервала: {(step.HighBound - step.LowBound):E10}\n\n");
            }

            var lastStep = steps[steps.Count - 1];
            outputBox.AppendText(new string('═', 70) + "\n");
            outputBox.AppendText($"ФИНАЛЬНЫЙ ИНТЕРВАЛ: [{lastStep.LowBound:F15}, {lastStep.HighBound:F15})\n");
            outputBox.AppendText($"ЗАКОДИРОВАННОЕ ЗНАЧЕНИЕ: {code:F15}\n");
            outputBox.AppendText(new string('═', 70) + "\n\n");

            // Автозаполнение полей для декодирования
            encodedCodeBox.Text = code.ToString("F15");
            lengthBox.Text = input.Length.ToString();
        }

        private void DisplayDecodingResult(string decoded, double code, List<ArithmeticCoding.DecodingStep> steps)
        {
            outputBox.AppendText("\n\n");
            outputBox.AppendText("╔════════════════════════════════════════════════════════════════════╗\n");
            outputBox.AppendText("║                  РЕЗУЛЬТАТ ДЕКОДИРОВАНИЯ                           ║\n");
            outputBox.AppendText("╚════════════════════════════════════════════════════════════════════╝\n\n");

            outputBox.AppendText($"Исходный код для декодирования: {code:F15}\n");
            outputBox.AppendText($"Ожидаемая длина: {steps.Count} символов\n\n");

            outputBox.AppendText("ПОШАГОВОЕ ДЕКОДИРОВАНИЕ:\n");
            outputBox.AppendText(new string('─', 70) + "\n");

            foreach (var step in steps)
            {
                outputBox.AppendText($"Шаг {step.StepNumber}: Декодирован символ '{step.DecodedSymbol}'\n");
                outputBox.AppendText($"  Цепочка: \"{step.DecodedChain}\"\n");
                outputBox.AppendText($"  Текущий код: {step.CodeValue:F10}\n");
                outputBox.AppendText($"  Интервал: [{step.LowBound:F10}, {step.HighBound:F10})\n");
                outputBox.AppendText($"  Код попадает в интервал: {(step.CodeValue >= step.LowBound && step.CodeValue < step.HighBound ? "✓ ДА" : "✗ НЕТ")}\n");

                // Показываем формулу пересчета (кроме последнего шага)
                if (step.StepNumber < steps.Count)
                {
                    double nextCode = (step.CodeValue - step.LowBound) / (step.HighBound - step.LowBound);
                    outputBox.AppendText($"  Пересчет кода: ({step.CodeValue:F10} - {step.LowBound:F10}) / ({step.HighBound:F10} - {step.LowBound:F10}) = {nextCode:F10}\n");
                }
                outputBox.AppendText("\n");
            }

            outputBox.AppendText(new string('═', 70) + "\n");
            outputBox.AppendText($"ДЕКОДИРОВАННОЕ СООБЩЕНИЕ: \"{decoded}\"\n");
            outputBox.AppendText(new string('═', 70) + "\n");

            // Проверка совпадения
            string original = msgForCoding.Text;
            if (original == decoded)
            {
                outputBox.AppendText("\n✓ УСПЕХ: Декодированное сообщение совпадает с исходным!\n");
                outputBox.SelectionStart = outputBox.Text.Length - 60;
                outputBox.SelectionLength = 60;
                outputBox.SelectionColor = Color.Green;
                outputBox.SelectionFont = new Font(outputBox.Font, FontStyle.Bold);
            }
            else
            {
                outputBox.AppendText("\n✗ ОШИБКА: Декодированное сообщение не совпадает с исходным!\n");
                outputBox.SelectionStart = outputBox.Text.Length - 70;
                outputBox.SelectionLength = 70;
                outputBox.SelectionColor = Color.Red;
                outputBox.SelectionFont = new Font(outputBox.Font, FontStyle.Bold);
            }
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
            // Можно добавить валидацию или другую логику
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Пример по умолчанию
            msgForCoding.Text = "ABACABAD";
            lengthBox.Text = "8";
            decodingBtn.Enabled = false;
        }
    }
}