// using TG.Attractions;

// namespace TG.Commands.MenuCommands.Exit
// {
//     public class SaveLastSessionTests
//     {
//         [Fact]
//         public async Task ShouldSaveLastSession()
//         {
//             // Arrange
//             var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
//             Directory.CreateDirectory(tempDir);
//             var tempFilePath = Path.Combine(tempDir, "history.json");

//             var saveLastSession = new SaveLastSessionMock(tempFilePath); // Используем Mock для тестов
//             var route = new List<RouteAttractions>
//             {
//                 new RouteAttractions { Id = 1, Name = "Eiffel Tower" },
//                 new RouteAttractions { Id = 2, Name = "Louvre" }
//             };

//             // Act
//             await saveLastSession.SaveLastSessionMAsync(route);
//             var allHistory = await saveLastSession.LoadAllHistoryAsync();

//             // Assert
//             Assert.NotEmpty(allHistory);
//             Assert.Single(allHistory);
//             Assert.Equal(2, allHistory[0].Count);
//             Assert.Contains(allHistory[0], r => r.Name == "Eiffel Tower");
//             Assert.Contains(allHistory[0], r => r.Name == "Louvre");

//             // Cleanup
//             Directory.Delete(tempDir, true);
//         }

//         private class SaveLastSessionMock : SaveLastSession
//         {
//             public SaveLastSessionMock(string customPath)
//             {
//                 typeof(SaveLastSession)
//                     .GetField("_historyFilePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//                     ?.SetValue(this, customPath);
//             }
//         }
//     }
// }
