using System;
using System.Collections.Generic;
using BlueScreenCategoriser;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace BlueScreenIO.Storage {
	public static class DatabaseStorage {
		public const string LibraryDefaultName = "library.sqlite";

		// ReSharper disable once InconsistentNaming
		public const int SQLiteVersionNum = 3;

		private static bool ExecuteQuery(string query, SQLiteConnection connection) {
			try {
				var c = new SQLiteCommand(query, connection);
				c.ExecuteNonQuery();
			}
			catch (SQLiteException e) {
				Debug.Print(e.Message);
				return false;
			}

			return true;
		}


		public static SQLiteConnection GetConnection() {
			var dbConnection = new SQLiteConnection($"Data Source={LibraryDefaultName};Version={SQLiteVersionNum.ToString()};");
			dbConnection.Open();

			return dbConnection;
		}

		public static bool AddFileToDatabase(EpisodeInfo episodeInfo) {
			var path = episodeInfo.path;
			var name = episodeInfo.Name;
			var episode = episodeInfo.Episode.ToString();
			var season = episodeInfo.Season.ToString();

			//build insert query here... insert into tablename path,name,episode,season,0watches,0lastplayposition
			var query = $"insert into  shows (path, showname,season,episode,lastwatch,playcount) VALUES ('{path}', '{name}', {season}, {episode}, null,0 )";
			var db = GetConnection();

			return ExecuteQuery(query, db);
		}


		public static bool UpdateFileInDatabase(int uid, string key, string value) {
			var query = "update shows set `{1}`={2} where id={0}";
			query = string.Format(query, uid.ToString(), key, value);

			var db = GetConnection();

			return ExecuteQuery(query, db);
		}

		public static bool RemoveFileFromDatabase(int uid) {
			var query = "delete from {0} where id={1}";
			query = string.Format(query, nameof(DefaultSchema.TableName.Shows), uid.ToString());

			var db = GetConnection();

			return ExecuteQuery(query, db);
		}

		public static List<string> GetAllShowNames() {
			const string query = "select distinct showname from shows";
			var results = new List<string> { };
			var connection = GetConnection();
			try {
				var c = new SQLiteCommand(query, connection);
				var r = c.ExecuteReader();

				while (r.Read()) {

					var n = r.GetString(0);

					results.Add(n);
				}
			}
			catch (Exception e) {
				Debug.Print(e.Message);
			}


			return results;
		}

		public static List<int> GetSeasonsForShow(string showname) {
			var query = $"select distinct season from shows where showname='{showname}'";
			var results = new List<int>();
			var connection = GetConnection();
			try {
				var c = new SQLiteCommand(query, connection);
				var r = c.ExecuteReader();

				while (r.Read()) {

					var n = r.GetInt32(0);

					results.Add(n);
				}
			}
			catch (Exception e) {
				Debug.Print(e.Message);
			}


			return results;
		}
		
		public static Dictionary<int,string> GetEpisodesForSeason(string showname, int season) {
			var query = $"select distinct episode,path from shows where showname='{showname}' and season={season.ToString()}";
			var results = new Dictionary<int,string>();
			var connection = GetConnection();
			try {
				var c = new SQLiteCommand(query, connection);
				var r = c.ExecuteReader();

				while (r.Read()) {

					var episodeNumer = r.GetInt32(0);
					var path = r.GetString(1);

					results.Add(episodeNumer,path);
				}
			}
			catch (Exception e) {
				Debug.Print(e.Message);
			}

			return results;
		}



		public static bool InitDatabase(string library = LibraryDefaultName) {
			if (!File.Exists(library)) {
				SQLiteConnection.CreateFile(library);

				using (var dbConnection = GetConnection()) {
					return DefaultSchema.DefaultSchemas.All(schema => ExecuteQuery(schema, dbConnection));
				}
			}

			//should return true if there is a functioning database - false if there was an error in importing
			return true;
		}
	}
}