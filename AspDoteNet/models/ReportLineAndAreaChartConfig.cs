namespace models ;
 using ReportField = models.ReportField; using ReportLineAndAreaChartType = classes.ReportLineAndAreaChartType;  public class ReportLineAndAreaChartConfig { public ReportLineAndAreaChartType Type { get; set; } 
 public List<ReportField> XAxes { get; set; } 
 public List<ReportField> YAxes { get; set; } 
 public List<ReportField> SecondaryYAxes { get; set; } 
 public List<ReportField> Legend { get; set; } 
 public List<ReportField> SmallMultiples { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportLineAndAreaChartConfig (  ReportLineAndAreaChartType type, List<ReportField> xaxes, List<ReportField> yaxes, List<ReportField> secondaryyaxes, List<ReportField> legend, List<ReportField> smallmultiples, List<ReportField> tooltips ) {
  Type=type;
  XAxes=xaxes;
  YAxes=yaxes;
  SecondaryYAxes=secondaryyaxes;
  Legend=legend;
  SmallMultiples=smallmultiples;
  Tooltips=tooltips;
 }
 }