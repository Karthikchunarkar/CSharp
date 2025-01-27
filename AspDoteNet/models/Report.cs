namespace models ;
 using ReportBaseConfig = models.ReportBaseConfig; using ReportCell = models.ReportCell; using ReportFilter = models.ReportFilter; using ReportRuleSet = models.ReportRuleSet; using string = System.string;  public class Report { public string Model { get; set; } 
 public string Name { get; set; } 
 public ReportCell Cells { get; set; } 
 public ReportBaseConfig Config { get; set; } 
 public List<ReportFilter> Filters { get; set; } 
 public ReportRuleSet Criteria { get; set; } 
 public Report (  string model, string name, ReportCell cells, ReportBaseConfig config, List<ReportFilter> filters, ReportRuleSet criteria ) {
  Model=model;
  Name=name;
  Cells=cells;
  Config=config;
  Filters=filters;
  Criteria=criteria;
 }
 }