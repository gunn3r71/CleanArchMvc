using CleanArchMvc.Domain.Validation;
using System;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public Product(string name,
                       string description,
                       decimal price,
                       int stock,
                       string image,
                       Guid categoryId)
        {
            SetName(name);
            SetDescription(description);
            SetPrice(price);
            SetStock(stock);
            SetImage(image);
            CategoryId = categoryId;
        }

        public Product(int id,
                       string name,
                       string description,
                       decimal price,
                       int stock,
                       string image,
                       Guid categoryId)
            : this(name,
                   description,
                   price,
                   stock,
                   image,
                   categoryId)
        {
            DomainExceptionValidation.When(id <= 0, "Invalid id value");
            Id = id;
        }
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        public void Update(string name,
                      string description,
                      decimal price,
                      int stock,
                      string image,
                      Guid categoryId)
        {
            SetName(name);
            SetDescription(description);
            SetPrice(price);
            SetStock(stock);
            SetImage(image);
            CategoryId = categoryId;
        }

        private void SetName(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Invalid name, Name is required");
            DomainExceptionValidation.When(name is { Length: < 3 }, "Invalid name, too short, minimum 3 characters");

            Name = name;
        }

        private void SetDescription(string description)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), "Invalid description. Description is required");
            DomainExceptionValidation.When(description is { Length: < 10 }, "Invalid description, too short, minimum 10 characters");

            Description = description;
        }

        private void SetPrice(decimal price)
        {
            DomainExceptionValidation.When(price <= 0, "Invalid price. Price must be greater than 0");

            Price = price;
        }

        private void SetStock(int stock)
        {
            DomainExceptionValidation.When(stock <= 0, "Invalid stock. Stock be greater than or equal to 0");

            Stock = stock;
        }

        private void SetImage(string image)
        {
            DomainExceptionValidation.When(image is { Length: > 254 }, "Invalid image, too long, maximum 250 characters");
            Image = image;
        }
    }
}