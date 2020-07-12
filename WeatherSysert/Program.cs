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
            WebClient wc = new WebClient();
            Console.Write("Сейчас в Сысерти {0}\u00B0C", getTemp(wc));


        }

        static string getTemp(WebClient w)
        {
            try
            {
                string html = w.DownloadString("https://yandex.ru/pogoda/sysert");
                string sub = html.Substring(html.IndexOf("Текущая температура"));
                sub = sub.Substring(sub.IndexOf("temp__value") + 13);
                string temp = sub.Substring(0, sub.IndexOf("</span>"));

                return temp;
            }
            catch (Exception)
            {
                return "Город не найден";
            }


        }

    }
}

