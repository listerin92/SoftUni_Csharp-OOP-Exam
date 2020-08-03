using NUnit.Framework;

namespace Aquariums.Tests
{
    public class FishTests
    {
        [Test]
        public void FishNameGettTest()
        {
            string expectedName = "Ivan";
            Fish fish = new Fish("Ivan");
            Assert.AreEqual(expectedName, fish.Name);
        }
        [Test]
        public void FishNameSetTest()
        {
            string expectedName = "Pesho";
            Fish fish = new Fish("Ivan");
            fish.Name = "Pesho";
            Assert.AreEqual(expectedName, fish.Name);
        }
        [Test]
        public void AvailableGetTest()
        {
            bool expectedAvailableState = true;
            Fish fish = new Fish("Ivan");
            Assert.AreEqual(expectedAvailableState, fish.Available);
        }
    }
}