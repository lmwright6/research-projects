using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; //to use googlechrome browser.

using OpenQA.Selenium.Support;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Net;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace LearnSeliniumTesting
{
    [TestClass]
    public class MainDriver
    {

        IWebDriver driver;
        IWebDriver boxDriver;
      //  string boxUrl =
        string url = "http://srda.cse.nd.edu/mediawiki/index.php/Special:UserLogin";
        string makingQueriesUrl = "http://srda.cse.nd.edu/mediawiki/index.php/Making_Queries";
        string queryFormUrl = "http://srda.cse.nd.edu/cgi-bin/form.pl";
        [TestInitialize]
            public void TestSetup()
            {
                driver = new ChromeDriver(@"C:\Users\Lauren\Documents\Research\SourceForge");
                driver.Navigate().GoToUrl(url);

               //boxDriver

                
            }
            [TestCleanup]
            public void Cleanup()
            {
                driver.Quit();
            }
            [TestMethod]
            public void Login()
            {

            //insert username
            driver.FindElement(By.Id("wpName1")).SendKeys("Uraja");

            //insert password
            driver.FindElement(By.Id("wpPassword1")).SendKeys("Alabama2017");

            //Click Login
            driver.FindElement(By.Id("wpLoginAttempt")).Click();

            //Go To Making Queries
            driver.Navigate().GoToUrl(makingQueriesUrl);

            //Go To Query Form
            driver.Navigate().GoToUrl(queryFormUrl);

            //Open Text File Containing all dataset names, this location will change
            StreamReader sr = new StreamReader(@"C:\Users\Lauren\Documents\Research\SourceForge\DataMonthYear.txt");

            //dataset array
            List<String> datasets = new List<string>();

            //fill datasets
            while (sr.Peek() != -1)
            {
                datasets.Add(sr.ReadLine());
            }



            String[] tables = { "groups", "users","project_group_list", "project_status", "project_task", "project_history", "project_assigned_to"};

         
            

            

            for (int i = 0; i < datasets.Count; i++)
            {
                for(int j = 0; j < tables.Length; j++)
                {

                    //insert text into select box
                    IWebElement selectBox = driver.FindElement(By.Id("uitems"));
                    selectBox.Clear();
                    selectBox.SendKeys("*");

                    //insert text into from box
                    IWebElement fromBox = driver.FindElement(By.Id("utables"));

                    fromBox.Clear();
                    fromBox.SendKeys(datasets[i] + "." +tables[j]);

                     IList <IWebElement> radioButtons = driver.FindElements(By.Name("useparator"));
                    radioButtons.ElementAt(4).Click();
                    if (radioButtons.ElementAt(0).Selected){
                        radioButtons.ElementAt(0).Clear();
                    }

                    IList<IWebElement> radioButtons2 = driver.FindElements(By.Name("append_query"));
                    radioButtons2.ElementAt(1).Click();
                    if (radioButtons2.ElementAt(0).Selected)
                    {
                        radioButtons2.ElementAt(0).Clear();
                    }
                    //click submit query

                    IWebElement submitQuery = driver.FindElement(By.XPath("//input[@value='Submit Query']"));

                    submitQuery.Click();
                    //this is where you need to get the download



                  //  IWebElement fileDownload = driver.FindElement(By.XPath("//*[@href='/qresult/uraja/uraja.txt']"));


                    //fileDownload.Click();

                    string urlDownload = "http://srda.cse.nd.edu/qresult/uraja/uraja.xml";



                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(urlDownload,
                                            @"D:\LaurensSourceForgeData\" + datasets[i] + "_" + tables[j] + ".xml");
                        
                    }
                    
                    

                    //Open box and store it there


                    //Microsoft.Office.Interop.Excel.Application excel2; // Create Excel app
                    //Microsoft.Office.Interop.Excel.Workbook DataSource; // Create Workbook
                    //Microsoft.Office.Interop.Excel.Worksheet DataSheet; // Create Worksheet
                    //excel2 = new Microsoft.Office.Interop.Excel.Application(); // Start an Excel app
                    //DataSource = (Microsoft.Office.Interop.Excel.Workbook)excel2.Workbooks.Add(1); // Add a Workbook inside
                    //string tempFolder = System.IO.Path.GetTempPath(); // Get folder 
                    //string FileName = openFileDialog1.FileName.ToString(); // Get xml file name


                    //var app = new Microsoft.Office.Interop.Excel.Application();
                    //app.Workbooks.OpenXML(@"C:\Users\Lauren\Documents\Research\SourceForge\XMLFiles\" + datasets[i] + "_" + tables[j] + ".xml");
                    //Microsoft.Office.Interop.Excel.Workbook newWorkbook = new Workbook();
                    //newWorkbook = app.Workbooks[0];

                    //      newWorkbook.SaveAs(@"C:\Users\Lauren\Documents\Research\SourceForge\C:\Users\Lauren\Documents\Research\SourceForge\ExcelWorkbookFromXML\" + datasets[i] + "_" + tables[j] + ".xls" );


                    // Workbooks wbs = app.Workbooks;

                    ////DataSet dataSet = new DataSet();
                    ////dataSet.ReadXml("input.xml", XmlReadMode.ReadSchema);
                    //////save it on box


                    // Console.ReadLine();

                    driver.Navigate().GoToUrl(queryFormUrl);

                }
                //Go To Query Form
            }
            

            
            Console.ReadLine();
            


            //This is the placeholder to write actual code.

        }

      

        
        /* IWebDriver driver;
         string loginUrl = "www.google.com";
           //  "http://srda.cse.nd.edu/mediawiki/index.php/Special:UserLogin";
         string makingQueriesUrl = "http://srda.cse.nd.edu/mediawiki/index.php/Making_Queries";

         [TestInitialize]
         public void TestSetup()
         {
             driver = new ChromeDriver();
             driver.Navigate().GoToUrl(loginUrl);
         }

         [TestMethod]
         public void TestMethodLogin()
         {


         }

         [TestCleanup]
         public void CleanUp()
         {
             driver.Quit();
         }*/
    }
}
