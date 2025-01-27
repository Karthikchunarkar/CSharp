namespace models ;
 using ReportField = models.ReportField; using ReportLineAndColumnChartType = classes.ReportLineAndColumnChartType;  public class ReportLineAndColumnChartConfig { public ReportLineAndColumnChartType Type { get; set; } 
 public List<ReportField> XAxes { get; set; } 
 public List<ReportField> ColumnYAxes { get; set; } 
 public List<ReportField> LineYAxes { get; set; } 
 public List<ReportField> ColumnLegend { get; set; } 
 public List<ReportField> SmallMultiples { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportLineAndColumnChartConfig (  ReportLineAndColumnChartType type, List<ReportField> xaxes, List<ReportField> columnyaxes, List<ReportField> lineyaxes, List<ReportField> columnlegend, List<ReportField> smallmultiples, List<ReportField> tooltips ) {
  Type=type;
  XAxes=xaxes;
  ColumnYAxes=columnyaxes;
  LineYAxes=lineyaxes;
  ColumnLegend=columnlegend;
  SmallMultiples=smallmultiples;
  Tooltips=tooltips;
 }
 }