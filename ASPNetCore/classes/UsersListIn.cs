namespace classes ;
  public class UsersListIn { public int pageSize ;
 
 public int offset ;
 
 public string orderBy ;
 
 public bool ascending ;
 
 public UsersListIn (  ) {
 }
 public UsersListIn (  bool ascending, int offset, string orderBy, int pageSize ) {
 this.pageSize = pageSize ;
 this.offset = offset ;
 this.orderBy = orderBy ;
 this.ascending = ascending ;
 }
 }