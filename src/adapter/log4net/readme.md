# <center>log4net使用</center>

## 概论

log4net主要由Appender,Logger,Filter,Layout和一个Object Render组成。

* Appender(附着器)主要用来确定日志记录的位置，也可以这么说就是确定日志是记录到数据库还是文件或其他终端。

* Logger(记录器) 记录器 看字面意思就应该可以猜到他就是用来确定要发送的日志信息。比如说，有一个异常发生，我们需要记录什么数据，都是由Logger负责的。

* Filter(过滤器) 可以这么理解，这个和MVC中的Filter还是有差别的，这里的Filter表示的是过滤，就像筛子 可以过滤一样，而MVC中的Filter也可以叫做过滤器，但是它更多的是完成Aop(面向切面或面向方面) 的横向的过滤导向，也可以认为是对某个Action或Controller施加的限制。

* Layout(布局) 布局很容易理解，就像装修一样，把我们要记录的数据整理成我们需要的格式。

* object render我在官方文章看了一下，个人理解就相当于渲染输出，就像服务器发送html到浏览器以后，浏览器要渲染输出的效果应该是一样。



简单的描述了一下log4net的组成，还有很重要的就是log4net的分级，其中log4net一种氛围5级，按从高到低的顺序依次为：

off>fatal>Error>Warn>Info>Debug>All

看到这个分级，大家一定要骂我了，一共有七个值，偏偏说5个分级，这里我解释一下:

* off   意思就是关闭log4net的日志输出，
* fatal 的级别最高，表示的就是特别严重的错误，一般是在应用程序崩溃的时候
* error 则是表示错误出现，一般用在出现了异常以后，
* Warn  表示警告，这个不会使程序出现异常，但是可能会影响程序性能，
* info  和debug则可以随意了
* ALL   表示的就是所有的分级都可以满足。

这里大家要注意一下的就是，这个分级是会影响程序日志输出的。比如说你在程序中定义了 log.Info(message) 和log.debug(message),但是你定义的日志应当输出的级别是info，很抱歉的告诉你，log.debug(message)中的信息不会写入日志，因为debug在顺序中是小于info的。

说了log4net中最重要的两个部分，现在就需要正式的启用log4net来输出日志信息了，签于园友们都有很详细的配置信息，所以我就把主要的两个简单的描述一下，如果有需要我会在注释中写入的。在项目中我们一般会用到输出日志到文件或者数据库中，这两种方式，我个人也非常喜欢这两种方式。一般我会把日志同时输出到文件和数据库中，虽然会有一点的性能损失，但是对于日志的完整性还是十分值得的，当然输出的日志越少性能会越高，大家都懂得。

## 配置



log4net是支持配置文件来更改其输出方式的，我们可以把配置文件放到项目配置文件web.config或winform的app.config中，也可以单独的放到固定的配置文件中，现在我们把配置文件放到项目配置文件中。

* 配置日志信息输出目的地 Appender (附加) 增加配置附加器有很多种，主要用到的以下几种

  |                                      |                                                          |
  | ------------------------------------ | -------------------------------------------------------- |
  | log4net.Appender.AdoNetAppender      | (记录到数据库配置access,sqlserver或者其他的数据库)       |
  | log4net.Appender.FileAppender        | (记录到记录日志到单个文件)                               |
  | log4net.Appender.EventLogAppender    | (记录到记录日志到操作系统的事件)                         |
  | log4net.Appender.RollingFileAppender | (记录到记录日志到文件，可以设置文件的名字，或者多个文件) |

* 配置日志信息的格式（布局）:其中， Log4j 提供的 layout 有以下 几种：

  |                              |                                            |
  | ---------------------------- | ------------------------------------------ |
  | log4net.Layout.HTMLLayout    | （以 HTML 表格形式布局）                   |
  | log4net.Layout.PatternLayout | （可以灵活地指定布局模式）                 |
  | log4net.Layout.SimpleLayout  | （包含日志信息的级别和信息字符串）         |
  | log4net.Layout.TTCCLayout    | （包含日志产生的时间、线程、类别等等信息） |

```xml
<configuration>
 
    <configSections>
		<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
    </configSections>
	
	<log4net>
		<!--FileAppender：将日志写到文件中-->
		<appender name="ErrorAppender" type="log4net.Appender.RollingFileAppender,log4net">
		
			<!--文件路径，如果RollingStyle为Composite或Date，则这里设置为目录，文件名在DatePattern里设置，其他则这里要有文件名。已经扩展支持虚拟目录-->  
			<param name="File" value=".\xLog\" /><!--   <param name="File" value="d:\Log\\" />-->
			
			<!--True/false，默认为true。当文件存在时，是否在原文件上追加内容。通常无需设置-->  
			<param name="AppendToFile" value="true" />
			
			<!--每天记录的日志文件个数，默认为0，与MaxFileSize配合使用 在CountDirection为负数时有效。-->  
			<param name="MaxSizeRollBackups" value="100" />
			
			<!--每个日志文件的最大大小，可用的单位:KB|MB|GB （好像只有在RollingStyle的值为Size时有效）-->  
			<param name="MaxFileSize" value="10240" />
			
			<!--True/false，默认为true。为true时，RollingStyler的date值将无效。且为true时需要在file里指定文件名，所有日志都会记录在这个文件里。-->
			<param name="StaticLogFileName" value="false" />
			
			<!--当RollingStyle为Composite或Date，这里设置文件名格式-->
			<param name="DatePattern" value="yyyyMMdd" /><!--  <param name="DatePattern" value="yyyy-MM-dd.TXT" />-->
			
			<!---创建新文件的方式，可选为Size（按文件大小），Date（按日期），Once（每启动一次创建一个文件），Composite（按日期及文件大小），默认为Composite-->
			<param name="RollingStyle" value="Date" />
			
			<!--默认值为-1。当文件超过MaximumFileSize的大小时，如果要创建新的文件来存储日志，会根据CountDirection的值来重命名文件。大于-1的值时，file里指定的文件名会依次加上.0,.1,.2递增。当等于或小于-1时，创建依赖于MaxSizeRollBackups参数值，创建备份日志数。-->
			<param name="CountDirection" value="-1" />
			
			
			
			<!---------------log4net记录错误的格式(即：用什么样的格式（布局）来记录错误)---------------->
			
			<layout type="log4net.Layout.PatternLayout">
				 <!--%m日志消息、%n换行、%d运行时间、%r运行时间毫秒、%C打印日志类、%p日志优先级、%-数字占位、%F文件名、%L行号、%t线程号-->
				<param name="conversionPattern" value="%date [%thread] %-5level %logger [%ndc] - %message%newline" />
				<!--<param name="ConversionPattern" value="时间:%d %n级别:%level %n类名:%c%n文件:%F 第%L行%n日志内容:%m%n-----------------%n%n" />-->
			</layout>
		
		</appender>
		
		<!---------------log4net记录错误的级别(即：在出现什么级别的错误才记录错误)---------------->
		
		<root>
			<level value="ALL" />
			<appender-ref ref="ErrorAppender" />
		</root>
		
		<!--<logger name="WebLogger">       
			<level value="ERROR" /> 
			<appender-ref ref="ErrorAppender"/>		
		</logger>-->
	</log4net>
 
</configuration>
```

layout一些常用的格式化的列表

|        |              |
| ------ | ------------ |
| %m     | 日志消息     |
| %n     | 换行         |
| %d     | 运行时间     |
| %r     | 运行时间毫秒 |
| %C     | 打印日志类   |
| %p     | 日志优先级   |
| %-数字 | 占位         |
| %F     | 文件名       |
| %L     | 行号         |
| %t     | 线程号       |