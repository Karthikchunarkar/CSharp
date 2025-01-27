namespace models ;
 using ReportField = models.ReportField;  public class ReportTableConfig { public List<ReportField> Columns { get; set; } 
 public ReportTableConfig (  List<ReportField> columns ) {
  Columns=columns;
 }
 }