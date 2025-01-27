namespace models ;
 using string = System.string;  public class ReportDataRow { public List<string> Row { get; set; } 
 public ReportDataRow (  List<string> row ) {
  Row=row;
 }
 }