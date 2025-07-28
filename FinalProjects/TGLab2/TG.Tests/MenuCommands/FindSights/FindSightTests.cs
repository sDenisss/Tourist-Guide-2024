using TG.Attractions;
using TG.Commands.MenuCommands.FindSight;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TG.DataBase;

namespace TG.Tests.Commands.MenuCommands
{
    public class FindSightTests
    {
        [Fact]
        public async Task FindSightM_ShouldReturnEmptyList_WhenRegionIsInvalid()
        {
            // Arrange
            var mockDb = new Mock<IData>();
            mockDb.Setup(db => db.GetAttractionsByRegionAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Attraction>()); // Пустой список

            var findSight = new FindSight();

            var consoleInput = new StringReader("InvalidRegion\n");
            Console.SetIn(consoleInput);

            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var result = await findSight.FindSightMAsync(mockDb.Object);

            // Assert
            Assert.Empty(result); // Убедимся, что результат пуст
        }


    }
}
