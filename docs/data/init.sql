CREATE DATABASE IF NOT EXISTS YunMall default charset utf8 COLLATE utf8_general_ci; 


/*用户表*/
CREATE TABLE `users` (
`uid`  int NOT NULL AUTO_INCREMENT ,
`username`  varchar(16) NOT NULL COMMENT '用户名' ,
`password`  varchar(32) NOT NULL COMMENT '密码' ,
`level`  int NULL DEFAULT 0 COMMENT '等级(0=会员,1=管理员,2=供货商,3=金牌代理商,4=银牌代理商,5=普通代理商)' ,
`roleId`  varchar(32) NULL COMMENT '角色id' ,
`parentId`  int NULL COMMENT '上级用户id' ,
`depth`  int NULL COMMENT '关系深度' ,
`regIp`  varchar(32) NULL COMMENT '注册ip' ,
`lastIp`  varchar(32) NULL COMMENT '登录ip' ,
`lastTime`  datetime NULL DEFAULT NOW() COMMENT '登录时间' ,
`qq`  varchar(15) NULL COMMENT '联系方式' ,
`state`  int NULL COMMENT '账户状态(0=正常,1=禁用)' ,
`returnRate`  decimal(10,2) NULL COMMENT '折扣率' ,
`cashAccount`  varchar(32) NULL COMMENT '绑定提现账户' ,
`realName`  varchar(32) NULL COMMENT '户主真实姓名' ,
`addTime`  datetime NULL DEFAULT NOW() COMMENT '注册时间' ,
`remark`  varchar(255) NULL COMMENT '摘要' ,
PRIMARY KEY (`uid`),
UNIQUE INDEX `uq_user` (`username`) 
)
COMMENT='用户表'
;

INSERT INTO `yunmall`.`users` (`username`, `password`, `level`, `roleId`, `parentId`, `depth`, `regIp`, `lastIp`, `lastTime`, `qq`, `state`, `returnRate`, `cashAccount`, `realName`, `addTime`, `remark`) VALUES ('admin', 'd7c6c07a0a04ba4e65921e2f90726384', '1', '1', '0', '0', NULL, NULL, '2018-09-21 22:37:18', NULL, NULL, NULL, NULL, NULL, '2018-09-21 22:37:18', NULL);



/*商品表*/
CREATE TABLE `products` (
`pid`  int NOT NULL AUTO_INCREMENT ,
`sid`  int NOT NULL COMMENT '商家id' ,
`categoryId`  int NOT NULL COMMENT '经营类目id' ,
`type`  int NULL COMMENT '类型(0=直充,1=非直充,2=全选)' ,
`mainImage`  varchar(255) NULL COMMENT '商品主图' ,
`productName`  varchar(255) NOT NULL COMMENT '商品名称' ,
`amount`  decimal(10,2) NOT NULL COMMENT '定价' ,
`description`  text NULL COMMENT '商品描述' ,
`status`  int NULL DEFAULT 0 COMMENT '商品状态(0=未上架,1=销售中,2=已下架)' ,
`addTime`  datetime NULL DEFAULT NOW() COMMENT '创建时间' ,
`editTime`  datetime NULL DEFAULT NOW() COMMENT '最后一次编辑时间' ,
PRIMARY KEY (`pid`)
) COMMENT='商品表'


/*权限关系表*/
CREATE TABLE `permission_relations` (
  `relationId` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL COMMENT '用户id',
  `permissionList` varchar(32) NOT NULL COMMENT '权限id数组',
  PRIMARY KEY (`relationId`),
  UNIQUE KEY `uq_permission` (`uid`,`permissionList`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `yunmall`.`permission_relations` (`uid`, `permissionList`) VALUES ('1', '1');



/*权限表*/
CREATE TABLE `permissions` (
  `permissionId` int(11) NOT NULL AUTO_INCREMENT,
  `roleName` varchar(32) NOT NULL,
  PRIMARY KEY (`permissionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='权限表';


INSERT INTO `yunmall`.`permissions` (`roleName`) VALUES ('admin');
INSERT INTO `yunmall`.`permissions` (`roleName`) VALUES ('supplier');
INSERT INTO `yunmall`.`permissions` (`roleName`) VALUES ('merchant');
INSERT INTO `yunmall`.`permissions` (`roleName`) VALUES ('member');



-- ----------------------------
-- Procedure structure for Query
-- ----------------------------
DROP PROCEDURE IF EXISTS `Query`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Query`(IN `_fields` varchar(2000),IN `_tableName` varchar(2000),IN `_where` varchar(2000),`_orderby` varchar(2000))
BEGIN

	#Routine body goes here...

      SET @strsql =CONCAT(' SELECT ',

      CASE

      IFNULL(_fields, '') 

      WHEN '' 

      THEN ' * ' 

      ELSE CONCAT('', _fields) 

			END

      ,

      ' FROM ',

      _tableName,

      CASE

      IFNULL(_where, '') 

      WHEN '' 

      THEN '' 

      ELSE CONCAT(' WHERE ', _where) 

			END,

			CASE

      IFNULL(_orderBy, '') 

      WHEN '' 

      THEN '' 

      ELSE CONCAT(' ORDER BY ', _orderby) 

    END);

    PREPARE strsql FROM @strsql ;

    EXECUTE strsql ;

END
;;
DELIMITER ;





-- ----------------------------
-- Procedure structure for PageQuery
-- ----------------------------
DROP PROCEDURE IF EXISTS `PageQuery`;
DELIMITER ;;
CREATE DEFINER=`productdb`@`%` PROCEDURE `PageQuery`(IN _fields varchar(2000),

    IN _tableName varchar(2000),

    IN _where varchar(2000),

    IN _orderby varchar(2000),

    IN _pageindex int,

    in _pagesize int, out _totalcount int)
BEGIN

  SET @startrow = _pagesize * (_pageindex - 1) ;

  SET @pagesize = _pagesize ;

  SET @rowindex = 0 ;

  SET @strsql = CONCAT(

    ' select ', 

    CASE

      IFNULL(_fields, '') 

      WHEN '' 

      THEN ' * ' 

      ELSE CONCAT('', _fields) 

    END,

    ' from ',

    _tableName,	

    CASE

      IFNULL(_where, '') 

      WHEN '' 

      THEN '' 

      ELSE CONCAT(' where ', _where) 

    END,

      CASE

      IFNULL(_orderby, '') 

      WHEN '' 

      THEN '' 

      ELSE CONCAT(' order by ', _orderby) 

    END,  

    ' limit ',

    @startRow,

    ',',

    @pageSize

  ) ;

 

  PREPARE strsql FROM @strsql;

  EXECUTE strsql ;



SET @strsql_count = CONCAT(

     ' select ', 

    CASE

      IFNULL(_fields, '') 

      WHEN '' 

      THEN ' * ' 

      ELSE CONCAT('', _fields) 

    END,

    ' from ',

    _tableName ,	

    CASE

      IFNULL(_where, '') 

      WHEN '' 

      THEN '' 

      ELSE CONCAT(' where ', _where) 

    END

  );



  PREPARE strsql_count FROM @strsql_count ;

  EXECUTE strsql_count ; 



  SET _totalcount = FOUND_ROWS() ;

-- PREPARE strsql FROM @strsql ;

 -- EXECUTE strsql ;

END
;;
DELIMITER ;