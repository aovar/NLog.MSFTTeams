
Imports System.Runtime.Serialization

<DataContract>
Public Class Section
    <DataMember(Name:="title")>
    Public Property Title As String ' title	String	
    <DataMember(Name:="activityTitle")>
    Public Property ActivityTitle As String ' activityTitle	String	
    <DataMember(Name:="activitySubtitle")>
    Public Property ActivitySubtitle As String 'activitySubtitle	String	
    <DataMember(Name:="activityImage")>
    Public Property ActivityImageUrl As String ' activityImage	URL To image	
    <DataMember(Name:="activityText")>
    Public Property ActivityText As String 'activityText	String	
    <DataMember(Name:="text")>
    Public Property Text As String 'text	String	Optional 
    <DataMember(Name:="markdown")>
    Public Property Markdown As Boolean 'markdown	bool

    'Awaits implementation 'facts Fact[], images Image[]	
End Class
