using FluentMigrator;

namespace VolNal.Chat.Migrator.Migrations;

[Migration(3)]
public class MessagesTable:Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.tables t 
                JOIN sys.schemas s ON (t.schema_id = s.schema_id) 
                WHERE s.name = 'Messages') 	
                CREATE TABLE Messages(
                    Id INT PRIMARY KEY IDENTITY,
                    ChatId INT,
                    UserId INT,
                    Content TEXT,
                    Date date
                    );");
    }


    public override void Down()
    {
        Execute.Sql("IF EXISTS (" +
                    "DROP TABLE dbo.Messages" +
                    ");");
    }
}