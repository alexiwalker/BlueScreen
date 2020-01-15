using System.Collections.Generic;

namespace BlueScreenIO.Storage {
	public static class DefaultSchema {
		
		public enum TableName {
			Shows,WatchHistory,AutoRename,Magnets
		}
		
		private const string TableSchemaShows = @" CREATE TABLE IF NOT EXISTS `Shows` (
		`path` text NOT NULL,
		`showname` text NOT NULL,
		`season` int(2) NOT NULL,
		`episode` int(2) NOT NULL,
		`lastwatch` timestamp default NULL,
		`playcount` int(3) default 0 NOT NULL, 
		`fileduration` bigint default 0 NOT NULL,
		`lastprogress`	bigint DEFAULT 0,
		CONSTRAINT episodedetails PRIMARY KEY (showname,season,episode)) ";

		private const string TableSchemaWatchHistory = @" CREATE TABLE IF NOT EXISTS `WatchHistory` (
		`showname` text NOT NULL,
		`season` int(2) NOT NULL,
		`episode` int(2) NOT NULL,
		`when` timestamp NOT NULL,
		`duration` int default 0,
		`progress` int default 0 )";

		private const string TableSchemaRename = @"CREATE TABLE IF NOT EXISTS `AutoRename` (
		`from` text not null, 
		`to` text not null)";

		private const string TableSchemaMagnets = @"CREATE TABLE IF NOT EXISTS `Magnets` (
		`show` text not null,
		`season` int not null,
		`episode` int not null,
		`magnet` text not null
		)";

		public static readonly List<string> DefaultSchemas = new List<string> {
			TableSchemaShows,
			TableSchemaWatchHistory,
			TableSchemaRename,
			TableSchemaMagnets
		};
	}
}