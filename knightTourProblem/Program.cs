// See https://aka.ms/new-console-template for more information

using System;
using System.Numerics;
using System.Runtime.Serialization;

class Backracking
{
    const int x= 8;//γραμμη σκακιερας
    const int y= 8;//στηλη σκακιερας
    static int[,] chess = new int[x,y];//δισδιάστατος πίνακας που αναπαριστά το τετράγωνο στη γραμμή i και στήλη j της σκακιέρας.

    //Αυτοί οι δύο πίνακες περιέχουν τις 8 δυνατές κινήσεις του ιππου στο σκάκ
    static int[] xs = { 2, 1, -1, -2, -2, -1, 1, 2 };
    static int[] ys = { 1, 2, 2, 1, -1, -2, -2, -1 };


    static void Main()
    {
        for (int i = 0; i<x; i++)
        {
            for(int j = 0; j<y; j++)//γεμίζω  την σκακιέρα 
            {
                chess[i, j] = -1;
            }
        }
        chess[0, 0] = 0;//πρωτη κινηση του ιππου

        if (SolveKnightTour(0, 0, 1))
        {
            PrintChessboard();//Αν η συνάρτηση επιστρέψει true τοτε  βρέθηκε διαδρομή και θα τυπωσω τον πίνακα με τις κινήσεις του ιππόυ.
        }
        else
        {
            Console.WriteLine("Καμία λύση δεν βρέθηκε.");//αν δεν βρω την λυση
        }
    }

    // Αναδρομική συνάρτηση backtracking
    static bool SolveKnightTour(int row, int col, int moves)
    {
        if (moves == x * y)
        {
            return true; //πλήρης διαδρομή
        }

        // Δοκιμάζω ολες τις  8 κινήσεις
        for (int i = 0; i < 8; i++)
        {
            int nextX = row + xs[i];
            int nextY = col + ys[i];

            if (IsValid(nextX, nextY))
            {
                chess[nextX, nextY] = moves;

                if (SolveKnightTour(nextX, nextY, moves + 1))
                    return true;

                //αν δεν οδηγήσει σε λύση, ακυρώνω την κίνηση
                chess[nextX, nextY] = -1;
            }
        }

        return false; // μη έγκυρη συνέχεια
    }

    // Έλεγχω αν η θέση είναι εντός σκακιέρας και αδιάβατη
    static bool IsValid(int row, int col)
    {
        return (row >= 0 && row < x && col >= 0 && col < y && chess[row, col] == -1);
    }

    // Εμφανιση της τελικής σκακιέρας με τη διαδρομή
    static void PrintChessboard()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Console.Write($"{chess[i, j]:D2} ");
            }
            Console.WriteLine();
        }
    }
}


    

