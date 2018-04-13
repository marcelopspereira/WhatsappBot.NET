using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using System.Threading;
using OpenQA.Selenium;
using System.IO;
using System.Text.RegularExpressions;




namespace WhatsBot_v._2
{
    class funktionen
    {
        int durchläufe = 0;
       // Wikipedia wikipedia = new Wikipedia();
        String[] lesen = new String[100];
        String[] neulesen = new String[100];
        string s;
        string hh;
        public void send(string text, FirefoxDriver d)
        {
            d.FindElementByClassName("_2S1VP").SendKeys(text);
            
            d.FindElementByClassName("_2S1VP").SendKeys(Keys.Enter);
        }

        public void auswerten( FirefoxDriver d)
        {
            
            for (int i = 0; i < lesen.Length; i++)
            {
               
               
                    if(lesen[i] != null)
                    {
                    try
                    {
                        s = lesen[i];
                        hh = s.Substring(0, 4);

                        if (hh == "!say")
                        {

                            s = lesen[i];
                            string gekürzt = s.Substring(5);
                            send(gekürzt, d);
                            lesen[i] = null;
                            continue;


                        }

                        s = lesen[i];
                        hh = s.Substring(0, 7);


                        if (hh == "!search")
                        {

                            s = lesen[i];

                            send("search is getting prepared...", d);
                            string gekürzt = s.Substring(8);

                            FirefoxDriver driver = new FirefoxDriver();
                            driver.Url = "https://www.wikipedia.org/";
                            driver.FindElementById("searchInput").SendKeys(gekürzt);
                            Thread.Sleep(50);
                            driver.FindElementById("searchInput").SendKeys(Keys.Enter);
                            Thread.Sleep(500);
                            string uri = driver.Url;                          
                            send("That's what i found:", d);
                            send(uri, d);
                            driver.Close();
                            driver.Dispose();
                            Thread.Sleep(200);

                            lesen[i] = null;
                            continue;


                        }

                     
                    }
                    catch
                    {


                    }




















                    }





            }


               
               




        }

        
        public void scanner(FirefoxDriver d)
        {
            durchläufe++;
            string tmp;
            
            IList<IWebElement> all = d.FindElements(By.ClassName("Tkt2p"));
           

            String[] eingelesen = new String[all.Count];
            int u = 0;
            
            foreach (IWebElement element in all)
            {
                try
                {
                    eingelesen[u++] = element.Text;
                }
                catch 
                {

                    
                }
                
            }

            
            File.WriteAllLines("tmp.txt", eingelesen);
            eingelesen = File.ReadAllLines("tmp.txt");




            int a =0;

            for (int i = 1; i < 3; i++)
            {
                if (eingelesen[eingelesen.Length - i].StartsWith("!"))
                {
                    tmp = eingelesen[eingelesen.Length - i];
                   
                    


                    lesen[a] = tmp;

                    a++;
                }

            }
            File.AppendAllLines("ausgabe.txt", lesen);
            

            int g = 0;

            auswerten( d);
        }


    

    }
}
