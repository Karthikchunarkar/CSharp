namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class ReportSingleRule :  ReportRule { public static int FIELD = 1 ;
 
 public static int TYPE = 2 ;
 
 public static int OPERATOR = 3 ;
 
 public static int VALUE1 = 4 ;
 
 public static int VALUE2 = 5 ;
 
 public static int FILTER = 6 ;
 
 public static int FIELDVALUE1 = 7 ;
 
 public static int FIELDVALUE2 = 8 ;
 
 public static int FIELDFROM = 9 ;
 
 private string field { get; set; } 
 private string type { get; set; } 
 private ReportRuleOperator operatorValue { get; set; } = ReportRuleOperator.Equal ;
 
 private string value1 { get; set; } 
 private string value2 { get; set; } 
 private ReportFilter filter { get; set; } 
 private string fieldValue1 { get; set; } 
 private string fieldValue2 { get; set; } 
 private ReportFieldFromType fieldFrom { get; set; } = ReportFieldFromType.Filter ;
 
 private ReportSingleRule Old { get; set; } 
 public ReportSingleRule (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportSingleRule ;
 }
 public string Type (  ) {
 return "ReportSingleRule" ;
 }
 public int FieldsCount (  ) {
 return 10 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public string Field (  ) {
 _CheckProxy() ;
 return this.field ;
 }
 public void Field (  string field ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.field,field) ) {
 return ;
 }
 fieldChanged(FIELD,this.field,field) ;
 this.field = field ;
 }
 public string Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  string type ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.type,type) ) {
 return ;
 }
 fieldChanged(TYPE,this.type,type) ;
 this.type = type ;
 }
 public ReportRuleOperator Operator (  ) {
 _CheckProxy() ;
 return this.operator ;
 }
 public void Operator (  ReportRuleOperator operator ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.operator,operator) ) {
 return ;
 }
 fieldChanged(OPERATOR,this.operator,operator) ;
 this.operator = operator ;
 }
 public string Value1 (  ) {
 _CheckProxy() ;
 return this.value1 ;
 }
 public void Value1 (  string value1 ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.value1,value1) ) {
 return ;
 }
 fieldChanged(VALUE1,this.value1,value1) ;
 this.value1 = value1 ;
 }
 public string Value2 (  ) {
 _CheckProxy() ;
 return this.value2 ;
 }
 public void Value2 (  string value2 ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.value2,value2) ) {
 return ;
 }
 fieldChanged(VALUE2,this.value2,value2) ;
 this.value2 = value2 ;
 }
 public ReportFilter Filter (  ) {
 _CheckProxy() ;
 return this.filter ;
 }
 public void Filter (  ReportFilter filter ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.filter,filter) ) {
 return ;
 }
 fieldChanged(FILTER,this.filter,filter) ;
 this.filter = filter ;
 }
 public string FieldValue1 (  ) {
 _CheckProxy() ;
 return this.fieldValue1 ;
 }
 public void FieldValue1 (  string fieldValue1 ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.fieldValue1,fieldValue1) ) {
 return ;
 }
 fieldChanged(FIELDVALUE1,this.fieldValue1,fieldValue1) ;
 this.fieldValue1 = fieldValue1 ;
 }
 public string FieldValue2 (  ) {
 _CheckProxy() ;
 return this.fieldValue2 ;
 }
 public void FieldValue2 (  string fieldValue2 ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.fieldValue2,fieldValue2) ) {
 return ;
 }
 fieldChanged(FIELDVALUE2,this.fieldValue2,fieldValue2) ;
 this.fieldValue2 = fieldValue2 ;
 }
 public ReportFieldFromType FieldFrom (  ) {
 _CheckProxy() ;
 return this.fieldFrom ;
 }
 public void FieldFrom (  ReportFieldFromType fieldFrom ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.fieldFrom,fieldFrom) ) {
 return ;
 }
 fieldChanged(FIELDFROM,this.fieldFrom,fieldFrom) ;
 this.fieldFrom = fieldFrom ;
 }
 public ReportSingleRule getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportSingleRule)old) ;
 }
 public bool equals (  Object a ) {
 return a is ReportSingleRule && base.Equals(a) ;
 }
 public ReportSingleRule DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportSingleRule _obj=((ReportSingleRule)dbObj);
 _obj.Field(field) ;
 _obj.Type(type) ;
 _obj.Operator(operatorValue) ;
 _obj.Value1(value1) ;
 _obj.Value2(value2) ;
 _obj.Filter(ctx.cloneRef(filter)) ;
 _obj.FieldValue1(fieldValue1) ;
 _obj.FieldValue2(fieldValue2) ;
 _obj.FieldFrom(fieldFrom) ;
 }
 public ReportSingleRule CloneInstance (  ReportSingleRule cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportSingleRule() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Field(this.Field()) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.Operator(this.Operator()) ;
 cloneObj.Value1(this.Value1()) ;
 cloneObj.Value2(this.Value2()) ;
 cloneObj.Filter(this.Filter()) ;
 cloneObj.FieldValue1(this.FieldValue1()) ;
 cloneObj.FieldValue2(this.FieldValue2()) ;
 cloneObj.FieldFrom(this.FieldFrom()) ;
 return cloneObj ;
 }
 public ReportSingleRule CreateNewInstance (  ) {
 return new ReportSingleRule() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 base.HandleChildChange(childIdx,set) ;
 }
 }