namespace models ;
 using ReportNamedCondition = models.ReportNamedCondition; using string = System.string;  public class ReportNamedConditionFilter { public string Name { get; set; } 
 public List<ReportNamedCondition> Conditions { get; set; } 
 public ReportNamedConditionFilter (  string name, List<ReportNamedCondition> conditions ) {
  Name=name;
  Conditions=conditions;
 }
 }