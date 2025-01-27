namespace models ;
 using BaseUser = models.BaseUser;  public class AllDevicesRequest { public List<BaseUser> Users { get; set; } 
 public AllDevicesRequest (  List<BaseUser> users ) {
  Users=users;
 }
 }