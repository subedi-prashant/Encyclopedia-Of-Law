using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Controllers
{
    public class PaymentController : Controller
    {
        public async Task<IActionResult> InitiatePayAsync(string lawyerId, string lawyerName)
        {
            var url = "https://a.khalti.com/api/v2/epayment/initiate/";

            var payload = new
            {
                return_url = "https://localhost:44395/Payment/PaymentReport",
                website_url = "https://localhost:44395/",
                amount = "1000",
                purchase_order_id = lawyerId,
                purchase_order_name = lawyerName,

            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "key ******************");

            var response = await client.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);

            var paymentUrl = responseObject["payment_url"].ToString();

            if (paymentUrl != null)
            {
                return Redirect(paymentUrl);
            }

            return Content("Error");
        }

        public IActionResult PaymentReport()
        {
            return View();
        }
    }
}
