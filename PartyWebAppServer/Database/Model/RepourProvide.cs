namespace DefaultNamespace;

public class RepourProvide
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid RepourToken { get; set; }
    public int Cost { get; set; }
}