namespace ConstraintSolverTest;

public class CSP
{
    public List<Variable> Variables = new();
    public DualKeyMap<string, string, Arc> Arcs = new();
    public float Cost = 0;


    public void AddVariable(Variable variable)
    {
        Variables.Add(variable);
    }

    public void AddArc(Arc arc)
    {
        Arcs.Add(arc.X.Name, arc.Y.Name, arc);
    }

    public bool ArcConsistent()
    {
        var worklist = new Queue<Arc>(Arcs.GetValues());

        while (worklist.Count > 0)
        {
            var arc = worklist.Dequeue();
            var (removed, cost) = arc.ArcReduce();
            Cost += cost;
            if (!removed) continue;
            if (arc.X.EmptyDomain())
            {
                return false;
            }
            else
            {
                foreach (var neighbour in Arcs.GetVariations(arc.X.Name))
                {
                    if (neighbour == arc) continue;
                    worklist.Enqueue(Arcs.GetSingleResult(neighbour.Y.Name, neighbour.X.Name));
                }
            }
        }

        return true;
    }

    public bool Backtrack()
    {
        // Pick a variable to assign - 1
        // Pick a value to assign to the variable - 2
        // Check if the assignment is consistent - 3
        // If it is, recurse - 4
        // If it isn't, backtrack - 5
        var variableDomains = Variables.Select(x => x.Domain.ToList()).ToList();
        var variable = Variables.FirstOrDefault(x => x.Domain.Count > 1);
        if (variable == null) return true;
        
        var domain = variable.Domain.ToList();

        foreach (var value in domain)
        {
            variable.Collapse(value);
            if (ArcConsistent())
            {
                if(Backtrack()) return true;
                
            }
            else
            {
                Variables.ForEach(x => x.Domain = variableDomains[Variables.IndexOf(x)]);
            }
        }
        return false;
    }
}