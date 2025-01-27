namespace models ;
 using ReportBarChartType = classes.ReportBarChartType; using ReportField = models.ReportField;  public class ReportBarChartConfig { public ReportBarChartType Type { get; set; } 
 public List<ReportField> XAxes { get; set; } 
 public List<ReportField> YAxes { get; set; } 
 public ReportField Legend { get; set; } 
 public List<ReportField> SmallMultiples { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportBarChartConfig (  ReportBarChartType type, List<ReportField> xaxes, List<ReportField> yaxes, ReportField legend, List<ReportField> smallmultiples, List<ReportField> tooltips ) {
  Type=type;
  XAxes=xaxes;
  YAxes=yaxes;
  Legend=legend;
  SmallMultiples=smallmultiples;
  Tooltips=tooltips;
 }
 }