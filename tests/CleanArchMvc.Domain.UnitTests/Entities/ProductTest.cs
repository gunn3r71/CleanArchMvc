using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.UnitTests.Entities
{
    public class ProductTest
    {
        [Fact(DisplayName = "Create product object with valid state")]
        public void Should_CreateProduct_With_Valid_Parameters()
        {
            Action action = () => new Product(1, "Product", "description", 100.00M, 10, "image", 1);

            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create product without id with object valid state")]
        public void Should_CreateProduct_without_Id_Parameter()
        {
            Action action = () => new Product("Product", "description", 100.00M, 10, "image", 1);

            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create product with invalid id")]
        public void Should_CreateProduct_With_Invalid_Id_Parameter_DomainExceptionValidation()
        {
            Action action = () => new Product(-1,"Product", "description", 100.00M, 10, "image", 1);

            action.Should()
                    .Throw<DomainExceptionValidation>()
                    .WithMessage("Invalid id value");
        }

        [Fact(DisplayName = "Create product without name")]
        public void Should_CreateProduct_With_Invalid_Name_Parameter_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "", "description", 100.00M, 10, "image", 1);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, Name is required");
        }

        [Fact(DisplayName = "Create product invalid name length")]
        public void Should_CreateProduct_With_Invalid_Name_Length_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "ac", "description", 100.00M, 10, "image", 1);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");
        }
    }
}