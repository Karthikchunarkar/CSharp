namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using MicroSoftAcc = models.MicroSoftAcc; using SchemaConstants = d3e.core.SchemaConstants;  public class MicroSoftAccRepository :  AbstractD3ERepository<MicroSoftAcc> { public int getTypeIndex (  ) {
 return SchemaConstants.MicroSoftAcc ;
 }
 }