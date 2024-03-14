using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.PageObjects;
using Xunit;

namespace Mobile.Test
{
    public class MainPageTests : IDisposable
    {
        private AndroidDriver<AndroidElement> _driver;

        public MainPageTests()
        {
            var driverOption = new AppiumOptions();
            driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOption.AddAdditionalCapability("appium:automationName", "UiAutomator2");
            //driverOption.AddAdditionalCapability(MobileCapabilityType.App, "com.companyname.mobile");
            //driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Pixel 5 (34)");
            driverOption.AddAdditionalCapability("noReset", true);

            _driver = new AndroidDriver<AndroidElement> (new Uri("http://localhost:4723"), driverOption);
            _driver.ActivateApp("com.companyname.mobile");

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Fact]
        public void ScrrenshotTest()
        {
            var photo = _driver.GetScreenshot();
            photo.SaveAsFile($@"C:\Users\mariu\Projekty\LoopLords\Mobile.Test\screenshot.png");

        }

        [Fact]
        public void ClickBtnTest()
        {
            By byAndroidUIAutomator = new ByAndroidUIAutomator("new UiSelector().clickable(true)");

            Assert.Equal("Click me", _driver.FindElementById("android:id/content").FindElement(byAndroidUIAutomator).Text);
            _driver.FindElementById("android:id/content").FindElement(byAndroidUIAutomator).Click();
            Assert.Equal("Clicked 1 time", _driver.FindElementById("android:id/content").FindElement(byAndroidUIAutomator).Text);
        }
    }
}