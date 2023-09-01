using AssessmentAPITesting.Models;

namespace AssessmentAPITesting.Services
{
    public interface ITableInterface
    {
        Task<Aotable> AddTable(Aotable table);
        Task<Aotable> EditTable(Guid id, Aotable table);
        Task<Aotable> GetTableByTableName(string tableName);
    }
}
