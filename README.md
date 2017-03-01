# log4net.DingtalkAppender

Send log to Dingtalk.

### log4net config

```xml
<log4net>
  <appender name="DingtalkAppender" type="log4net.Appender.DingtalkAppender, log4net.DingtalkAppender">
    <webhookUrl>{Your WebhookUrl}</webhookUrl>
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%type{1}.%method%newline%message" />
    </layout>
    <filter type="log4net.Filter.LevelRangeFilter">
      <levelMin value="NOTICE"/>
      <levelMax value="ERROR"/>
    </filter>
  </appender>
  <root>
    <level value="INFO"/>
    <appender-ref ref="DingtalkAppender"/>
  </root>
</log4net>
```
