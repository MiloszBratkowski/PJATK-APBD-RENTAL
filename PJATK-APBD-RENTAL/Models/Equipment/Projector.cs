namespace PJATK_APBD_RENTAL.Models;

public class Projector : Equipment
{
    public int BrightnessLumens { get; set; }
    public int MaxDiagonalInches { get; set; }
    
    public Projector(string name, string description, int brightness, int maxDiagonal) : base(name, description)
    {
        BrightnessLumens = brightness;
        MaxDiagonalInches = maxDiagonal;
    }
}