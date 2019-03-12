namespace FormsAdminGP.Domain
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool IsActive { get; set; }
    }

    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
