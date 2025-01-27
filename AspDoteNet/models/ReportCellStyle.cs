namespace models ;
 using ReportCellAllign = classes.ReportCellAllign; using int = System.int; using string = System.string;  public class ReportCellStyle { public int Width { get; set; } 
 public string Font { get; set; } 
 public int FontSize { get; set; } 
 public string TextColor { get; set; } 
 public string BgColor { get; set; } 
 public ReportCellAllign VAllign { get; set; } 
 public ReportCellAllign HAllign { get; set; } 
 public ReportCellStyle (  int width, string font, int fontsize, string textcolor, string bgcolor, ReportCellAllign vallign, ReportCellAllign hallign ) {
  Width=width;
  Font=font;
  FontSize=fontsize;
  TextColor=textcolor;
  BgColor=bgcolor;
  VAllign=vallign;
  HAllign=hallign;
 }
 }