using FluentMigrator;

namespace VolNal.Chat.Migrator.Migrations;


public class ChatsTable : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.tables t 
                JOIN sys.schemas s ON (t.schema_id = s.schema_id) 
                WHERE s.name = 'chats') 	
                CREATE TABLE chats(
                    id INT PRIMARY KEY IDENTITY,
                    name VARCHAR(255),
                    description VARCHAR(255),
                    avatar varbinary(max),
                    creator_id INT,
                    Type VARCHAR(7)
                    );");
    }

    public override void Down()
    {
        Execute.Sql("IF EXISTS (" +
                    "DROP TABLE dbo.chats" +
                    ");");
    }
}