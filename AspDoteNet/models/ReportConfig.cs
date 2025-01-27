namespace models ;
 using ReportConfigOption = models.ReportConfigOption; using string = System.string;  public class ReportConfig { public string Identity { get; set; } 
 public List<ReportConfigOption> Values { get; set; } 
 public ReportConfig (  string identity, List<ReportConfigOption> values ) {
  Identity=identity;
  Values=values;
 }
 }