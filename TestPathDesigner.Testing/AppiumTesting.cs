using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPathDesigner.Testing
{
    public class AppiumTesting
    {
        private string appId;
        private WindowsDriver<WindowsElement> driver;
        public AppiumTesting(string appName)
        {
            appId = appName;
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", appId);
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }
        public void StartTesting(List<string> elements)
        {
            foreach(var element in elements)
            {
                var e = driver.FindElementByName(element);
                e.Click();
            }
        }
    }
}
