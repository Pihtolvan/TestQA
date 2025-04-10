using System;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace DragAndDropTest
{
    public class DragAndDropTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void DragAndDropAction()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/drag_and_drop");

            var draggElem = driver.FindElement(By.Id("column-a"));
            var toDraggElem = driver.FindElement(By.Id("column-b"));

            Actions actions = new Actions(driver);
            actions
                .ClickAndHold(draggElem)
                .MoveToElement(toDraggElem)
                .Release()
                .Build()
                .Perform();

            if (driver.FindElement(By.Id("column-a")).Text.Trim().Contains("B") && driver.FindElement(By.Id("column-b")).Text.Trim().Contains("A"))
                Console.WriteLine("Dragged and dropped");
            else Console.WriteLine("something went wrong");

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile("screenshot.png");
            Console.WriteLine("Screenshot saved: screenshot.png");

        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        public static void Main(string[] args)
        {
            DragAndDropTest test = new DragAndDropTest();
            test.Setup();
            test.DragAndDropAction();
            test.Teardown();
        }
    }
}
