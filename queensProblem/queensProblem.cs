using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thema1
{ 
    class queensProblem
    {
        // Για την σκακιέρα 8x8
        const int N = 8; 

        // Μετρητής του αριθμού λύσεων που βρέθηκαν
        static int solutions = 0;

        public static void Main()
        {
            // Αρχικοποίηση σκακιέρας NxN
            int[,] board = new int[N, N];

            // Εκκίνηση αλγορίθμου από την πρώτη γραμμή (row = 0)
            solveProblem(board, 0);

            // Εκτύπωση συνολικού αριθμού λύσεων - στο τέλος της κονσόλας
            Console.WriteLine("Number of solutions found: " + solutions);
        }

        // Αναδρομική συνάρτηση επίλυσης του προβλήματος με backtracking
        static void solveProblem(int[,] board, int row)
        {
            // Αν έχουμε φτάσει στην Ν-οστή (8η) γραμμή, τότε έχουμε τοποθετήσει
            // N(8) βασίλισσες, άρα έχουμε καταλήξει σε μία λύση
            if(row == N)
            {
                // Αύξηση μετρητή λύσεων
                solutions++;

                // Εκτύπωση της λύσης
                // Q: Αν στο σημείο αυτό έχει τοποθετηθεί βασίλισσα
                // .: Αν στο σημείο αυτό δεν έχει τοποθετηθεί βασίλισσα
                Console.WriteLine("Solution number " + solutions + ":");
                for(int i = 0; i < N; i++)
                {
                    for(int j = 0; j < N; j++)
                    {
                        Console.Write(board[i, j] == 1 ? "Q " : ". ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                // Επιστροφή για να συνεχίσει με άλλες πιθανές λύσεις
                return;
            }

            // Δοκιμή τοποθέτησης βασίλισσας σε κάθε στήλη της τρέχουσας γραμμής
            for(int col = 0;  col < N; col++)
            {
                // Έλεγχος αν η θέση (row, col) είναι έγκυρη
                // χρησιμοποιώντας την συνάρτηση isValid
                bool queenPlaced = isValid(board, row, col);
                if (queenPlaced)
                {
                    // Τοποθέτηση βασίλισσας (σημείο = 1)
                    board[row, col] = 1;

                    // Αναδρομική κλήση για την επόμενη γραμμή
                    solveProblem(board, row + 1);

                    // Backtrack - αφαίρεση βασίλισσας
                    board[row, col] = 0;
                }
            }
        }

        // Συνάρτηση έλέγχου εγκυρότητας για τη θέση (row, col)
        static bool isValid(int[,] board, int row, int col)
        {
            // Έλεγχος για άλλη βασίλισσα στην ίδια στήλη
            for (int i = 0; i < row; i++)
                if (board[i, col] == 1) 
                    return false;

            // Έλεγχος κύριας διαγωνίου (πάνω αριστερά)
            // Ξεκινάμε από τη θέση (row - 1, col - 1) και 
            // ανεβαίνουμε προς πάνω αριστερά
            int i1 = row - 1;
            int j1 = col - 1;
            while(i1 >= 0 && j1 >= 0)
            {
                // Αν βρούμε ήδη τοποθετημένη βασίλισσα σε αυτή τη 
                // διαγώνιο, δεν είναι έγκυρη η θέση
                if (board[i1, j1] == 1)
                    return false;

                // Μετακινούμαστε μία γραμμή πάνω
                i1--; 

                // Και μία στήλη αριστερά
                j1--;
            }

            // Έλεγχος αντίθετης διαγωνίου (πάνω δεξιά)
            // Ξεκινάμε από τη θέση (row - 1, col + 1) και
            // ανεβαίνουμε πάνω και δεξιά
            int i2 = row - 1;
            int j2 = col + 1;
            while(i2 >= 0 && j2 < N)
            {
                // Αν βρούμε ήδη τοποθετημένη βασίλισσα σε αυτή τη
                // διαγώνιο, δεν είναι έγκυρη η θέση
                if (board[i2, j2] == 1)
                    return false;

                // Μετακινούμαστε μία γραμμή πάνω
                i2--; 

                // Και μία στήλη δεξιά
                j2++;
            }

            // Αν δεν βρέθηκε σύγκρουση, η θέση είναι έγκυρη
            return true;
        }
    }
}
