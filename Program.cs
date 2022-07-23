using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

DirectoryInfo assemblyDirectory = new DirectoryInfo(AppContext.BaseDirectory);
Console.WriteLine(assemblyDirectory.FullName);

var exitCode = Microsoft.Playwright.Program.Main(new[] { "install", "--with-deps", "chromium" });
if (exitCode != 0)
{
    throw new Exception($"Playwright exited with code {exitCode}");
}

host.Run();
