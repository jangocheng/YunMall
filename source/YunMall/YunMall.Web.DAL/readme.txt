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