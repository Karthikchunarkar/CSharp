namespace models ;
 using ReportField = models.ReportField;  public class ReportKeyInfluencerConfig { public ReportField Analyze { get; set; } 
 public List<ReportField> EexplainBy { get; set; } 
 public List<ReportField> ExpandBy { get; set; } 
 public ReportKeyInfluencerConfig (  ReportField analyze, List<ReportField> eexplainby, List<ReportField> expandby ) {
  Analyze=analyze;
  EexplainBy=eexplainby;
  ExpandBy=expandby;
 }
 }