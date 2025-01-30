namespace repository.jpa ;
 using d3e.core; using models;  public class AvatarRepository :  AbstractD3ERepository<Avatar> { public int getTypeIndex (  ) {
 return SchemaConstants.Avatar ;
 }
 }