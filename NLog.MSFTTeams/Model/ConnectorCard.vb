Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json

<DataContract>
Public Class ConnectorCard

    <DataMember(Name:="title")>
    Public Property Title As String

    <DataMember(Name:="text")>
    Public Property Text As String

    Private mThemeColorHex
    <DataMember(Name:="themeColor")>
    Public Property ThemeColorHex As String
        Get
            Return mThemeColorHex
        End Get
        Set(value As String)
            mThemeColorHex = value
            If Not mThemeColorHex.StartsWith("#") Then
                mThemeColorHex = "#" & mThemeColorHex
            End If
        End Set
    End Property

    <DataMember(Name:="summary")>
    Public Property Summary As String

    Private mSections As List(Of Section) = Nothing
    <DataMember(Name:="sections")>
    Public Property Sections As List(Of Section)
        Get
            If mSections Is Nothing Then
                mSections = New List(Of Section)
            End If
            Return mSections
        End Get
        Set(value As List(Of Section))
            mSections = value
        End Set
    End Property

    Public Function ToJson() As String

        Dim serializer = New DataContractJsonSerializer(GetType(ConnectorCard))
        'Dim memoryStream As MemoryStream
        Using MemStream = New MemoryStream

            serializer.WriteObject(MemStream, Me)
            MemStream.Position = 0

            Using reader = New StreamReader(MemStream)
                Dim json As String = reader.ReadToEnd()
                Return json
            End Using
        End Using
    End Function
End Class