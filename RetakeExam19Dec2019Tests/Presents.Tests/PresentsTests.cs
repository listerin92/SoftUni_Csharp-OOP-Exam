using NUnit.Framework;

namespace Presents.Tests
{
    using System;

    public class PresentsTests
    {
        private Bag bag;
        private Present presentOne;
        [SetUp]
        public void Initialize()
        {
            this.bag = new Bag();
            this.presentOne = new Present("Ivan", 300);
        }
        [Test]
        public void TestPresentConstructorAndGetterName()
        {
            string expectedName = "Pesho";
            double expectedMagic = 200;
            Present present = new Present(expectedName, expectedMagic);

            Assert.AreEqual(expectedName, present.Name);
        }
        [Test]
        public void TestPresentConstructorAndGetterMagic()
        {
            string expectedName = "Pesho";
            double expectedMagic = 200;
            Present present = new Present(expectedName, expectedMagic);

            Assert.AreEqual(expectedMagic, present.Magic);
        }
        [Test]
        public void TestPresentConstructorAndAndSetterName()
        {
            string name = "Pesho";
            double magic = 200;
            Present present = new Present(name, magic);
            string expectedName = "Ivan";
            present.Name = expectedName;
            Assert.AreEqual(expectedName, present.Name);
        }
        [Test]
        public void TestPresentConstructorAndAndSetterMagic()
        {
            string name = "Pesho";
            double magic = 200;
            Present present = new Present(name, magic);
            double expectedMagic = 300;

            present.Magic = expectedMagic;
            Assert.AreEqual(expectedMagic, present.Magic);
        }

        [Test]
        public void TestBagConstructorAndCreateAndGetPresents()
        {
            bag.Create(presentOne);
            Assert.AreEqual(1, bag.GetPresents().Count);
        }

        [Test]
        public void TestBagCreateMessage()
        {
            string expectedMessage = $"Successfully added present {presentOne.Name}.";
            string result = bag.Create(presentOne);
            Assert.AreEqual(expectedMessage, result);
        }
        [Test]
        public void TestBagConstructorNull()
        {
            Assert.IsNotNull(this.bag.GetPresents());
        }
        [Test]
        public void TestBagCreateExceptionNullPresent()
        {
            Present nullPresent = null;
            Assert.That(() =>
                {
                    this.bag.Create(nullPresent);

                },Throws.ArgumentNullException
                    .With.Message.EqualTo("Value cannot be null. (Parameter 'Present is null')"));


        }
        [Test]
        public void TestBagCreateExceptionNullPresentExist()
        {
            this.bag.Create(presentOne);
            Present anotherPresent = presentOne;
            Assert.That(() =>
            {
                this.bag.Create(anotherPresent);

            }, Throws.InvalidOperationException
                .With.Message.EqualTo("This present already exists!"));
        }

        [Test]
        public void TestRemovePresent()
        {
            this.bag.Create(presentOne);
            Present anotherPresent = new Present("Gosho",250);
            this.bag.Create(anotherPresent);
            
            Assert.AreEqual(true, this.bag.Remove(anotherPresent));
        }
        [Test]
        public void TestRemovePresentCount()
        {
            this.bag.Create(presentOne);
            Present anotherPresent = new Present("Gosho", 250);
            this.bag.Create(anotherPresent);
            this.bag.Remove(anotherPresent);
            Assert.AreEqual(1, this.bag.GetPresents().Count);
        }

        [Test]
        public void TestGetPresentWithLeastMagic()
        {
            //Present present = this.presents.OrderBy(p => p.Magic).First();
            Present anotherPresent = new Present("Gosho", 10);
            this.bag.Create(presentOne);
            this.bag.Create(anotherPresent);
            Present acutalPresent = this.bag.GetPresentWithLeastMagic();
            Assert.AreEqual(anotherPresent, acutalPresent);
        }

        [Test]
        public void TestGetPresent()
        {
            //Present present = this.presents.FirstOrDefault(p => p.Name == name);
            Present anotherPresent = new Present("Gosho", 10);
            this.bag.Create(presentOne);
            this.bag.Create(anotherPresent);
            var actualPresent = this.bag.GetPresent("Gosho");

            Assert.AreEqual(10, actualPresent.Magic);

        }
        [Test]
        public void TestGetPresentNulException()
        {
            //Present present = this.presents.FirstOrDefault(p => p.Name == name);
            Present anotherPresent = new Present("Gosho", 10);
            this.bag.Create(presentOne);
            this.bag.Create(anotherPresent);
            var actualPresent = this.bag.GetPresent("GoshoDetGoNema");

            Assert.AreEqual(null, actualPresent);
        }
    }
}
