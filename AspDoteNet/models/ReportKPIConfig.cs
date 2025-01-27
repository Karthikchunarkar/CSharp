namespace models ;
 using ReportField = models.ReportField;  public class ReportKPIConfig { public ReportField Value { get; set; } 
 public List<ReportField> Target { get; set; } 
 public ReportField Trend { get; set; } 
 public ReportKPIConfig (  ReportField value, List<ReportField> target, ReportField trend ) {
  Value=value;
  Target=target;
  Trend=trend;
 }
 }