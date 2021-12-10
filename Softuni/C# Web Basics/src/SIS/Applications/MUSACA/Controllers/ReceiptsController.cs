using MUSACA.Services.Receipts;
using MUSACA.ViewModels.Receipts;
using SIS.HTTP.Responses;
using SIS.WebServer.Authorization;
using SIS.WebServer.Controllers;
using SIS.WebServer.DataManager;

namespace MUSACA.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptsService receiptsService;

        public ReceiptsController(IReceiptsService receiptsService)
        {
            this.receiptsService = receiptsService;
        }

        [Authorize]
        public IHttpResponse Details(string id)
        {
            var user = this.GetUser();
            ReceiptDetailsViewModel receipts;

            if (user.Role == UserRole.Admin)
            {
                receipts = receiptsService.GetReceipt(id);
            }
            else
            {
                receipts = receiptsService.GetUserReceipt(this.GetUserId(), id);
            }

            return this.View(receipts);
        }

        [Authorize]
        public IHttpResponse All()
        {
            var user = this.GetUser();

            if (user.Role != UserRole.Admin)
            {
                return this.Redirect("/");
            }

            return this.View(receiptsService.GetAllReceipts());
        }
    }
}
