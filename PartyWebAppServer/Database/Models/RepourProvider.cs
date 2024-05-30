using System.ComponentModel.DataAnnotations;

public class RepourProvider
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid RepourToken { get; set; }
    public decimal RepourPrice { get; set; }
}