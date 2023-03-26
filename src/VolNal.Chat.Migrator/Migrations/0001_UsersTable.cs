using FluentMigrator;

namespace VolNal.Chat.Migrator.Migrations;

[Migration(1)]
public class UsersTable : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.tables t 
                JOIN sys.schemas s ON (t.schema_id = s.schema_id) 
                WHERE s.name = 'users') 	
                CREATE TABLE users(
                    id INT PRIMARY KEY IDENTITY,
                    name nvarchar(50),
                    email nvarchar(50),
                    avatar varbinary(max),
                    password nvarchar(50),
                    );");
    }

    public override void Down()
    {
        Execute.Sql("IF EXISTS (" +
                    "DROP TABLE dbo.users" +
                    ");");
    }
}