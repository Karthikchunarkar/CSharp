namespace repository.jpa ;
 using d3e.core; using models;  public class ReportConfigOptionRepository :  AbstractD3ERepository<ReportConfigOption> { public int getTypeIndex (  ) {
 return SchemaConstants.ReportConfigOption ;
 }
 }