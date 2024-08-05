using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Marshrutka
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Когда нужна маршрутка?\n0 - сегодня, 1 - завтра, 2 - послезавтра");

            int a;
            bool isValidInput = int.TryParse(Console.ReadLine(), out a);

            while (!isValidInput || (a != 0 && a != 1 && a != 2))
            {
                Console.WriteLine("Введена некорректная цифра!");
                isValidInput = int.TryParse(Console.ReadLine(), out a);
            }
            
            Console.WriteLine("Выберите желаемое время отправления:\n 1) 4:36 - 9:07\n 2) 5:06 - 9:37\n 3) 5.36 - 10:07\n 4) 6:06 - 10.37\n 5) 7:06 - 11:37\n 6) 7:36 - 12:07\n 7) 8:06 - 12:37\n 8) 8:36 - 13:07\n 9)9:06 - 13:37\n 10) 10:06 - 14:37\n 11) 11:06 - 15:37\n 12) 12:06 - 16:37\n 13) 13:06 - 17:37\n 14) 14:06 - 18:37\n 15) 15:06 - 19:37\n 16) 16:06 - 20:37\n 17) 17:06 - 21:37\n 18) 18:06 - 22:37\n 19) 19:06 - 23:37\n 20) 20:06 - 00:37");
            int time;
            bool isValidInputTime = int.TryParse(Console.ReadLine(), out time);
            
            


            IWebDriver driver = new ChromeDriver();

            try
            {
                
                driver.Navigate().GoToUrl("https://smilebus.by/routes/mozyr/minsk");

                //нажатие кнопки "Завтра" || Если на сегодня то индекс - 0, на завтра - 1, послезавтра - 2
                IList<IWebElement> buttons1 = driver.FindElements(By.ClassName("el-link__text"));
                buttons1[a].Click();

                //нажатие места посадки
                IList<IWebElement> from = driver.FindElements(By.ClassName("multiselect__tags"));
                from[2].Click();

                //Выбор места посадки
                IList<IWebElement> place = driver.FindElements(By.ClassName("multiselect__element"));
                place[247].Click();

                // нажатие и выбор места высадки
                IList<IWebElement> to = driver.FindElements(By.ClassName("multiselect__tags"));
                to[3].Click();
                place[264].Click();


                // Проверка наличия свободных мест и заказ билета
                IWebElement ticketButton = driver.FindElement(By.CssSelector("button[title='Забронировать']"));
                if (ticketButton.Enabled)
                {
                    ticketButton.Click();
                    Console.WriteLine("Билет успешно забронирован!");
                }
                else
                {
                    Console.WriteLine("На выбранное время нет свободных мест.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                // Закрытие браузера
                //driver.Quit();
            }

            Console.WriteLine("Программа завершена. Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }
    }
}