
using HW.Interfaces;
using HW.Models;
using HW.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FintechRestAPI.Controllers
{
    /// <summary>
    ///  FintechController class handles HTTP GET, POST, PUT, and DELETE requests.
    /// </summary>
    /// 
    [ApiController]
    [Route("[controller]")]

    public class FintechController : ControllerBase
    {
        /// <summary>
        /// _logger is property of  Controller  that implements ILogger interface. 
		/// It is used for logging purpose,it is defined  as private and readonly
        /// </summary>

        private readonly ILogger<FintechController> _logger;
        private readonly IFintechRepository _fintech;

        public FintechController(ILogger<FintechController> logger, IFintechRepository fintech)
        {
            _logger = logger;
            _fintech = fintech;
        }



		// <summary>
		/// Action for getting Item
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Fintech>))]
		public IActionResult getItems()
		{
			_logger.Log(LogLevel.Information, "Get Items");
			return Ok(_fintech.getItems());
		}

		/// <summary>
		/// Action for getting Item based on id
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Fintech))]
		[ProducesResponseType(404)]
		public IActionResult GetItem(int id)
		{
			Fintech get_data = _fintech.GetItem(id);
			if (get_data == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(get_data);
			}
		}

		/// <summary>
		/// Action for checking if the Account exists
		/// </summary>
		/// <param name="id"></param>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>
		[HttpGet("AccountExists")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult AccountExists(int id)
        {
            bool result = _fintech.AccountExists(id);

            if (!result)
            {
                return NotFound("No matching id");
            }
            else
            {
                return Ok("Account exists");
            }
        }

		/// <summary>
		/// Action for providing customer service contact
		/// </summary>
		/// <param name="id"></param>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>

		[HttpGet("CustomerService")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult CustomerService()
        {
            return Ok(_fintech.CustomerService());
        }

		// <summary>
		/// Action to view customer service requests
		/// </summary>
		/// <param name="id"></param>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>

		[HttpGet("GetCustomerServiceById")]
        [ProducesResponseType(200, Type = typeof(CustomerService))]
        [ProducesResponseType(404)]
        public IActionResult GetCustomerServiceById(int id)
        {
            CustomerService customerservice = _fintech.GetCustomerServiceById(id);
            if (customerservice == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(customerservice);
            }

        }

		// <summary>
		/// Action to create new customer service requests
		/// </summary>
		/// <param name="customerServiceDto"></param>
		/// <returns>action will return a 200 Ok status code and update in database</returns>


		[HttpPost("CreateCustomerService")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCustomerService([FromBody] CustomerService customerservice)
        {
            //var customerservice = new CustomerService()
            //{
            //    PhoneNumber = customerServiceDto.PhoneNumber,
            //    CustomerServiceDescription = customerServiceDto.CustomerServiceDescription,
            //    Date = customerServiceDto.Date,
            //};

            if (customerservice == null)
            {
                return BadRequest("Customer Service is null");
            }

            bool result = _fintech.CreateCustomerService(customerservice);
            return result ? Ok("Customer Service Request created") : BadRequest();
        }





		/// <summary>
		/// Action for creating Item
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>


		[HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult AddAccount([FromBody] Fintech account)
        {
            if (account == null)
            {
                return BadRequest("Account is null");
            }

            bool result = _fintech.AddAccount(account);
            return result ? Ok(result) : BadRequest();
        }



		/// <summary>
		/// Action for updating Item
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>
		[HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult EditAccount([FromBody] Fintech account)
        {
            if (account == null)
            {
                return BadRequest("Account is null");
            }
            
            Fintech result = _fintech.GetItem(account.Id);
            if (account == null)
            {
                return BadRequest("Account not found");
            }
            result.Firstname = account.Firstname;
            result.Lastname = account.Lastname;
            result.Address = account.Address;
            result.Email = account.Email;
            result.Contact = account.Contact;
            result.Age = account.Age;
            result.SSN = account.SSN;
            result.Balance = account.Balance;

            bool isUpdated = _fintech.EditAccount(result);
            return isUpdated ? Ok(result) : BadRequest();
        }



		// <summary>
		/// Action for deleting Item
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>

		[HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            bool result = _fintech.DeleteAccount(id);

            return result ? Ok(result) : BadRequest();
        }



		/// <summary>
		/// Action for getting Item
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>
		[HttpGet("GetAllExpenses")]
        [ProducesResponseType(200, Type = typeof(List<FinTechModel>))]

        public IActionResult getAllExpenses()
        {
            _logger.Log(LogLevel.Information, "Get All Expenses");
            return Ok(_fintech.getAllExpenses());
        }

		// <summary>
		/// Action for getting Item based on id
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>
		[HttpGet("GetExpenses")]
        [ProducesResponseType(200, Type = typeof(Fintech))]
        [ProducesResponseType(404)]

        public IActionResult GetExpense(int id)
        {
            FinTechModel get_data = _fintech.GetExpense(id);
            if (get_data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(get_data);
            }

        }



		/// <summary>
		/// Action for creating Expense
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>


		[HttpPost("AddExpense")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult AddExpense([FromBody] FinTechModel expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense is null");
            }

            bool result = _fintech.AddExpense(expense);
            return result ? Ok("Expense has been created") : BadRequest();
        }

		/// <summary>
		/// Action for updating Expense
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>


		[HttpPut("UpdateExpense")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult UpdateExpense([FromBody] FinTechModel updated)
        {
            if (updated == null)
            {
                return BadRequest("Expense is null");
            }
            bool result = _fintech.UpdateExpense(updated);

            return result ? Ok("Expense has been updated") : BadRequest();
        }


		/// <summary>
		/// Action for deleting Expense
		/// </summary>
		/// <returns>action will return a 200 Ok status code when it runs successfully</returns>


		[HttpDelete("DeleteExpense/{id}")]
        public IActionResult DeleteExpense(int id)
        {
            bool result = _fintech.DeleteExpense(id);

            return result ? Ok("Expense is Deleted") : BadRequest();
        }



		/// <summary>
		/// Action for calculating max, min, average of amount
		/// </summary>
		/// <returns>returns max, min, average</returns>

		[HttpGet("Analyse")]
        [ProducesResponseType(200)]
        public IActionResult Analyse()
        {
            _logger.Log(LogLevel.Information, "Get analysis");
            return Ok(_fintech.DataAnalysis());
        }

		/// <summary>
		/// Action for withdrawing amount from an account
		/// </summary>
		/// <returns>returns a 200 Ok status code if successful, otherwise returns a 400 Bad Request or 404 Not Found status code</returns>


		[HttpPut("WithdrawAmount/{id}/{withdrawAmount}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]

		public IActionResult WithdrawAmount(int id, double withdrawAmount)
		{
			Fintech account = _fintech.GetItem(id);
			if (account == null)
			{
				return NotFound();
			}

			bool result = _fintech.WithdrawAmount(id, withdrawAmount);
			return result ? Ok("Amount has been withdrawn") : BadRequest("Insufficient Funds");
		}

		/// <summary>
		/// Action for depositing checks into an account
		/// </summary>
		/// <returns>returns a 200 Ok status code if successful, otherwise returns a 400 Bad Request or 404 Not Found status code</returns>
        
		[HttpPut("DepositChecks/{id}/{Check_Amount}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]

		public IActionResult DepositCheck(int id, double Check_Amount)
		{
			Fintech account = _fintech.GetItem(id);
			if (account == null)
			{
				return NotFound();
			}

			bool result = _fintech.DepositCheck(id, Check_Amount);
			return result ? Ok("Check Successfully Deposited") : BadRequest("Check Deposit Failed");
		}

		/// <summary>
		/// Action for transferring amount from one account to another account
		/// </summary>
		/// <returns>returns a 200 Ok status code if successful, otherwise returns a 400 Bad Request or 404 Not Found status code</returns>


		[HttpPut("TransferAmount")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]

		public IActionResult TransferAmount(int A1, int A2, double Amount)
		{
			Fintech account = _fintech.GetItem(A1);
			if (account == null)
			{
				return NotFound();
			}

			bool result1 = _fintech.WithdrawAmount(A1, Amount);
			bool result2 = _fintech.DepositCheck(A2, Amount);
			//bool result = _fintech.TransferAmount(A1, A2, Amount);  
			if (result1 && result2 == true)
			{
				return Ok("Amount Successfully Transfered");
			}
			else
			{
				return BadRequest("transfer failed");
			}
			// result2? Ok("Amount Successfully Transfered") : BadRequest("Transfer Failed");
		}


	}



}