using System;
using System.Diagnostics;   // Нужна, чтобы запускать внешние процессы
using System.Net;           // Нужна, чтобы работать с Web
using System.Threading;

namespace YandexWeatherSysert
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser par = new Parser();
            string page = par.getPage();
            if (!page.Equals("Город не найден"))
            {
                Console.Write("Сейчас в {0} {1}\nДля Выхода нажмите любую клавишу...",par.getCity(page), par.getCurTemp(page));
            }

            else Console.Write(page);

            Console.ReadKey();
        }       

    }

    class Parser
    {
        public string getPage()    // скачать страницу с погодой
        {
            try
            {
                return (new WebClient()).DownloadString("https://yandex.ru/pogoda/sysert");
            }
            catch { return "Город не найден"; }
        }

        public string getCity(string s) // извлечь название города
        {
            string sub = s.Substring(s.IndexOf("Погода в") + 9);
            return sub.Substring(0, sub.IndexOf("<"));
        }


        public string getCurTemp(string s)     // извлечь из страницы значение текущей температуры
        {
            string sub = s.Substring(s.IndexOf("Текущая температура"));
            sub = sub.Substring(sub.IndexOf("temp__value") + 13);
            string temp = sub.Substring(0, sub.IndexOf("<"));
            sub = sub.Substring(sub.IndexOf("link__condition day-anchor i-bem") + 74);
            sub = sub.Substring(0, sub.IndexOf("<"));
            return temp + ("\u00B0C ") + sub;
        }

    }
}

