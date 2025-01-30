namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class ReportCellStyle :  DatabaseObject { public static int WIDTH = 0 ;
 
 public static int FONT = 1 ;
 
 public static int FONTSIZE = 2 ;
 
 public static int TEXTCOLOR = 3 ;
 
 public static int BGCOLOR = 4 ;
 
 public static int VALLIGN = 5 ;
 
 public static int HALLIGN = 6 ;
 
 private int width { get; set; } = 0 ;
 
 private string font { get; set; } 
 private int fontSize { get; set; } = 0 ;
 
 private string textColor { get; set; } 
 private string bgColor { get; set; } 
 private ReportCellAllign vAllign { get; set; } = ReportCellAllign.Center ;
 
 private ReportCellAllign hAllign { get; set; } = ReportCellAllign.Center ;
 
 private ReportCell masterReportCell { get; set; } 
 private ReportCellStyle Old { get; set; } 
 public ReportCellStyle (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportCellStyle ;
 }
 public string Type (  ) {
 return "ReportCellStyle" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReportCell != null ) {
 return masterReportCell ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is ReportCell ) {
 masterReportCell = ((ReportCell)master) ;
 }
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public void updateFlat (  DatabaseObject obj ) {
 super.updateFlat(obj) ;
 if ( masterReportCell != null ) {
 masterReportCell.UpdateFlat(obj) ;
 }
 }
 public int Width (  ) {
 _CheckProxy() ;
 return this.width ;
 }
 public void Width (  int width ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.width,width) ) {
 return ;
 }
 fieldChanged(WIDTH,this.width,width) ;
 this.width = width ;
 }
 public string Font (  ) {
 _CheckProxy() ;
 return this.font ;
 }
 public void Font (  string font ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.font,font) ) {
 return ;
 }
 fieldChanged(FONT,this.font,font) ;
 this.font = font ;
 }
 public int FontSize (  ) {
 _CheckProxy() ;
 return this.fontSize ;
 }
 public void FontSize (  int fontSize ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.fontSize,fontSize) ) {
 return ;
 }
 fieldChanged(FONTSIZE,this.fontSize,fontSize) ;
 this.fontSize = fontSize ;
 }
 public string TextColor (  ) {
 _CheckProxy() ;
 return this.textColor ;
 }
 public void TextColor (  string textColor ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.textColor,textColor) ) {
 return ;
 }
 fieldChanged(TEXTCOLOR,this.textColor,textColor) ;
 this.textColor = textColor ;
 }
 public string BgColor (  ) {
 _CheckProxy() ;
 return this.bgColor ;
 }
 public void BgColor (  string bgColor ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.bgColor,bgColor) ) {
 return ;
 }
 fieldChanged(BGCOLOR,this.bgColor,bgColor) ;
 this.bgColor = bgColor ;
 }
 public ReportCellAllign VAllign (  ) {
 _CheckProxy() ;
 return this.vAllign ;
 }
 public void VAllign (  ReportCellAllign vAllign ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.vAllign,vAllign) ) {
 return ;
 }
 fieldChanged(VALLIGN,this.vAllign,vAllign) ;
 this.vAllign = vAllign ;
 }
 public ReportCellAllign HAllign (  ) {
 _CheckProxy() ;
 return this.hAllign ;
 }
 public void HAllign (  ReportCellAllign hAllign ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.hAllign,hAllign) ) {
 return ;
 }
 fieldChanged(HALLIGN,this.hAllign,hAllign) ;
 this.hAllign = hAllign ;
 }
 public ReportCell MasterReportCell (  ) {
 return this.masterReportCell ;
 }
 public void MasterReportCell (  ReportCell masterReportCell ) {
 this.masterReportCell = masterReportCell ;
 }
 public ReportCellStyle getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportCellStyle)old) ;
 }
 public string DisplayName (  ) {
 return "ReportCellStyle" ;
 }
 public bool equals (  Object a ) {
 return a is ReportCellStyle && base.Equals(a) ;
 }
 public ReportCellStyle DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportCellStyle _obj=((ReportCellStyle)dbObj);
 _obj.Width(width) ;
 _obj.Font(font) ;
 _obj.FontSize(fontSize) ;
 _obj.TextColor(textColor) ;
 _obj.BgColor(bgColor) ;
 _obj.VAllign(vAllign) ;
 _obj.HAllign(hAllign) ;
 }
 public ReportCellStyle CloneInstance (  ReportCellStyle cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportCellStyle() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Width(this.Width()) ;
 cloneObj.Font(this.Font()) ;
 cloneObj.FontSize(this.FontSize()) ;
 cloneObj.TextColor(this.TextColor()) ;
 cloneObj.BgColor(this.BgColor()) ;
 cloneObj.VAllign(this.VAllign()) ;
 cloneObj.HAllign(this.HAllign()) ;
 return cloneObj ;
 }
 public ReportCellStyle CreateNewInstance (  ) {
 return new ReportCellStyle() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }