using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SqlRepository
    {
        SqlConnection conn;
        public string user { get; set; }
        DataTable getUsersMappedDBs(string user)
        {
            using (var con = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Integrated Security=true;"))
            {
                con.Open();

                using (SqlCommand command = con.CreateCommand())
                {
                    /*command.CommandText =
                        @"SELECT s.name, o.name
            FROM sys.objects o WITH(NOLOCK)
            JOIN sys.schemas s WITH(NOLOCK)
            ON o.schema_id = s.schema_id
            WHERE o.is_ms_shipped = 0 AND RTRIM(o.type) = 'U'
            ORDER BY s.name ASC, o.name ASC";*/
                    command.CommandText = @"  
                DECLARE @name SYSNAME = N'" + user + @"'; -- input param, presumably

                DECLARE @sql NVARCHAR(MAX) = N'';

                SELECT @sql += N'UNION ALL SELECT N''' + REPLACE(name,'''','''''') + ''' as db,
                  p.name, p.default_schema_name, STUFF((SELECT N'','' + r.name 
                  FROM ' + QUOTENAME(name) + N'.sys.database_principals AS r
                  INNER JOIN ' + QUOTENAME(name) + N'.sys.database_role_members AS rm
                   ON r.principal_id = rm.role_principal_id
                  WHERE rm.member_principal_id = p.principal_id
                  FOR XML PATH, TYPE).value(N''.[1]'',''nvarchar(max)''),1,1,N'''') as roles
                 FROM sys.server_principals AS sp
                 LEFT OUTER JOIN ' + QUOTENAME(name) + '.sys.database_principals AS p
                 ON sp.sid = p.sid
                 WHERE sp.name = @name '
                FROM sys.databases WHERE [state] = 0;

                SET @sql = STUFF(@sql, 1, 9, N'');

                exec master.sys.sp_executesql @sql, N'@name SYSNAME', @name;";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }

        public bool accessDataBase(string database_name, string user)
        {
            var dt = getUsersMappedDBs(user);
            foreach(DataRow row in dt.Rows)
            {
                
            }
            return false;
        }

        public DataTable getTables(string database_name)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database_name + @"
select* from sys.tables";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public DataTable getTableData(string table,string database_name)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database_name + @"
select* from "+table+@";";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public void updateTable(string table, string dataBase, DataTable dt)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"use " + dataBase + @"
select* from " + table + @";";
                var da = new SqlDataAdapter(cmd);
                var b = new SqlCommandBuilder(da);
                //var old_dt = new DataTable();
                //old_dt.Load(cmd.ExecuteReader());
                //da.Fill(old_dt);
                da.Update(dt);
            }
        }

        //drops
        public void dropTable(string table, string dataBase)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + dataBase + @"
Drop table " + table + @";";
                command.ExecuteReader().Close();
            }
        }

        public void dropIndex(string name, string table, string database, int is_primary)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                if (is_primary == 0)
                {
                    command.CommandText = @"use " + database + @"
 DROP INDEX " + name + @" on " + table + @";";
                }
                else
                {
                    command.CommandText = @"use " + database + @"
 ALTER TABLE " + table + @" DROP CONSTRAINT " + name+ @";";
                }
                command.ExecuteReader().Close();
            }
        }
        public void dropTrigger(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
 DROP TRIGGER " + name + @";";
                command.ExecuteReader().Close();
            }
        }
        public void dropStoreProcedure(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
DROP PROCEDURE " + name + @";";
                command.ExecuteReader().Close();
            }
        }
        public void dropCheck(string name, string table, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
ALTER TABLE "+table +@"
DROP CONSTRAINT "+name+@";
        GO";
                command.ExecuteReader().Close();
            }
        }
        public void dropFunction(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
DROP FUNCTION " + name + @";";
                command.ExecuteReader().Close();
            }
        }
        public void dropView(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
DROP VIEW " + name + @";";
                command.ExecuteReader().Close();
            }
        }
        public void dropUser(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText =@"use "+database+@"
DROP USER " + name + @";";
                command.ExecuteReader().Close();
            }
        }
        public void dropLogin(string name)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
        IF EXISTS(SELECT* FROM sys.server_principals WHERE name = N'"+name+ @"')
DROP LOGIN " + name + @";";
                command.ExecuteReader().Close();
            }
        }
        public void dropDataBase(string name)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + name + @"'
USE [master]
ALTER DATABASE " + name + @" SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
USE [master]
DROP DATABASE " + name + @";";
                command.ExecuteReader().Close();
            }
        }
        public void dropForeignKey(string name, string table,string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
IF EXISTS(SELECT* FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'["+name+ @"]') AND parent_object_id = OBJECT_ID(N'[" + table + @"]'))
ALTER TABLE [" + table + @"]
DROP CONSTRAINT[" + name + @"];";
                command.ExecuteReader().Close();
            }
        }

        //ddls
        public DataTable getTriggerDDL(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use "+ database +@"
exec sp_helptext " + name + @";";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getSP_FN_Views_DDL(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
exec sp_helptext '" + name + @"';";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        } 
        public DataTable getCheckDDL(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
declare @values table(Text nvarchar(MAX))
declare @table_id int, @definition varchar(max)
select @table_id = parent_object_id, @definition = definition from sys.check_constraints where name = '" + name + @"'
insert into @values values('use '+ '" + database + @"')
insert into @values values('go')
insert into @values values('alter table '+OBJECT_NAME(@table_id)+ ' with check add constraint " + name + @" check '+ @definition)
insert into @values values('go')
insert into @values values('alter table '+OBJECT_NAME(@table_id) + ' check constraint " + name + @"')
insert into @values values('go')
select* from @values;"; 
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public DataTable getForeignKeyDDL(string name, string table,string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
declare @column sysname,@R_table sysname, @R_column sysname
select
    @column = ac.FK_Column,
	@R_table = ac.PK_Table,
	@R_column = ac.PK_Column
from
(SELECT
    FK_Table = FK.TABLE_NAME,
    FK_Column = CU.COLUMN_NAME,
    PK_Table = PK.TABLE_NAME,
    PK_Column = PT.COLUMN_NAME,
    Constraint_Name = C.CONSTRAINT_NAME
FROM
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
    ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
    ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
    ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
INNER JOIN (
            SELECT
                i1.TABLE_NAME,
                i2.COLUMN_NAME
            FROM
                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
            WHERE
                i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
           ) PT
    ON PT.TABLE_NAME = PK.TABLE_NAME) as ac
    where ac.Constraint_Name = '"+name+ @"' and ac.FK_Table = '" + table + @"'


declare @t table(Text varchar(MAX))
insert into @t values('use " + database + @"')
insert into @t values('go')
insert into @t values('alter table " + table + @" with check add constraint " + name + @" foreign key('+@column+')')
insert into @t values('references '+@R_table+' ('+@R_column+')')
insert into @t values('go')
insert into @t values('alter table " + table + @" check constraint " + name + @"')
insert into @t values('go')
select* from @t;";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getUserDDL(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
declare @LoginName sysname;

declare @t table(UserName Sysname NULL, RoleName Sysname NULL, LoginName Sysname NULL, DefDBName Sysname NULL, DefSchemaName Sysname NULL, UserID smallint NULL, SID smallint NULL)
insert @t(UserName, RoleName, LoginName , DefDBName, DefSchemaName, UserID, SID)
EXEC sp_helpuser;
select distinct
    @LoginName = t.LoginName
from @t as t
    inner join
        sys.server_principals sp
            ON sp.name = t.UserName
where type in ('s') and UserName = '"+name+ @"'

declare @tb table(Text varchar(MAX))
insert into @tb values('use " + database + @"')
insert into @tb values('go')
insert into @tb values('create user " + name + @" for login '+@LoginName+' with default_schema = [dbo]')
insert into @tb values('go')
select* from @tb;";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getLoginDDL(string name)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
declare @default_database sysname, @default_language sysname
select @default_database = default_database_name, @default_language = default_language_name from sys.server_principals where name = '"+ name + @"'
declare @t table(Text varchar(MAX))
insert into @t values('create login ["+name+ @"] with password=N"""", default_database = ['+@default_database+'], default_language=['+@default_language+']')
insert into @t values('go')

DECLARE @name NVARCHAR(100)
DECLARE @cursor CURSOR

SET @cursor = CURSOR FOR
select
        spr.name as security_entity
    from sys.server_principals sp
    inner join sys.server_role_members srm
    on sp.principal_id = srm.member_principal_id
    inner join sys.server_principals spr
    on srm.role_principal_id = spr.principal_id
    where sp.type in ('s', 'u') and sp.name = '" + name + @"'

OPEN @cursor
FETCH NEXT
FROM @cursor INTO @name
WHILE @@FETCH_STATUS = 0
BEGIN
    insert into @t values ('exec sys.sp_addsrvrolemember @loginame = N""" + name+ @""", @rolename = '+@name)
    insert into @t values('go')

    FETCH NEXT
    FROM @cursor INTO @name
END
CLOSE @cursor
DEALLOCATE @cursor

insert into @t values('alter login [" + name + @"] disable')
insert into @t values('go')
select* from @t";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getTableDDL(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use "+database+ @"
declare @t table(Text varchar(MAX))

insert into @t values('create table [" + name + @"](' )

DECLARE @is_nullable INT, @max_length int, @is_identity int, @counter int
DECLARE @name sysname, @Type_name sysname
DECLARE @cursor CURSOR

select @counter = count(*)
from sys.all_columns ac inner join sys.types t
    on t.user_type_id = ac.user_type_id
where object_id = OBJECT_ID('" + name + @"')
group by object_id

SET @cursor = CURSOR FOR
select ac.name, ac.max_length, ac.is_nullable, ac.is_identity, t.name as Type_name
from sys.all_columns ac inner join sys.types t
    on t.user_type_id = ac.user_type_id
where object_id = OBJECT_ID('" + name + @"')
order by ac.column_id

OPEN @cursor
FETCH NEXT
FROM @cursor INTO @name, @max_length, @is_nullable, @is_identity, @Type_name
WHILE @@FETCH_STATUS = 0
BEGIN
    if(@counter< 2)
		if(@is_nullable = 0)
			if(@is_identity = 0)
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') not null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] not null')
			else
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') identity(1,1) not null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] identity(1,1) not null')
		else
			if(@is_identity = 0)
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] null')
			else
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') identity(1,1) null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] identity(1,1) null')
    else
		if(@is_nullable = 0)
			if(@is_identity = 0)
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') not null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] not null')
			else
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') identity(1,1) not null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] identity(1,1) not null')
		else
			if(@is_identity = 0)
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] null')
			else
				if(@Type_name like '%char%')
					insert into @t values('     ['+@name + '] ['+@Type_name+'] ('+CAST(@max_length AS varchar)+') identity(1,1) null')
				else
					insert into @t values('     ['+@name + '] ['+@Type_name+'] identity(1,1) null')

    set @counter = @counter - 1;
        FETCH NEXT

    FROM @cursor INTO @name, @max_length, @is_nullable, @is_identity, @Type_name
END
CLOSE @cursor
DEALLOCATE @cursor

insert @t values(') on [PRIMARY]')
select* from @t";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public string getDatabaseDDL(string name)
        { 
            string s = @"CREATE DATABASE ["+name+ @"] ON  PRIMARY 
( NAME = N'" + name + @"', FILENAME = N'C:\" + name + @".mdf' , SIZE = 2048KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'nueva_log', FILENAME = N'C:\" + name + @"_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [" + name + @"] SET COMPATIBILITY_LEVEL = 100
GO
ALTER DATABASE [" + name + @"] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [" + name + @"] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [" + name + @"] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [" + name + @"] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [" + name + @"] SET ARITHABORT OFF 
GO
ALTER DATABASE [" + name + @"] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [" + name + @"] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [" + name + @"] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [" + name + @"] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [" + name + @"] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [" + name + @"] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [" + name + @"] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [" + name + @"] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [" + name + @"] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [" + name + @"] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [" + name + @"] SET  DISABLE_BROKER 
GO
ALTER DATABASE [" + name + @"] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [" + name + @"] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [" + name + @"] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [" + name + @"] SET  READ_WRITE 
GO
ALTER DATABASE [" + name + @"] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [" + name + @"] SET  MULTI_USER 
GO
ALTER DATABASE [" + name + @"] SET PAGE_VERIFY CHECKSUM  
GO
USE [" + name + @"]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [" + name + @"] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO";
            return s;
        }

        public void closeConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                this.user = null;
            }
        }
        //listar
        public DataTable getIndexes(string table, string database)
        {
            
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use "+database+@"
select* from sys.indexes where object_id = OBJECT_ID('"+table+@"');";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getForeignKeys(string table, string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
select * from sys.foreign_keys where parent_object_id = OBJECT_ID('" + table + @"');";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getIndexDDL(string name, string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
declare @values table(Text nvarchar(MAX))
declare @table_id int, @is_unique int, @cloustered varchar(30), @is_primary int, @index_id int,@counter int;
select @table_id = object_id, @index_id = index_id, @is_unique = is_unique, @cloustered = type_desc, @is_primary = is_primary_key from sys.indexes where name like '%"+name + @"%';
select @counter = COUNT(*) from sys.index_columns where object_id = @table_id and index_id = @index_id

insert into @values values('use " + database + @"')
insert into @values values('go')
if (@is_primary = 1)
	insert into @values values('alter table '+OBJECT_NAME(@table_id)+ ' add constraint " + name + @" primary key '+ @cloustered)
else 
	insert into @values values('create '+ @cloustered+' index " + name + @" on '+OBJECT_NAME(@table_id))
insert into @values values('(')

DECLARE @asc INT
DECLARE @name NVARCHAR(100)
DECLARE @cursor CURSOR

SET @cursor = CURSOR FOR
select ac.name, ic.is_descending_key from sys.index_columns as ic
inner join sys.all_columns ac

    on ac.object_id = ic.object_id and ac.column_id = ic.column_id
where ac.object_id = @table_id and ic.index_id = @index_id
order by ic.index_column_id

OPEN @cursor
FETCH NEXT
FROM @cursor INTO @name, @asc
WHILE @@FETCH_STATUS = 0
BEGIN
    if(@counter< 2)
		if(@asc = 0)

            insert into @values values(@name + ' ASC')
		else
			insert into @values values(@name + ' DESC')
    else
		if(@asc = 0)
			insert into @values values(@name + ' ASC,')
		else
			insert into @values values(@name + ' DESC,')

    set @counter = @counter - 1;
        FETCH NEXT

    FROM @cursor INTO @name, @asc
END
CLOSE @cursor
DEALLOCATE @cursor
insert into @values values(')')
insert into @values values('go')
select* from @values";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public DataTable getTriggers(string table, string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
select* from sys.triggers where parent_id = OBJECT_ID('" + table + @"');";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getChecks(string table, string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
select* from sys.check_constraints where parent_object_id = OBJECT_ID('" + table + @"');";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public DataTable getStoredProcedures(string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
select p.*, s.name as Schema_Name 
from sys.procedures as p inner join 
sys.schemas as s 
on s.schema_id = p.schema_id;";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getFunctions(string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
SELECT o.name, o.object_id,m.definition, o.type_desc , s.name as Schema_Name
  FROM sys.sql_modules m 
INNER JOIN sys.objects o 
        ON m.object_id=o.object_id
inner join sys.schemas s
		ON o.schema_id = s.schema_id
WHERE type_desc like '%function%'";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getViews(string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"        
SELECT o.name, o.object_id,m.definition, o.type_desc , s.name as Schema_Name
  FROM sys.sql_modules m 
INNER JOIN sys.objects o 
        ON m.object_id=o.object_id
inner join sys.schemas s
		ON o.schema_id = s.schema_id
WHERE type_desc like '%view%'
";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getUsers()
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
SELECT* FROM sys.server_principals where type in ('s')";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getTableDesign(string name, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use "+database+@"
select ac.name as column_name, t.name as type_name, ac.max_length, ac.is_nullable, ac.is_identity
from sys.all_columns ac inner join sys.types t
    on t.user_type_id = ac.user_type_id
where object_id = OBJECT_ID('"+name+@"')
order by ac.column_id";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable getUsersDatabase(string database)
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed while getting indexes");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use "+database+ @"
declare @t table(UserName Sysname NULL, RoleName Sysname NULL, LoginName Sysname NULL, DefDBName Sysname NULL, DefSchemaName Sysname NULL, UserID smallint NULL, SID smallint NULL)
insert @t(UserName, RoleName, LoginName , DefDBName, DefSchemaName, UserID, SID)
EXEC sp_helpuser;
select distinct t.UserName, sp.type 
from @t as t 
    inner join 
        sys.server_principals sp
            ON sp.name = t.UserName
where type in ('s')
";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public void openConnection(string user, string pass)
        {
            conn = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Integrated Security=false;User ID=" + user + "; Password=" + pass + ";");
            try
            {
                conn.Open();
                this.user = user;
            }
            catch (Exception e)
            {
                throw new LogingException("Connection could'n be stablished\n" + e.Message);
            }
        }

        public DataTable getDatabases()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = @"select * from sys.databases";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            else
            {
                throw new ConnectionCloseException("The connection is closed");
            }
        }


        //execute
        public DataTable executeSQL(string sql)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            else
            {
                throw new ConnectionCloseException("The connection is closed");
            }
        }

        //create

        public string createTableProto(string database)
        {
            return @"--Simple CREATE TABLE Syntax (common if not using options)
USE "+ database +@"

CREATE TABLE   
    [ database_name . [ schema_name ] . | schema_name . ] table_name   
    ( { <column_definition> } [ ,...n ] )   
[ ; ]  ";
        }
        public string createDataBase()
        {
            return @"CREATE DATABASE databasename;";
        }
        public string createTrigger()
        {
            return @"

CREATE TRIGGER <Schema_Name, sysname, Schema_Name>.<Trigger_Name, sysname, Trigger_Name> 
   ON  <Schema_Name, sysname, Schema_Name>.<Table_Name, sysname, Table_Name> 
   AFTER <Data_Modification_Statements, , INSERT,DELETE,UPDATE>
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here

END";
        }

        public string createView(string database)
        {
            return @"
USE "+ database + @"

CREATE [ OR ALTER ] VIEW [ schema_name . ] view_name [ (column [ ,...n ] ) ]   
[ WITH <view_attribute> [ ,...n ] ]   
AS select_statement   
[ WITH CHECK OPTION ]   
[ ; ]  
  
<view_attribute> ::=   
{  
    [ ENCRYPTION ]  
    [ SCHEMABINDING ]  
    [ VIEW_METADATA ]       
}   ";
        }

        public string createIndex()
        {
            return @"

CREATE [ CLUSTERED | NONCLUSTERED ] INDEX index_name   
    ON [ database_name . [ schema ] . | schema . ] table_name   
        ( { column [ ASC | DESC ] } [ ,...n ] )  
    WITH ( DROP_EXISTING = { ON | OFF } )  
[;]  ";
        }

        public string createUser(string database)
        {
            return @"
USE "+ database + @"

CREATE USER <username> FOR LOGIN <loginname>

USE " + database + @"

ALTER USER <username> WITH DEFAULT_SCHEMA=[dbo]

USE " + database + @"

EXEC sp_addrolemember N'<privilege>', N'<username>'

--privileges options
--db_owner
--db_datareader
--db_datawriter
--db_denydatareader
--db_denydatawriter
--db_ddladmin
--db_accessadmin
";
        }
        public string createLogin()
        {
            return @"
USE [master]

CREATE LOGIN <loginname> WITH PASSWORD=N'<password>', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

EXEC master..sp_addsrvrolemember @loginame = N'<loginname>', @rolename = N'<rolename>'

--roles option 
--public
--sysadmin
--securityadmin
--serveradmin";
        }

        public string createStoreProcedure(string database)
        {
            return @"
USE "+database+@"

CREATE PROCEDURE <Procedure_Name, sysname, ProcedureName> 
	-- Add the parameters for the stored procedure here
	<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
END
GO";
        }

        public string createFunction(string database)
        {
            return @"
USE "+database+@"

CREATE FUNCTION <Scalar_Function_Name, sysname, FunctionName> 
(
	-- Add the parameters for the function here
	<@Param1, sysname, @p1> <Data_Type_For_Param1, , int>
)
RETURNS <Function_Data_Type, ,int>
AS
BEGIN
	-- Declare the return variable here
	DECLARE <@ResultVar, sysname, @Result> <Function_Data_Type, ,int>

	-- Add the T-SQL statements to compute the return value here
	SELECT <@ResultVar, sysname, @Result> = <@Param1, sysname, @p1>

	-- Return the result of the function
	RETURN <@ResultVar, sysname, @Result>

END";
        }

        public string createForeignKey(string table, string database)
        {
            return @"
USE "+database +@"

ALTER TABLE "+table+ @"
ADD FOREIGN KEY (<columnname>) REFERENCES Persons(<R_columnname>);";
        }

        public string createCheck(string table, string database)
        {
            return @"
USE "+database+@"

ALTER TABLE "+table+@"
ADD CONSTRAINT <checkname> CHECK (<expression>);";
        }
    }
}
