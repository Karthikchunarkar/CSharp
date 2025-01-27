namespace models ;
 using ReportCellStyle = models.ReportCellStyle; using ReportCellType = classes.ReportCellType; using int = System.int; using string = System.string;  public class ReportCell { public ReportCellType Type { get; set; } 
 public int X { get; set; } 
 public int Y { get; set; } 
 public ReportCellStyle Style { get; set; } 
 public string Value { get; set; } 
 public ReportCell (  ReportCellType type, int x, int y, ReportCellStyle style, string value ) {
  Type=type;
  X=x;
  Y=y;
  Style=style;
  Value=value;
 }
 }