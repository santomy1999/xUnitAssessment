using AssessmentAPITesting.Models;

namespace AssessmentAPITesting.Services
{
    public interface IColumnInterface
    {
        Task<Aocolumn> AddColumn(Guid tableId, Aocolumn column);
        Task<Aocolumn> DeleteColumn(Guid columnId);
        Task<IEnumerable<Aocolumn>> GetAllColumnByTableId(Guid tableId);
        Task<IEnumerable<Aocolumn>> GetAllColumnByTableName(string tableName);
        Task<IEnumerable<Aocolumn>> GetAllColumnByType(string dataType);
    }
}
