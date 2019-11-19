using System;
using System.IO;
using BlueScreenCategoriser;
using Utils;

namespace BlueScreenIO.IOInterface {
	public class FileSystemInterface : StorageInterface {
		private string _path;
		
		public FileSystemInterface(string path) {
			_path = path;
		}
		
		public m_Bool Move(string newPath) {
			try {
				new FileInfo(newPath).Directory?.Create();
				File.Move(_path, newPath);
			}
			catch (Exception e) {
				return new m_Bool(false,e.Message);
			}

			return new m_Bool(true);
		}

		public m_Bool Remove() {
			try {
				File.Delete(_path);
			}
			catch (Exception e) {
				return new m_Bool(false,e.Message);
			}
			return new m_Bool(true);
		}

		public m_Bool ChangeDetails(EpisodeInfo e) {
			//todo
			throw new NotImplementedException();
		}

		public m_Bool ChangeTarget(string path) {
			if (!File.Exists(path))
				return new m_Bool(false,"Could not set path; does not exist");

			_path = path;
			return new m_Bool(true);
		}
	}
}