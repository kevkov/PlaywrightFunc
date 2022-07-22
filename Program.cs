using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

DirectoryInfo assemblyDirectory = new DirectoryInfo(AppContext.BaseDirectory);
Console.WriteLine(assemblyDirectory.FullName);

host.Run();
