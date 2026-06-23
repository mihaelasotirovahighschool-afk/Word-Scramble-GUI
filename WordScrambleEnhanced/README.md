# Word Scramble Enhanced

A C# Windows Forms game where the player has to unscramble a randomly selected word before the timer runs out.

## Main features

- Random words loaded from `words.txt`
- Word scrambling with the Fisher-Yates shuffle algorithm
- Check, Skip, and Hint buttons
- Attempts counter
- Guessed words counter
- Score system
- Failed attempts list
- Light and dark mode switch
- Category selector: All words, Animals, Objects, Nature, People, Actions, Food
- Countdown timer with a progress bar that slowly runs out
- Automatic new word after too many wrong attempts or after the timer reaches 0
- Detailed Bulgarian code comments for easier presentation

## How to run

1. Open `WordScramble.sln` in Visual Studio.
2. Make sure the project is using `.NET 8`.
3. Press **Start**.
4. If Visual Studio shows build errors, install the `.NET desktop development` workload.

## How the game works

The program reads words from `words.txt`, stores them in lists, and then chooses a random word from the selected category. The real word is saved in `currentWord`, but the player only sees a scrambled version. The player types a guess and presses **Check** or Enter. If the guess is correct, the player earns points and the game moves to a new word. If the guess is wrong, the attempt is saved in the failed attempts box.

The timer uses a Windows Forms `Timer`. Every second, `secondsLeft` decreases. The progress bar also decreases, so the player can visually see how much time is left.

## Presentation note in Bulgarian

Моят проект е игра Word Scramble. Целта е играчът да познае разбъркана дума. Думите се зареждат от текстов файл, след което програмата избира случайна дума според избраната категория. Използвам списъци, броячи, таймер, събития на бутони и алгоритъм за разбъркване на буквите.

## Technologies

- C#
- Windows Forms
- .NET 8
- Visual Studio
