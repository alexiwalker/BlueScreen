using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlueScreenCategoriser
{
    public static class FileCategorizer
    {
        //Classic showname patterns as regularly used by TPB and EZTV single episodes
        private const string DefaultShowRegex = @"([A-Za-z]+)[\s.-][sS]0([0-9]+)[\s.]*?[eE]0([0-9]+)";
        private const string DefaultShowRegexFallBack = @"([a-zA-Z0-9(). -]*?)[.\s -][sS][0]*([0-9]+)[eE][0]*([0-9]+)";
        private const string DefaultShowWithXRegex = @"([A-Za-z0-9\s.\-]*?)([0-9]+)x([0-9]+)";
        private const string DefaultShowWithZeroDelimeter = @"([a-zA-Z0-9(). -]*?)[.\-\s]0?([0-9]+)0([0-9]+)";

        //this one looks for any [SHOWNAME][delimeters][S[eason]?]NUM[DELIMITERS][E[pisode]]NUM
        //more likely to be required for full season downloads as they use less predictable names
        //may be too broad though, should check it
        private const string HeavyDutyBulkMatcherWithDelimiter = @"([a-zA-Z0-9(). -]*?)[.\s -][sS][Ee]?[aA]?[sS]?[oO]?[nN]?[0. ]*([0-9]+)[.\\\-\s]?[eE][pP]?[iI]?[sS]?[oO]?[dD]?[eE]?[-.\s\/]?[0]*?[0. \s]?([0-9]+)";

        //if a name can be accurately described with a single regex, it should fit
        //into DefaultNameStructure
        //anything that requires more complicated structures should be handled by a different fn
        //which should be included in this list
        private static List<Func<string, EpisodeInfo>> _usableFunctions = new List<Func<string, EpisodeInfo>>
        {
            DefaultNameStructure
        };

        private static readonly List<string> ShowRegexes = new List<string>
        {
            DefaultShowWithXRegex,
            DefaultShowRegex,
            DefaultShowRegexFallBack,
            HeavyDutyBulkMatcherWithDelimiter,
            DefaultShowWithZeroDelimeter,
        };

        public static void AddFileParser(Func<string, EpisodeInfo> func)
        {    
            _usableFunctions.Append(func);
        }

        public static EpisodeInfo GetFileInfo(string path)
        {
            var fileInfo = new EpisodeInfo(false);
            
            foreach (var fn in _usableFunctions)
            {
                var nfileInfo = fn(path);
                if (!nfileInfo) continue;
                fileInfo = nfileInfo;
                break;
            }

            return fileInfo;
        }

        private static EpisodeInfo DefaultNameStructure(string path)
        {
            var fInfo = new EpisodeInfo();

            foreach (var regexString in ShowRegexes)
            {
                string name;
                int season;
                int episode;
                
                Regex regex = new Regex(regexString);
                var matches = regex.Match(path);

                if (matches.Length < 1)
                {
                    //nothing to match, nothing to update the info with
                    continue;
                }

                try
                {
                    name = matches.Groups[1].ToString();
                    season = Convert.ToInt32(matches.Groups[2].ToString());
                    episode = Convert.ToInt32(matches.Groups[3].ToString());
                }
                catch
                {
                    continue;
                }

                fInfo.SetValues(name, season, episode);

                if (!fInfo) continue;
                
                fInfo.TrimName(); 
                return fInfo;
 
            }
            
            return fInfo;
        }
    }
}