// See https://aka.ms/new-console-template for more information
using System;


class KnapsackBacktracking
{
    static int[] V = { 8, 12, 9, 10 };   // Αξίες 4 αντικειμένων(A1,A2,A3,A4)
    static int[] W = { 2, 6, 3, 2 };    // Βάρη  4 αντικειμένων
    static int n = 4;                         // Αριθμός 4 αντικειμένων
    static int C = 12;                // Μέγιστη χωρητικότητα σακιδιου

    static int maxValue = 0;      //οριζω μέγιστη αξία που βρήκα-προς το παρον 0
    static int[] bestSolution = new int[n]; //πίνακας που δείχνει ποια αντικείμενα δίνουν τη maxValue
    static int[] currentSolution = new int[n]; //πίνακας που διχνει ποια αντικείμενα εξετάζω τώρα
    static void Backtracking(int i, int currentW, int currentV)
    {
        if (currentW > C)//Αν ξεπεράσω το βάρος,σταματάω
            return;

        if (i == n)//έχω εξετάσει όλα τα αντικείμενα
        {
            if (currentV > maxValue)//Αν η αξία είναι καλύτερη από την προηγούμενη καλύτερη
            {
                maxValue = currentV;//Αποθηκεύω τη νέα καλύτερη λύση
                Array.Copy(currentSolution, bestSolution, n);
            }
            return;
        }

        // Επιλογή αντικειμένου i
        currentSolution[i] = 1;
        Backtracking(i + 1, currentW + W[i], currentV + V[i]);

        // Μη επιλογή αντικειμένου i
        currentSolution[i] = 0;
        Backtracking(i + 1, currentW, currentV);
    }

    static void Main(string[] args)
    {
        Backtracking(0, 0, 0);//εκιννηση της αναδρομής.
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Μέγιστη Αξία: " + maxValue);//μολις τελειώσει η αναδρομή και έχουν δοκιμαστεί όλοι οι συνδυασμοί τοτε εκτυπωνω τη μέγιστη συνολική αξία  που βρήκα.
        Console.WriteLine("Αντικείμενα που επιλέχθηκαν:");

        int totalW = 0;
        for (int i = 0; i < n; i++)
        {
            if (bestSolution[i] == 1)
            {
                Console.WriteLine($" - A{i + 1} (Βάρος: {W[i]}, Αξία: {V[i]})");
                totalW+= W[i];
               
            }
        }
        Console.WriteLine("Συνολικό Βάρος: " + totalW);
        
    }
}

