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
        private const string DefaultShowname = "Arrow.S01E02.HTV.WTF.mkv";
        private const string DefaultShownameWithSpace = "Arrow S01E02.mkv";
        private const string DefaultShownameWithX = "Arrow.1x2.mkv";

        private readonly List<Pair<string, EpisodeInfo>> _sourceExpectedList = new List<Pair<string, EpisodeInfo>>
        {
            new Pair<string, EpisodeInfo>("Arrow.S01E02.HTV.WTF.mkv", new EpisodeInfo("Arrow", 1, 2)),
            new Pair<string, EpisodeInfo>("Arrow S01E02.mkv", new EpisodeInfo("Arrow", 1, 2)),
            new Pair<string, EpisodeInfo>("Arrow.1x2.mkv", new EpisodeInfo("Arrow", 1, 2)),
            new Pair<string, EpisodeInfo>("Castle.2009.s08e13.mkv", new EpisodeInfo("Castle 2009", 8, 13)),
            new Pair<string, EpisodeInfo>(@"D:\Castle.Season.8\Ep-03.PhDead.mp4", new EpisodeInfo("Castle", 8, 3)),
            new Pair<string, EpisodeInfo>(@"D:\Castle.2009.Season.8\Ep-03.PhDead.mp4", new EpisodeInfo("Castle 2009", 8, 3)),
            new Pair<string, EpisodeInfo>(@"D:\Castle 2009 Season 8\Ep-03 PhDead.mp4", new EpisodeInfo("Castle 2009", 8, 3)),
            new Pair<string, EpisodeInfo>(@"C:\Users\Alexa\Downloads\tors\Jack Ryan - Season 2 [1080p]\Jack Ryan - S02E06 - Persona Non Grata.mkv", new EpisodeInfo("Jack Ryan", 2, 6)),
        };


        private static void CompareMembers(EpisodeInfo a, EpisodeInfo b)
        {
            Assert.AreEqual(a.Name, b.Name);
            Assert.True(a.Season == b.Season);
            Assert.True(a.Episode == b.Episode);
            Assert.True((bool) a == (bool) b);
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