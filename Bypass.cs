using System; using System.Net.Http; using HtmlAgilityPack; using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        Console.Write("Enter your HWID: ");
        string hardwareId = Console.ReadLine();
        var urls = new[] {$"https://flux.li/windows/start.php?updated_browser=true&HWID={hardwareId}","https://fluxteam.net/windows/checkpoint/check1.php","https://fluxteam.net/windows/checkpoint/check2.php","https://fluxteam.net/windows/checkpoint/main.php"};

        foreach (var url in urls)
        {
            await Request(url);
            Console.WriteLine($"\nBypassed {url}");
            await Task.Delay(1000);
        }

        var response = await Request(urls[^1]);
        var document = new HtmlDocument();
        document.LoadHtml(await response.Content.ReadAsStringAsync());
        var key = document.DocumentNode.SelectSingleNode("//main/code[2]")?.InnerText.Trim();

        Console.WriteLine("\nYour key is: " + (key ?? "(unknown error)"));
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static async Task<HttpResponseMessage> Request(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Referer", "https://linkvertise.com/");
        return await client.SendAsync(request);
    }
}
