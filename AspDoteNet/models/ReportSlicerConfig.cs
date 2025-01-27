namespace models ;
 using ReportField = models.ReportField;  public class ReportSlicerConfig { public List<ReportField> Fields { get; set; } 
 public ReportSlicerConfig (  List<ReportField> fields ) {
  Fields=fields;
 }
 }