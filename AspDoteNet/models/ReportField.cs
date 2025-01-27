namespace models ;
 using ReportAggregateType = classes.ReportAggregateType; using string = System.string;  public class ReportField { public string Name { get; set; } 
 public string Field { get; set; } 
 public ReportAggregateType Aggregate { get; set; } 
 public ReportField (  string name, string field, ReportAggregateType aggregate ) {
  Name=name;
  Field=field;
  Aggregate=aggregate;
 }
 }