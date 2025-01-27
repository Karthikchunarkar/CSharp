namespace models ;
 using ReportField = models.ReportField; using ReportPieChartType = classes.ReportPieChartType;  public class ReportPieChartConfig { public ReportPieChartType Type { get; set; } 
 public List<ReportField> Legend { get; set; } 
 public List<ReportField> Values { get; set; } 
 public List<ReportField> Details { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportPieChartConfig (  ReportPieChartType type, List<ReportField> legend, List<ReportField> values, List<ReportField> details, List<ReportField> tooltips ) {
  Type=type;
  Legend=legend;
  Values=values;
  Details=details;
  Tooltips=tooltips;
 }
 }