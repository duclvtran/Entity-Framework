namespace P04_Relationship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Email1",
                c => new
                    {
                        Email1Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        Person1_Person1Id = c.Int(),
                    })
                .PrimaryKey(t => t.Email1Id)
                .ForeignKey("dbo.Person1", t => t.Person1_Person1Id)
                .Index(t => t.Person1_Person1Id);
            
            CreateTable(
                "dbo.Person1",
                c => new
                    {
                        Person1Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                    })
                .PrimaryKey(t => t.Person1Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Email1", "Person1_Person1Id", "dbo.Person1");
            DropIndex("dbo.Email1", new[] { "Person1_Person1Id" });
            DropTable("dbo.Person1");
            DropTable("dbo.Email1");
        }
    }
}
