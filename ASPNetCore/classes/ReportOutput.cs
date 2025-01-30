namespace classes ;
 using classes; using d3e.core; using java.util; using store;  public class ReportOutput :  DBObject { public static int _REPORTOUTOPTION = 0 ;
 
 public static int _REPORTOUTCOLUMN = 1 ;
 
 public static int _SUBCOLUMNS = 2 ;
 
 public static int _REPORTOUTATTRIBUTE = 3 ;
 
 public static int _REPORTOUTROW = 4 ;
 
 private long id 
 private List < ReportOutOption > reportOutOption = new D3EPersistanceList<>(this,_REPORTOUTOPTION) ;
 
 private List < ReportOutColumn > reportOutColumn = new D3EPersistanceList<>(this,_REPORTOUTCOLUMN) ;
 
 private List < ReportOutColumn > subColumns = new D3EPersistanceList<>(this,_SUBCOLUMNS) ;
 
 private List < ReportOutAttribute > reportOutAttribute = new D3EPersistanceList<>(this,_REPORTOUTATTRIBUTE) ;
 
 private List < ReportOutRow > reportOutRow = new D3EPersistanceList<>(this,_REPORTOUTROW) ;
 
 public ReportOutput (  ) {
 }
 public ReportOutput (  List < ReportOutAttribute > reportOutAttribute, List < ReportOutColumn > reportOutColumn, List < ReportOutOption > reportOutOption, List < ReportOutRow > reportOutRow, List < ReportOutColumn > subColumns ) {
 this.ReportOutAttribute.addAll(reportOutAttribute) ;
 this.ReportOutColumn.addAll(reportOutColumn) ;
 this.ReportOutOption.addAll(reportOutOption) ;
 this.ReportOutRow.addAll(reportOutRow) ;
 this.SubColumns.addAll(subColumns) ;
 }
 public long getId (  ) {
 return id ;
 }
 public void setId (  long id ) {
 this.id = id ;
 }
 public List < ReportOutOption > getReportOutOption (  ) {
 return reportOutOption ;
 }
 public void setReportOutOption (  List < ReportOutOption > reportOutOption ) {
 ((D3EPersistanceList < ReportOutOption >)this.reportOutOption).setAll(reportOutOption) ;
 }
 public void addToReportOutOption (  ReportOutOption val, long index ) {
 if ( index == -1 ) {
 this.reportOutOption.add(val) ;
 }
 else {
 this.reportOutOption.add(((int)index),val) ;
 }
 }
 public void removeFromReportOutOption (  ReportOutOption val ) {
 this.reportOutOption.remove(val) ;
 }
 public List < ReportOutColumn > getReportOutColumn (  ) {
 return reportOutColumn ;
 }
 public void setReportOutColumn (  List < ReportOutColumn > reportOutColumn ) {
 ((D3EPersistanceList < ReportOutColumn >)this.reportOutColumn).setAll(reportOutColumn) ;
 }
 public void addToReportOutColumn (  ReportOutColumn val, long index ) {
 if ( index == -1 ) {
 this.reportOutColumn.add(val) ;
 }
 else {
 this.reportOutColumn.add(((int)index),val) ;
 }
 }
 public void removeFromReportOutColumn (  ReportOutColumn val ) {
 this.reportOutColumn.remove(val) ;
 }
 public List < ReportOutColumn > getSubColumns (  ) {
 return subColumns ;
 }
 public void setSubColumns (  List < ReportOutColumn > subColumns ) {
 ((D3EPersistanceList < ReportOutColumn >)this.subColumns).setAll(subColumns) ;
 }
 public void addToSubColumns (  ReportOutColumn val, long index ) {
 if ( index == -1 ) {
 this.subColumns.add(val) ;
 }
 else {
 this.subColumns.add(((int)index),val) ;
 }
 }
 public void removeFromSubColumns (  ReportOutColumn val ) {
 this.subColumns.remove(val) ;
 }
 public List < ReportOutAttribute > getReportOutAttribute (  ) {
 return reportOutAttribute ;
 }
 public void setReportOutAttribute (  List < ReportOutAttribute > reportOutAttribute ) {
 ((D3EPersistanceList < ReportOutAttribute >)this.reportOutAttribute).setAll(reportOutAttribute) ;
 }
 public void addToReportOutAttribute (  ReportOutAttribute val, long index ) {
 if ( index == -1 ) {
 this.reportOutAttribute.add(val) ;
 }
 else {
 this.reportOutAttribute.add(((int)index),val) ;
 }
 }
 public void removeFromReportOutAttribute (  ReportOutAttribute val ) {
 this.reportOutAttribute.remove(val) ;
 }
 public List < ReportOutRow > getReportOutRow (  ) {
 return reportOutRow ;
 }
 public void setReportOutRow (  List < ReportOutRow > reportOutRow ) {
 ((D3EPersistanceList < ReportOutRow >)this.reportOutRow).setAll(reportOutRow) ;
 }
 public void addToReportOutRow (  ReportOutRow val, long index ) {
 if ( index == -1 ) {
 this.reportOutRow.add(val) ;
 }
 else {
 this.reportOutRow.add(((int)index),val) ;
 }
 }
 public void removeFromReportOutRow (  ReportOutRow val ) {
 this.reportOutRow.remove(val) ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.ReportOutput ;
 }
 public String _type (  ) {
 return "ReportOutput" ;
 }
 public int _fieldsCount (  ) {
 return 5 ;
 }
 public void _convertToObjectRef (  ) {
 this.reportOutOption.forEach((  a ) => a._convertToObjectRef()) ;
 this.reportOutColumn.forEach((  a ) => a._convertToObjectRef()) ;
 this.subColumns.forEach((  a ) => a._convertToObjectRef()) ;
 this.reportOutAttribute.forEach((  a ) => a._convertToObjectRef()) ;
 this.reportOutRow.forEach((  a ) => a._convertToObjectRef()) ;
 }
 }