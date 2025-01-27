namespace models ;
 using ReportField = models.ReportField;  public class ReportWaterfallChartConfig { public List<ReportField> Category { get; set; } 
 public List<ReportField> BreakdownFields { get; set; } 
 public List<ReportField> YAxes { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportWaterfallChartConfig (  List<ReportField> category, List<ReportField> breakdownfields, List<ReportField> yaxes, List<ReportField> tooltips ) {
  Category=category;
  BreakdownFields=breakdownfields;
  YAxes=yaxes;
  Tooltips=tooltips;
 }
 }