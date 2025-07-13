using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using API;

[TestClass]
public class PopsicleControllerTests
{
    [TestMethod]
    public void CreatePopsicle_ReturnsConflict_WhenDuplicate()
    {
        // Arrange
        var controller = new PopsicleController();
        var popsicle = new Popsicle { Name = "Test", Flavor = "Grape" };

        // Act
        var result1 = controller.CreatePopsicle(popsicle);
        var result2 = controller.CreatePopsicle(popsicle);

        // Assert
        Assert.IsInstanceOfType(result2, typeof(ConflictObjectResult));
    }
}
