﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MEMCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.JsonPatch;

namespace MEMCore.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MEMController : ControllerBase
    {
        private IExpenseRepository _expenseRepository;
        private ICategoryRepository _categoryRepository;
        private ICurrencyRepository _currencyRepository;
        private IMapper _mapper;
        private LinkGenerator _linkGenerator;
        private List<Models.Expense> expensesMapping(List<Domain.Expense> exp)
        {
            var outExp = new List<MEMCore.Models.Expense>();
            foreach (var item in exp)
            {
                outExp.Add(new MEMCore.Models.Expense
                {
                    Id = item.Id,
                    ExpenseTitle = item.ExpenseTitle,
                    ExpensesAmount = item.ExpensesAmount,
                    ExpenseDate = item.ExpenseDate,
                    Signature = item.Signature,
                    ExpenseDetail = item.ExpenseDetail == null ? null : item.ExpenseDetail.Detail,
                    CategoryId = item.ExpenseCategoryId,
                    Category = item.Category.Category,
                    CurrencyId = item.CurrencyId,
                    Currency = item.Currency.CurrencyName
                });
            }
            return outExp;
        }
        public MEMController(IMapper mapper, LinkGenerator linkGenerator, IExpenseRepository expenseRepository,
            ICategoryRepository categoryRepository, ICurrencyRepository currencyRepository)
        {

            _expenseRepository = expenseRepository ?? throw new ArgumentException(nameof(expenseRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));
            _currencyRepository = currencyRepository ?? throw new ArgumentException(nameof(currencyRepository));

            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet()]
        public IActionResult DefaultMEM()
        {
            //return StatusCode(StatusCodes.Status200OK, new JsonResult(new
            //{
            //  Message = "Welcome to Monthly Expense Management System",
            //  Status = "Ok"
            //}));
            return Ok(new
            {
                Message = "Welcome to Monthly Expense Management System",
                Status = "Ok"
            });
        }
        [HttpGet("Info")]
        [MapToApiVersion("1.0")]
        public IActionResult GetVersion()
        {
            var output = new JsonResult(new
            {
                Version = 1.0,
                message = "MEM : Monthly Expense Management",
                APIStatus = "Active",
                date = DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm:ss.fff"),
                DatabaseStatus = "is Off-line"
            });
            output.ContentType = "application/json";
            output.StatusCode = 200;
            return output;
        }

        //[HttpGet("Info")]
        //[MapToApiVersion("1.1")]
        //public IActionResult GetVersion_v11()
        //{
        //    var output = new JsonResult(new
        //    {
        //        Version = 1.1,
        //        message = "MEM : Monthly Expense Management",
        //        APIStatus = "Active",
        //        date = DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm:ss.fff"),
        //        DatabaseStatus = "is Off-line"
        //    });
        //    output.ContentType = "application/json";
        //    output.StatusCode = 200;
        //    return output;
        //}

        //[HttpGet("GetCategories")]
        //[MapToApiVersion("1.0")]
        //public async Task<IActionResult> GetCategories_v10([FromQuery] bool IsSorted = false)
        //{
        //    try
        //    {
        //        var cat = await _categoryRepository.GetCategoriesAsync(IsSorted);
        //        MEMCore.Models.Category[] catModel = _mapper.Map<MEMCore.Models.Category[]>(cat);
        //        return Ok(catModel);

        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.Error(ex);
        //        System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
        //        return this.StatusCode(StatusCodes.Status500InternalServerError,
        //            new { Message = ex.Message, Status = "Error" });
        //    }
        //}

        [HttpGet("ExpCategory")]
        [MapToApiVersion("1.1")]
        public async Task<ActionResult<Models.Category[]>> GetCategories11([FromQuery] bool IsSorted = false)
        {
            try
            {
                var cat = await _categoryRepository.GetCategoriesAsync(IsSorted);
                Models.Category[] catModel = _mapper.Map<Models.Category[]>(cat);
                return catModel;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }

        [HttpGet("ExpCategory/{id:int}")]
        public async Task<ActionResult<Models.Category>> GetCategories([FromRoute] int id)
        {
            try
            {
                var cat = await _categoryRepository.GetExpenseCategoryAsync(id);
                if (cat == null) return NotFound();

                Models.Category catModel = _mapper.Map<Models.Category>(cat);

                return catModel;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }

        }

        [HttpGet("ExpCurrency/{id:int}")]
        public async Task<ActionResult<Models.Currency>> GetACurrency([FromRoute] int id)
        {
            try
            {
                var cur = await _currencyRepository.GetCurrencyAsync(id);
                if (cur == null) return NotFound();
                Models.Currency curModel = _mapper.Map<Models.Currency>(cur);
                return Ok(curModel);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }
        [HttpGet("ExpCurrency")]
        public async Task<ActionResult<Models.Currency[]>> GetCurrencies([FromQuery] bool IsSorted = false)
        {
            try
            {
                var cur = await _currencyRepository.GetCurrencyAsync(IsSorted);
                Models.Currency[] curModel = _mapper.Map<Models.Currency[]>(cur);

                return Ok(curModel);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }

        [HttpGet("Expenses")]
        public async Task<ActionResult<Models.Expense[]>> GetExpenses()
        {
            try
            {
                var exp = await _expenseRepository.GetExpensesAsync();
                Models.Expense[] expModel = _mapper.Map<Models.Expense[]>(exp);
                return expModel;                //return Ok(expensesMapping(exp.ToList()));
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }
        [HttpGet("Expense/{id:int}", Name = "GetExpense")]
        public async Task<ActionResult<Models.Expense>> GetExpense([FromRoute]int id)
        {
            try
            {
                var exp = await _expenseRepository.GetExpensesAsync(id);
                if (exp.Id <= 0) return NotFound();
                Models.Expense expModel = _mapper.Map<Models.Expense>(exp);
                return expModel;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }

        [HttpGet("ExpensesForLastMonth")]
        public async Task<ActionResult<Models.Expense[]>> GetExpensesForLastMonth()
        {
            try
            {
                var exp = await _expenseRepository.GetExpensesAsync(DateTime.Now.AddMonths(-1), DateTime.Now);
                Models.Expense[] expModel = _mapper.Map<Models.Expense[]>(exp);
                return expModel;                //return Ok(expensesMapping(exp.ToList()));
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }
        [HttpGet("ExpensesBetweenDates")]
        public async Task<ActionResult<Models.Expense[]>> GetExpensesBetweenDates([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            try
            {
                DateTime oFrom = from ?? DateTime.Now.AddMonths(-1);
                DateTime oTo = to ?? DateTime.Now;

                var exp = await _expenseRepository.GetExpensesAsync(oFrom, oTo);
                Models.Expense[] expModel = _mapper.Map<Models.Expense[]>(exp);
                return expModel;                //return Ok(expensesMapping(exp.ToList()));
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }

        [HttpPost("Expense")]
        public async Task<ActionResult<Models.Expense>> AddANewExpense([FromBody] Models.Expense exp)
        {
            try
            {
                var oExpenses = new Domain.Expense();

                if (exp != null)
                {
                    if (exp.ExpenseTitle == exp.ExpenseDetail)
                    {
                        ModelState.AddModelError("Description", "Expense Title and details can't be same");
                    }
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    //DK: Automapper doesn't work need to check later
                    //var expDomain = _mapper.Map<Domain.Expense>(exp);

                    //* Mapping the Model object to Domain object >> Business logic may apply here *//
                    oExpenses.ExpenseTitle = exp.ExpenseTitle;
                    oExpenses.ExpensesAmount = exp.ExpensesAmount;
                    oExpenses.ExpenseDate = exp.ExpenseDate;
                    oExpenses.CurrencyId = exp.CurrencyId;
                    oExpenses.ExpenseCategoryId = exp.CategoryId;
                    if (exp.ExpenseDetail != null)
                        oExpenses.ExpenseDetail = new Domain.ExpenseDetail { Detail = exp.ExpenseDetail };
                    oExpenses.Signature = exp.Signature;
                    var i = await _expenseRepository.NewExpensesAsync(oExpenses);
                    var returnExpense = _mapper.Map<Models.Expense>(oExpenses);

                    //approach1: get location url 
                    var baseUrl = HttpContext.Request.Scheme +"//"
                        + HttpContext.Request.Host.ToUriComponent() 
                        + Url.RouteUrl(RouteData.Values);
                    var newLocation = baseUrl + "/" + returnExpense.Id;
                    
                    //approach2: get location url, not working need to check
                    var location = _linkGenerator.GetPathByAction("Get", "MEM", returnExpense.Id);
                    //return Created(location, returnExpense);

                    //approach3: get location url, best alternative
                    return CreatedAtRoute("GetExpense", new { id = returnExpense.Id }, returnExpense);
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                    new JsonResult(new { Status = "Error", Message = "unable to add new expense" }));
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }

        }
        [HttpDelete("Expense/{id:int}")]
        public async Task<IActionResult> RemoveExpense([FromRoute] int? id = null)
        {
            int expId = id.HasValue ? (int)id : -1;
            if (expId > 0)
            {

                var result = await _expenseRepository.DeleteExpensesAsync(expId);
                return Ok();
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new JsonResult(new { Status = "Error", Message = "Unable to delete expense" }));
        }
        [HttpPut("Expense/{id:int}")]
        public async Task<IActionResult> UpdateExpense([FromBody] MEMCore.Models.ExpenseForUpdate exp, [FromRoute] int id)
        {
            var oExpenses = new Domain.Expense();
            try
            {
                if (exp.ExpenseTitle == exp.ExpenseDetail)
                    ModelState.AddModelError("Description", "Expense Title and details can't be same");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!await _expenseRepository.ExpenseExist(id))
                    return NotFound();
                //* Mapping the Model object to Domain object >> Business logic may apply here *//
                oExpenses.ExpenseTitle = exp.ExpenseTitle;
                oExpenses.ExpensesAmount = exp.ExpensesAmount;
                oExpenses.ExpenseDate = exp.ExpenseDate;
                oExpenses.CurrencyId = exp.CurrencyId;
                oExpenses.ExpenseCategoryId = exp.CategoryId;
                oExpenses.ExpenseDetail = new Domain.ExpenseDetail { Detail = exp.ExpenseDetail };
                oExpenses.Signature = exp.Signature;
                var i = await _expenseRepository.EditExpensesAsync(oExpenses, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
            //return StatusCode(StatusCodes.Status400BadRequest,
            //    new JsonResult(new { Status = "Error", Message = "unable to add new expense" }));
        }

        [HttpPatch("Expense/{id:int}")]
        public async Task<IActionResult> PartiallyUpdateExpense([FromBody] JsonPatchDocument<Models.ExpenseForUpdate> patchExp, [FromRoute] int id)
        {
            var expToPatch = new Models.ExpenseForUpdate();
            var oExpenses = new Domain.Expense();
            try
            {
                if (patchExp == null) return BadRequest();

                //Get the expense from the database
                var dbExp = await _expenseRepository.GetExpensesAsync(id);
                if (dbExp == null) return NotFound();

                //Create patch Expense from the database expense
                ////* Mapping the Domain object to Model object  >> Business logic may apply here *//
                expToPatch.ExpenseTitle = dbExp.ExpenseTitle;
                expToPatch.ExpensesAmount = dbExp.ExpensesAmount;
                expToPatch.ExpenseDate = dbExp.ExpenseDate;
                expToPatch.CurrencyId = dbExp.CurrencyId;
                expToPatch.CategoryId = dbExp.ExpenseCategoryId;
                expToPatch.ExpenseDetail = dbExp.ExpenseDetail == null ? null: dbExp.ExpenseDetail.Detail;
                expToPatch.Signature = dbExp.Signature;

                //DK: Problem in automapper need to fix
                //var expToPatch = _mapper.Map<Models.ExpenseForUpdate>(dbExp);

                // Apply Expense to ModelState
                patchExp.ApplyTo(expToPatch, ModelState);

                //Add custom validation rules if needed as below
                if (expToPatch.ExpenseTitle == expToPatch.ExpenseDetail)
                    ModelState.AddModelError("Description", "Expense Title and details can't be same");
                
                //Validate the model state
                TryValidateModel(expToPatch);
                if (!ModelState.IsValid) return BadRequest(ModelState);

                //Create an expense object to update the database
                //* Mapping the Model object to Domain object >> Business logic may apply here *//
                oExpenses.ExpenseTitle = expToPatch.ExpenseTitle;
                oExpenses.ExpensesAmount = expToPatch.ExpensesAmount;
                oExpenses.ExpenseDate = expToPatch.ExpenseDate;
                oExpenses.CurrencyId = expToPatch.CurrencyId;
                oExpenses.ExpenseCategoryId = expToPatch.CategoryId;
                if (expToPatch.ExpenseDetail != null)
                    oExpenses.ExpenseDetail = new Domain.ExpenseDetail { Detail = expToPatch.ExpenseDetail };
                oExpenses.Signature = expToPatch.Signature;

                var row_affected = await _expenseRepository.EditExpensesAsync(oExpenses, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }

    }
}