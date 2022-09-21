# log4net.DingtalkAppender

Send log to Dingtalk.

### log4net config

```xml
<log4net>
  <appender name="DingtalkAppender" type="log4net.Appender.DingtalkAppender, log4net.DingtalkAppender">
    <webhookUrl>{Your WebhookUrl}</webhookUrl>
    <secret>{Your Secret}</secret>
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%type{1}.%method%newline%message" />
    </layout>
    <emoticon>
      <notice>[广播]</notice>
      <warn>[流汗]</warn>
      <error>[发怒]</error>
    </emoticon>
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
