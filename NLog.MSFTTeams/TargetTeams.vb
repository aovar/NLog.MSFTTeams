Imports System.IO
Imports System.Net.Http
Imports NLog
Imports NLog.Common
Imports NLog.Config
Imports NLog.Targets
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Net.Http.Headers

<Target("MSFTTeamsTarget")>
Public Class TargetTeams
    Inherits TargetWithLayout

#Region " Properties"
    Private mWebHookUrl As String = ""
    <RequiredParameter>
    Public Property WebHookUrl As String
        Get
            Return mWebHookUrl
        End Get
        Set(value As String)
            mWebHookUrl = value
        End Set
    End Property

    <DefaultParameter>
    Public Property CardTitle As String = "NLog.MSFTTeams"

    <DefaultParameter>
    Public Property TextMarkDown As String = "NLog.MSFTTeams"

    <DefaultParameter>
    Public Property ThemeColorError As String = ""

    <DefaultParameter>
    Public Property ThemeColorDefault As String = ""

    Public Property LastInternalException As Exception

    Public Property LastResponse As HttpResponseMessage
#End Region

    Public Sub New()
        'Nothing to do here at the moment
    End Sub

    Protected Overrides Sub WriteAsyncThreadSafe(logEvent As AsyncLogEventInfo)
        Try
            Dim logMessage As String = Me.Layout.Render(logEvent.LogEvent)
            Dim conCard As ConnectorCard = Me.GetConnectorCard(logMessage, logEvent.LogEvent.Level)
            SendToWebhook(conCard)
        Catch ex As Exception
            LastInternalException = ex
        End Try
    End Sub

    Protected Overrides Sub Write(logEvent As AsyncLogEventInfo)
        Try
            Dim logMessage As String = Me.Layout.Render(logEvent.LogEvent)
            Dim conCard As ConnectorCard = Me.GetConnectorCard(logMessage, logEvent.LogEvent.Level)
            SendToWebhook(conCard)
        Catch ex As Exception
            LastInternalException = ex
        End Try
    End Sub

    Public Function GetConnectorCard(message As String, level As LogLevel)

        Dim conCard As ConnectorCard = New ConnectorCard()
        Dim tmpSection As Section

        With conCard
            .Title = Me.CardTitle

            If Not String.IsNullOrEmpty(Me.TextMarkDown) Then
                .Text = Me.TextMarkDown
            Else
                .Text = "\-\-\-\-\-\-\-\-\-\-\-"
            End If

            If level = LogLevel.Error Then
                .ThemeColorHex = Me.ThemeColorError
            Else
                .ThemeColorHex = Me.ThemeColorDefault
            End If

            'Section 1
            tmpSection = New Section
            With tmpSection
                .Markdown = True

                .Title = String.Format("Level: {0}", level.Name)
                .Text = "Log message:"
                .ActivityText = message.Replace("---", "\---")
            End With
            .Sections.Add(tmpSection)
        End With

        Return conCard
    End Function

    'Post a message using a Payload object
    Public Async Sub SendToWebhook(conCard As ConnectorCard)
        Dim payLoadJson As String = conCard.ToJson
        Dim content = New StringContent(payLoadJson)
        content.Headers.ContentType = New MediaTypeHeaderValue("application/json")
        Dim client = New HttpClient()
        Dim teamsUri = New Uri(WebHookUrl)
        LastResponse = Await client.PostAsync(teamsUri, content)
    End Sub

End Class



