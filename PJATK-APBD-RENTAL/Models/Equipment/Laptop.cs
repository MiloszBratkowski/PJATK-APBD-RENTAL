namespace PJATK_APBD_RENTAL.Models;

public class Laptop : Equipment
{
    public string ProcessorType { get; set; }
    public int RamSizeGb { get; set; }
    
    public Laptop(string name, string description, string processor, int ram) : base(name, description)
    {
        ProcessorType = processor;
        RamSizeGb = ram;
    }
    
}