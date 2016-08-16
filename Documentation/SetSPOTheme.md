#Set-SPOTheme
Sets the theme of the current web.
##Syntax
```powershell
Set-SPOTheme [-ColorPaletteUrl <String>] [-FontSchemeUrl <String>] [-BackgroundImageUrl <String>] [-ShareGenerated [<SwitchParameter>]] [-Web <WebPipeBind>]
```


##Detailed Description
 Sets the theme of the current web, if any of the attributes is not set, that value will be set to null

##Parameters
Parameter|Type|Required|Description
---------|----|--------|-----------
|BackgroundImageUrl|String|False|Specifies the Background Image Url based on the server relative url|
|ColorPaletteUrl|String|False|Specifies the Color Palette Url based on the site relative url|
|FontSchemeUrl|String|False|Specifies the Font Scheme Url based on the server relative url|
|ShareGenerated|SwitchParameter|False|true if the generated theme files should be placed in the root web, false to store them in this web. Default is false|
|Web|WebPipeBind|False|The web to apply the command to. Omit this parameter to use the current web.|
##Examples

###Example 1
```powershell
PS:> Set-SPOTheme
```
Removes the current theme

###Example 2
```powershell
PS:> Set-SPOTheme -ColorPaletteUrl /_catalogs/theme/15/company.spcolor
```


###Example 3
```powershell
PS:> Set-SPOTheme -ColorPaletteUrl /_catalogs/theme/15/company.spcolor -BackgroundImageUrl '/sites/teamsite/style library/background.png'
```

