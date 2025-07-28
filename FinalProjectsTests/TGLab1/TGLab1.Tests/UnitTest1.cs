using TG.Tests.Commands.MenuCommands;

namespace TGLab1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            new FindSightTests().FindSightM_ShouldReturnAttractions_WhenRegionIsValid();
        }
    }
}