using HtmlAgilityPack; using System; using System.Linq; using System.Net.Http; using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient Client = new HttpClient();

    static async Task Main()
    {
        Console.Write("Enter your HWID: ");
        string hwid = Console.ReadLine();
        var urls = new[] { $"https://flux.li/windows/start.php?updated_browser=true&HWID={hwid}", "https://fluxteam.net/windows/checkpoint/check1.php", "https://fluxteam.net/windows/checkpoint/check2.php", "https://fluxteam.net/windows/checkpoint/main.php" };
        var responsetasks = urls.Select(Request);
        var document = new HtmlDocument();
        foreach (var responsetask in responsetasks)
        Console.WriteLine($"Bypassed {(await responsetask).Url}\n");
        document.LoadHtml((await Request(urls.Last())).Content);
        Console.WriteLine($"\nYour key is: {document.DocumentNode.SelectSingleNode("//main/code[2]")?.InnerText.Trim() ?? "(unknown error)"}\nPress any key to exit...");
        Console.ReadKey();
    }

    static async Task<Response> Request(string url)
    {
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36");
        Client.DefaultRequestHeaders.Referrer = new Uri("https://linkvertise.com/");
        return new Response(url, await Client.GetStringAsync(url));
    }

    class Response
    {
        public string Url { get; }
        public string Content { get; }
        public Response(string url, string content) => (Url, Content) = (url, content);
    }
}
