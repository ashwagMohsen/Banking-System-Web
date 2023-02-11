using Banking_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banking_System.Controllers
{
    public class BankingController : Controller
    {  string outputValue1;
        BankingEntities db = new BankingEntities();
        // GET: Banking
        public ActionResult Index()
        {
            ViewBag.message1 = TempData["message1"];
            return View();
        }
        public ActionResult AddClient()
        {
           
            //ViewBag.job_Id = new SelectList(db.Clients, "id", "Jop_name");
            // var data = db.Clients.SqlQuery("[dbo].[InsertIntoClients]").ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(string FullName, string Jop, int Phone, string Address, decimal Balance, string typeOfAcc)
        {
            var parameters = new[] {
            new SqlParameter("@0", FullName),
              new SqlParameter("@1", Jop),
                new SqlParameter("@2", Phone),
                  new SqlParameter("@3", Address),
                   new SqlParameter("@4", typeOfAcc),
                    new SqlParameter("@5", Balance) ,
                    new SqlParameter("@6",SqlDbType.VarChar,200) {Direction=ParameterDirection.Output } };

            try
            {
               
                //Convert.ToInt32(Balance);
                //DBClass.ExcuteScalar("[dbo].[InsertIntoClients]",
                //  DBClass.CreateParameter("@FullName",FullName),
                // DBClass.CreateParameter("@Jop", Jop),
                // DBClass.CreateParameter("@PhoneNum", Phone),
                // DBClass.CreateParameter("@Address", Address),
                // DBClass.CreateParameter("@Typ_Acc", typeOfAcc),
                // DBClass.CreateParameter("@Balance", Balance));
                // DBClass.CreateParameter(outputValue1 = "@Message", ParameterDirection.Output)

            
                var data = db.Clients.SqlQuery("[dbo].[InsertIntoClients]@FullName=@0,@Jop=@1,@PhoneNum=@2,@Address=@3, @Typ_Acc=@4,@Balance=@5 , @Message=@6 output", parameters).ToList();

                //ViewBag.job_Id = new SelectList(db.Clients, "id", "Jop_name", db.Accounts.C_Id);
                //if (ModelState.IsValid)
                //{
                //}
                outputValue1 = (string)parameters[6].Value;

                TempData["message1"] = outputValue1;
                return RedirectToAction("Index");
            }
            catch
            {
                outputValue1 = (string)parameters[6].Value;

                TempData["message1"] = outputValue1;
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult AddAcount()
        {
            ViewBag.C_Id = new SelectList(db.Clients, "C_Id", "C_Id");
            ViewBag.Typ_Id = new SelectList(db.TypeOfAccounts, "Typ_Id", "Typ_Name");

            return View();
        }
        [HttpPost]
        public ActionResult AddAcount(int C_Id, decimal Balance, string typeOfAcc)
        {
            var parameters = new[] {
            new SqlParameter("@0", C_Id),
              new SqlParameter("@1", typeOfAcc),
                new SqlParameter("@2",Balance ),
                    new SqlParameter("@3",SqlDbType.VarChar,200) {Direction=ParameterDirection.Output } };
            try
            {
                //Convert.ToInt32(Balance);
                //string outputValue1;
                
                var data = db.Accounts.SqlQuery("[dbo].[InsertIntoAcc]@C_Id=@0,@Typ_Acc=@1,@Balance=@2 , @Message=@3 output", parameters).ToList();
                //db.Accounts.SqlQuery("EXEC @ReturnValue =[dbo].[InsertIntoAcc]@C_Id=@0,@typeOfAcc=@1,@Balance=@2 ", parameters);

                //.AccountsSqlQuery("[dbo].[InsertIntoAcc]@C_Id=@0,@typeOfAcc=@1,@Balance=@2 , @Message=@3 output", parameters).ToList();
                // db.InsertIntoAcc(C_Id, typeOfAcc,Convert.ToDecimal( Balance));
                //وفي حالة كان متغيرContextببحث عن استدعاء دوال كلاس ال
                 outputValue1 = (string)parameters[3].Value;
                TempData["message1"] = outputValue1;
                Accounts acc1 = new Accounts();
                ViewBag.C_Id = new SelectList(db.Clients, "C_Id", "C_Id", acc1.C_Id);
                //ViewBag.Typ_Id = new SelectList(db.TypeOfAccounts, "Typ_Id", "Typ_Name", acc1.Typ_Id);

                return RedirectToAction("Index");
            }
            catch
            {
                 outputValue1 = (string)parameters[3].Value;
                TempData["message1"] = outputValue1;
                Accounts acc1 = new Accounts();
                //ViewBag.Typ_Id = new SelectList(db.TypeOfAccounts, "Typ_Name", "Typ_Id", acc1.Typ_Id);

                ViewBag.C_Id = new SelectList(db.Clients, "C_Id", "C_Id", acc1.C_Id);
                return RedirectToAction("Index");
            }
            return View();
          
        }
        public ActionResult Deposit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Deposit(string AccNum, string Amount, string typeOfAcc)
        {
            // ObjectParameter y = new ObjectParameter("outputpar", typeof(string));


            var parameters = new[] {
            new SqlParameter("@0",  Convert.ToInt32(AccNum)),
              new SqlParameter("@1", typeOfAcc),
                new SqlParameter("@2",Convert.ToInt32(Amount) ),
                    new SqlParameter("@3",SqlDbType.VarChar,200) {Direction=ParameterDirection.Output } };
            try
            {
                var data = db.Accounts.SqlQuery("[dbo].[Deposit]@Typ_Acc=@1,@Amount=@2 ,@AccId=@0, @Message=@3 output", parameters).ToList();

                //var data = db.Deposit(typeOfAcc, Convert.ToDecimal(Amount), Convert.ToInt32(AccNum), y).ToString();
                 outputValue1 = (string)parameters[3].Value;
                TempData["message1"] = outputValue1;

                return RedirectToAction("Index");
            }
            catch
            {
                outputValue1 = (string)parameters[3].Value;
                TempData["message1"] = outputValue1;

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Withdraw()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Withdraw(string AccNum, string Amount, string typeOfAcc)
        {
            var parameters = new[] {
            new SqlParameter("@0",  Convert.ToInt32(AccNum)),
              new SqlParameter("@1", typeOfAcc),
                new SqlParameter("@2",  Convert.ToInt32(Amount) ),
                    new SqlParameter("@3",SqlDbType.VarChar,200) {Direction=ParameterDirection.Output } };

            try
            {
                var data = db.Accounts.SqlQuery("[dbo].[Withdraw]@Typ_Acc=@1,@Amount=@2 ,@AccId=@0, @Message=@3 output", parameters).ToList();
                 outputValue1 = (string)parameters[3].Value;

                TempData["message1"] = outputValue1;
                return RedirectToAction("Index");
            }
            catch
            {
                outputValue1 = (string)parameters[3].Value;

                TempData["message1"] = outputValue1;
                return RedirectToAction("Index");
            
        }
            return View();
        }
        public ActionResult Transefare()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Transefare(int AccNum, int AccNum2, string Balance, string typeOfAcc, string typeOfAcc2)
        {

            var parameters = new[] {
            new SqlParameter("@0", typeOfAcc),
              new SqlParameter("@1", typeOfAcc2),
                new SqlParameter("@2", Convert.ToDecimal(Balance) ),
                   new SqlParameter("@3",AccNum ),
                      new SqlParameter("@4", AccNum2 ),
                    new SqlParameter("@5",SqlDbType.VarChar,200) {Direction=ParameterDirection.Output } };
            try
            {

                var data = db.Accounts.SqlQuery("[dbo].[Transfare]@Typ_Acc=@0,@Typ_Acc2=@1,@Amount=@2,@AccId=@3,@AccId2=@4, @Message=@5 output", parameters).ToList();
                 outputValue1 = (string)parameters[5].Value;
                TempData["message1"] = outputValue1;
                return RedirectToAction("Index");
            }
            catch
            {
                outputValue1 = (string)parameters[5].Value;
                TempData["message1"] = outputValue1;
                return RedirectToAction("Index");
            }
            return View();
        }
        // SqlDataReader reader = DBClass.ExcuteReader("sp_reg1_select_By_Time_datte",
        //DBClass.CreateParameter("@date", date11),
        //       DBClass.CreateParameter("@time", time11));
        public ActionResult Display_Accounts()
        {
           
              var  data = db.Accounts.SqlQuery("[dbo].[Display_Accounts]");


            return View(data.ToList());
        }
        [HttpPost]
        public ActionResult Display_Accounts(string AccNum)
        {
            var parameters = new[] {
            new SqlParameter("@0", AccNum) };
           var data = db.Accounts.SqlQuery("[dbo].[Display_Specific_Account]@accNum=@0", parameters);

          
            return View(data.ToList());
        }
        public ActionResult Display_Clints()
        {
            var data =db.Clients.SqlQuery("[dbo].[Display_Clients]");

            return View(data.ToList());
        }
        [HttpPost]
        public ActionResult Display_Clints(string ClientName)
        {
            var parameters = new[] {
            new SqlParameter("@0", ClientName) };
            var data =db.Clients.SqlQuery("[dbo].[Display_Specific_Client]@Cli_name=@0", parameters);

       
            return View(data.ToList());
        }
        //---------------------------------------------------------------------
        public ActionResult Display_Employee ()
        {
           
              var  data = db.Employees.SqlQuery("[dbo].[Display_Employee]").ToList();

            return View(data);
        }
        [HttpPost]
        public ActionResult Display_Employee(string EmpName)
        {
            var parameters = new[] {
            new SqlParameter("@0", EmpName) };
            var data = db.Employees.SqlQuery("[dbo].[Display_Specific_Employee]@EmpName=@0", parameters);


            return View(data.ToList());
        }
        public ActionResult Display_Transactions()
        {
            var data = db.Transactions.SqlQuery("[dbo].[Display_Transactions]");

            return View(data.ToList());
        }
        [HttpPost]
        public ActionResult Display_Transactions(string name)
        {
            var parameters = new[] {
            new SqlParameter("@0", name) };
            var data = db.Transactions.SqlQuery("[dbo].[Display_Specific_Transaction]@name=@0", parameters);


            return View(data.ToList());
        }
    }
}