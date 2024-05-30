

using System.ComponentModel.DataAnnotations.Schema;

public class Repour
{
    [ForeignKey("RepourProvider")]
    public int ProviderId { get; set; }

    [ForeignKey("User")]
    public string Owner { get; set; }
    public int Quantity { get; set; }
}