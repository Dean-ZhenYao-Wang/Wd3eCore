# Content Fields (`OrchardCore.ContentFields`)

This module provides common content fields.

## Available Fields

| Name | Properties | Shape Type | Shape Class |
| --- | --- | --- | --- |
| `BooleanField` | `Value (bool)` | `BooleanField` | `DisplayBooleanFieldViewModel` |
| `ContentPickerField` | `ContentItemIds (string[])` | `ContentPickerField` | `DisplayContentPickerFieldViewModel` |
| `LocalizationSetContentPickerField` | `LocalizationSets (string[])` | `LocalizationSetContentPickerField` | `DisplayLocalizationSetContentPickerFieldViewModel` |
| `DateField` | `Value (DateTime?)` | `DateField` | `DisplayDateFieldViewModel` |
| `DateTimeField` | `Value (DateTime?)` | `DateTimeField` | `DisplayDateTimeFieldViewModel` |
| `HtmlField` | `Html (string)` | `HtmlField` | `DisplayHtmlFieldViewModel` |
| `LinkField` | `Url (string), Text (string)` | `LinkField` | `DisplayLinkFieldViewModel` |
| `NumericField` | `Value (decimal?)` | `NumericField` | `DisplayNumericFieldViewModel` |
| `TextField` | `Text (string)` | `TextField` | `DisplayTextFieldViewModel` |
| `TimeField` | `Value (TimeSpan?)` | `TimeField` | `DisplayTimeFieldViewModel` |
| `YoutubeField` | `EmbeddedAddress (string), RawAddress (string)` | `YoutubeField` | `YoutubeFieldDisplayViewModel` |

## Usage

From a `Content` template, you can reference a field's value like this
(if the content type is `Article` and has a Text Field named `MyField`):

``` liquid tab="Liquid"
{{ Model.ContentItem.Content.Article.MyField.Value }}
```

``` html tab="Razor"
var fieldValue = Model.ContentItem.Content.Article.MyField.Text;
```

From a field shape (see Shape Type in the table listing all the fields) you can also access properties specific to each view model.

### Common field properties

The convention for a field view model is to also expose these properties:

| Property | Description |
| --- | --- |
| `Field` | The ContentField. |
| `Part` | The ContentPart that contains the field. |
| `PartFieldDefinition` | The Content Part Field Definition that contains the part. Which also give access to the Content Type |

Some view models have special properties that are computed from the actual field data and which are more useful for templating.

### `HtmlField`

#### `DisplayHtmlFieldViewModel`

| Property | Description |
| --- | --- |
| `Html` | The processed HTML, once all liquid tags have been processed. |

#### Html Field Example

```liquid
{{ Model.Html }}
```

or, to display the raw content before the tags are converted:

```liquid
{{ Model.Field.Html }}
```

### `DateTimeField`

#### `DisplayDateTimeFieldViewModel`

| Property | Description |
| --- | --- |
| `LocalDateTime` | The date time in the time zone of the site. |

#### DateTime Field Example

```liquid
{{ Model.LocalDateTime }}
```

or, to display the UTC value before it is converted:

```liquid
{{ Model.Value }}
```

### `ContentPickerField`

#### ContentPicker Field Example

```liquid
{% assign contentItems = Model.ContentItemIds | content_item_id %}
{% for contentItem in contentItems %}
    {{ contentItem.DisplayText }}
{% endfor %}
```

```html tab="Razor"
@foreach (var contentItem in await Orchard.GetContentItemsByIdAsync(Model.ContentItemIds))
{
    @contentItem.DisplayText
}
```

Or to render the referenced content item:

``` liquid tab="Liquid"
{% assign contentItems = Model.ContentItemIds | content_item_id %}
{% for contentItem in contentItems %}
    {{ contentItem | shape_build_display: "Detail" | shape_render }}
{% endfor %}
```

``` html tab="Razor"
@foreach (var contentItem in await Orchard.GetContentItemsByIdAsync(Model.ContentItemIds))
{
    @await Orchard.DisplayAsync(contentItem, "Detail")
}
```

### `LocalizationSetContentPickerField`

This field allows you to store the `LocalizationSet` of a `ContentItem`, when a reference shouldn't point to a specific culture of a content item.  
This simplifies getting a content item of the correct culture on the frontend.

The following example uses the `localization_set` liquid filter which returns a single ContentItem 
per set based on the request culture, if no culture is specified.

#### LocalizationSet ContentPicker Field Example

```liquid
{% assign contentItems = Model.LocalizationSets | localization_set %}
{% for contentItem in contentItems %}
    {{ contentItem.DisplayText }}
{% endfor %}
```

``` html tab="Razor"
@model OrchardCore.ContentFields.ViewModels.DisplayLocalizationSetContentPickerFieldViewModel
@using Microsoft.AspNetCore.Localization

@inject OrchardCore.ContentLocalization.IContentLocalizationManager ContentLocalizationManager;

@{
    var currentCulture = Context.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
    var contentItems = await ContentLocalizationManager.GetItemsForSetsAsync(Model.LocalizationSets, currentCulture);
}
foreach (var contentItem in contentItems)
{
    <span class="value">@contentItem.DisplayText</span>
    if (contentItem != contentItems.Last())
    {
        <span>,</span>
    }
}

```

## Creating Custom Fields

### What to extend

Before creating a new field, the solution might be to provide a custom editor and formatter instead.

A field should represent some specific physical data and logical data. The same field can be customized
to be edited and rendered differently using both Editors and Formatters. Editors are shapes that can
be used to edit a field differently, for instance the WYSIWYG HTML editor is a custom editor for the HTML
field. Formatters are alternate shapes that can be used to render a field on the front end, for instance
a `Link` field could be rendered as a Youtube video player.

### Model Class

Create a class inheriting from `ContentField` that will represent the state of your field.
Its content will be serialized as part of the content item.
Json.NET classes can be used to customize the serialization.

Example:

```csharp
public class TextField : ContentField
{
    public string Text { get; set; }
}
```

This class needs to be registered in the DI like this:

```csharp
services.AddContentField<TextField>();
```

### Display Driver

The display driver is the component that drives how the field is displayed on the front end, edited on
the admin, updated and validated.

Create a class inheriting from `ContentFieldDisplayDriver<TextField>` and implement the three methods 
`Display`, `Edit` and `DisplayAsync` by looking at examples from this module.

This class needs to be registered in the DI like this:

```csharp
services.AddContentField<TextField>()
    .UseDisplayDriver<TextFieldDisplayDriver>();
```

This will register the display driver for use with all display modes and editors.

## Creating Custom Display Modes

For each field, the convention is to create an alternate that can target different display modes. To provide
a new choice in the list of available editors for a field, create a new shape template that matches this
TextField-Header.DisplayOption
template: `{FIELDTYPE}_DisplayOption__{DISPLAYMODE}`
This shape type will match a template file named `{FIELDTYPE}-{DISPLAYMODE}.DisplayOption.cshtml`

This template will need to render an `<option>` tag. Here is an example for a Header display mode options on the Text Field:

``` html
@{
    string currentDisplayMode = Model.DisplayMode;
}
<option value="Header" selected="@(currentDisplayMode == "Header")">@T["Header"]</option>
```

Then, you can create the display mode shape by adding a file named `{FIELDTYPE}_Display__{DISPLAYMODE}` which is
represented by a template file named `{FIELDTYPE}-{DISPLAYMODE}.Display.cshtml`.

For instance, the filename for the Header Display Mode on the Text Field is named `TextField-Header.Display.cshtml`.

## Creating Custom Editors

For each field, the convention is to create an alternate that can target different editors. To provide
a new choice in the list of available editors for a field, create a new shape template that matches this
template: `{FIELDTYPE}_Option__{EDITORNAME}`
This shape type will match a template file named `{FIELDTYPE}-{EDITORNAME}.Option.cshtml`

This template will need to render an `<option>` tag. Here is an example for a Wysiwyg options on the Html Field:

``` html tab="Razor"
@{
    string currentEditor = Model.Editor;
}
<option value="Wysiwyg" selected="@(currentEditor == "Wysiwyg")">@T["Wysiwyg editor"]</option>
```

Then you can create the editor shape by adding a file named `{FIELDTYPE}_Editor__{EDITORNAME}` which is
represented by a template file named `{FIELDTYPE}-{EDITORNAME}.Editor.cshtml`.

For instance the filename for the Wysiwyg editor on the Html Field is named `HtmlField-Wysiwyg.Editor.cshtml`.

### Customising Display Driver Registration

For both Display Modes and Editors you can customise the Display Driver that will be resolved for the particular mode.

This allows you to create custom display drivers that might return a different ViewModel to the standard Display Driver.

Alter the registration for an existing Field Type or Driver Type in your modules `Startup.cs`

```csharp
services.AddContentField<TextField>()
    .ForDisplayMode<TextFieldDisplayDriver>(d => String.IsNullOrEmpty(d))
    .ForDisplayMode<MyCustomTextFieldDisplayDriver>(d => d == "MyCustomDisplayMode");
```

This example will alter the registration for the `TextFieldDisplayDriver` to resolve for only the Standard (null)
display mode, and register `MyCustomTextFieldDisplayDriver` to resolve for only the custom display mode.

```csharp
services.AddContentField<TextField>()
    .ForEditor<TextFieldDisplayDriver>(d => d != "MyCustomEditor")
    .ForEditor<MyCustomTextFieldDisplayDriver>(d => d == "MyCustomEditor");
```

This example will alter the registration for the `TextFieldDisplayDriver` to resolve for all editors except the custom editor,
and register `MyCustomTextFieldDisplayDriver` to resolve for only the custom editor.

!!! note
    When registering a custom display mode or editor driver you must alter the registrations for existing drivers.
    You should also take a dependency in your modules `Manifest.cs` on the module that the fields reside in.
    This will make your modules `Startup.cs` run later, and allow your registrations to override the original modules. 

## CREDITS

### bootstrap-slider

<https://github.com/seiyria/bootstrap-slider>  
Copyright (c) 2017 Kyle Kemp, Rohit Kalkur, and contributors  
License: MIT

### Bootstrap Switch

<https://github.com/Bttstrp/bootstrap-switch>  
Copyright (c) 2013-2015 The authors of Bootstrap Switch  
License: MIT
