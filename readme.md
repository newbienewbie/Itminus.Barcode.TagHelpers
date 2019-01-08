

## How to Install

This libarary runs flawlessly on Windows. However, since the implementation depends on `System.Drawing.Common`, so if we want to use it on Linux/Mac OS, we need install the `libgdiplus` firstly.

```
> yum whatprovides libgdiplus
Loaded plugins: fastestmirror, langpacks
Loading mirror speeds from cached hostfile
libgdiplus-2.10-10.el7.x86_64 : An Open Source implementation of the GDI+ API
Repo        : epel
```

and now we can install the `libgdiplus` package :

```
> yum install libgdiplus-2.10-10.el7.x86_64
```

For development (dotnet-core sdk), we need install the `libgdiplush-devel` 

```
> yum whatprovides libgdiplus-devel
> yum install libgdiplus-devel-2.10-10.el7.x86_64
```


## How to Use

add TagHelper :

```csharp
@addTagHelper *, Itminus.Barcode.TagHelpers
```

and use the TagHelper to render barcode as you like :

```html
    <BitmapBarcode content="@Model.Content" alt="@Model.Alt"
        barcode-format="QR_CODE"
        width="@Model.Width" height="@Model.Height" margin="@Model.Margin"
        image-format="System.Drawing.Imaging.ImageFormat.Png"
    >
    </BitmapBarcode>
```

```html
    <SvgBarcode content="Hello" alt="world"
        barcode-format="QR_CODE"
        width="300" height="400" margin="10"
    >
    </SvgBarcode>
```