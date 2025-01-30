namespace repository.jpa ;
 using d3e.core; using models;  public class CategoryRepository :  AbstractD3ERepository<Category> { public int getTypeIndex (  ) {
 return SchemaConstants.Category ;
 }
 }