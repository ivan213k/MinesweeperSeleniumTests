using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;

namespace MinesweeperSeleniumTests
{
    [TestClass]
    public class GameSettingsTests
    {
        private IWebDriver driver;
        private string appURL = "https://ccfe6cfc.minesweeperblazor.pages.dev";
        private string applyButtonXPath = "/html/body/app/div[2]/div/div[2]/div/div/button[2]";
        private string specialFieldRadioButtonXPath = "//*[@id=\"flexRadioSpecial\"]";
        private string rowsCountInputXPath = "//*[@id=\"inputRows\"]";
        private string columnsCountInputXPath = "//*[@id=\"inputColumns\"]";
        private string bombsCountInputXPath = "//*[@id=\"inputBombs\"]";

        [TestMethod]
        [TestCategory("Chrome")]
        public void VerifySettingsPageTitle()
        {
            string titleElementXPath = "/html/body/app/div[2]/div/h3";
            NavigateToGameSettings();
            WebDriverWait(titleElementXPath);
            IWebElement titleElement = GetElementByXPath(titleElementXPath);

            Assert.AreEqual(expected: "Game settings page", actual: titleElement.Text, "Verified title of the page");
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void BackToGameNavigationTest() 
        {
            string cancelButtonXPath = "/html/body/app/div[2]/div/div[2]/div/div/button[1]";
            NavigateToGameSettings();
            WebDriverWait(cancelButtonXPath);
            IWebElement cancelButton = GetElementByXPath(cancelButtonXPath);
            cancelButton.Click();

            Assert.AreEqual(expected: $"{appURL}/game", actual: driver.Url);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void ApplyButtonNavigateToGameTest()
        {
            NavigateToGameSettings();
            WebDriverWait(applyButtonXPath);
            IWebElement applyButton = GetElementByXPath(applyButtonXPath);
            applyButton.Click();

            Assert.AreEqual(expected: $"{appURL}/game", actual: driver.Url);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void SpecialFieldBombsCounterTest() 
        {
            string bombsCounterLabelXPath = "/html/body/app/div[2]/div/div[1]/div/div[1]/label";
            NavigateToGameSettings();
            WebDriverWait(specialFieldRadioButtonXPath);
            IWebElement specialFieldRadio = GetElementByXPath(specialFieldRadioButtonXPath);
            specialFieldRadio.Click();

            IWebElement bombsCountInput = GetElementByXPath(bombsCountInputXPath);
            bombsCountInput.Clear();
            bombsCountInput.SendKeys("13");

            IWebElement applyButton = GetElementByXPath(applyButtonXPath);
            applyButton.Click();
            IWebElement counterElement = GetElementByXPath(bombsCounterLabelXPath);

            Assert.AreEqual(expected: "13", actual: counterElement.Text);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void GeneratedFieldSizeTest() 
        {
            string cellXPath = "/html/body/app/div[2]/div/div[2]/div";
            int rowsCount = 10;
            int columnsCount = 10;

            NavigateToGameSettings();
            WebDriverWait(specialFieldRadioButtonXPath);
            IWebElement specialFieldRadio = GetElementByXPath(specialFieldRadioButtonXPath);
            specialFieldRadio.Click();

            IWebElement rowsInput = GetElementByXPath(rowsCountInputXPath);
            rowsInput.Clear();
            rowsInput.SendKeys(rowsCount.ToString());

            IWebElement columnsInput = GetElementByXPath(columnsCountInputXPath);
            columnsInput.Clear();
            columnsInput.SendKeys(columnsCount.ToString());
            IWebElement applyButton = GetElementByXPath(applyButtonXPath);
            applyButton.Click();
            var cells = driver.FindElements(By.XPath(cellXPath));

            Assert.AreEqual(expected: rowsCount * columnsCount, actual: cells.Count);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void InvalidInputsTest() 
        {
            NavigateToGameSettings();
            WebDriverWait(specialFieldRadioButtonXPath);
            IWebElement specialFieldRadio = GetElementByXPath(specialFieldRadioButtonXPath);
            specialFieldRadio.Click();

            IWebElement rowsInput = GetElementByXPath(rowsCountInputXPath);
            rowsInput.Clear();
            rowsInput.SendKeys("9999");

            IWebElement columnsInput = GetElementByXPath(columnsCountInputXPath);
            columnsInput.Clear();
            columnsInput.SendKeys("9999");
            IWebElement applyButton = GetElementByXPath(applyButtonXPath);
            applyButton.Click();
            Assert.AreEqual(expected: $"{ appURL}/ game", actual: driver.Url);
        }

        private void NavigateToGameSettings()
        {
            driver.Navigate().GoToUrl(appURL + "/gameSettings");
        }

        private void WebDriverWait(string elementXPath, int seconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(driver => driver.FindElements(By.XPath(elementXPath)).Count > 0);
        }
        private IWebElement GetElementByXPath(string titleElementXPath)
        {
            return driver.FindElement(By.XPath(titleElementXPath));
        }

        [TestInitialize()]
        public void SetupTest()
        {
            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
