using AssessmentAPITesting.Data;
using AssessmentAPITesting.Models;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPITesting.Services
{
    public class TableRepository:ITableInterface
    {
        private readonly TableDbContext _dbContext;
        public TableRepository(TableDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Aotable> GetTableByTableName(string tableName)
        {
            var table = await _dbContext.Aotables.FirstOrDefaultAsync(t=>t.Name == tableName);
            if (table == null)
            {
                return null;
            }
            return table; 
        }
        public async Task<Aotable> AddTable(Aotable table)
        {
            var result = await _dbContext.Aotables.AddAsync(table);
            if(result == null) {
                return null;
            }
            await _dbContext.SaveChangesAsync();
            return table;
        }
        public async Task<Aotable> EditTable(Guid id, Aotable newtable)
        {
            var existingTable = await _dbContext.Aotables.FindAsync(id);
            if(existingTable==null)
            {
                return null;
            }
            existingTable.Id = newtable.Id;
            existingTable.Name = newtable.Name;
            existingTable.Type = newtable.Type;
            existingTable.Description = newtable.Description;
            existingTable.Comment = newtable.Comment;
            existingTable.History = newtable.History;
            existingTable.Boundary = newtable.Boundary;
            existingTable.Log = newtable.Log;
            existingTable.Cache = newtable.Cache;
            existingTable.Notify = newtable.Notify;
            existingTable.Identifier = newtable.Identifier;
            await _dbContext.SaveChangesAsync();
            return existingTable;
        
        }
    }
}
