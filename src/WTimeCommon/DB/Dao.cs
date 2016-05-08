using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LinqToDB;
using LinqToDB.DataProvider;
using WTimeCommon.DB.Model;

namespace WTimeCommon.DB
{
	public class Dao : LinqToDB.Data.DataConnection
	{
		public Dao() : base(GetDataProvider(), GetConnection())
		{
		}

		private static IDataProvider GetDataProvider()
		{
			return new LinqToDB.DataProvider.SQLite.SQLiteDataProvider();
		}

		private static IDbConnection GetConnection()
		{
			return new SQLiteConnection(ConfigurationManager.ConnectionStrings["WTimeLogger"].ConnectionString);
		}

		public ITable<TitleTime> TitleTime
		{
			get { return GetTable<TitleTime>(); }
		}

		//public static List<TitleTime> All()
		//{
		//	using (var db = new Dao())
		//	{
		//		var query = from p in db.TitleTime
		//			where p.ID > 25
		//			orderby p.Title descending
		//			select p;
		//		return query.ToList();
		//	}
		//}

		public static void CreateDatabaseIfNotExists()
		{
			var connection = GetConnection();
			var match = Regex.Match(connection.ConnectionString, "Data Source=(.+?);");
			var dbFile = new FileInfo(match.Groups[1].Value);
			if (!dbFile.Exists || dbFile.Length == 0)
			{
				connection.Open();
				try
				{
					var command = connection.CreateCommand();
					command.CommandText = @"
					CREATE TABLE TitleTime ( 
						id            INTEGER          PRIMARY KEY AUTOINCREMENT
													   NOT NULL
													   UNIQUE,
						startDateTime DATETIME         NOT NULL,
						endDateTime   DATETIME         NOT NULL,
						title         VARCHAR( 1000 )  NOT NULL,
						executable    VARCHAR( 1000 ) 
					);
				";
					command.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					var message = "Could not create database infrastructure: " + ex.Message;
					MessageBox.Show(message, "Error");
					Logger.Log(message);
					throw;
				}
				finally
				{
					connection.Close();
				}
			}
		}
	}
}