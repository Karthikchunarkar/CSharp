namespace models ;
 using ReportDataRow = models.ReportDataRow; using ReportDataSection = models.ReportDataSection;  public class ReportData { public List<ReportDataSection> Sections { get; set; } 
 public List<ReportDataRow> Rows { get; set; } 
 public ReportData (  List<ReportDataSection> sections, List<ReportDataRow> rows ) {
  Sections=sections;
  Rows=rows;
 }
 }