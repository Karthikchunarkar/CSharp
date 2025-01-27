namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using ReportConfigOption = models.ReportConfigOption; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportConfigOptionRepository :  AbstractD3ERepository<ReportConfigOption> { public int getTypeIndex (  ) {
 return SchemaConstants.ReportConfigOption ;
 }
 }