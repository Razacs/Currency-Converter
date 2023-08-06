using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CurrencyConverterTests.Tests
{
    [TestFixture]
    public class CurrencyConverterTests
    {
        private IWebDriver driver;
        private string converterUrl = "https://www.xe.com/currencyconverter/";

        [SetUp]
        public void TestSetup()
        {
            // Set up Chrome WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TestCleanup()
        {
            // Clean up resources
            driver.Quit();
        }

        [Test]
        public void TestCurrencyConversion_ValidAmount()
        {
            // Test logic for currency conversion with valid numeric amount
            // - Navigate to converterUrl
            driver.Navigate().GoToUrl(converterUrl);

            // - Enter valid numeric amount, source currency, and target currency
            IWebElement amountInput = driver.FindElement(By.XPath("//input[@name='Amount']"));
            IWebElement sourceCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='From']"));
            IWebElement targetCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='To']"));

            string validAmount = "10"; // Change this value as per your test scenario
            string sourceCurrency = "USD"; // Change this value as per your test scenario
            string targetCurrency = "EUR"; // Change this value as per your test scenario

            amountInput.SendKeys(validAmount);
            sourceCurrencyDropdown.SendKeys(sourceCurrency);
            targetCurrencyDropdown.SendKeys(targetCurrency);

            // - Click on the Convert button
            IWebElement convertButton = driver.FindElement(By.XPath("//button[@id='ucc_go_btn_svg']"));
            convertButton.Click();

            // - Validate the full conversion amount and the conversion rate of a single unit for both currencies
            IWebElement fullConversionResult = driver.FindElement(By.XPath("//span[@class='uccResultAmount']"));
            IWebElement singleUnitConversionResult = driver.FindElement(By.XPath("//span[@class='uccResUnit']"));

            string fullConversionAmount = fullConversionResult.Text;
            string singleUnitConversionRate = singleUnitConversionResult.Text;

            // Assert the conversion results using NUnit assertions
            // For example:
            Assert.IsTrue(fullConversionAmount.Contains(validAmount));
            Assert.IsTrue(fullConversionAmount.Contains(sourceCurrency));
            Assert.IsTrue(fullConversionAmount.Contains(targetCurrency));

            // You can also parse the conversion amount to double and perform mathematical validation:
            double conversionAmountValue = double.Parse(fullConversionAmount.Split(' ')[0]);
            double conversionRate = double.Parse(singleUnitConversionRate.Split(' ')[0]);

            // Perform mathematical validation as per the acceptance criteria (e.g., 10 USD = 8.90909 EUR)
            Assert.AreEqual(conversionAmountValue, 10 * conversionRate);
        }

        [Test]
        public void TestCurrencyConversion_InvalidAmount()
        {
            // - Navigate to converterUrl
            driver.Navigate().GoToUrl(converterUrl);

            // - Enter invalid numeric amount, source currency, and target currency
            IWebElement amountInput = driver.FindElement(By.XPath("//input[@name='Amount']"));
            IWebElement sourceCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='From']"));
            IWebElement targetCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='To']"));

            string invalidAmount = "abc"; 
            string sourceCurrency = "USD"; 
            string targetCurrency = "EUR"; 

            amountInput.SendKeys(invalidAmount);
            sourceCurrencyDropdown.SendKeys(sourceCurrency);
            targetCurrencyDropdown.SendKeys(targetCurrency);

            // - Click on the Convert button
            IWebElement convertButton = driver.FindElement(By.XPath("//button[@id='ucc_go_btn_svg']"));
            convertButton.Click();

            // - Validate the error message
            IWebElement errorMessage = driver.FindElement(By.XPath("//p[@class='error']"));

            // Assert the error message using NUnit assertions
            Assert.AreEqual("Please enter a valid amount.", errorMessage.Text);
        }

        [Test]
        public void TestCurrencyConversion_NegativeAmount()
        {
            driver.Navigate().GoToUrl(converterUrl);

            // - Enter negative numeric amount, source currency, and target currency
            IWebElement amountInput = driver.FindElement(By.XPath("//input[@name='Amount']"));
            IWebElement sourceCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='From']"));
            IWebElement targetCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='To']"));

            string negativeAmount = "-10"; // Change this value as per your test scenario (negative numeric amount)
            string sourceCurrency = "USD"; // Change this value as per your test scenario
            string targetCurrency = "EUR"; // Change this value as per your test scenario

            amountInput.SendKeys(negativeAmount);
            sourceCurrencyDropdown.SendKeys(sourceCurrency);
            targetCurrencyDropdown.SendKeys(targetCurrency);

            // - Click on the Convert button
            IWebElement convertButton = driver.FindElement(By.XPath("//button[@id='ucc_go_btn_svg']"));
            convertButton.Click();

            // - Validate the full conversion amount and the conversion rate of a single unit for both currencies
            IWebElement fullConversionResult = driver.FindElement(By.XPath("//span[@class='uccResultAmount']"));
            IWebElement singleUnitConversionResult = driver.FindElement(By.XPath("//span[@class='uccResUnit']"));

            string fullConversionAmount = fullConversionResult.Text;
            string singleUnitConversionRate = singleUnitConversionResult.Text;

            // Assert the conversion results using NUnit assertions
            // For example:
            Assert.IsTrue(fullConversionAmount.Contains(negativeAmount));
            Assert.IsTrue(fullConversionAmount.Contains(sourceCurrency));
            Assert.IsTrue(fullConversionAmount.Contains(targetCurrency));

            // You can also parse the conversion amount to double and perform mathematical validation:
            double conversionAmountValue = double.Parse(fullConversionAmount.Split(' ')[0]);
            double conversionRate = double.Parse(singleUnitConversionRate.Split(' ')[0]);

            // Perform mathematical validation as per the acceptance criteria (e.g., -10 USD = -8.90909 EUR)
            Assert.AreEqual(conversionAmountValue, -10 * conversionRate);
        }

        [Test]
        public void TestCurrencyConversion_SwapCurrencies()
        {
            // Test logic for swapping currencies and validating inverse conversion
            driver.Navigate().GoToUrl(converterUrl);

            // - Enter valid numeric amount, source currency, and target currency
            IWebElement amountInput = driver.FindElement(By.XPath("//input[@name='Amount']"));
            IWebElement sourceCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='From']"));
            IWebElement targetCurrencyDropdown = driver.FindElement(By.XPath("//select[@name='To']"));

            string validAmount = "10"; // Change this value as per your test scenario (valid numeric amount)
            string initialSourceCurrency = "USD"; // Change this value as per your test scenario
            string initialTargetCurrency = "EUR"; // Change this value as per your test scenario

            amountInput.SendKeys(validAmount);
            sourceCurrencyDropdown.SendKeys(initialSourceCurrency);
            targetCurrencyDropdown.SendKeys(initialTargetCurrency);

            // - Click on the Swap button
            IWebElement swapButton = driver.FindElement(By.XPath("//button[@id='ucc_swap_btn_svg']"));
            swapButton.Click();

            // - Validate that the source currency is now the previous target currency and vice versa
            string swappedSourceCurrency = sourceCurrencyDropdown.Text;
            string swappedTargetCurrency = targetCurrencyDropdown.Text;

            // Assert the swapped currencies using NUnit assertions
            Assert.AreEqual(initialTargetCurrency, swappedSourceCurrency);
            Assert.AreEqual(initialSourceCurrency, swappedTargetCurrency);

            // - Click on the Convert button
            IWebElement convertButton = driver.FindElement(By.XPath("//button[@id='ucc_go_btn_svg']"));
            convertButton.Click();

            // - Validate the full conversion amount and the conversion rate of a single unit for both currencies
            IWebElement fullConversionResult = driver.FindElement(By.XPath("//span[@class='uccResultAmount']"));
            IWebElement singleUnitConversionResult = driver.FindElement(By.XPath("//span[@class='uccResUnit']"));

            string fullConversionAmount = fullConversionResult.Text;
            string singleUnitConversionRate = singleUnitConversionResult.Text;

            // Assert the conversion results using NUnit assertions
            Assert.IsTrue(fullConversionAmount.Contains(validAmount));
            Assert.IsTrue(fullConversionAmount.Contains(swappedSourceCurrency));
            Assert.IsTrue(fullConversionAmount.Contains(swappedTargetCurrency));

            // You can also parse the conversion amount to double and perform mathematical validation:
            double conversionAmountValue = double.Parse(fullConversionAmount.Split(' ')[0]);
            double conversionRate = double.Parse(singleUnitConversionRate.Split(' ')[0]);

            // Perform mathematical validation as per the acceptance criteria (e.g., 10 EUR = 11.11111 USD if 1 EUR = 1.11111 USD)
            Assert.AreEqual(conversionAmountValue, 10 / conversionRate);
        }





        


    }
}