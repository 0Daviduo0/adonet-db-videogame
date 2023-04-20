using System.Data.SqlClient;
namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideogameManager videogameManager = new VideogameManager();
            bool fermaEsecuzione = false;

            while (!fermaEsecuzione)
            {
                Console.WriteLine("----VideogameManager.exe----");
                Console.WriteLine("1. Inserire un nuovo videogioco");
                Console.WriteLine("2. Ricercare un videogioco per id");
                Console.WriteLine("3. Ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input");
                Console.WriteLine("4. Cancellare un videogioco");
                Console.WriteLine("5. Chiudere il programma");

                Console.Write("Scelta: ");
                string choiceString = Console.ReadLine();
                if (int.TryParse(choiceString, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            videogameManager.Inserisci_Videogioco();
                            break;
                        case 2:
                            videogameManager.GameID_Search();
                            break;
                        case 3:
                            videogameManager.GameName_Search();
                            break;
                        case 4:
                            videogameManager.GameID_Delete();
                            break;
                        case 5:
                            fermaEsecuzione = true;
                            break;
                        default:
                            Console.WriteLine("Scelta non valida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Scelta non valida.");
                }

                Console.WriteLine();
            }
        }
    }
}