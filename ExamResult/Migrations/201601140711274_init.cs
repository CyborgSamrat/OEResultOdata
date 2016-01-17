namespace ExamResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Guid(nullable: false),
                        Participator = c.String(),
                        Time = c.DateTime(nullable: false),
                        Exam = c.String(),
                        NumberOfQuestion = c.Int(nullable: false),
                        NumberOfRightAnswer = c.Int(nullable: false),
                        NumberOfWrongAnswer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Results");
        }
    }
}
