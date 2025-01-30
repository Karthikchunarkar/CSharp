namespace repository.jpa ;
 using d3e.core; using models;  public class SignUpRequestRepository :  AbstractD3ERepository<SignUpRequest> { public int getTypeIndex (  ) {
 return SchemaConstants.SignUpRequest ;
 }
 }