namespace repository.jpa ;
 using d3e.core; using models;  public class ReportConfigRepository :  AbstractD3ERepository<ReportConfig> { public int getTypeIndex (  ) {
 return SchemaConstants.ReportConfig ;
 }
 }