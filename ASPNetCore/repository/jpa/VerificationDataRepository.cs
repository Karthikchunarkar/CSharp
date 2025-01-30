namespace repository.jpa ;
 using d3e.core; using models;  public class VerificationDataRepository :  AbstractD3ERepository<VerificationData> { public int getTypeIndex (  ) {
 return SchemaConstants.VerificationData ;
 }
 }