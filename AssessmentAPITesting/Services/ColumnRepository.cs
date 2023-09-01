using AssessmentAPITesting.Data;
using AssessmentAPITesting.Models;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPITesting.Services
{
    public class ColumnRepository : IColumnInterface
    {
        private readonly TableDbContext _dbContext;

        public ColumnRepository(TableDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Aocolumn>> GetAllColumnByType(string dataType)
        {
            var columns = await _dbContext.Aocolumns.Where(c=>c.DataType ==dataType).ToListAsync();
            if(columns.Any())
            {
                return columns;
            }
            return null;

        }
        public async Task<IEnumerable<Aocolumn>> GetAllColumnByTableId(Guid tableId)
        {
            var column =  await _dbContext.Aocolumns.Where(c => c.TableId== tableId).ToListAsync();
            if (column.Any())
            {
                return column;
            }
            return null;


        }
        public async Task<IEnumerable<Aocolumn>> GetAllColumnByTableName(string tableName)
        {
            var table = await _dbContext.Aotables.FirstOrDefaultAsync(t=>t.Name==tableName);

            if (table == null)
            {
                return null;
            }
            var tableId = table.Id;
            var columns = await _dbContext.Aocolumns.Where(c=>c.TableId==tableId).ToListAsync();
            if (columns.Any())
            {
                return columns;
            }
            return null;
        }
        public async Task<Aocolumn> AddColumn(Guid tableId, Aocolumn column)
        {
            if (column == null)
            {
                return null;
            }
            var table = await _dbContext.Aotables.FindAsync(tableId);
            if (table == null)
            {
                return null;
            }
           
            column.TableId = tableId;
            var result = await _dbContext.AddAsync(column);
            
            if(result == null) {
                return null;
            }
            await _dbContext.SaveChangesAsync();
            return column;
        }
        public async Task<Aocolumn> DeleteColumn(Guid columnId)
        {
            var column = await _dbContext.Aocolumns.FindAsync(columnId);
            if (column == null)
            {
                return null;
            }
            _dbContext.Aocolumns.Remove(column);
            await _dbContext.SaveChangesAsync();
            return column;
        }
    }
}
