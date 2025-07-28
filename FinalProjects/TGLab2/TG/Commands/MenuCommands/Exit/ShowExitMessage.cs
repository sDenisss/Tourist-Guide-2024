using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TG.Commands.MenuCommands.Exit
{
    public class ShowExitMessage
    {
        public async void ShowExitMessageM()
        {
            // Выводим текст при завершении программы
            System.Console.WriteLine("Спасибо за использование нашего приложения!");
        }
    }
}