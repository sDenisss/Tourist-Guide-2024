using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Commands.MenuCommands;
using TG.Attractions;

namespace TG.Commands.MenuCommands.Exit
{
    public class Exit
    {
        public SaveLastSession saveLastSession;
        public ShowExitMessage showExitMessage;

        public Exit()
        {
            // Создаем экземпляр SaveLastSession
            saveLastSession = new SaveLastSession();
            showExitMessage = new ShowExitMessage();
        }

        public async Task ExitProgram(List<RouteAttractions> route)
        {
            if (route.Count > 0)
            {
                var saveSession = new SaveLastSession();
                await saveSession.SaveLastSessionMAsync(route);
                showExitMessage.ShowExitMessageM();
                Console.WriteLine("Данные сессии сохранены.");
            }
            else
            {
                Console.WriteLine("Сессия пустая, ничего не сохраняется.");
            }
        }
    }
}