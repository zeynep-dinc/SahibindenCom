using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
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

        [When(@"search malikane")]
        public void WhenSearchMalikane()
        {
            IWebElement element = driver.FindElement(By.Name("query_text"));
            element.Click();
            element.SendKeys("malikane" + Keys.Enter);

        }

        [When(@"control the found list")]
        public void WhenControlTheFoundList()
        {
            beklemeSuresi = 0;
            String a=driver.FindElement(By.ClassName("facetedFilteredLink")).Text;
            while (beklemeSuresi == 10)
            {
                Thread.Sleep(beklemeSuresi);
                if (a == "malikane")
                {
                    Console.WriteLine("Malikane değeri bulundu.");
                }
                else
                {
                    driver.Close();
                    Console.WriteLine("değer eşleşmedi");
                }
            }
            Console.WriteLine("doğrulama {0} sürede yapıldı", beklemeSuresi);
        }
        
        [When(@"write found good score")]
        public void WhenWriteFoundGoodScore()
        {
            string score = driver.FindElement(By.XPath("//*[@id=\"searchResultsSearchForm\"]/div/div[3]/div[1]/div[2]/div[1]/div[1]/span")).Text;
            Console.WriteLine("Ürün sayısı= ", score);
            driver.Quit();
        }
    }
}
