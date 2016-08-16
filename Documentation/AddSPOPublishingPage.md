#Add-SPOPublishingPage
Adds a publishing page
##Syntax
```powershell
Add-SPOPublishingPage [-Title <String>] -PageName <String> -PageTemplateName <String> [-Publish [<SwitchParameter>]] [-Web <WebPipeBind>]
```


##Parameters
Parameter|Type|Required|Description
---------|----|--------|-----------
|PageName|String|True||
|PageTemplateName|String|True|The name of the page layout you want to use. Specify without the .aspx extension. So 'ArticleLeft' or 'BlankWebPartPage'|
|Publish|SwitchParameter|False|Publishes the page. Also Approves it if moderation is enabled on the Pages library.|
|Title|String|False||
|Web|WebPipeBind|False|The web to apply the command to. Omit this parameter to use the current web.|
