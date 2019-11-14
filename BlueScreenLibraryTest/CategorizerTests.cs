using System;
using System.Collections.Generic;
using System.IO;
using BlueScreenCategoriser;
using NUnit.Framework;
using Utils;

namespace BlueScreenLibraryTest
{
    [TestFixture]
    public class CategorizerTests
    {
        private const string DefaultShowname = "Arrow.S01E02.HTV.WTF";
        private const string DefaultShownameWithSpace = "Arrow S01E02";
        private const string DefaultShownameWithX = "Arrow.1x2";

        private readonly List<Pair<string, EpisodeInfo>> _sourceExpectedList = new List<Pair<string, EpisodeInfo>>
        {
            new Pair<string, EpisodeInfo>("Arrow.S01E02.HTV.WTF", new EpisodeInfo("Arrow", 1, 2)),
            new Pair<string, EpisodeInfo>("Arrow S01E02", new EpisodeInfo("Arrow", 1, 2)),
            new Pair<string, EpisodeInfo>("Arrow.1x2", new EpisodeInfo("Arrow", 1, 2)),
            new Pair<string, EpisodeInfo>("Castle.2009.s08e13", new EpisodeInfo("Castle 2009", 8, 13)),
            new Pair<string, EpisodeInfo>(@"D:\Castle.Season.8\Ep-03.PhDead.mp4", new EpisodeInfo("Castle", 8, 3)),
            new Pair<string, EpisodeInfo>(@"D:\Castle.2009.Season.8\Ep-03.PhDead.mp4", new EpisodeInfo("Castle 2009", 8, 3)),
            new Pair<string, EpisodeInfo>(@"D:\Castle 2009 Season 8\Ep-03 PhDead.mp4", new EpisodeInfo("Castle 2009", 8, 3)),
        };


        public static void CompareMembers(EpisodeInfo a, EpisodeInfo b)
        {
            Assert.AreEqual(a.Name, b.Name);
            Assert.AreEqual(a.Season, b.Season);
            Assert.AreEqual(a.Episode, b.Episode);
            Assert.AreEqual((bool) a, (bool) b);
        }

        [Test]
        public void StandardShownameTest()
        {
            var expectedShowDetails = new EpisodeInfo("Arrow", 1, 2);
            var incorrectShowDetails = new EpisodeInfo("Arrow", 2, 1);
            var returnedShowDetails = FileCategorizer.GetFileInfo(DefaultShowname);
            CompareMembers(expectedShowDetails, returnedShowDetails);
            Assert.AreEqual(expectedShowDetails, returnedShowDetails);
            Assert.AreNotEqual(incorrectShowDetails, returnedShowDetails);
        }

        [Test]
        public void StandardShownameTestWithSpace()
        {
            var expectedShowDetails = new EpisodeInfo("Arrow", 1, 2);
            var incorrectShowDetails = new EpisodeInfo("Arrow", 2, 1);
            var returnedShowDetails = FileCategorizer.GetFileInfo(DefaultShownameWithSpace);
            CompareMembers(expectedShowDetails, returnedShowDetails);
            Assert.AreEqual(expectedShowDetails, returnedShowDetails);
            Assert.AreNotEqual(incorrectShowDetails, returnedShowDetails);
        }

        [Test]
        public void StandardShownameTestWithX()
        {
            var expectedShowDetails = new EpisodeInfo("Arrow", 1, 2);
            var incorrectShowDetails = new EpisodeInfo("Arrow", 2, 1);
            var returnedShowDetails = FileCategorizer.GetFileInfo(DefaultShownameWithX);
            CompareMembers(expectedShowDetails, returnedShowDetails);
            Assert.AreEqual(expectedShowDetails, returnedShowDetails);
            Assert.AreNotEqual(incorrectShowDetails, returnedShowDetails);
        }

        [Test]
        public void TestAll()
        {
            foreach (var pair in _sourceExpectedList)
            {
                var source = pair.First;
                var expected = pair.Second;
                var actual = FileCategorizer.GetFileInfo(source);
                CompareMembers(expected, actual);
            }
        }
    }
}