# OrchardCore.Logging.Serilog

`OrchardCore.Logging.Serilog` integrates [Serilog](https://serilog.net/) structured logging with OrchardCore

## How to use

add a reference to `OrchardCore.Logging.Serilog`

### add serilog configuration in appsettings.json

``` json
  "Serilog": {
    "MinimumLevel": {
      "Default": "Warning",
      "Override": {
        "Default": "Warning",
        "Microsoft": "Error",
        "System": "Error"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
          "outputTemplate": "{Timestamp:HH:mm:ss}|{TenantName}|{RequestId}|{SourceContext}|{Level:u3}|{Message:lj}{NewLine}{Exception}",
          "restrictedToMinimumLevel": "Information"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "app_data/logs/orchard-log.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.ffff}|{TenantName}|{RequestId}|{SourceContext}|{Level:u3}|{Message:lj}{NewLine}{Exception}",
          "restrictedToMinimumLevel": "Warning"
        }
      }
    ]
  }
```

### Modify `program.cs` to use Serilog

``` csharp
        using Serilog;
        ...
        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.ClearProviders())
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    .UseStartup<Startup>())
                .Build();
```

### Modify `startup.cs` to include TenantName in LogContext

``` csharp
        using OrchardCore.Logging;
        ...
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseOrchardCore(c => c.UseSerilogTenantNameLogging());
        }
```

## Credits

### Serilog

<https://github.com/serilog/serilog-aspnetcore>

Apache-2.0 License
