//============================================================================
// LinAlgEq.cs  Defines a class for a linear algebraic equations solver
//============================================================================
using System;

public class LinAlgEq
{
    private int n = 3;       // number of algebraic equations (number of unknowns too)
    private double[][] _A;   // coefficient matrix --- [][] is a jagged array
    private double[] _b;     // right hand side
    private double[][] M;    // augmented matrix
    private double[] _x;     // solution

    //--------------------------------------------------------------------
    // Constructor for the class.
    //--------------------------------------------------------------------
    public LinAlgEq(int nn = 3)
    {
        _b = new double[1];   // these three lines get rid of the warning
        _A = new double[1][];
        M  = new double[1][];
        _x = new double[1];

        Resize(nn);
    }

    //--------------------------------------------------------------------
    // resize: resize the matrices to hold the right number of equations
    //         and unknowns.
    //--------------------------------------------------------------------
    public void Resize(int nn)
    {
        // ##### should check if nn is bigger than zero
        n = nn;

        _b = new double[n];
        _A = new double[n][];
        M  = new double[n][];
        _x = new double[n];

        int i,j;
        for(i=0;i<n;++i)
        {
            _A[i] = new double[n];
            M[i] = new double[n+1];
                
            for(j=0;j<n;++j)
            {
                _A[i][j] = 0.0;
            }
            _A[i][i] = 1.0;
            _b[i] = 0.0;
            _x[i] = 0.0;
        }
    }

    //--------------------------------------------------------------------
    // SolveGauss: Solve by Gauss Eliminition
    //--------------------------------------------------------------------
    public void SolveGauss()
    {
        // form augmented matrix
        int i, j; // I don't think k is supposed to be here... 
        for(i=0;i<n;++i)
        {
            for(j=0;j<n;++j)
            {
                M[i][j] = _A[i][j];
            }
            M[i][n] = _b[i];
        }

        Console.WriteLine("Form Augmented Matrix: ");
        PrintMatrix(M);
        GaussElimination(M);
    }

    private void GaussElimination(double[][] M)
    {
        int rowCount = M.Length;
        int colCount = M[0].Length - 1; // Last column is the augmented column

        for (int pivotRow = 0; pivotRow < rowCount - 1; pivotRow++)
        {
        double[] holder;
        double maxElem = Math.Abs(M[pivotRow][pivotRow]);
        int rowIdx = pivotRow;
        int i;

        for(i = pivotRow+1; i<rowCount; ++i)
        {
            // find largest element in jth column
            if(Math.Abs(M[i][pivotRow])>maxElem)
            {
                maxElem = Math.Abs(M[i][pivotRow]);
                rowIdx = i;
            }
        }

        // swap rows
        if(rowIdx != pivotRow)
        {
            holder = M[pivotRow];
            M[pivotRow] = M[rowIdx];
            M[rowIdx] = holder;
            //Console.WriteLine("Swap " + j.ToString());
        }

            // Perform elimination
            for (int currentRow = pivotRow + 1; currentRow < rowCount; currentRow++)
            {
                double factor = M[currentRow][pivotRow] / M[pivotRow][pivotRow];

                for (int currentCol = pivotRow; currentCol <= colCount; currentCol++)
                {
                    M[currentRow][currentCol] -= factor * M[pivotRow][currentCol];
                }
            }
        }

        // Back substitution
        for (int row = rowCount - 1; row >= 0; row--)
        {
            double sum = 0;

            for (int col = row + 1; col < colCount; col++)
            {
                sum += M[row][col] * M[col][colCount];
            }

            M[row][colCount] = (M[row][colCount] - sum) / M[row][row];
        }

        Console.WriteLine("\nMatrix after Gaussian Elimination");
        PrintMatrix(M);
    }


    //--------------------------------------------------------------------
    // PivotRow
    //--------------------------------------------------------------------
    private void PivotRow(int j)
    {
        double[] holder;
        double maxElem = Math.Abs(M[j][j]);
        int rowIdx = j;
        int i;

        for(i = j+1; i<n; ++i)
        {
            // find largest element in jth column
            if(Math.Abs(M[i][j])>maxElem)
            {
                maxElem = Math.Abs(M[i][j]);
                rowIdx = i;
            }
        }

        // swap rows
        if(rowIdx != j)
        {
            holder = M[j];
            M[j] = M[rowIdx];
            M[rowIdx] = holder;
            //Console.WriteLine("Swap " + j.ToString());
        }

    }

    //--------------------------------------------------------------------
    // Check: checks the solution
    //--------------------------------------------------------------------
    public double Check()
    {
        double sum = 0.0;
        double sum2 = 0.0;

        int i,j;
        for(i=0;i<n;++i)
        {
            sum = 0.0;
            for(j=0;j<n;++j)
            {
                sum += _A[i][j] * _x[j];
            }
            double delta = sum - _b[i];
            sum2 += delta*delta; 
        }

        return(Math.Sqrt(sum2/(1.0*n)));
    }

    //--------------------------------------------------------------------
    // getters and setters 
    //--------------------------------------------------------------------
    public double[] b 
    {
        get
        {
            return _b;
        }            
        set
        {
            _b = value;
        }
    }

    public double[][] A
    {
        get
        {
            return _A;
        }
        set
        {
            _A = value;
        }
    }

    public double[] sol
    {
        get
        {
            return _x;
        }
    }


    static void PrintMatrix(double[][] M)
    {
        int rowCount = M.Length;
        int colCount = M[0].Length;

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                Console.Write($"{M[row][col],8:F4} ");
            }
            Console.WriteLine();
        }
    }



}