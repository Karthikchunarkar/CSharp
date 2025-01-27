namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using SchemaConstants = d3e.core.SchemaConstants; using VerificationData = models.VerificationData;  public class VerificationDataRepository :  AbstractD3ERepository<VerificationData> { public int getTypeIndex (  ) {
 return SchemaConstants.VerificationData ;
 }
 }