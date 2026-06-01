using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WordScrambleApp
{
    public partial class IndexForm : Form
    {
        // --- GAME DATA ---
        List<string> bgWords = new List<string>();
        List<string> enWords = new List<string>();

        // --- CURRENT GAME STATE VARIABLES ---
        string currentWord = "";
        string scrambledWord = "";
        int score = 0;
        int errors = 0;
        int timeLeft = 30;
        bool hintUsed = false;
        string currentLanguage = "BG";

        Random random = new Random();

        public IndexForm()
        {
            InitializeComponent();
        }

        private void IndexForm_Load(object sender, EventArgs e)
        {
            LoadWordsFromFile();

            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 2;
            }

            currentLanguage = "BG";
            LoadNewWord();
        }

        // --- METHOD TO LOAD WORDS FROM YOUR TEXT FILE ---
        private void LoadWordsFromFile()
        {
            string filePath = "words.txt";

            // Clear lists first to avoid duplicates when reloading
            bgWords.Clear();
            enWords.Clear();

            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string word = line.Trim();
                        if (word.Length == 5)
                        {
                            if (word.Any(c => (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я')))
                            {
                                bgWords.Add(word);
                            }
                            else
                            {
                                enWords.Add(word);
                            }
                        }
                    }
                }
                else
                {
                    bgWords.AddRange(new[] { "книга", "молив", "чанта", "дъска", "папка" });
                    enWords.AddRange(new[] { "about", "world", "house", "water", "board" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading words: " + ex.Message);
            }
        }

        // --- NEW METHOD: ADDING A NEW WORD TO THE FILE ---
        private void btnAddNewWord_Click(object sender, EventArgs e)
        {
            string newWord = txtNewWord.Text.Trim();

            // 1. Validation check - must be exactly 5 letters
            if (newWord.Length != 5)
            {
                if (currentLanguage == "EN")
                    MessageBox.Show("The word must be exactly 5 letters long!", "Invalid Word", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Думата трябва да бъде точно от 5 букви!", "Невалидна дума", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string filePath = "words.txt";

            try
            {
                // 2. Append the new word to the text file on a new line
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(newWord);
                }

                // 3. Inform the user
                if (currentLanguage == "EN")
                    MessageBox.Show($"Word '{newWord}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Думата '{newWord}' беше добавена успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the input field
                txtNewWord.Text = "";

                // 4. Reload the words list so the new word can be played immediately
                LoadWordsFromFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving word: " + ex.Message);
            }
        }

        // --- METHOD TO PREPARE AND SCRAMBLE A WORD ---
        private void LoadNewWord()
        {
            List<string> selectedList = currentLanguage == "EN" ? enWords : bgWords;

            if (selectedList.Count == 0)
            {
                if (currentLanguage == "EN") enWords.AddRange(new[] { "about", "world", "house" });
                else bgWords.AddRange(new[] { "книга", "молив", "чанта" });
                selectedList = currentLanguage == "EN" ? enWords : bgWords;
            }

            int randomIndex = random.Next(0, selectedList.Count);
            currentWord = selectedList[randomIndex];

            char[] letters = currentWord.ToCharArray();
            for (int i = 0; i < letters.Length; i++)
            {
                int j = random.Next(0, letters.Length);
                char temp = letters[i];
                letters[i] = letters[j];
                letters[j] = temp;
            }
            scrambledWord = new string(letters);

            if (scrambledWord == currentWord && currentWord.Length > 1)
            {
                letters = scrambledWord.ToCharArray();
                Array.Reverse(letters);
                scrambledWord = new string(letters);
            }

            lblScrambledWord.Text = scrambledWord.ToUpper();
            txtGuess.Text = "";

            if (currentLanguage == "EN")
                lblHintDisplay.Text = "💡 Click hint to get help!";
            else
                lblHintDisplay.Text = "💡 Натисни подсказка за помощ!";

            hintUsed = false;
            btnHint.Enabled = true;

            timeLeft = 30;
            prgTime.Value = 30;
            gameTimer.Start();
        }

        // --- BUTTONS LOGIC ---
        private void btnCheck_Click(object sender, EventArgs e)
        {
            string playerGuess = txtGuess.Text.Trim().ToLower();

            if (playerGuess == currentWord.ToLower())
            {
                score++;
                if (currentLanguage == "EN")
                    MessageBox.Show("Correct! Well done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Точно така! Браво!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblScore.Text = currentLanguage == "EN" ? $"Score: {score}" : $"Резултат: {score}";
                LoadNewWord();
            }
            else
            {
                errors++;
                lblErrors.Text = currentLanguage == "EN" ? $"Errors: {errors}" : $"Грешки: {errors}";

                if (!lstWrongWords.Items.Contains(currentWord))
                {
                    lstWrongWords.Items.Add(currentWord);
                }

                if (currentLanguage == "EN")
                    MessageBox.Show("Wrong guess! Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Грешен отговор! Опитай пак.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!lstWrongWords.Items.Contains(currentWord))
            {
                lstWrongWords.Items.Add(currentWord);
            }
            LoadNewWord();
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            if (!hintUsed && currentWord.Length > 2)
            {
                char firstLetter = currentWord[0];
                char lastLetter = currentWord[currentWord.Length - 1];

                if (currentLanguage == "EN")
                    lblHintDisplay.Text = $"💡 Hint: Starts with '{firstLetter}' and ends with '{lastLetter}'";
                else
                    lblHintDisplay.Text = $"💡 Подсказка: Започва с '{firstLetter}' и завършва на '{lastLetter}'";

                hintUsed = true;
                btnHint.Enabled = false;
            }
        }

        private void btnLangBG_Click(object sender, EventArgs e)
        {
            currentLanguage = "BG";
            btnLangBG.Text = "BG 🇧🇬 (Active)";
            btnLangEN.Text = "EN 🇺🇸";

            btnCheck.Text = "Провери";
            btnNext.Text = "Смени думата";
            btnHint.Text = "💡 Подсказка";
            btnAddNewWord.Text = "Добави дума"; // Translate new button
            lblScore.Text = $"Резултат: {score}";
            lblErrors.Text = $"Грешки: {errors}";

            LoadNewWord();
        }

        private void btnLangEN_Click(object sender, EventArgs e)
        {
            currentLanguage = "EN";
            btnLangBG.Text = "BG 🇧🇬";
            btnLangEN.Text = "EN 🇺🇸 (Active)";

            btnCheck.Text = "Check Answer";
            btnNext.Text = "Change Word";
            btnHint.Text = "💡 Get Hint";
            btnAddNewWord.Text = "Add Word"; // Translate new button
            lblScore.Text = $"Score: {score}";
            lblErrors.Text = $"Errors: {errors}";

            LoadNewWord();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                prgTime.Value = timeLeft;

                if (currentLanguage == "EN")
                    lblTimerDisplay.Text = $"Time: {timeLeft}s";
                else
                    lblTimerDisplay.Text = $"Време: {timeLeft}сек.";
            }
            else
            {
                gameTimer.Stop();
                if (!lstWrongWords.Items.Contains(currentWord))
                {
                    lstWrongWords.Items.Add(currentWord);
                }

                if (currentLanguage == "EN")
                    MessageBox.Show($"Time's up! The word was: {currentWord}", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show($"Времето изтече! Думата беше: {currentWord}", "Времето изтече", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LoadNewWord();
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Created)
            {
                LoadNewWord();
            }
        }
        private void txtNewWord_TextChanged(object sender, EventArgs e)
        {
            // Тук кодът ще се изпълнява всеки път, когато потребителят пише в полето.
            // За момента можеш да го оставиш празен, за да изчезне грешката.
        }
    }

}
