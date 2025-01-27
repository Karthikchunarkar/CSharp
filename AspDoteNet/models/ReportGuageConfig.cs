namespace models ;
 using ReportField = models.ReportField;  public class ReportGuageConfig { public List<ReportField> Value { get; set; } 
 public List<ReportField> Min { get; set; } 
 public List<ReportField> Max { get; set; } 
 public List<ReportField> Target { get; set; } 
 public List<ReportField> Tooltips { get; set; } 
 public ReportGuageConfig (  List<ReportField> value, List<ReportField> min, List<ReportField> max, List<ReportField> target, List<ReportField> tooltips ) {
  Value=value;
  Min=min;
  Max=max;
  Target=target;
  Tooltips=tooltips;
 }
 }