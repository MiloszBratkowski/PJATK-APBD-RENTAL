namespace PJATK_APBD_RENTAL.Models;

public class Camera : Equipment
{
    public string Resolution { get; set; }
    public string LensType { get; set; }
    
    public Camera(string name, string description, string resolution, string lensType) : base(name, description)
    {
        Resolution = resolution;
        LensType = lensType;
    }
}