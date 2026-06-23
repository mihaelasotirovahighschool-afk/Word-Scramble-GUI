using System;
using System.Windows.Forms;

namespace WordScramble;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        // Тази настройка подготвя Windows Forms приложението.
        // Тя включва правилните визуални стилове и DPI настройки за формите.
        ApplicationConfiguration.Initialize();

        // Тук стартираме главната форма на играта.
        Application.Run(new IndexForm());
    }
}
