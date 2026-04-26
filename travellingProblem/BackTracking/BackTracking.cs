using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Exer1
{
    class BackTracking
    {
        static int minDistance; // Αποθηκεύει τη μικρότερη απόσταση
        static bool[] visited = new bool[10]; // Πίνακας που δείχνει ποιες πόλεις έχουν ήδη επισκεφθεί

        static int[] bestPath = new int[11]; // Η τελική λύση - η καλύτερη διαδρομή που έχει βρεθεί
        static int[] currentPath = new int[11]; // Η τωρινή διαδρομή που εξετάζεται
        // Έχουν μέγεθος 11 καθώς ο πλανόδιος πωλητής πρέπει να επιστρέψει στην αρχική πόλη

        // Χρησιμοποιούμε τη συντάρτηση backTrack αναδρομικά με παραμέτρους:
        // - τον πίνακα αποστάσεων μεταξύ των πόλεων
        // - την πόλη στην οποία βρίσκεται αυτή την στιγμή - δηλαδή το i του 2διαστατου πίνακα distances
        // - Ο αριθμός των πόλεων που έχουν επισκεφθεί μέχρι στιγμής
        // - την συνολική απόσταση που έχει διανυθεί εώς τώρα
        static void backTrack(int[,] distances, int currentCity, int citiesVisited, int currentDistance)
        { 
            // Αν έχουν επισκεφθεί όλες οι πόλεις
            if (citiesVisited == 10)
            {
                //προσθέτουμε την απόσταση από την τελευταία μέχρι την πρώτη πόλη
                int totalDistance = currentDistance + distances[currentCity, 0];

                // Αν η συνολική απόσταση της διαδρομής είναι μικρότερη από κάποια προηγούμενη
                // minDistance που είχε βρεθεί
                if (totalDistance < minDistance)
                {
                    // Ενημερώνουμε την βέλτιστη απόσταση
                    minDistance = totalDistance;

                    // και αντικαθιστούμε τη προηγούμενη βέλτιστη απόσταση με την καινούρια που βρήκαμε
                    Array.Copy(currentPath, bestPath, 10);

                    // Θέτουμε ως τελεύταίο στοίχείο της βέλτιστης διαδρομής την αρχική πόλη 
                    bestPath[10] = 0;
                }

                // Επιστροφή στον προηγούμενο αναδρομικό κόμβο και συνέχιση εκτέλεσης από τη γραμμή 76
                return;
            }

            // Δοκιμάζουμε κάθε πιθανή πόλη (nextCity)
            for (int nextCity = 0; nextCity < 10; nextCity++)
            {
                // που δεν έχει επισκεφθεί(visited[nextCity] = false)
                // και είναι εφικτή αυτή η διαδρομή (distances[currentCity, nextCity] > 0)
                if (!visited[nextCity] && distances[currentCity, nextCity] > 0)
                {
                    // Σημειώνουμε ότι επισκεφθήκαμε την πόλη
                    visited[nextCity] = true;

                    // Προσθέτουμε την πόλη στη τωρινή διαδρομή
                    currentPath[citiesVisited] = nextCity;

                    // Προσθέτουμε την απόσταση στο currentDistance
                    currentDistance += distances[currentCity, nextCity];

                    // Καλούμε τη συνάρτηση backTrack αναδρομικά χρησιμοποιώντας ως ορίσματα
                    // τις ανανεωμένες τιμές 
                    backTrack(distances, nextCity, citiesVisited + 1, currentDistance);

                    // Αναιρούμε την αλλαγη στην επισκεψιμότητα της πόλης (visited[nextCity] = false)
                    visited[nextCity] = false;

                    // καθώς και την αλλαγή στην απόσταση για να δοκιμάσουμε άλλες διαδρομές
                    currentDistance -= distances[currentCity, nextCity];
                }
            }
        }

        // Συνάρτηση που δέχεται τον 2διάστατο πίνακα αποστάσεων
        static void printResults(int[,] distances)
        {
            // Διαχωριστική γραμμή
            Console.WriteLine(new string('-', 65));

            // Εκτύπωση πίνακα αποστάσεων (10x10)
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // Σε μορφή συμβολοσειράς με τουλάχιστον 2 ψηφία
                    Console.Write(distances[i, j].ToString("D2") + " ");
                }
                Console.WriteLine();
            }

            // Εμφανίση βέλτιστης διαδρομής
            Console.Write("Best Path: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(bestPath[i] + " -> ");
            }
            Console.Write(bestPath[10] + "\n");

            // Εμφάνιση μικρότερης απόστασης
            Console.WriteLine("Min Distance: " + minDistance);
        }

        static void Main()
        {
            // 1η Εκτέλεση - Σταθερός πίνακας αποστάσεων
            int[,] distances1 = {
                { 0, 10, 20, 15, 30, 25, 40, 35, 50, 45 },
                { 10, 0, 12, 22, 18, 28, 35, 42, 55, 48 },
                { 20, 12, 0, 14, 26, 20, 30, 45, 53, 41 },
                { 15, 22, 14, 0, 16, 20, 25, 30, 40, 42 },
                { 30, 18, 26, 16, 0, 24, 28, 39, 45, 37 },
                { 25, 28, 20, 20, 24, 0, 27, 35, 50, 30 },
                { 40, 35, 30, 25, 28, 27, 0, 20, 38, 32 },
                { 35, 42, 45, 30, 39, 35, 20, 0, 25, 22 },
                { 50, 55, 53, 40, 45, 50, 38, 25, 0, 15 },
                { 45, 48, 41, 42, 37, 30, 32, 22, 15, 0 }
            };  

            // 2η Εκτέλεση - Δημιουργία συμμετρικού πίνακα αποστάσεων με τυχαίες τιμές 
            // με τιμές που διαφέρουν μεταξύ τους από 10 μέχρι 100 μονάδες
            Random rand = new Random();
            int[,] distances2 = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    int distance = rand.Next(10, 100); // Τυχαία απόσταση μεταξύ 10 και 99
                    distances2[i, j] = distance;

                    // Συμμετρικός πίνκας καθώς το ταξίδι μεταξύ δύο πόλεων
                    // έχει ίδια απόσταση και προς τις δύο κατευθύνσεις
                    distances2[j, i] = distance;
                }
            }

            // Θέτουμε τη μέγιστη δυνατή τιμή ως αρχική τιμή ελάχιστης απόστασης
            // ώστε οποιαδήποτε διαδρομή βρεθεί να έχει μικρότερη απόσταση από αυτή
            minDistance = int.MaxValue;

            // Αρχικοποιούμε τον πίνακα bestPath με -1 για να δείξουμε ότι δεν έχει βρεθεί
            // κάποια διαδρομή ακόμα
            Array.Fill(bestPath, -1);

            // Θέτουμε όλες τις πόλεις ως μη επισκέψιμες 
            Array.Fill(visited, false);

            // Δηλώνουμε ότι η πρώτη πολή (0) έχει ήδη επισκεφθεί (true)
            visited[0] = true;

            // Δηλώνουμε ότι η διαδρομή ξεκινά από την πόλη 0 
            currentPath[0] = 0;

            // Καλούμε τη συνάρτηση backTrack με ορίσματα:
            // - distances1:τον πίνακα αποστάσεων 
            // - 0: Ξεκινάμε από την πρώτη πόλη
            // - 1: Έχουμε ήδη επισκεφθεί μία πόλη (την αρχική)
            // - 0: Η απόσταση που έχει διανυθεί εώς τώραι είναι 0
            backTrack(distances1, 0, 1, 0);

            // Καλούμε τη συνάρτηση printResults για να εκτυπώσουμε
            // τα ζητούμενα αποτελέσματα - bestPath και minDistance
            printResults(distances1);

            // Παρόμοιως για την 2η δοκιμή (τυχαίο πίνακα)
            // Αρχικοποιούμε τα minDistance, bestPath και το visited στις αρχικές τιμές
            minDistance = int.MaxValue;
            Array.Fill(bestPath, -1);
            Array.Fill(visited, false);
            visited[0] = true;
            currentPath[0] = 0;

            backTrack(distances2, 0, 1, 0);
            printResults(distances2);

            // Διαχωριστική γραμμή
            Console.WriteLine(new string('-', 65));
        }
    }
}
