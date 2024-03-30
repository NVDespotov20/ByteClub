namespace IdeaInvestigator.BusinessLogic.Models.VM;

public class ProductVM
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Image { get; set; } = null!;
}