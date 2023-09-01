using AssessmentAPITesting.Controllers;
using AssessmentAPITesting.Models;
using AssessmentAPITesting.Services;
using FluentAssertions;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AssessmentAPITesting.Test.Controllers
{
    public class ColumnControllerTests
    {
        private readonly IFixture _fixture;
        private ColumnController _columnController;
        private TableController _tableController;
        private Mock<IColumnInterface> _columnInterface;
        private Mock<ITableInterface> _tableInterface;

        public ColumnControllerTests()
        {
            _fixture = new Fixture();
            _columnInterface = _fixture.Freeze<Mock<IColumnInterface>>();
            _columnController = new ColumnController(_columnInterface.Object);
            _tableInterface = _fixture.Freeze<Mock<ITableInterface>>();
            _tableController = new TableController(_tableInterface.Object);
        }

        //Test for the GetColumnByType function
        [Fact]
        public void GetAllColumnByType_ShouldReturn_Ok_WhenSuccess()
        {
            //Arrange
            var columns = _fixture.Create<IEnumerable<Aocolumn>>();
            var dataType = _fixture.Create<string>();
            _columnInterface.Setup(c => c.GetAllColumnByType(dataType)).ReturnsAsync(columns);

            //Act
            var result = _columnController.GetAllColumnByType(dataType);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            _columnInterface.Verify(c => c.GetAllColumnByType(dataType), Times.Once());
        }
        [Fact]
        public void GetAllColumnByType_ShouldReturn_NotFound_WhenDataNotFound()
        {
            //Arrange
            var dataType = _fixture.Create<string>();
            _columnInterface.Setup(c => c.GetAllColumnByType(dataType)).Returns(Task.FromResult<IEnumerable<Aocolumn>>(null));

            //Act
            var result = _columnController.GetAllColumnByType(dataType);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            _columnInterface.Verify(c => c.GetAllColumnByType(dataType), Times.Once());
        }
        [Fact]
        public void GetAllColumnByType_ShouldReturn_BadRequest_WhenExceptionOccured()
        {
            //Arrange
            var dataType = _fixture.Create<string>();
            _columnInterface.Setup(c => c.GetAllColumnByType(dataType)).Throws(new Exception());


            //Act
            var result = _columnController.GetAllColumnByType(dataType);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.GetAllColumnByType(dataType), Times.Once());
        }
        //Tests for GetColumnByTableName function
        [Fact]
        public async void GetAllColumnByTableName_ShouldReturn_OkResult_WhenSuccess()
        {
            //Arrange
            var tableName = _fixture.Create<string>();
            var columns = _fixture.Create<IEnumerable<Aocolumn>>();
            _columnInterface.Setup(c => c.GetAllColumnByTableName(tableName)).ReturnsAsync(columns);


            //Act
            var result = _columnController.GetAllColumnByTableName(tableName);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            _columnInterface.Verify(c => c.GetAllColumnByTableName(tableName), Times.Once());

        }

        [Fact]
        public async Task GetAllColumnByTableName_ShouldReturn_NotFound_WhenTableOrColumnDoesNotExist()
        {
            //Arrange
            var tableName = _fixture.Create<string>();
            _columnInterface.Setup(c => c.GetAllColumnByTableName(tableName)).Returns(Task.FromResult<IEnumerable<Aocolumn>>(null));

            //Act
            var result = _columnController.GetAllColumnByTableName(tableName);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            _columnInterface.Verify(c => c.GetAllColumnByTableName(tableName), Times.Once());
        }

        [Fact]
        public async Task GetAllColumnByTableName_ShouldReturn_BadRequest_WhenErrorOCcured()
        {
            //Arrange
            var tableName = _fixture.Create<string>();
            var table = _fixture.Create<IEnumerable<Aotable>>();
            _columnInterface.Setup(c => c.GetAllColumnByTableName(tableName)).Throws(new Exception());


            //Act
            var result = _columnController.GetAllColumnByTableName(tableName);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.GetAllColumnByTableName(tableName), Times.Once());
        }
        [Fact]
        public async Task GetAllColumnByTableName_ShouldReturn_BadRequest_WhenTableNameIsNullOrEmpty()
        {
            //Arrange
            string tableName = null;
            var table = _fixture.Create<IEnumerable<Aotable>>();
            _columnInterface.Setup(c => c.GetAllColumnByTableName(tableName));


            //Act
            var result =  _columnController.GetAllColumnByTableName(tableName);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.GetAllColumnByTableName(tableName), Times.Never());
        }
        //Tests for AddColumn 
        [Fact]
        public void AddColumn_ShouldReturn_Ok_WhenSuccess()
        {
            //Arrange
            var column = _fixture.Create<Aocolumn>();
            Guid tableId = Guid.NewGuid();
            var columnReturn = _fixture.Create<Aocolumn>();
            _columnInterface.Setup(c => c.AddColumn(tableId,column)).ReturnsAsync(columnReturn);

            //Act
            var result = _columnController.AddColumn(tableId,column);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            _columnInterface.Verify(c => c.AddColumn(tableId, column), Times.Once());
        }
        
        [Fact]
        public void AddColumn_ShouldReturn_BadRequest_WhenColumnIsNull()
        {
            //Arrange
            Aocolumn column = null;
            Guid tableId = new Guid();
            _columnInterface.Setup(c => c.AddColumn(tableId,column)).ReturnsAsync((Aocolumn)null);

            //Act
            var result = _columnController.AddColumn(tableId,column);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.AddColumn(tableId,column), Times.Never());

        }
        [Fact]
        public void AddColumn_ShouldReturn_BadRequest_WhenAddFails()
        {
            //Arrange
            var tableId = new Guid();
            var column = _fixture.Create<Aocolumn>();
            _columnInterface.Setup(c => c.AddColumn(tableId,column)).ReturnsAsync((Aocolumn)null);

            //Act
            var result = _columnController.AddColumn(tableId,column);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.AddColumn(tableId,column), Times.Once());

        }
        [Fact]
        public void AddColumn_ShouldReturn_BadRequest_WhenExceptionOccures()
        {
            //Arrange
            var column = _fixture.Create<Aocolumn>();
            Guid tableId = _fixture.Create<Guid>();
            var tableReturn = _fixture.Create<Aotable>();
            _columnInterface.Setup(c => c.AddColumn(tableId,column)).Throws(new Exception());

            //Act
            var result = _columnController.AddColumn(tableId,column);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.AddColumn(tableId,column), Times.Once());
        }
        //Test for deleting column records
        [Fact]
        public void DeleteColumn_ShouldReturn_OkRespose_WhenSuccess()
        {
            //Arrange
            var column = _fixture.Create<Aocolumn>();
            Guid columnId = _fixture.Create<Guid>();
            _columnInterface.Setup(c=>c.DeleteColumn(columnId)).ReturnsAsync(column);

            //Act
            var result = _columnController.DeleteColumn(columnId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            _columnInterface.Verify(c=>c.DeleteColumn(columnId), Times.Once());
        }
        [Fact]
        public void DeleteColumn_ShouldReturn_BadRequest_WhenDeleteFails()
        {
            //Arrange
            var columnId = new Guid();
            var column = _fixture.Create<Aocolumn>();
            _columnInterface.Setup(c => c.DeleteColumn(columnId)).ReturnsAsync((Aocolumn)null);

            //Act
            var result = _columnController.DeleteColumn(columnId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.DeleteColumn( columnId), Times.Once());

        }
        [Fact]
        public void DeleteColumn_ShouldReturn_BadRequest_WhenExceptionOccures()
        {
            //Arrange
            var column = _fixture.Create<Aocolumn>();
            Guid columnId = _fixture.Create<Guid>();
            _columnInterface.Setup(c => c.DeleteColumn(columnId)).Throws(new Exception());

            //Act
            var result = _columnController.DeleteColumn(columnId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _columnInterface.Verify(c => c.DeleteColumn(columnId), Times.Once());
        }
    }
}
