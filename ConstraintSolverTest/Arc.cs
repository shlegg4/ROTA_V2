namespace ConstraintSolverTest;

public class Arc
{
    public readonly Variable X;
    public readonly Variable Y;
    private readonly List<Tuple<Func<int, int, bool>, float>> _constraints = new();
    public string Name;

    public Arc(Variable x, Variable y)
    {
        X = x;
        Y = y;
        Name = $"{x.Name} -> {y.Name}";
    }

    public void AddConstraint(Func<int, int, bool> constraint, float cost = float.MaxValue)
    {
        _constraints.Add(new Tuple<Func<int, int, bool>, float>(constraint, cost));
    }


    public Tuple<bool, float> ArcReduce()
    {
        var removed = false;
        float cost = 0;
        foreach (var x in X.Domain.ToList())
        {
            var minCost = float.MaxValue;
            var satisfied = false;
            foreach (var y in Y.Domain)
            {
                foreach (var constraint in _constraints)
                {
                    satisfied = constraint.Item1(x, y);
                    if (satisfied) break;

                    minCost = Math.Min(minCost, constraint.Item2);
                }

                if (satisfied) break;
            }


            if (satisfied) continue;
            if (minCost < float.MaxValue)
            {
                cost = minCost;
                continue;
            }

            X.remove_value(x);
            removed = true;
        }

        return new Tuple<bool, float>(removed, cost);
    }
}