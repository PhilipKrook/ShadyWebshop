namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using LIAuppgift.Models.Blocks;

    public class SuperBlockController : BlockController<SuperBlock>
    {
        public override ActionResult Index(SuperBlock currentBlock)
        {
            return PartialView("~/Views/Shared/Blocks/SuperBlock.cshtml", currentBlock);
        }
    }
}
