using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

// Sealed -> classe não pode ser herdada
public sealed class Category : Entity
{    
    public string Name { get; private set; }

    public ICollection<Product> Products { get; private set; }

    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid id value. Id most be greater than 0");
        Id = id;
        ValidateDomain(name);
    }

    public void Update(string name){
        ValidateDomain(name);
    }

    private void ValidateDomain(string name){
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name. Name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name. Name is too short, minimum 3 characters");

        Name = name;
    }
}
