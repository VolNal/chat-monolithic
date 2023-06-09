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
                WHERE s.name = 'Users') 	
                CREATE TABLE Users(
                    Id INT PRIMARY KEY IDENTITY,
                    Name nvarchar(50),
                    Email nvarchar(50),
                    Avatar varbinary(max),
                    Password nvarchar(256),
                    );");
    }

    public override void Down()
    {
        Execute.Sql("IF EXISTS (" +
                    "DROP TABLE dbo.Users" +
                    ");");
    }
}