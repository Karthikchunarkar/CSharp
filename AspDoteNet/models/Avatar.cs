namespace models ;
 using D3EImage = models.D3EImage; using string = System.string;  public class Avatar { public D3EImage Image { get; set; } 
 public string CreateFrom { get; set; } 
 public Avatar (  D3EImage image, string createfrom ) {
  Image=image;
  CreateFrom=createfrom;
 }
 }