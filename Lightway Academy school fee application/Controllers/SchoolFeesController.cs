using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lightway_Academy_school_fee_application.Models;
using Newtonsoft.Json;

namespace Lightway_Academy_school_fee_application.Controllers
{
    public class SchoolFeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchoolFees
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string order_Id, string SearchString, string SearchStringFrom, string SearchStringTo)
        {
            if (order_Id != null)
            {
                SchoolFee getdetail = db.SchoolFees.FirstOrDefault(u => u.PaymentOrderId == order_Id);
                ViewBag.Message = getdetail.Statues;
                return View(db.SchoolFees.ToList().Where(t => t.NameOfStudent == getdetail.NameOfStudent));
            }

            if (SearchString == null && SearchStringFrom == null && SearchStringTo == null)
            {
                return View(db.SchoolFees.ToList().OrderByDescending(t => t.DateOfPayment).Take(20));
            }
            else if (SearchString != "" && SearchStringFrom == "" && SearchStringTo == "")
            {
                var Fees = from r in db.SchoolFees
                           where r.NameOfStudent.Contains(SearchString.Trim()) ||
                           r.RRR.Contains(SearchString.Trim()) ||
                           r.PhoneNumber.Contains(SearchString.Trim()) ||
                           r.EmailAddress.Contains(SearchString.Trim()) ||
                           r.Session.Contains(SearchString.Trim()) ||
                           r.Term.Contains(SearchString.Trim())
                           select r;
                return View(Fees.ToList().OrderByDescending(t => t.DateOfPayment).Where(t => t.Statues == "Approved" || t.Statues == "Transaction Successsfully Completed"));
            }
            else if (SearchString != "" && SearchStringFrom != "" && SearchStringTo != "")
            {
                var DateFrom = Convert.ToDateTime(SearchStringFrom);
                var DateTo = Convert.ToDateTime(SearchStringTo);


                var Fees = from r in db.SchoolFees
                           where r.NameOfStudent.Contains(SearchString.Trim()) ||
                           r.RRR.Contains(SearchString.Trim()) ||
                           r.PhoneNumber.Contains(SearchString.Trim()) ||
                           r.EmailAddress.Contains(SearchString.Trim()) ||
                            r.Session.Contains(SearchString.Trim()) ||
                            r.Term.Contains(SearchString.Trim())
                           select r;
                return View(Fees.ToList().OrderByDescending(t => t.DateOfPayment).Where(y => y.DateOfPayment >= DateFrom && y.DateOfPayment <= DateTo && y.Statues == "Approved" || y.Statues == "Transaction Successfully Completed"));
            }
            else if (SearchString == "" && SearchStringFrom != "" && SearchStringTo != "")
            {
                var DateFrom = Convert.ToDateTime(SearchStringFrom);
                var DateTo = Convert.ToDateTime(SearchStringTo);

                return View(db.SchoolFees.ToList().OrderByDescending(t => t.DateOfPayment).Where(y => y.DateOfPayment >= DateFrom && y.DateOfPayment <= DateTo && y.Statues == "Approved" || y.Statues == "Transaction Successfully Completed"));
            }
            else if (SearchString == "" && SearchStringFrom == "" && SearchStringTo == "")
            {
                return View(db.SchoolFees.ToList().OrderByDescending(t => t.DateOfPayment).OrderByDescending(t => t.DateOfPayment).Take(20));
            }

            return View(db.SchoolFees.ToList().OrderByDescending(u => u.DateOfPayment).Take(20));
        }

        //getting report of school fee payment by class
        public ActionResult Report(string SearchString, string SearchStringFrom, string SearchStringTo)
        {

            ViewBag.Class = new SelectList(db.ClassTerms.OrderBy(t=>t.ClassName), "ClassName", "ClassName");
            if (SearchString == null && SearchStringFrom == null && SearchStringTo == null)
            {
                return View(db.SchoolFees.ToList().OrderByDescending(t => t.DateOfPayment).Take(20));
            }
            else if (SearchString != "" && SearchStringFrom == "" && SearchStringTo == "")
            {
                var Fees = from r in db.SchoolFees
                           where r.ClassTerm.Contains(SearchString)
                           select r;
                ViewBag.Message = Fees.Where(t => t.Statues == "Approved" || t.Statues == "Transaction Successsfully Completed").Count();
                return View(db.SchoolFees.ToList().OrderByDescending(t => t.DateOfPayment).Where(r => r.ClassTerm == SearchString).Where(t => t.Statues == "Approved" || t.Statues == "Transaction Successsfully Completed"));
            }
            else if (SearchString != "" && SearchStringFrom != "" && SearchStringTo != "")
            {
                var DateFrom = Convert.ToDateTime(SearchStringFrom);
                var DateTo = Convert.ToDateTime(SearchStringTo);


                var Fees = from r in db.SchoolFees
                           where r.ClassTerm.Contains(SearchString)
                           select r;
                ViewBag.Message = Fees.Where(y => y.DateOfPayment >= DateFrom && y.DateOfPayment <= DateTo).Where(t => t.Statues == "Approved" || t.Statues == "Transaction Successsfully Completed").Count();
                return View(Fees.ToList().OrderByDescending(t => t.DateOfPayment).Where(y => y.DateOfPayment >= DateFrom && y.DateOfPayment <= DateTo).Where(t => t.Statues == "Approved" || t.Statues == "Transaction Successsfully Completed"));
            }
            else if (SearchString == "" && SearchStringFrom != "" && SearchStringTo != "")
            {
                var DateFrom = Convert.ToDateTime(SearchStringFrom);
                var DateTo = Convert.ToDateTime(SearchStringTo);

                ViewBag.Message = db.SchoolFees.Where(t => t.Statues == "Approved" || t.Statues == "Transaction Successsfully Completed").Where(y => y.DateOfPayment >= DateFrom && y.DateOfPayment <= DateTo).Count();
                return View(db.SchoolFees.ToList().OrderByDescending(t => t.DateOfPayment).Where(y => y.DateOfPayment >= DateFrom && y.DateOfPayment <= DateTo).Where(t => t.Statues == "Approved" || t.Statues == "Transaction Successsfully Completed"));
            }
            else if (SearchString == "" && SearchStringFrom == "" && SearchStringTo == "")
            {
                return View();
            }

            return View();
        }



        // GET: SchoolFees/Details/5
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolFee schoolFee = db.SchoolFees.Find(id);
            if (schoolFee == null)
            {
                return HttpNotFound();
            }

            return View(schoolFee);
        }

        // GET: SchoolFees/Create
        public ActionResult Create(int? id)
        {
            Fees getFeeDetail = db.Fees.Find(id);

            //ViewBag.ClassName = getFeeDetail.ClassName;
            //ViewBag.Session = getFeeDetail.Session;
            //ViewBag.Term = getFeeDetail.Term;

            var amt = getFeeDetail.Amount;
            var Tamt = amt + 200;
            ViewBag.TotalAmount = Tamt.ToString("###,###.00");
            ViewBag.Amount = getFeeDetail.Amount.ToString("###,###.00");
            ViewBag.ProcessingFee = "200";

            TempData["ClassName"] = getFeeDetail.ClassName;
            TempData["Session"] = getFeeDetail.Session;
            TempData["Term"] = getFeeDetail.Term;
            TempData["Amount"] = getFeeDetail.Amount;
            TempData["Tamt"] = Tamt;


            ViewBag.ChangeButton = "Pay";

            return View();
        }

        // POST: SchoolFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameOfStudent,ClassTerm,Session,Amount,PhoneNumber,EmailAddress,RRR,PaymentOrderId,DateOfPayment,Term,ProcessingFee")] SchoolFee schoolFee, string PaidAmt, string ProFee)
        {


            //declaring variables
            string merchant_id = "000000";
            string serviceType_id = "000000";
            string apiKey = "0000";
            string gatewayURL = "http://www.remitademo.net/remita/ecomm/split/init.reg";
            //string returnedURLReceiptPage = "http://localhost:52062/SchoolFees/Receipt";
            string returnedURLReceiptPage = "http://lightwayfeesportal.azurewebsites.net/SchoolFees/Receipt";
            string order_Id;
            string totalAmount;
            string name;
            string phone;
            string email;
            string returnedURL;

            //configuring remita payment
            returnedURL = returnedURLReceiptPage;
            name = schoolFee.NameOfStudent;
            email = schoolFee.EmailAddress;
            phone = schoolFee.PhoneNumber;
            //totalAmount = "150";

            var amt = Convert.ToDecimal(ProFee);
            var tamt = Convert.ToDecimal(TempData["Amount"]) + amt;
            totalAmount = tamt.ToString();

            long milliseconds = DateTime.Now.Ticks;
            order_Id = milliseconds.ToString();
            string hash_string = merchant_id + serviceType_id + order_Id + totalAmount + returnedURL + apiKey;
            string hashed = SHA512(hash_string);
            string jsondata = "";
            string json = "{\"merchantId\":\"" + merchant_id + "\",\"serviceTypeId\":\"" + serviceType_id + "\",\"totalAmount\":\"" + totalAmount + "\",\"hash\":\"" + hashed + "\",\"orderId\":\"" + order_Id + "\",\"responseurl\":\"" + returnedURL + "\",\"payerName\":\"" + name + "\",\"payerEmail\":\"" + email + "\",\"payerPhone\":\"" + phone + "\",\"lineItems\":[ {\"lineItemsId\":\"881004944703\",\"beneficiaryName\":\"Department of Software Development\",\"beneficiaryAccount\":\"0360883515\",\"bankCode\":\"000\",\"beneficiaryAmount\":\"" + totalAmount + "\",\"deductFeeFrom\":\"1\"}]}";
            //string json = "{\"merchantId\":\"" + merchant_id + "\",\"serviceTypeId\":\"" + serviceType_id + "\",\"totalAmount\":\"" + totalAmount + "\",\"hash\":\"" + hashed + "\",\"orderId\":\"" + order_Id + "\",\"responseurl\":\"" + returnedURL + "\",\"payerName\":\"" + name + "\",\"payerEmail\":\"" + email + "\",\"payerPhone\":\"" + phone + "\",\"lineItems\":[ {\"lineItemsId\":\"881004944703\",\"beneficiaryName\":\"Department of Software Development\",\"beneficiaryAccount\":\"0360883515\",\"bankCode\":\"000\",\"beneficiaryAmount\":\"100\",\"deductFeeFrom\":\"1\"},{\"lineItemsId\":\"881004944703\",\"beneficiaryName\":\"Department of Software Development\",\"beneficiaryAccount\":\"6020067886\",\"bankCode\":\"000\",\"beneficiaryAmount\":\"50\",\"deductFeeFrom\":\"2\"}]}";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                try
                {

                    jsondata = client.UploadString(gatewayURL, "POST", json);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            jsondata = jsondata.Replace("jsonp(", "");
            jsondata = jsondata.Replace(")", "");
            SplitResponseVO result = JsonConvert.DeserializeObject<SplitResponseVO>(jsondata);

            if (result.statuscode != null && result.statuscode.Equals("025"))
            {
                ViewBag.MechantID = merchant_id;
                ViewBag.rrr = result.RRR;
                string rrrPaymenthash_string = merchant_id + result.RRR + apiKey;
                string rrrPaymentSHA = SHA512(rrrPaymenthash_string);
                ViewBag.Hash = rrrPaymentSHA;
                ViewBag.ResponseURL = returnedURL;
                ViewBag.paytype = "Remita";

                //saving other record to db
                schoolFee.RRR = result.RRR;
                schoolFee.PaymentOrderId = result.orderId;

                //saving other information
                schoolFee.DateOfPayment = DateTime.Now;

                schoolFee.ClassTerm = Convert.ToString(TempData["ClassName"]);
                schoolFee.Session = Convert.ToString(TempData["Session"]);
                schoolFee.Term = Convert.ToString(TempData["Term"]);
                schoolFee.Amount = Convert.ToDecimal(TempData["Amount"]);

                //viewbag for total amount
                ViewBag.TotalAmount = Convert.ToString(TempData["Tamt"]);

                db.SchoolFees.Add(schoolFee);
                db.SaveChanges();

                ViewBag.ChangeButton = "Remita";

            }
            else
            {
                return RedirectToAction("Index", "Fess");
            }
            return View();

        }

        //get receipt 
        public ActionResult Receipt()
        {

            string merchant_id = "2547916";
            string apiKey = "1946";
            string hashed;
            string order_Id;
            string checkstatusurl = "http://www.remitademo.net/remita/ecomm";
            string url;
            string message;
            string rrr;
            string statuscode;

            order_Id = Request.QueryString["orderID"].ToString();
            string hash_string = order_Id + apiKey + merchant_id;
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            url = checkstatusurl + "/" + merchant_id + "/" + order_Id + "/" + hashed + "/" + "orderstatus.reg";
            string jsondata = new WebClient().DownloadString(url);
            RemitaResponse result = JsonConvert.DeserializeObject<RemitaResponse>(jsondata);

            message = result.message;
            rrr = result.RRR;
            statuscode = result.status;

            //returning name, phone, email of the payer
            SchoolFee detail = db.SchoolFees.FirstOrDefault(t => t.PaymentOrderId == order_Id);

            ViewBag.statuscode = statuscode;
            ViewBag.rrr = rrr;
            ViewBag.Name = detail.NameOfStudent;
            ViewBag.Phone = detail.PhoneNumber;
            ViewBag.Email = detail.EmailAddress;
            ViewBag.Message = message;

            //updating rrr statues
            SchoolFee updaterrstatues = db.SchoolFees.FirstOrDefault(r => r.PaymentOrderId == order_Id);
            updaterrstatues.Statues = message;

            db.Entry(updaterrstatues).State = EntityState.Modified;
            db.SaveChanges();


            return View();
        }

        //Verify Payment
        [Authorize(Roles = "Admin")]
        public ActionResult Verify(int? id)
        {
            //getting detail of school fee payment using Id
            SchoolFee getdetal = db.SchoolFees.Find(id);

            string merchant_id = "2547916";
            string apiKey = "1946";
            string hashed;
            string order_Id;
            string checkstatusurl = "http://www.remitademo.net/remita/ecomm";
            string url;
            string message;
            string rrr;
            string statuscode;

            order_Id = getdetal.PaymentOrderId;
            string hash_string = order_Id + apiKey + merchant_id;
            hashed = SHA512(hash_string);
            url = checkstatusurl + "/" + merchant_id + "/" + order_Id + "/" + hashed + "/" + "orderstatus.reg";
            string jsondata = new WebClient().DownloadString(url);
            RemitaResponse result = JsonConvert.DeserializeObject<RemitaResponse>(jsondata);
            if (result != null)
            {
                message = result.message;
                rrr = result.RRR;
                statuscode = result.status;
            }


            //updating school fee database
            SchoolFee updaterrstatues = db.SchoolFees.FirstOrDefault(r => r.PaymentOrderId == order_Id);
            updaterrstatues.Statues = result.message;

            db.Entry(updaterrstatues).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index", "schoolfees", new { order_Id });
        }

        // GET: SchoolFees/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolFee schoolFee = db.SchoolFees.Find(id);
            if (schoolFee == null)
            {
                return HttpNotFound();
            }
            return View(schoolFee);
        }

        // POST: SchoolFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolFee schoolFee = db.SchoolFees.Find(id);
            db.SchoolFees.Remove(schoolFee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private string SHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
