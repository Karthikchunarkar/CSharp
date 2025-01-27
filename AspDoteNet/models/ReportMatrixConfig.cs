namespace models ;
 using ReportField = models.ReportField;  public class ReportMatrixConfig { public List<ReportField> Columns { get; set; } 
 public List<ReportField> Rows { get; set; } 
 public List<ReportField> Values { get; set; } 
 public ReportMatrixConfig (  List<ReportField> columns, List<ReportField> rows, List<ReportField> values ) {
  Columns=columns;
  Rows=rows;
  Values=values;
 }
 }