using AssessmentAPITesting.Models;
using AssessmentAPITesting.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace AssessmentAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        public IColumnInterface _columnInterface { get; }
        public ITableInterface _tableInterface { get; }
        public ColumnController(IColumnInterface columnInterface, ITableInterface tableInterface = null)
        {
            _columnInterface = columnInterface;
            _tableInterface = tableInterface;
        }
        //Get all records with a specific DataType.DataType needs to be a parameter.
        [HttpGet("type/{dataType}")]
        public async Task<IActionResult> GetAllColumnByType(string dataType)
        {
            try
            {
                var columns = await _columnInterface.GetAllColumnByType(dataType);
                if(columns == null)
                {
                    return NotFound($"No records with datatype {dataType}");
                }
                return Ok(columns);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }/*
        //Get all records from AOColumn based on Table id.
        [HttpGet("tableid/{tableId}")]
        public async Task<IActionResult> GetAllColumnByTableId(Guid tableId)
        {
            try
            {
                var columns = await _columnInterface.GetAllColumnByTableId(tableId);
                if(columns == null)
                {
                    return NotFound("No Column found with the given tableId");
                }
                return Ok(columns);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
        //Get all records from AOColumn based on Table Name.
        //Table Name should be a parameter and the same be displayed once in the result. 
        [HttpGet("tablename/{tableName}")]
        public async Task<IActionResult> GetAllColumnByTableName(string tableName)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest("Table name cannot be null or empty");
                }
                var columns = await _columnInterface.GetAllColumnByTableName(tableName);
                if (columns == null)
                {
                    return NotFound($"No Column with table name {tableName}");
                }
                var result = new
                {
                    TableName = tableName,
                    columns
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Add a column for the AOTable record 
        [HttpPost("AddColumn/{tableId}")]
        public async Task<IActionResult> AddColumn(Guid tableId, Aocolumn column)
        {
            try
            {
                if(column == null)
                {
                    return BadRequest("Column is empty in the request");
                }
                column.Id = Guid.NewGuid();
                var result = await _columnInterface.AddColumn(tableId, column);
                if(result == null)
                {
                    return BadRequest("Cannot add column");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        // 	Delete a record
        [HttpDelete("DeleteColumn/{columnId}")]
        public async Task<IActionResult> DeleteColumn(Guid columnId)
        {
            try
            {
                var deletedResult = await _columnInterface.DeleteColumn(columnId);
                if(deletedResult == null)
                {
                    return BadRequest("Unable to delete the column");
                }

                return Ok(deletedResult);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
