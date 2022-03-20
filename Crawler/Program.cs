using System;
using System.Collections;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                string websiteUrl = args[0];
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(websiteUrl);
                string content = await response.Content.ReadAsStringAsync();

                Regex regex = new Regex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
                MatchCollection matchCollection = regex.Matches(content);

                Hashtable hashtable = new Hashtable();
                foreach (var match in matchCollection)
                {
                    String record = match.ToString();
                    if (hashtable.Contains(record) == false)
                        hashtable.Add(record, string.Empty);
                }

                foreach (DictionaryEntry element in hashtable)
                {
                    Console.WriteLine(element.Key);
                }

                if (matchCollection[0] == null)
                {
                    Console.WriteLine("Nie znaleziono adresów email");
                }

                httpClient.Dispose();
                response.Dispose();
            }
            catch(IndexOutOfRangeException)
            {
                throw new Exception();
                Console.WriteLine("IndexOutOfRangeException");
            }
            catch(ArgumentNullException)
            {
                throw new Exception();
                Console.WriteLine("ArgumentNullException");
            }
            catch(InvalidOperationException)
            {
                throw new Exception();
                Console.WriteLine("InvalidOperationException");
            }
            catch(HttpRequestException)
            {
                throw new Exception();
                Console.WriteLine("Błąd w czasie pobierania strony");
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new Exception();
                Console.WriteLine("ArgumentOutOfRangeException");
            }
        }

        public void Dispose(bool v)
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
