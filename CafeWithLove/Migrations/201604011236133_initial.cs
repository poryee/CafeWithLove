namespace CafeWithLove.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
           
            AddColumn("dbo.CafeOutlets", "numOfLike", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CafeOutlets", "numOfLike");
            DropColumn("dbo.CafeOutlets", "cafeRegion");
            DropTable("dbo.CarParks");
        }
    }
}
