using Castle.DynamicProxy.Generators;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public async Task StartTesting(ObservableCollection<TestModel> elements, Action<string> returnFunc)
        {
            Thread.Sleep(1000);
            await Task.Run(() => {
                foreach (var element in elements)
                {
                    ResolveTest(element, returnFunc);
                }
            });
        }
        public void ResolveTest(TestModel element, Action<string> returnFunc)
        {
            Thread.Sleep(500);
            var foundElement = GetElement(element.ElementType, element.ElementName);
            GetAction(foundElement, element.Action);
            returnFunc.Invoke(@$"Invoked {element.Action} on element ""{element.ElementName}"" using {element.ElementType}");
        }
        public void GetAction(WindowsElement element, ActionEnum action)
        {
            switch(action)
            {
                case ActionEnum.Click:
                    element.Click(); 
                    break;
                case ActionEnum.Wait:
                    Thread.Sleep(500);
                    break;
                case ActionEnum.Printscreen:
                    driver.TakeScreenshot();
                    break;
                default:
                    break;
            }
        }
        public WindowsElement GetElement(ElementTypeEnum type, string element)
        {
            switch(type)
            {
                case ElementTypeEnum.FindElementByName:
                    return driver.FindElementByName(element);
                case ElementTypeEnum.FindElementByAccessibilityId:
                    return driver.FindElementByAccessibilityId(element);
                case ElementTypeEnum.Wait:
                    return null;
                case ElementTypeEnum.Printscreen:
                    return null;
                default:
                    throw new Exception("No elements found");
            }
        }
    }
}
