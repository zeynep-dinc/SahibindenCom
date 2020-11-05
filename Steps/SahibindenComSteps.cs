using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq.Expressions;
using System.Threading;
using TechTalk.SpecFlow;

namespace SahibindenCom.Steps
{
   
    [Binding]
    public class SahibindenComSteps
    {
        IWebDriver driver;
        readonly string url = "https://www.sahibinden.com/";
        int beklemeSuresi = 5;
        
        [Given(@"go to url sahibinden\.com")]
        public void GivenGoToUrlSahibinden_Com()
        {
            try
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.FullScreen();
                string a = driver.Url.ToString();
                while (a == "Sahibinden Satılık, Kiralık, Emlak, Oto, Alışveriş Ürünleri")
                {
                    Thread.Sleep(beklemeSuresi);
                    beklemeSuresi = +5;
                    if (beklemeSuresi > 10)
                    {
                        Console.WriteLine("10 saniye geçildiği için uygulama kapatıldı");
                        driver.Close();
                    }
                }
                Console.WriteLine("doğrulama {0} sürede yapıldı", beklemeSuresi);
            }
            catch(Exception hata)
            {
                Console.WriteLine("Kaynak: ",hata.Source,"Mesaj: ",hata.Message);
            }
        }

        [When(@"search malikane")]
        public void WhenSearchMalikane()
        {
            try
            {

                IWebElement element = driver.FindElement(By.Name("query_text"));
                element.Click();
                element.SendKeys("malikane" + Keys.Enter);
            }
            catch (NotFoundException found)
            {
                Console.WriteLine(found.Message);
            }
            catch(Exception hata)
            {
                Console.WriteLine("Hata Kaynağı: ",hata.Source,"Mesaj: ",hata.Message);
            }
        }

        [Then(@"finish the test")]
        public void ThenFinishTheTest()
        {
            driver.Close();
        }

    }
}
