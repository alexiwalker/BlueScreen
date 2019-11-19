namespace BlueScreenCategoriser {
	public class EpisodeInfo {
		public string Name { get; private set; }

		public int Season { get; private set; }

		public int Episode { get; private set; }

		public string path { get; private set; }

		private bool _valid;

		public EpisodeInfo(string name, int season, int episode) {
			this.Name = name;
			this.Season = season;
			this.Episode = episode;
			this._valid = true;
			this.path = "";
		}

		public EpisodeInfo(string path, string name, int season, int episode) {
			this.Name = name;
			this.Season = season;
			this.Episode = episode;
			this._valid = true;
			this.path = path;
		}

		public EpisodeInfo() {
			Name = "";
			Season = -1;
			Episode = -1;
			_valid = false;
		}

		public static void poke() { }

		/**
		 * Intended for returning an EpisodeInfo that is known to be bad, eg from a non-valid categoriser input
		 * might cause some problems if you do new EpisodeInfo(true) as the (bool)EpisodeInfo repends on the valid state
		 * which is set here
		 */
		public EpisodeInfo(bool valid) {
			Name = "";
			Season = 0;
			Episode = 0;
			_valid = valid;
		}

		public static implicit operator bool(EpisodeInfo f) {
			return f._valid;
		}

		public bool SetValues(string p, string n, int s, int e) {
			if (p == "" | n == "" || s < 0 || e < 0) {
				return false;
			}

			Name = n;
			Season = s;
			Episode = e;
			path = p;
			_valid = true;
			return true;
		}

		public static bool operator ==(EpisodeInfo a, EpisodeInfo b) {
			return a.Equals(b);
		}

		public static bool operator !=(EpisodeInfo a, EpisodeInfo b) {
			return !(a == b);
		}

		public override int GetHashCode() {
			return ToString().GetHashCode();
		}

		public override string ToString() {
			return $"{Name}.S0{Season}E0{Episode}".Replace(" ", ".");
		}

		public string toDirectory() {
			return $"{Name}\\season {Season}\\{ToString().Replace(" ",".")}";

		}

		public override bool Equals(object obj) {
			return GetHashCode() == obj.GetHashCode() && obj.GetType() == GetType();
		}

		public void TrimName() {
			Name = Name.Replace(".", " ");
			Name = Name.Trim();
		}
	}
}