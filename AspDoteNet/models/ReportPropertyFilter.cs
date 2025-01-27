namespace models ;
 using bool = System.bool; using string = System.string;  public class ReportPropertyFilter { public string Name { get; set; } 
 public string Property { get; set; } 
 public string Type { get; set; } 
 public bool IsEnum { get; set; } 
 public bool IsReference { get; set; } 
 public bool AllowMultiple { get; set; } 
 public bool ApplyRange { get; set; } 
 public ReportPropertyFilter (  string name, string property, string type, bool isenum, bool isreference, bool allowmultiple, bool applyrange ) {
  Name=name;
  Property=property;
  Type=type;
  IsEnum=isenum;
  IsReference=isreference;
  AllowMultiple=allowmultiple;
  ApplyRange=applyrange;
 }
 }