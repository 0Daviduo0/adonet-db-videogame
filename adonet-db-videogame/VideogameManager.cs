using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    internal class VideogameManager
    {
        public void Inserisci_Videogioco()
        {
            // definisci la stringa di connessione al database
            string connectionString = "Data Source=localhost;Initial Catalog=videogiochi;Integrated Security=True";

            // definisci la query SQL per l'inserimento del nuovo videogioco
            string insertQuery = "INSERT INTO videogames (name, overview, release_date, software_house_id) VALUES (@Name, @Overview, @ReleaseDate, @SoftwareHouseId)";

            // chiedi all'utente gli input per ogni specifica del gioco
            Console.Write("Inserisci il nome del gioco: ");
            string name = Console.ReadLine();

            Console.Write("Inserisci la descrizione del gioco: ");
            string overview = Console.ReadLine();

            Console.Write("Inserisci la data di rilascio del gioco [yyyy-mm-dd]: ");
            DateTime releaseDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Le scelte possibili per la Software House sono: \n" +
                "[1] Nintendo \n" +
                "[2] Rockstar Games \n" +
                "[3] Valve Corporation \n" +
                "[4] Electronic Arts \n" +
                "[5] Ubisoft \n" +
                "[6] Konami \n");
            Console.Write("Inserisci l'ID della Software House: ");
            int softwareHouseId = int.Parse(Console.ReadLine());

            // crea una nuova connessione al database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // apri la connessione al database
                connection.Open();

                // crea un nuovo comando SQL per l'inserimento del nuovo videogioco
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // definisci i parametri della query SQL
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Overview", overview);
                    command.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                    command.Parameters.AddWithValue("@SoftwareHouseId", softwareHouseId);

                    // esegui la query SQL
                    int rowsAffected = command.ExecuteNonQuery();

                    // stampa il numero di righe modificate dalla query SQL
                    Console.WriteLine("Numero righe modificate:  " + rowsAffected);
                }
            }
        }

        public void GameID_Search()
        {
            // definisci la stringa di connessione al database
            string connectionString = "Data Source=localhost;Initial Catalog=videogiochi;Integrated Security=True";

            // Crea una connessione al database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Apri la connessione al database
                connection.Open();

                // Chiedi all'utente di inserire l'id del videogioco da cercare
                Console.WriteLine("Inserisci l'ID del videogioco da cercare:");
                string gameIdString = Console.ReadLine();

                // Converti l'input dell'utente in un numero intero
                if (int.TryParse(gameIdString, out int gameId))
                {
                    // Crea una query SQL per cercare il videogioco per id
                    string query = "SELECT * FROM videogames WHERE id = @id";

                    // Crea un comando SQL con la query e i parametri
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", gameId);

                        // Esegui il comando SQL e leggi i risultati
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Stampa a schermo i giochi che corrispondono alla ricerca
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader.GetInt64(0)}, Nome: {reader.GetString(1)}, Data di rilascio: {reader.GetDateTime(3)}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Input non valido. Inserire un numero intero.");
                }
            }
        }

        public void GameName_Search()
        {
            // definisci la stringa di connessione al database
            string connectionString = "Data Source=localhost;Initial Catalog=videogiochi;Integrated Security=True";

            // Crea una connessione al database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Apri la connessione al database
                connection.Open();

                // Chiedi all'utente di inserire la stringa di ricerca del nome del videogioco
                Console.WriteLine("Inserisci il nome del videogioco da cercare:");
                string gameNameSearchString = Console.ReadLine();

                // Crea una query SQL per cercare i videogiochi con il nome che contiene la stringa di ricerca
                string query = "SELECT * FROM videogames WHERE name LIKE '%' + @name + '%'";

                // Crea un comando SQL con la query e i parametri
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", gameNameSearchString);

                    // Esegui il comando SQL e leggi i risultati
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Stampa a schermo i giochi che corrispondono alla ricerca
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader.GetInt64(0)}, Nome: {reader.GetString(1)}, Data di rilascio: {reader.GetDateTime(3)}");
                        }
                    }
                }
            }
        }

        public void GameID_Delete()
        {
            // Chiedi all'utente l'id del videogioco da eliminare
            Console.WriteLine("Inserisci l'ID del videogioco da eliminare:");
            string gameIdString = Console.ReadLine();

            // Converti l'input dell'utente in un numero intero
            if (int.TryParse(gameIdString, out int gameId))
            {
                // definisci la stringa di connessione al database
                string connectionString = "Data Source=localhost;Initial Catalog=videogiochi;Integrated Security=True";

                // Crea una connessione al database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Apri la connessione al database
                    connection.Open();

                    // Crea una query SQL per eliminare il videogioco per id
                    string query = "DELETE FROM videogames WHERE id = @id";

                    // Crea un comando SQL con la query e i parametri
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", gameId);

                        // Esegui il comando SQL
                        int rowsAffected = command.ExecuteNonQuery();

                        // Stampa a schermo il messaggio di riuscita eliminazione
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Il videogioco con ID {gameId} è stato eliminato con successo.");
                        }
                        else
                        {
                            Console.WriteLine($"Nessun videogioco con ID {gameId} trovato.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Input non valido. Inserire un numero / numero intero.");
            }
        }
    }
}
