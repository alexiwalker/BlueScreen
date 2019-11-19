using System.Collections.Generic;
using BlueScreenCategoriser;


namespace BlueScreenIO.Storage {
	public static class DatabaseStorage {

		private const string tablename = "shows";

		private static bool exec(string q) {

			return false;
		}

		public static bool AddFileToDatabase(EpisodeInfo episodeInfo) {
			var path = episodeInfo.path;
			var name = episodeInfo.Name;
			var episode = episodeInfo.Episode;
			var season = episodeInfo.Season;

			//build insert query here... insert into tablename path,name,episode,season,0watches,0lastplayposition
			
//			var query = $"insert into {tablename} values ()";

			//m_bool = databaseinterface.execute(query)
			//if(m_bool) return true else
			return false;
		}


		public static bool UpdateFileInDatabase(int uid, string key, string value) {

			var query = "update shows set `{1}`={2} where id={0}";
			query = string.Format(query,uid.ToString(), key, value);
			
			return exec(query);
		}

		public static bool RemoveFileFromDatabase(int uid) {
			var query = "delete from {0} where id={1}";
			query = string.Format(query, tablename, uid.ToString());

			return exec(query);
		}

		private const string SchemaEpisodes = "create table if not exists `shows` (id int, path varchar(255), show varchar(255), season int, episode int, watchcount int, duration bigint);";
		private const string SchemaRenames = "create table if not exists `renames` (fromShow varchar(255), toShow varchar(255));";
		
		
		private static readonly List<string> _schemas = new List<string> {
			SchemaEpisodes,
			SchemaRenames,
		};

		public static bool InitDatabase() {
			foreach (var schema in _schemas) {
				var b = exec(schema);
				if (!b) return false;
			}

			return true;
		}
	}
}