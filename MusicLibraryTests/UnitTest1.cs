using NUnit.Framework;

namespace MusicLibraryTests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.That(true, Is.True);
        }
    }
}