namespace Models;

public class PlanOccurence
{
    public DateTime Date { get; set; }
    public string Course { get; set; }
    public TimeSpan StartTime { get; set; }
    public string Lecturer { get; set; }
}
