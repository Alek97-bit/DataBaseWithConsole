using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Console.App
{
	class Program
	{
		private static string connectionString = ConfigurationManager.ConnectionStrings["nrthwnd"].ConnectionString;
		private static SqlConnection sqlConnection = null;
		static void Main(string []args)
		{
			sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();

			SqlDataReader sqlDataReader = null;
			string command = string.Empty;

			while (true)
			{
				System.Console.Write("> ");
				command = System.Console.ReadLine();

				if (command.ToLower().Equals("exit"))
				{
					if (sqlConnection.State == ConnectionState.Open)
					{
						sqlConnection.Close();
					}

					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
					}

					break;
				}

				SqlCommand sqlCommand = null;
				string[]commandArray = command.ToLower().Split(' ');

				switch (commandArray[0])
				{
					case "select":
						 sqlCommand = new SqlCommand(command, sqlConnection);

						sqlDataReader = sqlCommand.ExecuteReader();

						while (sqlDataReader.Read())
						{
							System.Console.WriteLine($"{sqlDataReader["CustomerID"]} {sqlDataReader["CompanyName"]}+" +
							 $"{sqlDataReader["ContactName"]} {sqlDataReader["Address"]}" +
							$"{sqlDataReader["City"]} {sqlDataReader["PostalCode"]} " +
							$"{sqlDataReader["Phone"]}");

							System.Console.WriteLine(new string('-', 30));
						}


						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}

						break;

					case "insert":
						sqlCommand = new SqlCommand(command, sqlConnection);

						System.Console.WriteLine($"Dobawleno: {sqlCommand.ExecuteNonQuery()} stroka");
						break;
					case "update":
						sqlCommand = new SqlCommand(command, sqlConnection);

						System.Console.WriteLine($"Zmieniono: {sqlCommand.ExecuteNonQuery()} stroka");
						break;
					case "delete":
						sqlCommand = new SqlCommand(command, sqlConnection);

						System.Console.WriteLine($"Udaleno: {sqlCommand.ExecuteNonQuery()} stroka");
						break;						
					case "max":
						sqlCommand = new SqlCommand(command, sqlConnection);

						System.Console.WriteLine($"Max: {sqlCommand.ExecuteScalar()} stroka");
						break;
					case "min":
						sqlCommand = new SqlCommand(command, sqlConnection);

						System.Console.WriteLine($"Min: {sqlCommand.ExecuteScalar()} stroka");
						break;
					case "sortby":
						sqlCommand = new SqlCommand("SELECT * FROM [Customers] ORDER BY {commandArray[1]} {commandArray[2]} {commandArray[3]} {commandArray[4]}", sqlConnection);
						sqlDataReader = sqlCommand.ExecuteReader();

						while (sqlDataReader.Read())
						{
							System.Console.WriteLine($"{sqlDataReader["CustomerID"]} {sqlDataReader["CompanyName"]}+" +
							 $"{sqlDataReader["ContactName"]} {sqlDataReader["Address"]}" +
							$"{sqlDataReader["City"]} {sqlDataReader["PostalCode"]} " +
							$"{sqlDataReader["Phone"]}");

							System.Console.WriteLine(new string('-', 30));
						}


						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						break;

					default:
						System.Console.WriteLine($"Komanda{command} niekorektna");
						break;
				}
			}
			System.Console.ReadLine();
		
		}
	}
}
