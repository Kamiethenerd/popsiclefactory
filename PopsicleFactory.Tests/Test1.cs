using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API;

[TestClass]
public class PopsicleControllerTests
{
    
    [TestMethod]
    public void CreatePopsicle_ReturnsConflict_WhenDuplicate()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        var popsicle = new PopsicleModel.Popsicle { Name = "Test", Flavor = "Grape" };

        // Act
        var result1 = controller.CreatePopsicle(popsicle);
        var result2 = controller.CreatePopsicle(popsicle);

        // Assert
        Assert.IsInstanceOfType(result2, typeof(ConflictObjectResult));
    }
}
