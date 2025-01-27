namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using Report = models.Report; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportRepository :  AbstractD3ERepository<Report> { public int getTypeIndex (  ) {
 return SchemaConstants.Report ;
 }
 }