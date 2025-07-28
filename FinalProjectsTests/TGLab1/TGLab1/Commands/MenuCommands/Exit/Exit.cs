using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Commands.MenuCommands;
using TG.Attractions;

namespace TG.Commands.MenuCommands
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
            // Вызываем метод сохранения последней сессии
            await saveLastSession.SaveLastSessionMAsync(route);
            showExitMessage.ShowExitMessageM();
        }
    }
}