using FormsAdmin.Core.Interfaces;

namespace FormsAdmin.Core.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        public virtual bool Active { get; set; }
    }
}
