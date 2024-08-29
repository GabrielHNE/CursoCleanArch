using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTeste1
{
    [Fact(DisplayName = "Create Category with valid state")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid id value. Id most be greater than 0");
    }

    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is too short, minimum 3 characters");
    }

    [Fact]
    public void CreateCategory_NullNameValue_DomainExceptionNullName()
    {
        Action action = () => new Category(1, null);
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Name. Name is required");
    }

    [Fact]
    public void CreateCategory_EmptyNameValue_DomainExceptionEmptyName()
    {
        Action action = () => new Category(1, "");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Name. Name is required");
    }
}