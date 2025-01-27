namespace models ;
 using string = System.string;  public class ReportDataSection { public string Header { get; set; } 
 public List<string> Columns { get; set; } 
 public ReportDataSection (  string header, List<string> columns ) {
  Header=header;
  Columns=columns;
 }
 }