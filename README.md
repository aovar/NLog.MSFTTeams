# NLog.MSFTTeams
NLog target to send logging ta a Microsoft Teams channel (O365 Webhook Connector)

v. 0.1.4
v. 0.1.3
v. 0.1.2

Example Nlog.Config

<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <extensions>
    <add assembly="NLog.MSFTTeams" />
  </extensions>  <targets>
    <target xsi:type="MSFTTeamsTarget" name="msftTeams" layout="${longdate} ${uppercase:${level}} ${message}" CardTitle="User: " TextMarkdown="##### InstallationId: " ThemeColorError="FF0000" ThemeColorDefault="" WebHookUrl="[webhook URL as received from Teams Webhook Connector]"/>
  </targets>
  <rules>
    <logger name="*" minlevel="Info" writeTo="msftTeams" />
  </rules>
</nlog>

----