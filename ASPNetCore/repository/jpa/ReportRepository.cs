namespace repository.jpa ;
 using d3e.core; using models;  public class ReportRepository :  AbstractD3ERepository<Report> { public int getTypeIndex (  ) {
 return SchemaConstants.Report ;
 }
 }