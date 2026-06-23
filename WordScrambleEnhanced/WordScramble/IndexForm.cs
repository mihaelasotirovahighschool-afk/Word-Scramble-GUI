using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WordScramble;

public partial class IndexForm : Form
{
    // Името на файла, от който зареждаме всички думи.
    // Файлът се копира автоматично до .exe файла чрез настройката в .csproj файла.
    private const string WordsFileName = "words.txt";

    // Максимален брой грешни опити за една дума.
    // След този брой играта автоматично преминава към нова дума.
    private const int MaxAttempts = 9;

    // Време за един рунд в секунди.
    // ProgressBar-ът използва тази стойност като максимум.
    private const int RoundSeconds = 45;

    // Тук пазим всички грешни предположения на играча за текущата дума.
    private readonly List<string> failedAttempts = new();

    // Тук пазим всички думи, прочетени от words.txt.
    private readonly List<string> allWords = new();

    // Речник с категории. Ключът е името на категорията, а стойността е списък с думи.
    private readonly Dictionary<string, List<string>> categoryWords = new(StringComparer.OrdinalIgnoreCase);

    // Това е списъкът с думи, които остават за избраната категория.
    // Когато играчът познае дума, тя се маха от този списък.
    private List<string> currentWordPool = new();

    // Random се използва за избиране на случайна дума и за разбъркване на буквите.
    private readonly Random random = new();

    // Брояч за грешните опити към текущата дума.
    private int attempts;

    // Брояч за всички познати думи от началото на играта.
    private int guessedWords;

    // Точки на играча. Дават се според дължината на думата, оставащото време и грешките.
    private int score;

    // Оставащо време за текущия рунд.
    private int secondsLeft;

    // Показва дали играчът вече е използвал подсказка за тази дума.
    // Ако е използвал подсказка, получава по-малко точки.
    private bool hintUsed;

    // Използва се, за да не се изпълнява логиката за категории преди формата да е заредила думите.
    private bool gameIsReady;

    // Текущата истинска дума, която играчът трябва да познае.
    private string currentWord = string.Empty;

    // Текущата избрана категория от падащото меню.
    private string currentCategory = "All words";

    public IndexForm()
    {
        InitializeComponent();
    }

    private void IndexFormLoad(object? sender, EventArgs e)
    {
        // 1. Четем думите от текстовия файл.
        GetAllWords();

        // 2. Правим категориите, които ще се сменят от ComboBox-а.
        BuildCategoryDictionary();

        // 3. Позволяваме на събитията да работят, защото данните вече са готови.
        gameIsReady = true;

        // 4. Избираме началната категория. Това автоматично стартира първата дума.
        comboBoxCategory.SelectedIndex = 0;

        // 5. Прилагаме светлия режим като начален изглед.
        ApplyTheme();
    }

    private void GetAllWords()
    {
        allWords.Clear();

        // AppContext.BaseDirectory е папката, в която се намира стартираното приложение.
        string wordsPath = Path.Combine(AppContext.BaseDirectory, WordsFileName);

        // Когато работим във Visual Studio, понякога файлът може да е в текущата директория.
        // Това е резервна проверка, за да избегнем грешка при стартиране.
        if (!File.Exists(wordsPath))
        {
            wordsPath = Path.Combine(Environment.CurrentDirectory, WordsFileName);
        }

        if (!File.Exists(wordsPath))
        {
            MessageBox.Show("The words.txt file was not found.", "Missing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
            return;
        }

        // StreamReader чете файла ред по ред.
        // Всеки ред е отделна дума.
        using StreamReader reader = new(wordsPath);

        while (!reader.EndOfStream)
        {
            string? word = reader.ReadLine();

            // Пропускаме празни редове и правим думите с малки букви.
            if (!string.IsNullOrWhiteSpace(word))
            {
                allWords.Add(word.Trim().ToLower());
            }
        }
    }

    private void BuildCategoryDictionary()
    {
        categoryWords.Clear();

        // "All words" използва целия файл words.txt.
        categoryWords["All words"] = new List<string>(allWords);

        // Категориите са подбрани от думите във файла.
        // Така проектът има допълнителна функция, без да е прекалено сложен.
        categoryWords["Animals"] = GetExistingWords(
            "horse", "birds", "whale", "snake", "bears", "mouse", "tiger", "shark", "camel", "lambs", "lions", "frogs", "goats", "ducks", "herds");

        categoryWords["Objects"] = GetExistingWords(
            "house", "table", "chair", "piano", "truck", "radio", "clock", "phone", "sword", "knife", "cabin", "boats", "wagon", "plate", "cards", "shelf", "brick", "cable", "lamps", "shirt");

        categoryWords["Nature"] = GetExistingWords(
            "water", "earth", "night", "light", "ocean", "rocks", "stone", "waves", "beach", "cloud", "wheat", "storm", "lakes", "coral", "frost", "skies", "flame", "tides", "vapor");

        categoryWords["People"] = GetExistingWords(
            "woman", "child", "human", "girls", "uncle", "mayor", "poets", "kings", "clerk", "owner", "agent", "guest", "adult", "tribe", "youth");

        categoryWords["Actions"] = GetExistingWords(
            "write", "think", "learn", "build", "solve", "speak", "watch", "drive", "dance", "cried", "began", "moves", "bring", "trade", "teach", "climb", "throw", "laugh", "shout");

        categoryWords["Food"] = GetExistingWords(
            "bread", "sugar", "fruit", "cream", "honey", "lemon", "bacon", "peach", "olive", "flour", "juice", "beans", "wheat", "sauce", "candy");
    }

    private List<string> GetExistingWords(params string[] words)
    {
        // Тази функция взима само думите, които наистина присъстват във файла.
        // Ако някоя дума бъде изтрита от words.txt, програмата няма да се счупи.
        List<string> result = new();

        foreach (string word in words)
        {
            if (allWords.Contains(word))
            {
                result.Add(word);
            }
        }

        return result;
    }

    private void StartSelectedCategory()
    {
        currentCategory = comboBoxCategory.SelectedItem?.ToString() ?? "All words";

        // Взимаме копие на думите за категорията.
        // Използваме копие, защото после ще махаме познатите думи от currentWordPool.
        if (categoryWords.TryGetValue(currentCategory, out List<string>? selectedWords) && selectedWords.Count > 0)
        {
            currentWordPool = new List<string>(selectedWords);
        }
        else
        {
            currentCategory = "All words";
            currentWordPool = new List<string>(allWords);
        }

        // При смяна на категорията започваме нов рунд, но оставяме точките и общия брой познати думи.
        failedAttempts.Clear();
        GenerateNewWord();
        UpdateLabels();
    }

    private void GenerateNewWord()
    {
        if (currentWordPool.Count == 0)
        {
            MessageBox.Show("You guessed all words in this category. The category will restart.", "Category finished");
            currentWordPool = new List<string>(categoryWords[currentCategory]);
        }

        // Избираме случайна позиция от списъка и взимаме думата на тази позиция.
        int randomIndex = random.Next(currentWordPool.Count);
        currentWord = currentWordPool[randomIndex];

        // След избиране на нова дума нулираме информацията за текущия рунд.
        ResetRoundInfo();
    }

    private void ResetRoundInfo()
    {
        attempts = 0;
        secondsLeft = RoundSeconds;
        hintUsed = false;
        failedAttempts.Clear();

        // Показваме разбърканата дума, а не истинската дума.
        labelScrambledWord.Text = ScrambleWord(currentWord);
        labelHintValue.Text = "No hint used yet";

        // Рестартираме таймера за новия рунд.
        roundTimer.Stop();
        roundTimer.Start();

        UpdateTimerVisuals();
    }

    private string ScrambleWord(string word)
    {
        // Fisher-Yates shuffle алгоритъм.
        // Идеята е да минем отзад напред през масива от букви
        // и всяка буква да я разменим със случайна буква преди нея.
        if (word.Length <= 1)
        {
            return word;
        }

        for (int shuffleAttempt = 0; shuffleAttempt < 10; shuffleAttempt++)
        {
            char[] chars = word.ToCharArray();

            for (int n = chars.Length - 1; n > 0; n--)
            {
                int k = random.Next(n + 1);
                (chars[n], chars[k]) = (chars[k], chars[n]);
            }

            string scrambled = new(chars);

            // Ако случайно разбърканата дума е същата като оригинала, опитваме пак.
            if (scrambled != word)
            {
                return scrambled;
            }
        }

        // Резервен вариант: ако 10 пъти случайно се получи същата дума, обръщаме буквите.
        return new string(word.Reverse().ToArray());
    }

    private void ButtonCheckClick(object? sender, EventArgs e)
    {
        CheckTheWord();
        UpdateLabels();
    }

    private void CheckTheWord()
    {
        string input = textBoxInput.Text.Trim().ToLower();

        if (string.IsNullOrEmpty(input))
        {
            MessageBox.Show("Please write a guess first.", "Empty answer");
            return;
        }

        // Ако въведената дума е равна на истинската дума, играчът печели рунда.
        if (input == currentWord)
        {
            SuccessfulAttempt();
        }
        else
        {
            UnsuccessfulAttempt(input);
        }
    }

    private void SuccessfulAttempt()
    {
        // Формула за точки:
        // - по-дългите думи дават повече точки;
        // - оставащото време дава бонус;
        // - грешните опити намаляват точките;
        // - използвана подсказка намалява резултата.
        int points = currentWord.Length * 10 + secondsLeft - attempts * 2;

        if (hintUsed)
        {
            points -= 10;
        }

        // Гарантираме, че при позната дума играчът винаги получава поне 5 точки.
        points = Math.Max(5, points);

        score += points;
        guessedWords++;

        // Махаме думата от текущата категория, за да не се пада веднага отново.
        currentWordPool.Remove(currentWord);

        MessageBox.Show($"Correct! You earned {points} points.", "Good job");
        GenerateNewWord();
    }

    private void UnsuccessfulAttempt(string input)
    {
        attempts++;
        failedAttempts.Add(input);

        // Ако играчът сгреши твърде много пъти, играта показва правилната дума и сменя рунда.
        if (attempts > MaxAttempts)
        {
            MessageBox.Show($"Too many attempts! The correct word was: {currentWord}", "New word");
            GenerateNewWord();
        }
    }

    private void UpdateLabels()
    {
        labelAttemptsValue.Text = attempts.ToString();
        labelGuessedWordsValue.Text = guessedWords.ToString();
        labelScoreValue.Text = score.ToString();
        textBoxFailedAttempts.Text = string.Join(Environment.NewLine, failedAttempts);

        UpdateTimerVisuals();

        // След всеки опит изчистваме полето, за да е готово за следващото предположение.
        textBoxInput.Clear();
        textBoxInput.Focus();
    }

    private void ButtonSkipClick(object? sender, EventArgs e)
    {
        MessageBox.Show($"Skipped! The word was: {currentWord}", "Skipped word");
        GenerateNewWord();
        UpdateLabels();
    }

    private void ButtonHintClick(object? sender, EventArgs e)
    {
        if (hintUsed)
        {
            MessageBox.Show("You already used the hint for this word.", "Hint used");
            return;
        }

        hintUsed = true;

        // Подсказката показва първата и последната буква.
        // Междинните букви са скрити с долни черти, за да не е твърде лесно.
        if (currentWord.Length <= 2)
        {
            labelHintValue.Text = currentWord[0].ToString();
        }
        else
        {
            string hiddenMiddle = new('_', currentWord.Length - 2);
            labelHintValue.Text = $"{currentWord[0]}{hiddenMiddle}{currentWord[^1]}";
        }
    }

    private void RoundTimerTick(object? sender, EventArgs e)
    {
        secondsLeft--;
        UpdateTimerVisuals();

        if (secondsLeft <= 0)
        {
            roundTimer.Stop();
            failedAttempts.Add("Time expired");
            MessageBox.Show($"Time is up! The correct word was: {currentWord}", "Time challenge");
            GenerateNewWord();
            UpdateLabels();
        }
    }

    private void UpdateTimerVisuals()
    {
        // ProgressBar-ът се пълни в началото и постепенно намалява до 0.
        progressBarTime.Maximum = RoundSeconds;
        progressBarTime.Value = Math.Max(0, Math.Min(secondsLeft, RoundSeconds));
        labelTimeLeftValue.Text = secondsLeft + "s";

        // Цветовете са визуален сигнал: нормално време, предупреждение и опасност.
        if (secondsLeft <= 10)
        {
            labelTimeLeftValue.BackColor = Color.Firebrick;
        }
        else if (secondsLeft <= 20)
        {
            labelTimeLeftValue.BackColor = Color.DarkOrange;
        }
        else
        {
            labelTimeLeftValue.BackColor = Color.FromArgb(50, 135, 160);
        }

        labelTimeLeftValue.ForeColor = Color.White;
    }

    private void TextBoxInputKeyDown(object? sender, KeyEventArgs e)
    {
        // Enter работи като натискане на бутона Check.
        // Така играта е по-удобна за играча.
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            ButtonCheckClick(sender, e);
        }
    }

    private void ComboBoxCategorySelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!gameIsReady)
        {
            return;
        }

        // ComboBox-ът работи като превключвател между категориите.
        // При смяна на категорията веднага започва нов рунд с думи от избраната тема.
        StartSelectedCategory();
    }

    private void CheckBoxDarkModeCheckedChanged(object? sender, EventArgs e)
    {
        // CheckBox с Appearance = Button изглежда като бутон-превключвател.
        // Когато е включен, използваме тъмен режим; когато е изключен, светъл режим.
        ApplyTheme();
    }

    private void ApplyTheme()
    {
        bool darkMode = checkBoxDarkMode.Checked;

        Color background = darkMode ? Color.FromArgb(28, 30, 39) : Color.FromArgb(246, 247, 255);
        Color textColor = darkMode ? Color.FromArgb(235, 238, 245) : Color.FromArgb(35, 35, 45);
        Color inputBack = darkMode ? Color.FromArgb(45, 48, 62) : Color.White;
        Color outputBack = darkMode ? Color.FromArgb(55, 58, 72) : Color.FromArgb(220, 210, 210);
        Color accent = darkMode ? Color.FromArgb(75, 150, 185) : Color.FromArgb(50, 135, 160);
        Color accentSecond = darkMode ? Color.FromArgb(95, 110, 190) : Color.FromArgb(70, 110, 170);

        BackColor = background;

        foreach (Control control in Controls)
        {
            control.ForeColor = textColor;

            if (control is TextBox textBox)
            {
                textBox.BackColor = textBox == textBoxFailedAttempts ? outputBack : inputBack;
                textBox.ForeColor = textColor;
            }
        }

        comboBoxCategory.BackColor = inputBack;
        comboBoxCategory.ForeColor = textColor;

        StyleButton(buttonCheck, accent);
        StyleButton(buttonSkip, accent);
        StyleButton(buttonHint, accentSecond);

        checkBoxDarkMode.BackColor = darkMode ? Color.FromArgb(230, 230, 240) : Color.FromArgb(45, 48, 62);
        checkBoxDarkMode.ForeColor = darkMode ? Color.FromArgb(30, 30, 40) : Color.White;
        checkBoxDarkMode.Text = darkMode ? "Light Mode" : "Dark Mode";

        StyleValueLabel(labelAttemptsValue, accent);
        StyleValueLabel(labelGuessedWordsValue, accent);
        StyleValueLabel(labelScoreValue, accent);
        UpdateTimerVisuals();
    }

    private static void StyleButton(Button button, Color backColor)
    {
        button.BackColor = backColor;
        button.ForeColor = Color.White;
        button.FlatAppearance.BorderColor = Color.White;
    }

    private static void StyleValueLabel(Label label, Color backColor)
    {
        label.BackColor = backColor;
        label.ForeColor = Color.White;
    }
}
