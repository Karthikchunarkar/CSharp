namespace models ;
 using ReportField = models.ReportField;  public class ReportScatterChartConfig { public List<ReportField> Values { get; set; } 
 public List<ReportField> XAxes { get; set; } 
 public List<ReportField> YAxes { get; set; } 
 public List<ReportField> Size { get; set; } 
 public List<ReportField> Legends { get; set; } 
 public List<ReportField> PlayAxis { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportScatterChartConfig (  List<ReportField> values, List<ReportField> xaxes, List<ReportField> yaxes, List<ReportField> size, List<ReportField> legends, List<ReportField> playaxis, List<ReportField> tooltips ) {
  Values=values;
  XAxes=xaxes;
  YAxes=yaxes;
  Size=size;
  Legends=legends;
  PlayAxis=playaxis;
  Tooltips=tooltips;
 }
 }