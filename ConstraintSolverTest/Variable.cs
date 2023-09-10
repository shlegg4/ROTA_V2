namespace ConstraintSolverTest;

public class Variable
{
    public string Name { get; set; }
    public List<int> Domain = new();
    
    public Variable(string name)
    {
        Name = name;
    }
    
    public void InitializeDomain(List<int> initialDomain)
    {
        Domain = new List<int>(initialDomain);
    }
    
    public bool EmptyDomain()
    {
        return Domain.Count == 0;
    }
    
    public void remove_value(int value)
    {
        Domain.Remove(value);
    }
    
    public void Collapse(int value)
    {
        Domain = new List<int> {value};
    }
}
