using System.Drawing;
using System.Windows.Forms;

namespace WordScramble;

partial class IndexForm
{
    private System.ComponentModel.IContainer components = null!;

    private Label labelTitle = null!;
    private Label labelAttempts = null!;
    private Label labelAttemptsValue = null!;
    private Label labelGuessedWords = null!;
    private Label labelGuessedWordsValue = null!;
    private Label labelScore = null!;
    private Label labelScoreValue = null!;
    private Label labelTimeLeft = null!;
    private Label labelTimeLeftValue = null!;
    private Label labelCategory = null!;
    private ComboBox comboBoxCategory = null!;
    private CheckBox checkBoxDarkMode = null!;
    private ProgressBar progressBarTime = null!;
    private Label labelScrambledWord = null!;
    private TextBox textBoxInput = null!;
    private Button buttonCheck = null!;
    private Button buttonSkip = null!;
    private Button buttonHint = null!;
    private Label labelHint = null!;
    private Label labelHintValue = null!;
    private Label labelFailedAttempts = null!;
    private TextBox textBoxFailedAttempts = null!;
    private System.Windows.Forms.Timer roundTimer = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        labelTitle = new Label();
        labelAttempts = new Label();
        labelAttemptsValue = new Label();
        labelGuessedWords = new Label();
        labelGuessedWordsValue = new Label();
        labelScore = new Label();
        labelScoreValue = new Label();
        labelTimeLeft = new Label();
        labelTimeLeftValue = new Label();
        labelCategory = new Label();
        comboBoxCategory = new ComboBox();
        checkBoxDarkMode = new CheckBox();
        progressBarTime = new ProgressBar();
        labelScrambledWord = new Label();
        textBoxInput = new TextBox();
        buttonCheck = new Button();
        buttonSkip = new Button();
        buttonHint = new Button();
        labelHint = new Label();
        labelHintValue = new Label();
        labelFailedAttempts = new Label();
        textBoxFailedAttempts = new TextBox();
        roundTimer = new System.Windows.Forms.Timer(components);
        SuspendLayout();
        // 
        // labelTitle
        // 
        labelTitle.Anchor = AnchorStyles.Top;
        labelTitle.Font = new Font("Georgia", 26F, FontStyle.Bold, GraphicsUnit.Point);
        labelTitle.Location = new Point(12, 22);
        labelTitle.Name = "labelTitle";
        labelTitle.Size = new Size(700, 50);
        labelTitle.TabIndex = 0;
        labelTitle.Text = "Word Scramble";
        labelTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // labelAttempts
        // 
        labelAttempts.AutoSize = true;
        labelAttempts.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        labelAttempts.Location = new Point(49, 100);
        labelAttempts.Name = "labelAttempts";
        labelAttempts.Size = new Size(84, 18);
        labelAttempts.TabIndex = 1;
        labelAttempts.Text = "Attempts:";
        // 
        // labelAttemptsValue
        // 
        labelAttemptsValue.BackColor = Color.FromArgb(50, 135, 160);
        labelAttemptsValue.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        labelAttemptsValue.ForeColor = Color.White;
        labelAttemptsValue.Location = new Point(139, 95);
        labelAttemptsValue.Name = "labelAttemptsValue";
        labelAttemptsValue.Size = new Size(36, 28);
        labelAttemptsValue.TabIndex = 2;
        labelAttemptsValue.Text = "0";
        labelAttemptsValue.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // labelGuessedWords
        // 
        labelGuessedWords.AutoSize = true;
        labelGuessedWords.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        labelGuessedWords.Location = new Point(219, 100);
        labelGuessedWords.Name = "labelGuessedWords";
        labelGuessedWords.Size = new Size(132, 18);
        labelGuessedWords.TabIndex = 3;
        labelGuessedWords.Text = "Guessed words:";
        // 
        // labelGuessedWordsValue
        // 
        labelGuessedWordsValue.BackColor = Color.FromArgb(50, 135, 160);
        labelGuessedWordsValue.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        labelGuessedWordsValue.ForeColor = Color.White;
        labelGuessedWordsValue.Location = new Point(357, 95);
        labelGuessedWordsValue.Name = "labelGuessedWordsValue";
        labelGuessedWordsValue.Size = new Size(36, 28);
        labelGuessedWordsValue.TabIndex = 4;
        labelGuessedWordsValue.Text = "0";
        labelGuessedWordsValue.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // labelScore
        // 
        labelScore.AutoSize = true;
        labelScore.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        labelScore.Location = new Point(452, 100);
        labelScore.Name = "labelScore";
        labelScore.Size = new Size(56, 18);
        labelScore.TabIndex = 5;
        labelScore.Text = "Score:";
        // 
        // labelScoreValue
        // 
        labelScoreValue.BackColor = Color.FromArgb(50, 135, 160);
        labelScoreValue.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        labelScoreValue.ForeColor = Color.White;
        labelScoreValue.Location = new Point(514, 95);
        labelScoreValue.Name = "labelScoreValue";
        labelScoreValue.Size = new Size(74, 28);
        labelScoreValue.TabIndex = 6;
        labelScoreValue.Text = "0";
        labelScoreValue.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // labelCategory
        // 
        labelCategory.AutoSize = true;
        labelCategory.Font = new Font("Georgia", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
        labelCategory.Location = new Point(49, 147);
        labelCategory.Name = "labelCategory";
        labelCategory.Size = new Size(82, 17);
        labelCategory.TabIndex = 7;
        labelCategory.Text = "Category:";
        // 
        // comboBoxCategory
        // 
        comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxCategory.Font = new Font("Georgia", 10F, FontStyle.Regular, GraphicsUnit.Point);
        comboBoxCategory.FormattingEnabled = true;
        comboBoxCategory.Items.AddRange(new object[] { "All words", "Animals", "Objects", "Nature", "People", "Actions", "Food" });
        comboBoxCategory.Location = new Point(139, 143);
        comboBoxCategory.Name = "comboBoxCategory";
        comboBoxCategory.Size = new Size(150, 24);
        comboBoxCategory.TabIndex = 1;
        comboBoxCategory.SelectedIndexChanged += ComboBoxCategorySelectedIndexChanged;
        // 
        // checkBoxDarkMode
        // 
        checkBoxDarkMode.Appearance = Appearance.Button;
        checkBoxDarkMode.Font = new Font("Georgia", 10F, FontStyle.Bold, GraphicsUnit.Point);
        checkBoxDarkMode.Location = new Point(312, 140);
        checkBoxDarkMode.Name = "checkBoxDarkMode";
        checkBoxDarkMode.Size = new Size(122, 31);
        checkBoxDarkMode.TabIndex = 2;
        checkBoxDarkMode.Text = "Dark Mode";
        checkBoxDarkMode.TextAlign = ContentAlignment.MiddleCenter;
        checkBoxDarkMode.UseVisualStyleBackColor = true;
        checkBoxDarkMode.CheckedChanged += CheckBoxDarkModeCheckedChanged;
        // 
        // labelTimeLeft
        // 
        labelTimeLeft.AutoSize = true;
        labelTimeLeft.Font = new Font("Georgia", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
        labelTimeLeft.Location = new Point(452, 147);
        labelTimeLeft.Name = "labelTimeLeft";
        labelTimeLeft.Size = new Size(80, 17);
        labelTimeLeft.TabIndex = 10;
        labelTimeLeft.Text = "Time left:";
        // 
        // labelTimeLeftValue
        // 
        labelTimeLeftValue.BackColor = Color.FromArgb(50, 135, 160);
        labelTimeLeftValue.Font = new Font("Georgia", 10F, FontStyle.Bold, GraphicsUnit.Point);
        labelTimeLeftValue.ForeColor = Color.White;
        labelTimeLeftValue.Location = new Point(538, 141);
        labelTimeLeftValue.Name = "labelTimeLeftValue";
        labelTimeLeftValue.Size = new Size(50, 28);
        labelTimeLeftValue.TabIndex = 11;
        labelTimeLeftValue.Text = "45";
        labelTimeLeftValue.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // progressBarTime
        // 
        progressBarTime.Location = new Point(49, 189);
        progressBarTime.Maximum = 45;
        progressBarTime.Name = "progressBarTime";
        progressBarTime.Size = new Size(637, 24);
        progressBarTime.TabIndex = 12;
        progressBarTime.Value = 45;
        // 
        // labelScrambledWord
        // 
        labelScrambledWord.Anchor = AnchorStyles.Top;
        labelScrambledWord.Font = new Font("Georgia", 22F, FontStyle.Bold, GraphicsUnit.Point);
        labelScrambledWord.Location = new Point(49, 240);
        labelScrambledWord.Name = "labelScrambledWord";
        labelScrambledWord.Size = new Size(637, 55);
        labelScrambledWord.TabIndex = 13;
        labelScrambledWord.Text = "scrambled";
        labelScrambledWord.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // textBoxInput
        // 
        textBoxInput.Font = new Font("Georgia", 13F, FontStyle.Regular, GraphicsUnit.Point);
        textBoxInput.Location = new Point(102, 326);
        textBoxInput.Name = "textBoxInput";
        textBoxInput.PlaceholderText = "Type your guess here";
        textBoxInput.Size = new Size(200, 27);
        textBoxInput.TabIndex = 3;
        textBoxInput.KeyDown += TextBoxInputKeyDown;
        // 
        // buttonCheck
        // 
        buttonCheck.BackColor = Color.FromArgb(50, 135, 160);
        buttonCheck.FlatStyle = FlatStyle.Flat;
        buttonCheck.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        buttonCheck.ForeColor = Color.White;
        buttonCheck.Location = new Point(317, 323);
        buttonCheck.Name = "buttonCheck";
        buttonCheck.Size = new Size(93, 34);
        buttonCheck.TabIndex = 4;
        buttonCheck.Text = "Check";
        buttonCheck.UseVisualStyleBackColor = false;
        buttonCheck.Click += ButtonCheckClick;
        // 
        // buttonSkip
        // 
        buttonSkip.BackColor = Color.FromArgb(50, 135, 160);
        buttonSkip.FlatStyle = FlatStyle.Flat;
        buttonSkip.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        buttonSkip.ForeColor = Color.White;
        buttonSkip.Location = new Point(421, 323);
        buttonSkip.Name = "buttonSkip";
        buttonSkip.Size = new Size(83, 34);
        buttonSkip.TabIndex = 5;
        buttonSkip.Text = "Skip";
        buttonSkip.UseVisualStyleBackColor = false;
        buttonSkip.Click += ButtonSkipClick;
        // 
        // buttonHint
        // 
        buttonHint.BackColor = Color.FromArgb(70, 110, 170);
        buttonHint.FlatStyle = FlatStyle.Flat;
        buttonHint.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        buttonHint.ForeColor = Color.White;
        buttonHint.Location = new Point(515, 323);
        buttonHint.Name = "buttonHint";
        buttonHint.Size = new Size(83, 34);
        buttonHint.TabIndex = 6;
        buttonHint.Text = "Hint";
        buttonHint.UseVisualStyleBackColor = false;
        buttonHint.Click += ButtonHintClick;
        // 
        // labelHint
        // 
        labelHint.AutoSize = true;
        labelHint.Font = new Font("Georgia", 11F, FontStyle.Bold, GraphicsUnit.Point);
        labelHint.Location = new Point(102, 392);
        labelHint.Name = "labelHint";
        labelHint.Size = new Size(48, 18);
        labelHint.TabIndex = 18;
        labelHint.Text = "Hint:";
        // 
        // labelHintValue
        // 
        labelHintValue.Font = new Font("Georgia", 10F, FontStyle.Italic, GraphicsUnit.Point);
        labelHintValue.Location = new Point(156, 390);
        labelHintValue.Name = "labelHintValue";
        labelHintValue.Size = new Size(500, 24);
        labelHintValue.TabIndex = 19;
        labelHintValue.Text = "No hint used yet";
        labelHintValue.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // labelFailedAttempts
        // 
        labelFailedAttempts.Anchor = AnchorStyles.Top;
        labelFailedAttempts.AutoSize = true;
        labelFailedAttempts.Font = new Font("Georgia", 12F, FontStyle.Bold, GraphicsUnit.Point);
        labelFailedAttempts.Location = new Point(286, 435);
        labelFailedAttempts.Name = "labelFailedAttempts";
        labelFailedAttempts.Size = new Size(149, 18);
        labelFailedAttempts.TabIndex = 20;
        labelFailedAttempts.Text = "Failed attempts:";
        // 
        // textBoxFailedAttempts
        // 
        textBoxFailedAttempts.BackColor = Color.FromArgb(220, 210, 210);
        textBoxFailedAttempts.Font = new Font("Georgia", 11F, FontStyle.Regular, GraphicsUnit.Point);
        textBoxFailedAttempts.Location = new Point(102, 465);
        textBoxFailedAttempts.Multiline = true;
        textBoxFailedAttempts.Name = "textBoxFailedAttempts";
        textBoxFailedAttempts.ReadOnly = true;
        textBoxFailedAttempts.ScrollBars = ScrollBars.Vertical;
        textBoxFailedAttempts.Size = new Size(496, 95);
        textBoxFailedAttempts.TabIndex = 7;
        // 
        // roundTimer
        // 
        roundTimer.Interval = 1000;
        roundTimer.Tick += RoundTimerTick;
        // 
        // IndexForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(246, 247, 255);
        ClientSize = new Size(734, 591);
        Controls.Add(textBoxFailedAttempts);
        Controls.Add(labelFailedAttempts);
        Controls.Add(labelHintValue);
        Controls.Add(labelHint);
        Controls.Add(buttonHint);
        Controls.Add(buttonSkip);
        Controls.Add(buttonCheck);
        Controls.Add(textBoxInput);
        Controls.Add(labelScrambledWord);
        Controls.Add(progressBarTime);
        Controls.Add(labelTimeLeftValue);
        Controls.Add(labelTimeLeft);
        Controls.Add(checkBoxDarkMode);
        Controls.Add(comboBoxCategory);
        Controls.Add(labelCategory);
        Controls.Add(labelScoreValue);
        Controls.Add(labelScore);
        Controls.Add(labelGuessedWordsValue);
        Controls.Add(labelGuessedWords);
        Controls.Add(labelAttemptsValue);
        Controls.Add(labelAttempts);
        Controls.Add(labelTitle);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "IndexForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Word Scramble";
        Load += IndexFormLoad;
        ResumeLayout(false);
        PerformLayout();
    }
}
