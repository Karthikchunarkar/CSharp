namespace models ;
 using BaseUser = models.BaseUser; using UserDevice = models.UserDevice; using bool = System.bool; using string = System.string;  public class PushNotification { public List<BaseUser> Users { get; set; } 
 public bool SkipThisDevice { get; set; } 
 public string DeviceToken { get; set; } 
 public string Title { get; set; } 
 public string Body { get; set; } 
 public string Path { get; set; } 
 public List<string> Data { get; set; } 
 public bool Failed { get; set; } 
 public List<UserDevice> FailedDevices { get; set; } 
 public PushNotification (  List<BaseUser> users, bool skipthisdevice, string devicetoken, string title, string body, string path, List<string> data, bool failed, List<UserDevice> faileddevices ) {
  Users=users;
  SkipThisDevice=skipthisdevice;
  DeviceToken=devicetoken;
  Title=title;
  Body=body;
  Path=path;
  Data=data;
  Failed=failed;
  FailedDevices=faileddevices;
 }
 }