using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API;

[TestClass]
public class PopsicleControllerTests
{
    //create method tests
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
    //test create popsicle method for invalid popsicle
    [TestMethod]
    public void CreatePopsicle_ReturnsBadRequest_WhenInvalid()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        var popsicle = new PopsicleModel.Popsicle { Name = "", Flavor = "" }; // Invalid popsicle with empty name and flavor  
        controller.ModelState.AddModelError("Name", "Name is required");
        controller.ModelState.AddModelError("Flavor", "Flavor is required");
        // Act
        var result = controller.CreatePopsicle(popsicle);
        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
    //test create popsicle method for valid popsicle
    [TestMethod]
    public void CreatePopsicle_ReturnsCreated_WhenValid()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        var popsicle = new PopsicleModel.Popsicle { Name = "Test", Flavor = "Grape" };
        // Act
        var result = controller.CreatePopsicle(popsicle);
        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    //replace popsicle method tests
    [TestMethod]
    public void ReplacePopsicle_ReturnsNotFound_WhenPopsicleDoesNotExist()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        var popsicle = new PopsicleModel.Popsicle { Name = "Test", Flavor = "Grape" };  // Popsicle does not exist in the repository        
        // Act
        var result = controller.ReplacePopsicle(0, popsicle);
        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
    [TestMethod]
    public void ReplacePopsicle_ReturnsBadRequest_WhenInvalid()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        var popsicle = new PopsicleModel.Popsicle
        {
            Name = "",
            Flavor = ""
        }; // Invalid popsicle with empty name and flavor
        controller.ModelState.AddModelError("Name", "Name is required");
        controller.ModelState.AddModelError("Flavor", "Flavor is required");
        // Act
        var result = controller.ReplacePopsicle(1, popsicle);
        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
    [TestMethod]
    public void ReplacePopsicle_ReturnsOk_WhenValid()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        var popsicle = new PopsicleModel.Popsicle
        {
            Name = "Test",
            Flavor = "Grape"
        }; // Valid popsicle

        // Act
        var result = controller.ReplacePopsicle(1, popsicle);
        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    //test getpopsicle method
    [TestMethod]
    public void GetPopsicle_ReturnsNotFound_WhenPopsicleDoesNotExist()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        // Act
        var result = controller.GetPopsicleById(0);
        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
    [TestMethod]
    public void GetPopsicle_ReturnsOk_WhenPopsicleExists()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        // Act
        var result = controller.GetPopsicleById(1);
        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
    //test get all popsicles method
    [TestMethod]
    public void GetAllPopsicles_ReturnsOk_WhenPopsiclesExist()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        // Act
        var result = controller.GetAllPopsicles();
        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
    //test search popsicles method
    [TestMethod]
    public void SearchPopsicles_ReturnsNotFound_WhenNoResults()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        // Act
        var result = controller.SearchPopsicles("NonExistentFlavor");
        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
    [TestMethod]
    public void SearchPopsicles_ReturnsOk_WhenResultsFound()
    {
        // Arrange
        var repo = new InMemoryPopsicleRepository();
        var controller = new PopsicleController(repo);
        // Act
        var result = controller.SearchPopsicles("Cherry");
        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
    
}
