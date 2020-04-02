# Code Generation Templates

Orchard Core Templates uses `dotnet new` template configurations for creating new websites, themes and modules from the command shell.

More information about `dotnet new` can be found at <https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new>

## Installing the Orchard CMS templates

Once the .NET Core SDK has been installed, type the following command to install the templates for creating Orchard Core web applications:

```CMD
dotnet new -i OrchardCore.ProjectTemplates::1.0.0-rc1-*
```

This will use the most stable release of Orchard Core. In order to use the latest `dev` branch of Orchard Core, the following command can be used:

```CMD
dotnet new -i OrchardCore.ProjectTemplates::1.0.0-rc1-* --nuget-source https://www.myget.org/F/orchardcore-preview/api/v3/index.json  
```

## Create a new website

### From Command Shell (automated way)

#### Generate an Orchard Cms Web Application

```CMD
dotnet new occms  
```

The above command will use the default options.

You can pass the following CLI parameters to setup options

```CMD
Orchard Core Cms Web App (C#)
Author: Orchard Project
Options:
  -lo|--logger           Configures the logger component.
                             nlog       - Configures NLog as the logger component.
                             serilog    - Configures Serilog as the logger component.
                             none       - Do not use a logger.
                         Default: nlog

  -ov|--orchard-version  Specifies which version of Orchard Core packages to use.
                         string - Optional
                         Default: 1.0.0-rc1
```

Logging can be ignored with this command:

```CMD
dotnet new occms --logger none
```

#### Generate a modular ASP.NET MVC Core Web Application

```CMD
dotnet new ocmvc  
```

### From Visual Studio (manual way)

Fire up Visual Studio, create a new solution file (`.sln`) by creating a new ASP.NET Core Web Application :

![image](../assets/images/templates/orchard-screencast-1.gif)

Now that we created a new Web Application we need to add proper dependencies so that this new Web Application be targeted as an Orchard Core application.

See [Adding Orchard Core Nuget Feed](#adding-orchard-core-nuget-feed)

![image](../assets/images/templates/orchard-screencast-2.gif)

Finally we will need to register Orchard CMS service in our `Startup.cs` file like this :

```C#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyNewWebsite
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOrchardCms();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseOrchardCore();
        }
    }
}
```

## Create a new CMS module

### New module from Command Shell (automated way)

#### Module commands

```CMD
dotnet new ocmodulecms
```

The above command will use the default options.

You can pass the following CLI parameters to setup options

```CMD
Orchard Core Module (C#)
Author: Orchard Project
Options:
  -A|--AddPart           Add dependency injection for part in Startup.cs. If PartName is not provided, default name will be used
                         bool - Optional
                         Default: false / (*) true

  -P|--PartName          Add all files required for a part
                         string - Optional
                         Default: MyTest

  -ov|--orchard-version  Specifies which version of Orchard Core packages to use.
                         string - Optional
                         Default: 1.0.0-rc1
```

```CMD
dotnet new ocmodulecms -n ModuleName.OrchardCore

dotnet new ocmodulecms -n ModuleName.OrchardCore --AddPart true

dotnet new ocmodulecms -n ModuleName.OrchardCore --AddPart true --PartName TestPart 
```

### New module from Visual Studio (manual way)

Fire up Visual Studio, open Orchard Core solution file (`.sln`), select `OrchardCore.Modules` folder, right click and select "add --> new project" and create a new .NET Standard Class Library:

![image](../assets/images/templates/38450533-6c0fbc98-39ed-11e8-91a5-d26a1105b91a.png)

For marking this new Class Library as an Orchard Module we will now need to reference `OrchardCore.Module.Targets` Nuget package.

See [adding Orchard Core Nuget Feed](#adding-orchard-core-nuget-feed).

Each of these `*.Targets` Nuget packages are used to mark a Class Library as a specific Orchard Core functionality. `OrchardCore.Module.Targets` is the one we are interested in for now. We will mark our new Class Library as a module by adding `OrchardCore.Module.Targets` as a dependency. For doing so you will need to right click on `MyModule.OrchardCore` project and select "Manage Nuget Packages" option. To find the packages in Nuget Package Manager you will need to check "include prerelease" and make sure you have Orchard Core feed that we added earlier selected. Once you have found it, click on the Install button on the right panel next to Version : Latest prerelease x.x.x.x

![image](../assets/images/templates/38450558-f4b83098-39ed-11e8-93c7-0fd9e5112dff.png)

Once done, your new module will look like this :

![image](../assets/images/templates/38450628-31c8e2b0-39ef-11e8-9de7-c15f0c6544c5.png)

For Orchard Core to identify this module it will now require a `Manifest.cs` file. Here is an example of that file:

```C#
using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "TemplateModule.OrchardCore",
    Author = "The Orchard Team",
    Website = "http://orchardproject.net",
    Version = "0.0.1",
    Description = "Template module."
)]

```

For this module to start, we now will need to add a `Startup.cs` file to our new module. See this file as an example:  
[`OrchardCore.Templates.Cms.Module/Startup.cs`](https://github.com/OrchardCMS/OrchardCore/tree/dev/src/Templates/OrchardCore.ProjectTemplates/content/OrchardCore.Templates.Cms.Module/Startup.cs)

Last step is to add our new module to the `OrchardCore.Cms.Web` project as a reference for including it as part as our website modules. After that, you should be all set for starting building your custom module. You can refer to our [template module](https://github.com/OrchardCMS/OrchardCore/tree/dev/src/Templates/OrchardCore.ProjectTemplates/content/OrchardCore.Templates.Cms.Module/) for examples of what's basically needed normally.

## Create a new theme

### New theme From Command Shell (automated way)

#### Theme commands

`dotnet new octheme -n "ThemeName.OrchardCore"`

### New theme from Visual Studio (manual way)

Should be the same procedure as with modules, but instead we need to reference `OrchardCore.Theme.Targets`, and the `Manifest.cs` file differs slightly:

```C#
using OrchardCore.DisplayManagement.Manifest;

[assembly: Theme(
    Name = "TemplateTheme.OrchardCore",
    Author = "The Orchard Team",
    Website = "https://orchardproject.net",
    Version = "0.0.1",
    Description = "The TemplateTheme."
)]
```

## Adding Orchard Core Nuget Feed

In order to be able to use the __dev__ feed from Visual Studio, open the Tools menu under Nuget Package Manager --> Package Manager Settings.
The feed url is <https://www.myget.org/F/orchardcore-preview/api/v3/index.json>

![image](../assets/images/templates/38450422-63670f1c-39eb-11e8-9c14-0743f0a4da42.png)
