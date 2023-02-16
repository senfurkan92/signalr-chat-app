namespace CORE.Models
{
    public interface IBaseModel
    {
        DateTime CreateDate { get; set; }
        int Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime UpdateDate { get; set; }
    }
}