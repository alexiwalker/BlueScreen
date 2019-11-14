namespace BlueScreenCategoriser
{
    public class EpisodeInfo
    {
        public string Name { get; private set; }

        public int Season { get; private set; }

        public int Episode { get; private set; }

        private bool _valid;

        public EpisodeInfo(string name, int season, int episode)
        {
            this.Name = name;
            this.Season = season;
            this.Episode = episode;
            this._valid = true;
        }

        public EpisodeInfo()
        {
            Name = "";
            Season = -1;
            Episode = -1;
            _valid = false;
        }

        public EpisodeInfo(bool valid)
        {
            this.Name = "";
            this.Season = 0;
            this.Episode = 0;
            this._valid = valid;
        }

        public static implicit operator bool(EpisodeInfo f)
        {
            return f._valid;
        }

        public bool SetValues(string n, int s, int e)
        {
            if (n == "" || s < 0 || e < 0)
            {
                return false;
            }

            Name = n;
            Season = s;
            Episode = e;
            _valid = true;
            return true;
        }

        public static bool operator ==(EpisodeInfo a, EpisodeInfo b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(EpisodeInfo a, EpisodeInfo b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}.S0{Season}E0{Episode}";
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode() && obj.GetType() == GetType();
        }

        public void TrimName()
        {
            Name = Name.Replace(".", " ");
            Name = Name.Trim();
        }
    }
}