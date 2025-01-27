namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using Avatar = models.Avatar; using SchemaConstants = d3e.core.SchemaConstants;  public class AvatarRepository :  AbstractD3ERepository<Avatar> { public int getTypeIndex (  ) {
 return SchemaConstants.Avatar ;
 }
 }