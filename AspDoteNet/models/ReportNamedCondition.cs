namespace models ;
 using ReportRule = models.ReportRule; using string = System.string;  public class ReportNamedCondition { public string Name { get; set; } 
 public ReportRule Condition { get; set; } 
 public ReportNamedCondition (  string name, ReportRule condition ) {
  Name=name;
  Condition=condition;
 }
 }