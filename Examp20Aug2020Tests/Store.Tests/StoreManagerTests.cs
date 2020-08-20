using NUnit.Framework;

namespace Store.Tests
{
    public class StoreManagerTests
    {
        private Product product;
        private StoreManager storeManager;
        private const string NotPositiveQuantityExceptionMessage = "Product count can't be below or equal to zero.";
        private const string NoSuchProductExceptionMessage = "There is no such product.";
        private const string NotEnoughQuantityExceptionMessage = "There is not enough quantity of this product.";
        [SetUp]
        public void Setup()
        {
            this.product = new Product("Apple", 5, 2.20m);
            this.storeManager = new StoreManager();
        }

        [Test]
        public void ProductConstructorNormalTest()
        {

            Assert.AreEqual("Apple", this.product.Name);
            Assert.AreEqual(5, this.product.Quantity);
            Assert.AreEqual(2.20m, this.product.Price);
        }

        [Test]
        public void ProductQuantitySetter()
        {
            this.product.Quantity = 10;
            Assert.AreEqual(10, this.product.Quantity);
        }

        [Test]
        public void StoreManagerCount()
        {
            this.storeManager.AddProduct(product);
            Assert.AreEqual(1, storeManager.Count);
        }
        [Test]
        public void StoreManagerAddProductsCount()
        {
            this.storeManager.AddProduct(product);
            Assert.AreEqual(1, storeManager.Products.Count);
        }

        [Test]
        public void StoreManagerAddNullProduct()
        {

            Assert.That(() =>
            {
                this.storeManager.AddProduct(null);
            }, Throws.ArgumentNullException.With.Message.EqualTo("Value cannot be null. (Parameter 'product')"));

        }
        [Test]
        public void StoreManagerAddProductLessThanZero()
        {
            Product productZero = new Product("Orange", -1, 2.2m);
            Assert.That(() =>
            {
                this.storeManager.AddProduct(productZero);
            }, Throws.ArgumentException.With.Message.EqualTo(NotPositiveQuantityExceptionMessage));

        }

        [Test]
        public void StoreManagerBuyProduct()
        {
            Product product1 = new Product("Orange", 10, 2.4m);
            this.storeManager.AddProduct(product);
            this.storeManager.AddProduct(product1);
            var finalPrice = this.storeManager.BuyProduct("Orange", 5);
            Assert.AreEqual(12, finalPrice);
            Assert.AreEqual(5, product1.Quantity);
        }
        [Test]
        public void StoreManagerBuyProductNoSuchProduct()
        {
            Product product1 = new Product("Orange", 10, 2.4m);
            this.storeManager.AddProduct(product);
            this.storeManager.AddProduct(product1);
            Assert.That(() =>
            {
                var finalPrice = this.storeManager.BuyProduct("OrangeBlue", 5);

            }, Throws.ArgumentNullException.With.Message.EqualTo("There is no such product. (Parameter 'product')"));
        }
        [Test]
        public void StoreManagerBuyProductNotEnoughProduct()
        {
            Product product1 = new Product("Orange", 10, 2.4m);
            this.storeManager.AddProduct(product);
            this.storeManager.AddProduct(product1);
            Assert.That(() =>
            {
                var finalPrice = this.storeManager.BuyProduct("Orange", 15);

            }, Throws.ArgumentException.With.Message.EqualTo(NotEnoughQuantityExceptionMessage));
        }

        [Test]
        public void GetTheMostExpensiveProductCorrect()
        {
            Product product1 = new Product("Orange", 10, 2.4m);
            Product product2 = new Product("Banana", 10, 3.5m);
            this.storeManager.AddProduct(product);
            this.storeManager.AddProduct(product1);
            this.storeManager.AddProduct(product2);
            var mostExpensive = this.storeManager.GetTheMostExpensiveProduct();
            Assert.AreSame(product2, mostExpensive);
        }
    }
}