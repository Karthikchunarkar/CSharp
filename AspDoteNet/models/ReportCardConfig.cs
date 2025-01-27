namespace models ;
 using ReportField = models.ReportField;  public class ReportCardConfig { public ReportField Value { get; set; } 
 public ReportCardConfig (  ReportField value ) {
  Value=value;
 }
 }