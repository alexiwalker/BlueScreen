using System.Collections.Generic;

namespace TestResources
{
    public class CategorizerResources
    {
        
        private readonly List<Pair<string, FileInfo>> _sourceExpectedList = new List<Pair<string, FileInfo>>
        {
            new Pair<string, FileInfo>("Arrow.S01E02.HTV.WTF", new FileInfo("Arrow", 1, 2)),
            new Pair<string, FileInfo>("Arrow S01E02", new FileInfo("Arrow", 1, 2)),
            new Pair<string, FileInfo>("Arrow.1x2", new FileInfo("Arrow", 1, 2)),
            new Pair<string, FileInfo>("Castle.2009.s08e13", new FileInfo("Castle 2009", 8, 13)),
            new Pair<string, FileInfo>(@"D:\Castle.Season.8\Ep-03.PhDead.mp4", new FileInfo("Castle", 8, 3)),
            new Pair<string, FileInfo>(@"D:\Castle.2009.Season.8\Ep-03.PhDead.mp4", new FileInfo("Castle 2009", 8, 3)),
            new Pair<string, FileInfo>(@"D:\Castle 2009 Season 8\Ep-03 PhDead.mp4", new FileInfo("Castle 2009", 8, 3)),
        };

        
    }
}