using AssessmentAPITesting.Controllers;
using AssessmentAPITesting.Models;
using AssessmentAPITesting.Services;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AssessmentAPITesting.Test.Controllers
{
    public class TableControllerTests
    {
        private readonly IFixture _fixture;
        private TableController _tableController;
        private Mock<ITableInterface> _tableInterface;

        public TableControllerTests()
        {
            _fixture = new Fixture();
            _tableInterface = _fixture.Freeze<Mock<ITableInterface>>();
            _tableController = new TableController(_tableInterface.Object);
        }


//AddTable Test
        [Fact]
        public void AddTable_ShouldReturn_Ok_WhenSuccess()
        {
            //Arrange
            var table = _fixture.Create<Aotable>();
            var tableReturn = _fixture.Create<Aotable>();
            _tableInterface.Setup(c=>c.AddTable(table)).ReturnsAsync(tableReturn);

            //Act
            var result =  _tableController.AddTable(table);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>().Subject.Value.Should().Be(tableReturn);
            _tableInterface.Verify(t => t.AddTable(table), Times.Once());
        }
        
        [Fact]
        public void AddTable_ShouldReturn_BadRequest_WhenTableIsNull()
        {
            //Arrange
            Aotable table = null;
            _tableInterface.Setup(c => c.AddTable(table)).ReturnsAsync((Aotable)null);

            //Act
            var result = _tableController.AddTable(table);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>().Which.Value.Should().Be("Body doesn't contain any values");
            _tableInterface.Verify(t => t.AddTable(table), Times.Never());

        }
        [Fact]
        public void AddTable_ShouldReturn_BadRequest_WhenAddFails()
        {
            //Arrange
            var table = _fixture.Create<Aotable>();
            _tableInterface.Setup(c => c.AddTable(table)).ReturnsAsync((Aotable)null);

            //Act
            var result = _tableController.AddTable(table);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>().Subject.Value.Should().Be("Error Occured while adding the table");
            _tableInterface.Verify(t => t.AddTable(table), Times.Once());

        }
        [Fact]
        public void AddTable_ShouldReturn_BadRequest_WhenExceptionOccures()
        {
            //Arrange
            var table = _fixture.Create<Aotable>();
            var tableReturn = _fixture.Create<Aotable>();
            _tableInterface.Setup(c => c.AddTable(table)).Throws(new Exception("Exception Occured"));

            //Act
            var result = _tableController.AddTable(table);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>().Subject.Value.Should().Be("Exception Occured");
            _tableInterface.Verify(t => t.AddTable(table), Times.Once());
        }
        //Edittable
        [Fact]
        public void EditTable_ReturnsOk_WhenSuccess()
        {
            //Arrange
            Guid id =_fixture.Create<Guid>();
            var newTable = _fixture.Create<Aotable>();
            var existingTable = _fixture.Create<Aotable>();
            _tableInterface.Setup(c => c.EditTable(id,newTable)).ReturnsAsync(existingTable);

            //Act
            var result = _tableController.EditTable(id,newTable);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>().Subject.Value.Should().Be(existingTable);
            _tableInterface.Verify(t => t.EditTable(id,newTable), Times.Once());
        }
        [Fact]
        public void EditTable_ReturnsBadReuqest_WhenExceptionOccures()
        {
            //Arrange
            Guid id = _fixture.Create<Guid>();
            var newTable = _fixture.Create<Aotable>();
            var existingTable = _fixture.Create<Aotable>();
            _tableInterface.Setup(c => c.EditTable(id, newTable)).ThrowsAsync(new Exception("An exception occured"));

            //Act
            var result = _tableController.EditTable(id, newTable);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>().Which.Value.Should().Be("An exception occured");
            _tableInterface.Verify(t => t.EditTable(id, newTable), Times.Once());
        }
        [Fact]
        public void EditTable_ReturnsBadReuqest_WhenObjectIsNull()
        {
            //Arrange
            var id = _fixture.Create<Guid>();
            Aotable newTable = null;
            var existingTable = _fixture.Create<Aotable>();
            _tableInterface.Setup(c => c.EditTable(id, newTable)).ReturnsAsync((Aotable)null);

            //Act
            var result = _tableController.EditTable(id, newTable);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>().Which.Value.Should().Be("Null values present in the request");

            _tableInterface.Verify(t => t.EditTable(id, newTable), Times.Never());
        }
        [Fact]
        public void EditTable_ReturnsNotFound_WhenTableNotFound()
        {
            //Arrange
            Guid id = _fixture.Create<Guid>();
            var newTable = _fixture.Create<Aotable>();
            var existingTable = _fixture.Create<Aotable>();
            _tableInterface.Setup(c => c.EditTable(id, newTable)).ReturnsAsync((Aotable)null);

            //Act
            var result = _tableController.EditTable(id, newTable);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>().Which.Value.Should().Be($"No table found with table id {id}");
            _tableInterface.Verify(t => t.EditTable(id, newTable), Times.Once());
        }
    }
}