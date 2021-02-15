namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using LIAuppgift.Models.Blocks;

    public class TestBlockController : BlockController<TestBlock>
    {
        public override ActionResult Index(TestBlock currentBlock)
        {
            return PartialView("~/Views/Shared/Blocks/SuperBlock.cshtml", currentBlock);
        }
    }
}
