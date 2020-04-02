# Menu (`OrchardCore.Menu`)

## Shapes

### `Menu`

The `Menu` shape is used to render a Menu.

| Property | Description |
| --------- | ------------ |
| `Model.ContentItemId` | If defined, contains the content item identifier of the menu to render. |
| `Model.Items` | The list of menu items shapes for the menu. These are shapes of type `MenuItem`. |
| `Model.Differentiator` | If defined, contains the formatted name of the menu (title). For instance `MainMenu`. |

#### Menu Alternates

| Definition | Template | Filename|
| ---------- | --------- | ------------ |
| `Menu__[Differentiator]` | `Menu__MainMenu` | `Menu-MainMenu.cshtml` |

#### Menu Example

``` liquid tab="Liquid"
<nav>
    <ul class="nav navbar-nav {{ Model.Classes | join: " " }}">
        {% for item in Model.Items %}
            {{ item | shape_render }}
        {% endfor %}
    </ul>
</nav>
```

``` html tab="Razor"
@{
    TagBuilder tag = Tag(Model, "ul");
    tag.AddCssClass("nav navbar-nav");

    foreach (var item in Model.Items)
    {
        tag.InnerHtml.AppendHtml(await DisplayAsync(item));
    }
}

@tag
```

### `MenuItem`

The `MenuItem` shape is used to render a menu item.

| Property | Description |
| --------- | ------------ |
| `Model.Menu` | The `Menu` shape owning this item. |
| `Model.ContentItem` | The content item representing this menu item. |
| `Model.Level` | The level of the menu item. `0` for top level menu items. |
| `Model.Items` | The list of sub menu items shapes. These are shapes of type `MenuItem`. |
| `Model.Differentiator` | If defined, contains the formatted name of the menu. For instance `MainMenu`. |

#### MenuItem Alternates

| Definition | Template | Filename|
| ---------- | --------- | ------------ |
| `MenuItem__level__[level]` | `MenuItem__level__2` | `MenuItem-level-2.cshtml` |
| `MenuItem__[ContentType]` | `MenuItem__HtmlMenuItem` | `MenuItem-HtmlMenuItem.cshtml` |
| `MenuItem__[ContentType]__level__[level]` | `MenuItem__HtmlMenuItem__level__2` | `MenuItem-HtmlMenuItem-level-2.cshtml` |
| `MenuItem__[MenuName]` | `MenuItem__MainMenu` | `MenuItem-MainMenu.cshtml` |
| `MenuItem__[MenuName]__level__[level]` | `MenuItem__MainMenu__level__2` | `MenuItem-MainMenu-level-2.cshtml` |
| `MenuItem__[MenuName]__[ContentType]` | `MenuItem__MainMenu__HtmlMenuItem` | `MenuItem-MainMenu-HtmlMenuItem.cshtml` |
| `MenuItem__[MenuName]__[ContentType]__level__[level]` | `MenuItem__MainMenu__HtmlMenuItem__level__2` | `MenuItem-MainMenu-HtmlMenuItem-level-2.cshtml` |

#### MenuItem Example

```liquid
<li class="nav-item{% if Model.HasItems %} dropdown{% endif %}">
    {% shape_clear_alternates Model %}
    {% shape_type Model "MenuItemLink" %}
    {{ Model | shape_render }}
    {% if Model.HasItems %}
    <div class="dropdown-menu">
        {% for item in Model.Items %}
        {{ item | shape_render }}
        {% endfor %}
    </div>
    {% endif %}
</li>
```

``` html tab="Razor"
@{
    TagBuilder tag = Tag(Model, "li");

    if ((bool)Model.HasItems)
    {
        tag.AddCssClass("dropdown");
    }

    // Morphing the shape to keep Model untouched
    Model.Metadata.Alternates.Clear();
    Model.Metadata.Type = "MenuItemLink";

    tag.InnerHtml.AppendHtml(await DisplayAsync(Model));

    if ((bool)(Model.HasItems))
    {
        TagBuilder parentTag = Tag(Model, "div");
        parentTag.AddCssClass("dropdown-menu");

        foreach (var item in Model.Items)
        {
            item.ParentTag = parentTag;
            parentTag.InnerHtml.AppendHtml(await DisplayAsync(item));
        }

        tag.InnerHtml.AppendHtml(parentTag);
    }
}

@tag
```

### `MenuItemLink`

The `MenuItemLink` shape is used to render a menu item link.
This shape is created by morphing a `MenuItem` shape into a `MenuItemLink`. Hence all the properties
available on the `MenuItem` shape are still available.

| Property | Description |
| --------- | ------------ |
| `Model.Menu` | The `Menu` shape owning this item. |
| `Model.ContentItem` | The content item representing this menu item. |
| `Model.Level` | The level of the menu item. `0` for top level menu items. |
| `Model.Items` | The list of sub menu items shapes. These are shapes of type `MenuItem`. |
| `Model.Differentiator` | If defined, contains the formatted name of the menu. For instance `MainMenu`. |

#### `MenuItemLink` Alternates

| Definition | Template | Filename|
| ---------- | --------- | ------------ |
| `MenuItemLink__level__[level]` | `MenuItemLink__level__2` | `MenuItemLink-level-2.cshtml` |
| `MenuItemLink__[ContentType]` | `MenuItemLink__HtmlMenuItem` | `MenuItemLink-HtmlMenuItem.cshtml` |
| `MenuItemLink__[ContentType]__level__[level]` | `MenuItemLink__HtmlMenuItem__level__2` | `MenuItemLink-HtmlMenuItem-level-2.cshtml` |
| `MenuItemLink__[MenuName]` | `MenuItemLink__MainMenu` | `MenuItemLink-MainMenu.cshtml` |
| `MenuItemLink__[MenuName]__level__[level]` | `MenuItemLink__MainMenu__level__2` | `MenuItemLink-MainMenu-level-2.cshtml` |
| `MenuItemLink__[MenuName]__[ContentType]` | `MenuItemLink__MainMenu__HtmlMenuItem` | `MenuItemLink-MainMenu-HtmlMenuItem.cshtml` |
| `MenuItemLink__[MenuName]__[ContentType]__level__[level]` | `MenuItemLink__MainMenu__HtmlMenuItem__level__2` | `MenuItemLink-MainMenu-HtmlMenuItem-level-2.cshtml` |

#### `MenuItemLink` Example

``` liquid tab="Liquid"
{% assign link = Model.ContentItem.Content.LinkMenuItemPart %}

{% if Model.HasItems %}
    <a href="{{ link.Url | href }}" class="nav-link dropdown-toggle">{{ link.Name }}<b class="caret"></b></a>
{% else %}
    <a href="{{ link.Url | href }}" class="nav-link">{{ link.Name }}</a>
{% endif %}
```

``` html tab="Razor"
@using OrchardCore.ContentManagement

@{
    ContentItem contentItem = Model.ContentItem;
    var link = contentItem.Content["LinkMenuItemPart"];
}

if ((bool)(Model.HasItems))
{
    <a class="nav-link dropdown-toggle" data-toggle="dropdown" href="@Url.Content((string)link.Url)">@link.Name<b class="caret"></b></a>
}
else
{
    <a class="nav-link" href="@Url.Content((string)link.Url)">@link.Name</a>
}
```

## CREDITS

### nestedSortable jQuery plugin

<https://github.com/ilikenwf/nestedSortable>  
License: MIT
