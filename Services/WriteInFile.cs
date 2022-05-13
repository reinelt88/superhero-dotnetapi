namespace SuperHeroAPI.Services;

public class WriteInFile : IHostedService
{
    private readonly IWebHostEnvironment env;
    private readonly string fileName = "log.txt";
    private Timer timer;

    public WriteInFile(IWebHostEnvironment env)
    {
        this.env = env;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(doWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        Write("Process Started at: " + DateTime.Now.ToString());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer.Dispose();
        Write("Process Ended at: " + DateTime.Now.ToString());
        return Task.CompletedTask;
    }
    
    public void doWork(object state)
    {
        Write("Process Started at: " + DateTime.Now.ToString());
    }
    
    public void Write(string message)
    {
        var path = $@"{env.ContentRootPath}\wwwroot\{fileName}";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
        }
    }
}