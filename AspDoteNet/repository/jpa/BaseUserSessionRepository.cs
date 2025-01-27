namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using BaseUserSession = models.BaseUserSession; using SchemaConstants = d3e.core.SchemaConstants;  public class BaseUserSessionRepository :  AbstractD3ERepository<BaseUserSession> { public int getTypeIndex (  ) {
 return SchemaConstants.BaseUserSession ;
 }
 }