namespace models ;
 using ReportField = models.ReportField;  public class ReportFunnelChartConfig { public List<ReportField> CategoryFields { get; set; } 
 public List<ReportField> Values { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportFunnelChartConfig (  List<ReportField> categoryfields, List<ReportField> values, List<ReportField> tooltips ) {
  CategoryFields=categoryfields;
  Values=values;
  Tooltips=tooltips;
 }
 }