using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PayPal.Api;
using OJP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OnlJbPt.Models;

namespace OnlJbPt.Controllers;

public class PaymentController : Controller
{
    private IHttpContextAccessor httpContextAccessor;
    IConfiguration _configuration;
    private readonly ILogger<PaymentController> _logger;
    public PaymentController(ILogger<PaymentController> logger, IHttpContextAccessor Context, IConfiguration iconfiguration)
    {
        _logger = logger;
        httpContextAccessor = Context;
        _configuration = iconfiguration;
    }
    public IActionResult Index()
    {
        return View();
    }
    public ActionResult PaymentWithPaypal(string Cancel = null, string blogId = "", string PayerID = "", string guid = "")
    {
        var ClientID = _configuration.GetValue<string>("PayPal:Key");
        var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
        var mode = _configuration.GetValue<string>("PayPal:mode");
        APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID, ClientSecret, mode);

        try
        {
            string payerId = PayerID;
            if (string.IsNullOrEmpty(payerId))
            {
                string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Payment/PaymentWithPayPal?";
                var guidd = Convert.ToString((new Random()).Next(100000));
                guid = guidd;
                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);
                var links = createdPayment.links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    Links lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.href;
                    }
                }
                httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                return Redirect(paypalRedirectUrl);
            }
            else
            {
                var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                if (executedPayment.state.ToLower() != "approved")
                {
                    return View("PaymentFailed");
                }
                var blogIds = executedPayment.transactions[0].item_list.items[0].sku;
                return View("PaymentSuccess");
            }
        }
        catch (Exception ex)
        {
            return View("PaymentFailed");
        }
        return View("SuccessView");
    }
    private PayPal.Api.Payment payment;
    private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
    {
        var paymentExecution = new PaymentExecution()
        {
            payer_id = payerId
        };
        this.payment = new Payment()
        {
            id = paymentId
        };
        return this.payment.Execute(apiContext, paymentExecution);
    }
    private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
    {
        var itemList = new ItemList()
        {
            items = new List<Item>()
        };
        itemList.items.Add(new Item()
        {
            name = "Item Detail",
            currency = "USD",
            price = "1.00",
            quantity = "1",
            sku = "asd"
        });
        var payer = new Payer()
        {
            payment_method = "paypal"
        };
        var redirUrls = new RedirectUrls()
        {
            cancel_url = redirectUrl + "&Cancel=true",
            return_url = redirectUrl
        };

        var amount = new Amount()
        {
            currency = "USD",
            total = "1.00",
        };
        var transactionList = new List<Transaction>();

        transactionList.Add(new Transaction()
        {
            description = "Transaction description",
            invoice_number = Guid.NewGuid().ToString(),
            amount = amount,
            item_list = itemList
        });
        this.payment = new Payment()
        {
            intent = "sale",
            payer = payer,
            transactions = transactionList,
            redirect_urls = redirUrls
        };
        return this.payment.Create(apiContext);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
