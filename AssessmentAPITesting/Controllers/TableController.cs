using AssessmentAPITesting.Models;
using AssessmentAPITesting.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableInterface _tableInterface;
        public TableController(ITableInterface tableInterface)
        {
            this._tableInterface = tableInterface;
        }
        [HttpGet("{tableName}")]
        public async Task<IActionResult> GetTableByTableName(string tableName)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest("TableName cannot be null or empty");
                }
                var table = await _tableInterface.GetTableByTableName(tableName);
                if (table == null)
                {
                    return NotFound($"No table with table name {tableName}");
                }
                return Ok(table);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
        }
        //Add record
        [HttpPost("AddTable")]
        public async Task<IActionResult> AddTable([FromBody] Aotable table)
        {
            try
            {
                if (table == null)
                {
                    return BadRequest("Body doesn't contain any values");
                }
                table.Id= Guid.NewGuid();
                var result = await _tableInterface.AddTable(table);
                if (result == null)
                {
                    return BadRequest("Error Occured while adding the table");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        //Edit record
        [HttpPatch("EditTable/{id}")]
        public async Task<IActionResult> EditTable(Guid id,[FromBody] Aotable newTable)
        {
            try
            {
                if(newTable==null)
                {
                    return BadRequest("Null values present in the request");
                }
                var result = await _tableInterface.EditTable(id, newTable);
                if(result == null)
                {
                    return NotFound($"No table found with table id {id}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }

}
