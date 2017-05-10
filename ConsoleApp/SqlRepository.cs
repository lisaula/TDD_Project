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

        public void dropIndex(string name, string table, string database)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                throw new ConnectionCloseException("The connection is closed");
            }
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"use " + database + @"
 DROP INDEX "+ name +@" on " + table + @";";
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

        public void closeConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                this.user = null;
            }
        }

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

    }
}
