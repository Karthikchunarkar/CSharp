namespace classes ;
  public class CategoriesListIn { public int pageSize ;
 
 public int offset ;
 
 public string orderBy ;
 
 public bool ascending ;
 
 public CategoriesListIn (  ) {
 }
 public CategoriesListIn (  bool ascending, int offset, string orderBy, int pageSize ) {
 this.pageSize = pageSize ;
 this.offset = offset ;
 this.orderBy = orderBy ;
 this.ascending = ascending ;
 }
 }