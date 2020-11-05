using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using LIAuppgift.Models.Blocks;

namespace LIAuppgift.Controllers
{
    public class SuperBlockController : BlockController<SuperBlock>
    {
        public override ActionResult Index(SuperBlock currentBlock)
        {
            return PartialView("~/Views/Shared/Blocks/SuperBlock.cshtml", currentBlock);
        }
    }
}
