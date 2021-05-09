using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace MultipleBrowserParallelTest_NUnit
{
    [TestFixture(typeof(ChromeOptions))]
    [TestFixture(typeof(FirefoxOptions))]
    [Parallelizable]                                           // tests will run in parallel
    public class MultipleBrowserParallelTest<Options> where Options : DriverOptions, new()
    {
        [ThreadStatic]
        private static IWebDriver driver;   // define driver as [Threastatic] to get a separate instance for each test!


        [OneTimeSetUp]
        public void Setup()
        {
            var options = new Options();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);

           // var optionsFF = new FirefoxOptions();
           // driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), optionsFF);
        }

       
        [Test]
        public void Test_NakovComTitle()
        {

            driver.Navigate().GoToUrl("https://nakov.com");
            var pageTitle = driver.Title;   // В C# можем да декларираме така!


            Assert.That(pageTitle.Contains("Svetlin Nakov"));
            Assert.That(driver.Title.Contains("Official Web Site and Blog"));
        }
        [Test]
        public void Test_SoftUniHeadlineNews()
        {
            driver.Navigate().GoToUrl("https://softuni.bg/");
            driver.FindElement(By.XPath("/html/body/div[2]/div/header/div/div[2]/div[1]/a")).Click();


            // F12 в сайта, намирам заглавието и десен бутон-> copy-> copy XPath Full!
            var headLineTitle = driver.FindElement(By.XPath(
                "/html/body/footer/div/ul/li[3]/div/ul/li[1]/a"));

            var itemText = headLineTitle.Text;

            Assert.That(itemText != null && itemText != "");
            System.Console.WriteLine(itemText);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}