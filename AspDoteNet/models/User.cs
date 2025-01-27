namespace models ;
 using DFile = d3e.core.DFile; using DateTime = System.DateTime; using UserRole = classes.UserRole; using bool = System.bool; using string = System.string;  public class User { public string FirstName { get; set; } 
 public string LastName { get; set; } 
 public string Email { get; set; } 
 public string Password { get; set; } 
 public string PhoneNumber { get; set; } 
 public UserRole Role { get; set; } 
 public bool Status { get; set; } 
 public DateTime LastLogin { get; set; } 
 public DFile Profile { get; set; } 
 public bool IsRememberMe { get; set; } 
 public string TwilioNumber { get; set; } 
 public DateTime CreatedDate { get; set; } 
 public DateTime UpdatedDate { get; set; } 
 public User (  string firstname, string lastname, string email, string password, string phonenumber, UserRole role, bool status, DateTime lastlogin, DFile profile, bool isrememberme, string twilionumber, DateTime createddate, DateTime updateddate ) {
  FirstName=firstname;
  LastName=lastname;
  Email=email;
  Password=password;
  PhoneNumber=phonenumber;
  Role=role;
  Status=status;
  LastLogin=lastlogin;
  Profile=profile;
  IsRememberMe=isrememberme;
  TwilioNumber=twilionumber;
  CreatedDate=createddate;
  UpdatedDate=updateddate;
 }
 }