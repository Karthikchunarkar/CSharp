namespace models ;
 using ReportProperty = models.ReportProperty; using string = System.string;  public class ReportModel { public string Name { get; set; } 
 public List<ReportProperty> Properties { get; set; } 
 public ReportModel (  string name, List<ReportProperty> properties ) {
  Name=name;
  Properties=properties;
 }
 }