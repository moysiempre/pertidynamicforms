namespace FormsAdmin.Core.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool Active { get; set; }
    }
}
