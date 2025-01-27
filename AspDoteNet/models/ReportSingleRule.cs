namespace models ;
 using ReportFieldFromType = classes.ReportFieldFromType; using ReportFilter = models.ReportFilter; using ReportRuleOperator = classes.ReportRuleOperator; using string = System.string;  public class ReportSingleRule { public string Field { get; set; } 
 public string Type { get; set; } 
 public ReportRuleOperator Operator { get; set; } 
 public string Value1 { get; set; } 
 public string Value2 { get; set; } 
 public ReportFilter Filter { get; set; } 
 public string FieldValue1 { get; set; } 
 public string FieldValue2 { get; set; } 
 public ReportFieldFromType FieldFrom { get; set; } 
 public ReportSingleRule (  string field, string type, ReportRuleOperator operator, string value1, string value2, ReportFilter filter, string fieldvalue1, string fieldvalue2, ReportFieldFromType fieldfrom ) {
  Field=field;
  Type=type;
  Operator=operator;
  Value1=value1;
  Value2=value2;
  Filter=filter;
  FieldValue1=fieldvalue1;
  FieldValue2=fieldvalue2;
  FieldFrom=fieldfrom;
 }
 }