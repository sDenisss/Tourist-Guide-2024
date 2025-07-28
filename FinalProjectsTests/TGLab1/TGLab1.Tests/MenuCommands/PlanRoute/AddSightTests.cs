using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Attractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace TG.Commands.MenuCommands
{
    public class AddSightTests
    {
        [Fact]
        public void AddSightM_ShouldReturnRoute()
        {
            // Arrange
            var db = new AttractionList();
            db.Add(new Attraction(0, "Louvre", "Paris"));
            db.Add(new Attraction(1, "Eiffel Tower", "Paris"));
            db.Add(new Attraction(2, "Coliseum", "Rome"));
            List<Attraction> attractions = db.GetAttractionsByRegion("Paris");

            var addSight = new AddSight();

            // Simulated inputs
            Queue<string> simulatedInputs = new Queue<string>(new[] { "1", "1", "1", "2", "2" });

            // Act
            var result = addSight.AddSightM(attractions, simulatedInputs);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, a => a.Name == "Louvre");
            Assert.Contains(result, a => a.Name == "Eiffel Tower");
        }
    }
}


