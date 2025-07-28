// using TG.Attractions;
// using Xunit;
// using Moq;
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Threading.Tasks;
// using TG.DataBase;
// using TG.Commands.MenuCommands.PlanRoute;

// namespace TG.Tests.Commands.MenuCommands
// {
//     public class AddSightTests
//     {
//         [Fact]
//         public async Task AddSightMAsync_ShouldReturnRoute_WhenValidSelectionsAreMade()
//         {
//             // Arrange
//             var mockDbHelper = new Mock<IData>();
//             mockDbHelper
//                 .Setup(db => db.GetAttractionsByRegionAsync(It.IsAny<string>()))
//                 .ReturnsAsync(new List<Attraction>
//                 {
//                     new Attraction(1, "Louvre", "Paris"),
//                     new Attraction(2, "Eiffel Tower", "Paris")
//                 });

//             var addSight = new AddSight();

//             // Симуляция ввода пользователя
//             var simulatedInput = new StringReader("1\n1\n1\n2\n2\n");
//             Console.SetIn(simulatedInput);

//             // Перехват вывода
//             var output = new StringWriter();
//             Console.SetOut(output);

//             // Act
//             var attractions = await mockDbHelper.Object.GetAttractionsByRegionAsync("Paris");
//             var result = await addSight.AddSightMAsync(mockDbHelper.Object, attractions);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(2, result.Count); // Должно быть 2 достопримечательности в маршруте
//             Assert.Contains(result, r => r.Name == "Louvre");
//             Assert.Contains(result, r => r.Name == "Eiffel Tower");

//             // Проверка консольного вывода
//             var consoleOutput = output.ToString();
//             Assert.Contains("Louvre добавлена в маршрут.", consoleOutput);
//             Assert.Contains("Eiffel Tower добавлена в маршрут.", consoleOutput);
//             Assert.Contains("Готово", consoleOutput);
//         }
//     }
// }


using TG.Attractions;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TG.DataBase;
using TG.Commands.MenuCommands.PlanRoute;

namespace TG.Tests.Commands.MenuCommands
{
    public class AddSightTests
    {
        [Fact]
        public async Task AddSightM_ShouldReturnRoute_WhenValidSelectionsAreMade()
        {
            // Arrange
            var mockDb = new Mock<IData>();
            mockDb.Setup(db => db.GetAttractionsByRegionAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Attraction>
                {
                    new Attraction(1, "Louvre", "Paris"),
                    new Attraction(2, "Eiffel Tower", "Paris")
                });

            var addSight = new AddSight();

            // Симуляция пользовательского ввода
            Queue<string> simulatedInputs = new Queue<string>(new[] { "1", "1", "1", "2", "2" });

            // Act
            var attractions = await mockDb.Object.GetAttractionsByRegionAsync("Paris");
            var result = await addSight.AddSightMAsync(attractions, simulatedInputs);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Name == "Louvre");
            Assert.Contains(result, r => r.Name == "Eiffel Tower");

        }


    }
}