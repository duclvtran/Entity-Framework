namespace P04_Relationship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CompanyPersons", newName: "PersonCompanies");
            DropPrimaryKey("dbo.PersonCompanies");
            CreateTable(
                "dbo.Company1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonWithCompany",
                c => new
                    {
                        RefPersonId = c.Int(nullable: false),
                        RefCompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RefPersonId, t.RefCompanyId })
                .ForeignKey("dbo.Person1", t => t.RefPersonId, cascadeDelete: true)
                .ForeignKey("dbo.Company1", t => t.RefCompanyId, cascadeDelete: true)
                .Index(t => t.RefPersonId)
                .Index(t => t.RefCompanyId);
            
            AddPrimaryKey("dbo.PersonCompanies", new[] { "Person_PersonId", "Company_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonWithCompany", "RefCompanyId", "dbo.Company1");
            DropForeignKey("dbo.PersonWithCompany", "RefPersonId", "dbo.Person1");
            DropIndex("dbo.PersonWithCompany", new[] { "RefCompanyId" });
            DropIndex("dbo.PersonWithCompany", new[] { "RefPersonId" });
            DropPrimaryKey("dbo.PersonCompanies");
            DropTable("dbo.PersonWithCompany");
            DropTable("dbo.Company1");
            AddPrimaryKey("dbo.PersonCompanies", new[] { "Company_Id", "Person_PersonId" });
            RenameTable(name: "dbo.PersonCompanies", newName: "CompanyPersons");
        }
    }
}
