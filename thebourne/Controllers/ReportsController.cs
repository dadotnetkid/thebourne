using System.Linq;
using System.Web.Mvc;
using thebourne.Models;

namespace thebourne.Controllers
{
    public class ReportsController : Controller
    {
        private TheBournEntity db = new TheBournEntity();

        // GET: Reports
        public ActionResult ReportViewer(int? reservationId)
        {
            var list = db.Reservations.Include("AspNetUsers").Where(m => m.Id == reservationId).ToList();
            ReceiptReport receiptReport = new ReceiptReport()
            {
                DataSource = list
            };
            return PartialView(receiptReport);
        }


    }
}