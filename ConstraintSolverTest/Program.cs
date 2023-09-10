// See https://aka.ms/new-console-template for more information

using ConstraintSolverTest;

var csp = new CSP();

//
// // Initialize variables
// var grid = new Variable[9];
// grid[0] = new Variable($"X0");
// grid[0].InitializeDomain(new List<int> { 1 });
// csp.AddVariable(grid[0]);
// for (var i = 1; i < 9; i++)
// {
//     grid[i] = new Variable($"X{i}");
//     grid[i].InitializeDomain(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
//     csp.AddVariable(grid[i]);
// }
//
//
// // Initialize arcs
// int[][] neighbours = new[]
// {
//     new[] { -1, 0 }, // Top
//     new[] { 0, -1 }, // Left
//     new[] { 1, 0 }, // Bottom
//     new[] { 0, 1 } // Right
// };
//
// for (var i = 0; i < 9; i++)
// {
//     var x = i % 3;
//     var y = i / 3;
//     for (var j = 0; j < 2; j++)
//     {
//         // Backward Constraint
//         var nx = x + neighbours[j][0];
//         var ny = y + neighbours[j][1];
//         if (!(nx < 0 || nx > 2 || ny < 0 || ny > 2))
//         {
//             var arc = new Arc(grid[i], grid[nx + ny * 3]);
//             arc.AddConstraint((a, b) => a > b);
//             csp.AddArc(arc);
//         }
//
//         // Forward Constraint
//         nx = x + neighbours[j + 2][0];
//         ny = y + neighbours[j + 2][1];
//         if (!(nx < 0 || nx > 2 || ny < 0 || ny > 2))
//         {
//             var arc = new Arc(grid[i], grid[nx + ny * 3]);
//             arc.AddConstraint((a, b) => a < b);
//             csp.AddArc(arc);
//         }
//
//     }
// }


var grid = new Variable[10];
grid[0] = new Variable($"M_F");
grid[0].InitializeDomain(new List<int> { 0, 2, 4, 5, 6 });

grid[1] = new Variable($"T_F");
grid[1].InitializeDomain(new List<int> { 2, 3, 4, 5, 6 });

grid[2] = new Variable($"W_F");
grid[2].InitializeDomain(new List<int> { 1, 2, 3, 4 });

grid[3] = new Variable($"Th_F");
grid[3].InitializeDomain(new List<int> { 0, 2, 3, 5 });

grid[4] = new Variable($"F_F");
grid[4].InitializeDomain(new List<int> { 1, 3, 4, 5 });

grid[5] = new Variable($"M_H");
grid[5].InitializeDomain(new List<int> { 0, 2, 4, 5, 6 });

grid[6] = new Variable($"T_H");
grid[6].InitializeDomain(new List<int> { 2, 3, 4, 5, 6 });

grid[7] = new Variable($"W_H");
grid[7].InitializeDomain(new List<int> { 1, 2, 3, 4 });

grid[8] = new Variable($"Th_H");
grid[8].InitializeDomain(new List<int> { 0, 2, 3, 5 });

grid[9] = new Variable($"F_H");
grid[9].InitializeDomain(new List<int> { 1, 3, 4, 5 });


for (var i = 0; i < 10; i++)
{
    csp.AddVariable(grid[i]);
}

for(int i=0; i<5; i++)
{
    for(int j=0; j<5; j++)
    {
        if(i == j) continue;
        var arc = new Arc(grid[i], grid[j]);
        var arc2 = new Arc(grid[i+5], grid[j+5]);
        arc2.AddConstraint((a, b) => a != b);
        arc.AddConstraint((a, b) => a != b);
        csp.AddArc(arc2);
        csp.AddArc(arc);
    }
    var arc3 = new Arc(grid[i], grid[i+5]);
    var arc4 = new Arc(grid[i+5], grid[i]);
    arc3.AddConstraint((a, b) => a != b);
    arc4.AddConstraint((a, b) => a != b);
    csp.AddArc(arc3);
    csp.AddArc(arc4);
}


csp.Backtrack();


Console.WriteLine("Fini!");