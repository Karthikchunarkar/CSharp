namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using ResourceData = models.ResourceData; using SchemaConstants = d3e.core.SchemaConstants;  public class ResourceDataRepository :  AbstractD3ERepository<ResourceData> { public int getTypeIndex (  ) {
 return SchemaConstants.ResourceData ;
 }
 }