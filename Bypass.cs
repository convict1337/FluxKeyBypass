using System; using System.Net.Http; using HtmlAgilityPack; using System.Threading.Tasks;

class P
{
    static readonly HttpClient c = new HttpClient();
    
    static async Task Main()
    {
        Console.Write("Enter your HWID: ");
        string h = Console.ReadLine();
        var s = new[] { $"https://flux.li/windows/start.php?updated_browser=true&HWID={h}", "https://fluxteam.net/windows/checkpoint/check1.php", "https://fluxteam.net/windows/checkpoint/check2.php", "https://fluxteam.net/windows/checkpoint/main.php" };
        
        foreach (var x in s) { await R(x); Console.WriteLine($"\nBypassed {x}"); }
        
        var r = await R(s[^1]);
        var d = new HtmlDocument();
        d.LoadHtml(await r.Content.ReadAsStringAsync());
        var k = d.DocumentNode.SelectSingleNode("//main/code[2]")?.InnerText.Trim();
        
        Console.WriteLine("\nYour key is: " + (k ?? "(unknown error)"));
        Console.WriteLine("Press any key to exit..."); Console.ReadKey();
    }
    
    static async Task<HttpResponseMessage> R(string u)
    {
        var r = new HttpRequestMessage(HttpMethod.Get, u); r.Headers.Add("Referer", "https://linkvertise.com/");
        return await c.SendAsync(r);
    }
}
