using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace webcore001_Test
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.252.54:30501");
            HttpResponseMessage result = await Policy
              .Handle<Exception>()
              .WaitAndRetryAsync(new TimeSpan[] {
                  TimeSpan.FromSeconds(1),
                  TimeSpan.FromSeconds(3),
                  TimeSpan.FromSeconds(5)
              })
              .ExecuteAsync(() =>
              {
                  Console.WriteLine(DateTime.Now);
                  var response = client.GetAsync("/waittime?second=60000");
                  return response;
              });
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine("{0},{1}", result.StatusCode, content);
            Console.ReadLine();
        }
    }
}
