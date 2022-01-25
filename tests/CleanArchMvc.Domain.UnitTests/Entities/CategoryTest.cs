using System;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.UnitTests.Entities
{
    public class CategoryTest
    {
        [Fact(DisplayName = "Create category object with valid state")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Books");

            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create category without id with valid state")]
        public void CreateCategory_WithoutId_ResultObjectValidState()
        {
            Action action = () => new Category("Books");

            action.Should()
                    .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create category object with invalid id")]
        public void CreateCategory_WithInvalidIdParameter_DomainExceptionValidation()
        {
            Action action = () => new Category(-1, "Books");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Create category object with invalid name")]
        public void CreateCategory_WithInvalidNameParameter_DomainExceptionValidation()
        {
            Action action = () => new Category(1, "");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, Name is required");
        }

        [Fact(DisplayName = "Create category object with invalid name length")]
        public void CreateCategory_WithInvalidNameLengthParameter_DomainExceptionValidation()
        {
            Action action = () => new Category(1, "ab");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");
        }
    }
}