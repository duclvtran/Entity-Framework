namespace P04_Relationship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Email2",
                c => new
                    {
                        Email2Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        RefToPerson = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Email2Id)
                .ForeignKey("dbo.Person2", t => t.RefToPerson, cascadeDelete: true)
                .Index(t => t.RefToPerson);
            
            CreateTable(
                "dbo.Person2",
                c => new
                    {
                        Person2Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                    })
                .PrimaryKey(t => t.Person2Id);
            
            CreateTable(
                "dbo.Email3",
                c => new
                    {
                        Email3Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        RefToPerson = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Email3Id)
                .ForeignKey("dbo.Person3", t => t.RefToPerson, cascadeDelete: true)
                .Index(t => t.RefToPerson);
            
            CreateTable(
                "dbo.Person3",
                c => new
                    {
                        Person3Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                    })
                .PrimaryKey(t => t.Person3Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Email3", "RefToPerson", "dbo.Person3");
            DropForeignKey("dbo.Email2", "RefToPerson", "dbo.Person2");
            DropIndex("dbo.Email3", new[] { "RefToPerson" });
            DropIndex("dbo.Email2", new[] { "RefToPerson" });
            DropTable("dbo.Person3");
            DropTable("dbo.Email3");
            DropTable("dbo.Person2");
            DropTable("dbo.Email2");
        }
    }
}
