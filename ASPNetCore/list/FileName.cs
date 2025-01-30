namespace classes;
public class ReportRuleOperator
{
    public static ReportRuleOperator Equal = new ReportRuleOperator("");
    private string propType;
    private ReportRuleOperator(String propType)
    {
        this.propType = propType;
    }
    public String PropType()
    {
        return this.propType;
    }
}
