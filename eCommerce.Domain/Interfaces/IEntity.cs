namespace eCommerce.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
        string CreatedBy { get; }
        string UpdatedBy { get; }
        bool IsActive { get; }
    }
} 