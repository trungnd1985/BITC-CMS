﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.34014
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System


'This class was auto-generated by the StronglyTypedResourceBuilder
'class via a tool like ResGen or Visual Studio.
'To add or remove a member, edit your .ResX file then rerun ResGen
'with the /str option, or rebuild your VS project.
'''<summary>
'''  A strongly-typed resource class, for looking up localized strings, etc.
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
 Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
 Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
Public Class CommonResource
    
    Private Shared resourceMan As Global.System.Resources.ResourceManager
    
    Private Shared resourceCulture As Global.System.Globalization.CultureInfo
    
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
    Friend Sub New()
        MyBase.New
    End Sub
    
    '''<summary>
    '''  Returns the cached ResourceManager instance used by this class.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Public Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
        Get
            If Object.ReferenceEquals(resourceMan, Nothing) Then
                Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("BITC.CMS.Resource.CommonResource", GetType(CommonResource).Assembly)
                resourceMan = temp
            End If
            Return resourceMan
        End Get
    End Property
    
    '''<summary>
    '''  Overrides the current thread's CurrentUICulture property for all
    '''  resource lookups using this strongly typed resource class.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Public Shared Property Culture() As Global.System.Globalization.CultureInfo
        Get
            Return resourceCulture
        End Get
        Set
            resourceCulture = value
        End Set
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Add new.
    '''</summary>
    Public Shared ReadOnly Property AddNew() As String
        Get
            Return ResourceManager.GetString("AddNew", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Cancel.
    '''</summary>
    Public Shared ReadOnly Property Cancel() As String
        Get
            Return ResourceManager.GetString("Cancel", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Created by.
    '''</summary>
    Public Shared ReadOnly Property CreatedBy() As String
        Get
            Return ResourceManager.GetString("CreatedBy", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Created date.
    '''</summary>
    Public Shared ReadOnly Property CreatedDate() As String
        Get
            Return ResourceManager.GetString("CreatedDate", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Delete.
    '''</summary>
    Public Shared ReadOnly Property Delete() As String
        Get
            Return ResourceManager.GetString("Delete", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Description.
    '''</summary>
    Public Shared ReadOnly Property Description() As String
        Get
            Return ResourceManager.GetString("Description", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Edit.
    '''</summary>
    Public Shared ReadOnly Property Edit() As String
        Get
            Return ResourceManager.GetString("Edit", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Inactive.
    '''</summary>
    Public Shared ReadOnly Property Inactive() As String
        Get
            Return ResourceManager.GetString("Inactive", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Insert.
    '''</summary>
    Public Shared ReadOnly Property Insert() As String
        Get
            Return ResourceManager.GetString("Insert", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Page size.
    '''</summary>
    Public Shared ReadOnly Property PageSize() As String
        Get
            Return ResourceManager.GetString("PageSize", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Save.
    '''</summary>
    Public Shared ReadOnly Property Save() As String
        Get
            Return ResourceManager.GetString("Save", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Setting.
    '''</summary>
    Public Shared ReadOnly Property Setting() As String
        Get
            Return ResourceManager.GetString("Setting", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Sort order.
    '''</summary>
    Public Shared ReadOnly Property SortOrder() As String
        Get
            Return ResourceManager.GetString("SortOrder", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Looks up a localized string similar to Update.
    '''</summary>
    Public Shared ReadOnly Property Update() As String
        Get
            Return ResourceManager.GetString("Update", resourceCulture)
        End Get
    End Property
End Class
