using System.Collections.Generic;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        private Category()
        {
        }

        public Category(string name)
        {
            SetName(name);
        }

        public Category(int id, string name)
            : this(name)
        {
            SetId(id);
        }
        
        public string Name { get; private set; }
        public ICollection<Product> Products { get; private set; }

        public void Update(string name)
        {
            SetName(name);
        }

        private void SetId(int id)
        {
            DomainExceptionValidation.When(id <= 0, "Invalid Id value");
            Id = id;
        }

        private void SetName(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Invalid name, Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");

            Name = name;
        }
    }
}