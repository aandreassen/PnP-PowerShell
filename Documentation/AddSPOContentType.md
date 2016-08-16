#Add-SPOContentType
Adds a new content type
##Syntax
```powershell
Add-SPOContentType -Name <String> [-ContentTypeId <String>] [-Description <String>] [-Group <String>] [-ParentContentType <ContentType>] [-Web <WebPipeBind>]
```


##Parameters
Parameter|Type|Required|Description
---------|----|--------|-----------
|ContentTypeId|String|False|If specified, in the format of 0x0100233af432334r434343f32f3, will create a content type with the specific ID|
|Description|String|False|Specifies the description of the new content type|
|Group|String|False|Specifies the group of the new content type|
|Name|String|True|Specify the name of the new content type|
|ParentContentType|ContentType|False|Specifies the parent of the new content type|
|Web|WebPipeBind|False|The web to apply the command to. Omit this parameter to use the current web.|
##Examples

###Example 1
```powershell
PS:> Add-SPOContentType -Name "Project Document" -Description "Use for Contoso projects" -Group "Contoso Content Types" -ParentContentType $ct
```
This will add a new content type based on the parent content type stored in the $ct variable.
