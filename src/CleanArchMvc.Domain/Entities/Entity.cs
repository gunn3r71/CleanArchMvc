using System;

namespace CleanArchMvc.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        public int Id { get; protected set; }
    }
}