using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact(DisplayName = "Create Category with valid state")]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid id value");
    }

    [Fact]
    public void CreateProduct_NameEmpty_DomainExceptionEmptyName()
    {
        Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "product image");;
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required!");
    }

    [Fact]
    public void CreateProduct_NameNull_DomainExceptionNullName()
    {
        Action action = () => new Product(1, null, "Product Description", 9.99m, 99, "product image");;
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required!");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");;
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is to short, minimum 3 characters");
    }

    [Fact]
    public void CreateProduct_ShortDescriptionValue_DomainExceptionShortDescription()
    {
        Action action = () => new Product(1, "Product", "1234", 9.99m, 99, "product image");

        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid description. description is to short, minimum 5 characters");
    }

    [Fact]
    public void CreateProduct_PriceNegative_DomainExceptionInvalidPrice()
    {
        Action action = () => new Product(1, "Product", "Product Description", -9.99m, 99, "product image");;
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }

    [Theory] //usando quando possui parametros no metodo
    [InlineData(-5)]
    public void CreateProduct_StockNegative_DomainExceptionInvalidStock(int value)
    {
        Action action = () => new Product(1, "Product", "Product Description", 9.99m, value, "product image");;
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }
    
    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () => new Product(
            1, 
            "Product",
            "Product Description", 
            9.99m, 
            99,
            "producsdasdasdasdasdsadasdasdasdasdt idasdasdasdasdaskdjasdljaskldjasdjalskjdlaksjdkalsjdlakshdlaskjdlaskjdkalsjdlkasjdklasjdlkasjdlkasjdlkasjdklasjdlaskjdaklsjdalksjdkasjdklajkldasjkaddasdasdadasdasdasdasdasdasdasdasdasdasdaslkdjaskjdjalsdjasjdaklsjdlaksjdalksdjkasjkdaldjlkamage");;
        
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image name. Image name is too long, maximum 250 characters");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => new Product(
            1, 
            "Product",
            "Product Description", 
            9.99m, 
            99,
            null);
        
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () => new Product(
            1, 
            "Product",
            "Product Description", 
            9.99m, 
            99,
            null);
        
        action
            .Should()
            .NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () => new Product(
            1, 
            "Product",
            "Product Description", 
            9.99m, 
            99,
            "");
        
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }
}