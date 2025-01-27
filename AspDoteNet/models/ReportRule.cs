namespace models ;
 using ReportRule = models.ReportRule;  public class ReportRule { public ReportRule Parent { get; set; } 
 public ReportRule (  ReportRule parent ) {
  Parent=parent;
 }
 }