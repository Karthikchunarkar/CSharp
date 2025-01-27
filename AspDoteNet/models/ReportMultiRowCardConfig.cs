namespace models ;
 using ReportField = models.ReportField;  public class ReportMultiRowCardConfig { public List<ReportField> Values { get; set; } 
 public ReportMultiRowCardConfig (  List<ReportField> values ) {
  Values=values;
 }
 }