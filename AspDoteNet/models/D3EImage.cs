namespace models ;
 using DFile = d3e.core.DFile; using int = System.int;  public class D3EImage { public int Size { get; set; } 
 public int Width { get; set; } 
 public int Height { get; set; } 
 public DFile File { get; set; } 
 public D3EImage (  int size, int width, int height, DFile file ) {
  Size=size;
  Width=width;
  Height=height;
  File=file;
 }
 }