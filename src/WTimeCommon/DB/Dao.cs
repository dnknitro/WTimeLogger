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
			//Logger.Log("Db File location: {0}; Working Folder: {1}", GetDbFileInfo().FullName, Directory.GetCurrentDirectory());
		}

		public static string CustomConnectionString { get; set; }

		private static IDataProvider GetDataProvider()
		{
			return new LinqToDB.DataProvider.SQLite.SQLiteDataProvider();
		}

		public static string GetConnectionString()
		{
			return CustomConnectionString ?? ConfigurationManager.ConnectionStrings["WTimeLogger"].ConnectionString;
		}

		private static IDbConnection GetConnection()
		{
			return new SQLiteConnection(GetConnectionString());
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

		public static FileInfo GetDbFileInfo()
		{
			var match = Regex.Match(GetConnectionString(), "Data Source=(.+?);");
			return new FileInfo(match.Groups[1].Value);
		}

		public static void CreateDatabaseIfNotExists()
		{
			var dbFile = GetDbFileInfo();
			if (!dbFile.Exists || dbFile.Length == 0)
			{
				using (var connection = GetConnection())
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
}