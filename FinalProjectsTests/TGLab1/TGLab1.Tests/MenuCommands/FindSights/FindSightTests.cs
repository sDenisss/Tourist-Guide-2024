using TG.Attractions;
using TG.Commands;
using TG.Commands.MenuCommands;
using Xunit;

namespace TG.Tests.Commands.MenuCommands
{
    public class FindSightTests
    {
        // [Fact]
        public void FindSightM_ShouldReturnAttractions_WhenRegionIsValid()
        {
            // Arrange
            var db = new AttractionList();
            db.Add(new Attraction(0, "Louvre", "Paris"));
            db.Add(new Attraction(1, "Eiffel Tower", "Paris"));
            db.Add(new Attraction(2, "Coliseum", "Rome"));
            var findSight = new FindSight();

            // Act
            // FindSight.region="Paris";
            var result = findSight.FindSightM(db, "Paris");

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count); // Убедитесь, что нашлось 2 достопримечательности
            Assert.Contains(result, a => a.Name == "Louvre");
            Assert.Contains(result, a => a.Name == "Eiffel Tower");
        }
    }
}
