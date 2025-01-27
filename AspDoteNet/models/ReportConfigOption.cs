namespace models ;
 using string = System.string;  public class ReportConfigOption { public string Identity { get; set; } 
 public string Value { get; set; } 
 public ReportConfigOption (  string identity, string value ) {
  Identity=identity;
  Value=value;
 }
 }