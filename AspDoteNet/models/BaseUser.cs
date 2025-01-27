namespace models ;
 using UserDevice = models.UserDevice; using bool = System.bool; using string = System.string;  public class BaseUser { public bool IsActive { get; set; } 
 public string DeviceToken { get; set; } 
 public UserDevice Devices { get; set; } 
 public BaseUser (  bool isactive, string devicetoken, UserDevice devices ) {
  IsActive=isactive;
  DeviceToken=devicetoken;
  Devices=devices;
 }
 }