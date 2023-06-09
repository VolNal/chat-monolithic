using FluentMigrator;

namespace VolNal.Chat.Migrator.Migrations;

[Migration(2)]
public class ChatsTable : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.tables t 
                JOIN sys.schemas s ON (t.schema_id = s.schema_id) 
                WHERE s.name = 'Chats') 	
                CREATE TABLE Chats(
                    Id INT PRIMARY KEY IDENTITY,
                    Name VARCHAR(255),
                    Description VARCHAR(255),
                    Avatar varbinary(max),
                    CreatorId INT,
                    Type VARCHAR(7)
                    );");
    }

    public override void Down()
    {
        Execute.Sql("IF EXISTS (" +
                    "DROP TABLE dbo.Chats" +
                    ");");
    }
}