日志记录及说明

1. 项目引用源码类库
2. 如果是BS项目，需要在Global.asax.cs的Application_Start中添加DF.Log.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(System.Threading.Thread.GetDomain().BaseDirectory + "log4net.config"));
	2.1	如果想自己实现Logger记录器的话需要自己配置log4net.config文件就行了，这样实现十分灵活。
	2.2	并将log4net.config文件目录注入DF.Log.XmlConfigurator.ConfigureAndWatch("log4net.config的文件目录")这样就按照自己的配置记录日志;

	注意：如果使用默认的log4net.config记录日志的话日志记录到默认的数据库中数据库连接为<connectionString value="server=192.168.17.221;port=3306;User Id=b2b;Password=b2b;charset=utf8;database=HSLOG;Connect Timeout=60;Treat Tiny As Boolean=False;allow zero datetime=true" providerName="MySql.Data.MySqlClient;" />
		 否则自己配置数据库连接字符串
	2.3	数据库表设计sql语句为

		CREATE TABLE `log` (
		  `LogId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
		  `CreateTime` datetime NOT NULL COMMENT '创建时间',
		  `Thread` int(11) NOT NULL COMMENT '线程号',
		  `Logger` varchar(50) NOT NULL COMMENT '日志记录器',
		  `Level` varchar(50) NOT NULL COMMENT '日志级别',
		  `Source` varchar(200) NOT NULL COMMENT '日志记录源',
		  `FilePath` varchar(200) NOT NULL COMMENT '日志文件地址',
		  `TranCode` varchar(50) DEFAULT NULL COMMENT '日志编码(通过次编码可以查询这一类日志信息)',
		  `Mark` varchar(50) DEFAULT NULL COMMENT '日志标识(例如有订单的交易此可以是订单号)',
		  `Message` varchar(2000) NOT NULL COMMENT '日志内容',
		  PRIMARY KEY (`LogId`)
		) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='日志主表';
		SET FOREIGN_KEY_CHECKS=1;

3. 日志的配置文件为log4net.config
	3.1	配置文件已经配置好将日志写入到文件和数据库
	3.2	日志文件在运行目录log目录下
	3.3	数据库连接字符串配置节点为log4net.config文件中的connectionString节点
	3.4	注意数据库用什么就添加什么数据访问DLL就行了
	3.5	什么意思呢？就是说是Mysql数据库要在项目启动项引用相应的数据访问MySql.Data.DLL（一般情况会自动引入）

	各个业务模块，日志格式目录为log\{yyyyMMdd}\{source}\{tranCode}.xml
	系统日志，日志格式目录为log\{yyyyMMdd}\{source}\{tranCode}.xml

5. 使用DF.Log.MyLog记录器记录业务日志	Debug记录调试日志INFO反馈系统的当前状态给最终用户的日志WARN记录警告日志ERROR记录普通错误日志FATAL记录严重性错误日志
6. 使用DF.Log.Log4NetLog记录系统日志Debug记录调试日志INFO反馈系统的当前状态给最终用户的日志WARN记录警告日志ERROR记录普通错误日志FATAL记录严重性错误日志


7. 日志级别level说明：
level定义记录的日志级别,就是说,你要记录哪个级别以上的日志,级别由高往低依次是:
None
Fatal
ERROR
WARN
INFO
DEBUG
ALL

级别的定义要注意,如果你定义DEBUG,那么低于DEBUG级别以下的信息,将不会记入日志,啥意思呢?
就是说,就算你在程序里,用log.Debug()来写入一个日志信息,可是你在配置中指定level为Info,
由于DEBUG级别低于INFO,所以,不会被记入日志.这样的处理非常灵活

1：DEBUG 这个级别最低的东东，一般的来说，在系统实际运行过程中，一般都是不输出的。因此这个级别的信息，可以随意的使用，任何觉得有利于在调试时更详细的了解系统运行状态的东东，比如变量的值等等，都输出来看看也无妨。
2：INFO 这个应该用来反馈系统的当前状态给最终用户的，所以，在这里输出的信息，应该对最终用户具有实际意义，也就是最终用户要能够看得明白是什么意思才行。从某种角度上说，Info 输出的信息可以看作是软件产品的一部分（就像那些交互界面上的文字一样），所以需要谨慎对待，不可随便。
3：WARN、ERROR和FATAL：警告、错误、严重错误，这三者应该都在系统运行时检测到了一个不正常的状态，他们之间的区别，要区分还真不是那么简单的事情。
      所谓警告，应该是这个时候进行一些修复性的工作，应该还可以把系统恢复到正常状态中来，系统应该可以继续运行下去。
      所谓错误，就是说可以进行一些修复性的工作，但无法确定系统会正常的工作下去，系统在以后的某个阶段，很可能会因为当前的这个问题，导致一个无法修复的错误（例如宕机），但也可能一直工作到停止也不出现严重问题。
      所谓Fatal，那就是相当严重的了，可以肯定这种错误已经无法修复，并且如果系统继续运行下去的话，可以肯定必然会越来越乱。这时候采取的最好的措施不是试图将系统状态恢复到正常，而是尽可能地保留系统有效数据并停止运行。
      也就是说，选择 Warn、Error、Fatal 中的具体哪一个，是根据当前的这个问题对以后可能产生的影响而定的，如果对以后基本没什么影响，则警告之，如果肯定是以后要出严重问题的了，则Fatal之，拿不准会怎么样，则 Error 之。


注意：个人观点

测试阶阶段的调试和测试日志可以用Debug级别记录日志
正式发布后会屏蔽掉Debug级别的日志记录
