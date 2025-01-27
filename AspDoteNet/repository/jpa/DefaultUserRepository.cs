namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using DefaultUser = models.DefaultUser; using SchemaConstants = d3e.core.SchemaConstants;  public class DefaultUserRepository :  AbstractD3ERepository<DefaultUser> { public int getTypeIndex (  ) {
 return SchemaConstants.DefaultUser ;
 }
 }