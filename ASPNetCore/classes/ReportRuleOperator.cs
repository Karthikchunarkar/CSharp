namespace classes ;
  public class ReportRuleOperator { private List < String > propType ;
 
 public static ReportRuleOperator Equal = new ReportRuleOperator(d3e.core.ListExt.asList("String","Integer","Double","Object","DateTime","Date","Time","OptionSet")) ;
 
 public static ReportRuleOperator NotEqual = new ReportRuleOperator(d3e.core.ListExt.asList("String","Integer","Double","Object","DateTime","Date","Time","OptionSet")) ;
 
 public static ReportRuleOperator Between = new ReportRuleOperator(d3e.core.ListExt.asList("DateTime","Date","Time","Integer","Double")) ;
 
 public static ReportRuleOperator IsIn = new ReportRuleOperator(d3e.core.ListExt.asList("String","Object","OptionSet")) ;
 
 public static ReportRuleOperator IsNotIn = new ReportRuleOperator(d3e.core.ListExt.asList("String","Object","OptionSet")) ;
 
 public static ReportRuleOperator GreaterThan = new ReportRuleOperator(d3e.core.ListExt.asList("Integer","Double","DateTime","Date","Time")) ;
 
 public static ReportRuleOperator LessThan = new ReportRuleOperator(d3e.core.ListExt.asList("Integer","Double","DateTime","Date","Time")) ;
 
 public static ReportRuleOperator GreaterThanorEqual = new ReportRuleOperator(d3e.core.ListExt.asList("Integer","Double","DateTime","Date","Time")) ;
 
 public static ReportRuleOperator LessThanorEqual = new ReportRuleOperator(d3e.core.ListExt.asList("Integer","Double","DateTime","Date","Time")) ;
 
 public static ReportRuleOperator Contains = new ReportRuleOperator(d3e.core.ListExt.asList("String","Double","Object","Integer")) ;
 
 public static ReportRuleOperator NotContains = new ReportRuleOperator(d3e.core.ListExt.asList("String","Object","Integer","Double")) ;
 
 public static ReportRuleOperator StartsWith = new ReportRuleOperator(d3e.core.ListExt.asList("String","Object")) ;
 
 public static ReportRuleOperator EndsWith = new ReportRuleOperator(d3e.core.ListExt.asList("String","Object")) ;
 
 public static ReportRuleOperator IsSet = new ReportRuleOperator(d3e.core.ListExt.asList("String","Integer","Double","Object","DateTime","Date","Time","OptionSet")) ;
 
 public static ReportRuleOperator IsNotSet = new ReportRuleOperator(d3e.core.ListExt.asList("String","Integer","Double","Object","DateTime","Date","Time","OptionSet")) ;
 
 public static ReportRuleOperator Match = new ReportRuleOperator(d3e.core.ListExt.asList("Object")) ;
 
 public static ReportRuleOperator NotMatch = new ReportRuleOperator(d3e.core.ListExt.asList("Object")) ;
 
 public static ReportRuleOperator Is = new ReportRuleOperator(d3e.core.ListExt.asList("Boolean")) ;
 
 public static ReportRuleOperator IsNot = new ReportRuleOperator(d3e.core.ListExt.asList("Boolean")) ;
 
 public static ReportRuleOperator IsWithin = new ReportRuleOperator(d3e.core.ListExt.asList("DateTime","Date","Time")) ;
 
 private ReportRuleOperator (  List < String > propType ) {
 this.propType = propType ;
 }
 public List < String > PropType (  ) {
 return this.propType ;
 }
 }