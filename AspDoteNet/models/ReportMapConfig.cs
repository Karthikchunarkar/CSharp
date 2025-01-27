namespace models ;
 using ReportField = models.ReportField; using ReportMapType = classes.ReportMapType;  public class ReportMapConfig { public ReportMapType Type { get; set; } 
 public List<ReportField> Location { get; set; } 
 public List<ReportField> Legend { get; set; } 
 public ReportField Latitude { get; set; } 
 public ReportField Longitude { get; set; } 
 public List<ReportField> Size { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportMapConfig (  ReportMapType type, List<ReportField> location, List<ReportField> legend, ReportField latitude, ReportField longitude, List<ReportField> size, List<ReportField> tooltips ) {
  Type=type;
  Location=location;
  Legend=legend;
  Latitude=latitude;
  Longitude=longitude;
  Size=size;
  Tooltips=tooltips;
 }
 }