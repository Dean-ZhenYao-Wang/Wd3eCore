# Scripting `OrchardCore.Scripting`

## Purpose

The scripting module provides an API allowing you to evaluate custom scripts in different languages.

## Usage

### Executing some script

The main interface is `IScriptingManager`.

```csharp
public interface IScriptingManager
{
    IScriptingEngine GetScriptingEngine(string prefix);
    object Evaluate(string directive);  
    IList<IGlobalMethodProvider> GlobalMethodProviders { get; }
}
```

To evaluate an expression using a scripting engine, you must know which ones are available in the system.  
For instance, a JavaScript one is available by default and its prefix is `js`.  
To return the current date and time as a string we could do something like this:

```csharp
var scriptingManager = serviceProvider.GetService<IScriptingManager>();
var date = scriptingManager.Evaluate("js: Date().toISOString()");
```

The `js:` prefix is used to describe in which language the code is written. Any module can provide
a new scripting engine by implementing the `IScriptingEngine` interface.

### Customizing the scripting environment

Any module can provide custom methods for scripts independently of the chosen language.  
For instance the `Contents` module provides a `uuid()` helper method that computes a unique content item identifier.

To create a global method, implement `IGlobalMethodProvider` then add it to the current `IScriptingManager` instance like this:

```csharp
var scriptingManager = serviceProvider.GetService<IScriptingManager>();
var globalMethodProvider = new MyGlobalMethodProvider();
scriptingManager.GlobalMethodProviders.Add(globalMethodProvider);
```
Or register your GlobalMethodProvider as a singleton in your Module's `Startup`
```csharp
 services.AddSingleton<IGlobalMethodProvider, MyGlobalMethodProvider>();
```

## File

The File scripting engine provides methods to read file contents.

| Name | Example | Description |
| ---- | ---- | -------- |
| `text` | `file:text('../wwwroot/template.html')` | Returns the content of a text file. |
| `base64` | `file:base64('../wwwroot/image.jpg')` | Returns the base64 encoded content of a file. |


## JavaScript `OrchardCore.Scripting.JavaScript`

The JavaScript scripting module implements a `IScriptingEngine` that uses [Esprima.NET](https://github.com/sebastienros/esprima-dotnet) to evaluate scripts.

### Methods

Here is a list of javascript methods provided by Orchard Modules.

#### Generic functions

| Function | Description 
| -------- | ----------- |
| `log(level: String, text: String, param: Object): void` | Formats and writes a log message at the specified log level. |
| `uuid(): String` | Generates a unique identifier for a content item. |
| `base64(String): String` | Decodes the specified string from Base64 encoding. Use https://www.base64-image.de/ to convert your files to base64. |
| `html(String): String` | Decodes the specified string from HTML encoding. |
| `gzip(String): String` | Decodes the specified string from gzip/base64 encoding. Use http://www.txtwizard.net/compression to gzip your strings. |
| `variables()` | Declare variables at the root of a recipe. Ex: `"variables": { "blogContentItemId": "[js:uuid()]" }`  Retrieve a variable value like this: `"ContentItemId": "[js: variables('blogContentItemId')]"` |
| `parameters()` | Retrieves the parameters specified during the setup. Ex: `"Owner": "[js: parameters('AdminUsername')]"` See the available [Setup Recipe parameters](../Setup/#recipe-parameters) |
| `configuration()` | Retrieves the specified configuration setting by its name. |

#### Content (`OrchardCore.Contents`)

| Function | Description 
| -------- | ----------- |
|`newContentItem(contentTypeName: String): IContent`| Creates a new instance of a ContentType (does not persist)|
|`createContentItem(contentTypeName: String, publish: Boolean, properties: Object): IContent`| Creates and persists a new ContentItem. Conditionally publishes it. |
|`updateContentItem(contentItem: IContent, properties: Object)`| Updates an existing content item with the properties |
|`deleteContentItem(contentItem: IContent)`| Deletes an existing content item |
|`getUrlPrefix(path: String): String `| Prefixes the path with the Tenant prefix (if specified) |

#### Layers (`OrchardCore.Layers`)

| Function | Description 
| -------- | ----------- |
| `isHomepage(): Boolean` | Returns true if the current request Url is the current homepage |
| `isAnonymous(): Boolean` | Returns true if there is no authenticated user on the current request |
| `isAuthenticated(): Boolean` | Returns true if there is an authenticated user on the current request |
| `url(url: String): Boolean` | Returns true if the current url matches the provided url. Add a `*` to the end of the url parameter to match any url that start with  |
| `culture(name: String): Boolean` | Returns true if the current culture name or the current culture's parent name matches the `name` argument |

#### Queries (`OrchardCore.Queries`)

| Function | Description 
| -------- | ----------- |
| `executeQuery(name: String, parameters: Dictionary<string,object>): IEnumerable<object>` | Returns the result of the query. |


#### HTTP (`OrchardCore.Workflows.Http`)

| Function | Description 
| -------- | ----------- |
|  `httpContext(): HttpContext` | Returns the `HttpContext` which encapsulates all HTTP-specific information about an individual HTTP request. |
| `queryString(name: String): String | Array` | Returns the entire query string (including the leading `?`) when invoked with no arguments, or the value(s) of the parameter name passed in as an argument. |
| `responseWrite(text: String): void` | Writes the argument string directly to the HTTP response stream. |
| `absoluteUrl(relativePath: String): String` | Returns the absolute URL for the relative path argument.  |
| `readBody(): String` | Returns the raw HTTP request body.  |
| `requestForm(name: String): String | Array` | Returns the value(s) of the form field name passed in as an argument. |
| `queryStringAsJson(): JObject` | Returns the entire query string as a JSON object. Example: ` { "param1": [ "param1-value1", "param1-value2" ], "param2": [ "param2-value1", "param2-value2" ], ... }` |
| `requestFormAsJson(): JObject` | Returns the entire request form as a JSON object. Example: ` { "field1": [ "field1-value1", "field1-value2" ], "field2": [ "field2-value1", "field2-value2" ], ... }` |


#### Workflows (`OrchardCore.Workflows.Http`)

The following JavaScript functions are available by default to any workflow activity that supports script expressions:

| Function | Description |
| -------- | ----------- |
| `workflow(): WorkflowExecutionContext` | Returns the `WorkflowExecutionContext` which provides access to all information related to the current workflow execution context. |
| `workflowId(): String` | Returns the unique workflow ID. |
| `input(name: String): Any` | Returns the input parameter with the specified name. Input to the workflow is provided when the workflow is executed by the workflow manager. |
| `output(name: String, value: Any): void` | Sets an output parameter with the specified name. Workflow output can be collected by the invoker of the workflow. |
|  `property(name: String): Any` | Returns the property value with the specified name. Properties are a dictionary that workflow activities can read and write information from and to. |
| `lastResult(): Any` | Returns the value that the previous activity provided, if any. |
| `correlationId(): String` | Returns the correlation value of the workflow instance. |
| `signalUrl(signal: String): String` | Returns workflow trigger URL with a protected SAS token into which the specified signal name is encoded. Use this to generate URLs that can be shared with trusted parties to trigger the current workflow if it is blocked on the Signal activity that is configured with the same signal name. |
|`setOutcome(outcome: String): void` | Adds the provided outcome to the list of outcomes of the current activity |
| `createWorkflowToken(workflowTypeId: String, activityId: String, expiresInDays: Integer): String` | Generates a workflow SAS token for the specidied workflowTypeid, activityId. You can also set the expiration date in number of days. |
