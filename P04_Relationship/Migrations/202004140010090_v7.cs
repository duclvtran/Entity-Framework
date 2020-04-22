namespace P04_Relationship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Email4",
                c => new
                    {
                        Email4Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        RefToPerson = c.Int(nullable: false),
                        Owner_Person4Id = c.Int(),
                    })
                .PrimaryKey(t => t.Email4Id)
                .ForeignKey("dbo.Person4", t => t.Owner_Person4Id)
                .Index(t => t.Owner_Person4Id);
            
            CreateTable(
                "dbo.Person4",
                c => new
                    {
                        Person4Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                    })
                .PrimaryKey(t => t.Person4Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Email4", "Owner_Person4Id", "dbo.Person4");
            DropIndex("dbo.Email4", new[] { "Owner_Person4Id" });
            DropTable("dbo.Person4");
            DropTable("dbo.Email4");
        }
    }
}
