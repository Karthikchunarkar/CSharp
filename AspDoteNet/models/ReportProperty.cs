namespace models ;
 using bool = System.bool; using string = System.string;  public class ReportProperty { public string Name { get; set; } 
 public string Property { get; set; } 
 public string Type { get; set; } 
 public bool Child { get; set; } 
 public bool Collection { get; set; } 
 public bool IsEnum { get; set; } 
 public bool IsReference { get; set; } 
 public ReportProperty (  string name, string property, string type, bool child, bool collection, bool isenum, bool isreference ) {
  Name=name;
  Property=property;
  Type=type;
  Child=child;
  Collection=collection;
  IsEnum=isenum;
  IsReference=isreference;
 }
 }