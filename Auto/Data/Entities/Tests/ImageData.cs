namespace Auto.Data.Entities.Tests;

public class ImageData
{
    public int ImageId { get; set; }
    
    public byte[] Data { get; set; }
    
    public Question? Question { get; set; }
}