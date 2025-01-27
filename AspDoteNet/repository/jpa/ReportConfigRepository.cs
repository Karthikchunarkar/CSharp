namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using ReportConfig = models.ReportConfig; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportConfigRepository :  AbstractD3ERepository<ReportConfig> { public int getTypeIndex (  ) {
 return SchemaConstants.ReportConfig ;
 }
 }