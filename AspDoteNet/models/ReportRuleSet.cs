namespace models ;
 using ReportRule = models.ReportRule; using bool = System.bool;  public class ReportRuleSet { public bool All { get; set; } 
 public List<ReportRule> Rules { get; set; } 
 public ReportRuleSet (  bool all, List<ReportRule> rules ) {
  All=all;
  Rules=rules;
 }
 }