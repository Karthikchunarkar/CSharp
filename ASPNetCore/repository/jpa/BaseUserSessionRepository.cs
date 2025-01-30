namespace repository.jpa ;
 using d3e.core; using models;  public class BaseUserSessionRepository :  AbstractD3ERepository<BaseUserSession> { public int getTypeIndex (  ) {
 return SchemaConstants.BaseUserSession ;
 }
 }