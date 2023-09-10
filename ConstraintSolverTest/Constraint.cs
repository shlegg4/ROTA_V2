namespace ConstraintSolverTest;

class Constraint
{
    public Variable X { get; set; }
    public Variable Y { get; set; }
    public Func<int, int, bool> Test { get; set; }
    private bool Bidirectional { get; set; }
    
    public Constraint(Variable x, Variable y, Func<int, int, bool> test, bool bidirectional = true)
    {
        X = x;
        Y = y;
        Test = test;
        Bidirectional = bidirectional;
    }
    
    
    public bool IsSatisfied(int x, int y)
    {
        return Test(x, y) && (!Bidirectional || Test(y, x));
    }
}