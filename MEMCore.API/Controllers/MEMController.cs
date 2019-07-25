using System;
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
        //private List<Models.Expense> expensesMapping(List<Domain.Expense> exp)
        //{
        //    var outExp = new List<MEMCore.Models.Expense>();
        //    foreach (var item in exp)
        //    {
        //        outExp.Add(new MEMCore.Models.Expense
        //        {
        //            Id = item.Id,
        //            ExpenseTitle = item.ExpenseTitle,
        //            ExpensesAmount = item.ExpensesAmount,
        //            ExpenseDate = item.ExpenseDate,
        //            Signature = item.Signature,
        //            ExpenseDetail = item.ExpenseDetail == null ? null : item.ExpenseDetail.Detail,
        //            CategoryId = item.ExpenseCategoryId,
        //            Category = item.Category.Category,
        //            CurrencyId = item.CurrencyId,
        //            Currency = item.Currency.CurrencyName
        //        });
        //    }
        //    return outExp;
        //}
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
                var dCategory = await _categoryRepository.GetCategoriesAsync(IsSorted);
                Models.Category[] mCategory = _mapper.Map<Models.Category[]>(dCategory);
                return mCategory;
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
                var dCategory = await _categoryRepository.GetExpenseCategoryAsync(id);
                if (dCategory == null) return NotFound();

                Models.Category mCategory = _mapper.Map<Models.Category>(dCategory);

                return mCategory;
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
                var dCurrency = await _currencyRepository.GetCurrencyAsync(id);
                if (dCurrency == null) return NotFound();
                Models.Currency mCurrency = _mapper.Map<Models.Currency>(dCurrency);
                return Ok(mCurrency);
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
                var dCurrency = await _currencyRepository.GetCurrencyAsync(IsSorted);
                Models.Currency[] mCurrency = _mapper.Map<Models.Currency[]>(dCurrency);

                return Ok(mCurrency);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }


        [HttpGet("ExpensesWithoutId", Name = "ListOfExpenses_WithoutId's")]
        public async Task<ActionResult<List<Models.ExpenseForList>>> GetExpensesNoId()
        {
            try
            {
                var dExpense = await _expenseRepository.GetExpensesAsync();
                List<Models.ExpenseForList> mExpense = _mapper.Map<List<Models.ExpenseForList>>(dExpense);
                return mExpense;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, " +
                    $"Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }


        [HttpGet("Expenses", Name="ListOfExpenses_WithId's")]
        public async Task<ActionResult<List<Models.Expense>>> GetExpenses()
        {
            try
            {
                var dExpense = await _expenseRepository.GetExpensesAsync();
                //Models.Expense[] expModel = _mapper.Map<Models.Expense[]>(exp);
                List<Models.Expense> mExpense = _mapper.Map<List<Models.Expense>>(dExpense);
                return mExpense;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                System.Diagnostics.Debug.WriteLine($"Error Type {ex.GetType().FullName}, " +
                    $"Error Message: {ex.Message}.\n More Detailed: {ex.InnerException}");

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message, Status = "Error" });
            }
        }


        [HttpGet("Expense/{id:int}", Name = "GetExpenseByID")]
        public async Task<ActionResult<Models.ExpenseForList>> GetExpense([FromRoute]int id)
        {
            try
            {
                var dExpense = await _expenseRepository.GetExpensesAsync(id);
                if (dExpense.Id <= 0) return NotFound();

                Models.ExpenseForList mExpenseList = _mapper.Map<Models.ExpenseForList>(dExpense);
                return mExpenseList;
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
        public async Task<ActionResult<List<Models.Expense>>> GetExpensesForLastMonth()
        {
            try
            {
                var dExpense = await _expenseRepository.GetExpensesAsync(DateTime.Now.AddMonths(-1), DateTime.Now);
                List<Models.Expense> mExpense = _mapper.Map<List<Models.Expense>>(dExpense);
                return mExpense;          
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
        public async Task<ActionResult<List<Models.Expense>>> GetExpensesBetweenDates([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            try
            {
                DateTime oFrom = from ?? DateTime.Now.AddMonths(-1);
                DateTime oTo = to ?? DateTime.Now;

                var dExpense = await _expenseRepository.GetExpensesAsync(oFrom, oTo);
                List<Models.Expense> mExpense = _mapper.Map< List<Models.Expense>>(dExpense);
                return mExpense;               
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
        public async Task<ActionResult<Models.Expense>> AddANewExpense([FromBody] Models.ExpenseForInsert newExpense)
        {
            try
            {

                if (newExpense != null)
                {
                    if (newExpense.ExpenseTitle == newExpense.ExpenseDetail)
                    {
                        ModelState.AddModelError("Description", "Expense Title and details can't be same");
                    }
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    //DK: Auto mapper doesn't work for conditional mapping, need to check & fix later
                    //var oExpenses = _mapper.Map<Domain.Expense>(newExpense);
                    //var i = await _expenseRepository.NewExpensesAsync(oExpenses);
                    //var returnExpense = _mapper.Map<Models.Expense>(oExpenses);

                    //****** approach 2: Mapping the Model object to Domain object >> Business logic may apply here ******//
                    var dExpense = new Domain.Expense();
                    dExpense.ExpenseTitle = newExpense.ExpenseTitle;
                    dExpense.ExpensesAmount = newExpense.ExpensesAmount;
                    dExpense.ExpenseDate = newExpense.ExpenseDate;
                    dExpense.CurrencyId = newExpense.CurrencyId;
                    dExpense.ExpenseCategoryId = newExpense.CategoryId;
                    if (Enum.TryParse(newExpense.PaymentMethod.ToString(), out Domain.PaymentMethod paymentMethod))
                        dExpense.PaymentMethod = paymentMethod;
                    if (Enum.TryParse(newExpense.PaymentType.ToString(), out Domain.PaymentType paymentType))
                        dExpense.PaymentType = paymentType;
                    //oExpenses.PaymentType = newExpense.PaymentType;
                    if (newExpense.ExpenseDetail != null)
                        dExpense.ExpenseDetail = new Domain.ExpenseDetail { Detail = newExpense.ExpenseDetail };
                    dExpense.Signature = newExpense.Signature;

                    var i = await _expenseRepository.NewExpensesAsync(dExpense);
                    var returnExpense = _mapper.Map<Models.Expense>(dExpense);
                    //****************************Mapping approach 2 ends********************//

                    //approach1: get location url 
                    var baseUrl = HttpContext.Request.Scheme +"//"
                        + HttpContext.Request.Host.ToUriComponent() 
                        + Url.RouteUrl(RouteData.Values);
                    var newLocation = baseUrl + "/" + returnExpense.Id;
                    
                    //approach2: only in Version 2.2 and above, get location url, not working need to check further
                    var location = _linkGenerator.GetPathByAction("GetExpense", "MEM", returnExpense.Id);
                    //return Created(location, returnExpense);

                    //approach3: get location url, best alternative
                    return CreatedAtRoute("GetExpenseByID", new { id = returnExpense.Id }, returnExpense);
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
        public async Task<IActionResult> UpdateExpense([FromBody] MEMCore.Models.ExpenseForUpdate mExpenseUodate, [FromRoute] int id)
        {
           try
            {
                if (mExpenseUodate.ExpenseTitle == mExpenseUodate.ExpenseDetail)
                    ModelState.AddModelError("Description", "Expense Title and details can't be same");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!await _expenseRepository.ExpenseExist(id))
                    return NotFound();

                //* Mapping the Model object to Domain object >> Business logic may apply here *//
                var dExpense = new Domain.Expense();
                dExpense.ExpenseTitle = mExpenseUodate.ExpenseTitle;
                dExpense.ExpensesAmount = mExpenseUodate.ExpensesAmount;
                dExpense.ExpenseDate = mExpenseUodate.ExpenseDate;
                dExpense.CurrencyId = mExpenseUodate.CurrencyId;
                dExpense.ExpenseCategoryId = mExpenseUodate.CategoryId;
                if (Enum.TryParse(mExpenseUodate.PaymentMethod.ToString(), out Domain.PaymentMethod paymentMethod))
                    dExpense.PaymentMethod = paymentMethod;
                if (Enum.TryParse(mExpenseUodate.PaymentType.ToString(), out Domain.PaymentType paymentType))
                    dExpense.PaymentType = paymentType;
                if (mExpenseUodate.ExpenseDetail != null)
                    dExpense.ExpenseDetail = new Domain.ExpenseDetail { Detail = mExpenseUodate.ExpenseDetail };
                dExpense.Signature = mExpenseUodate.Signature;

                var i = await _expenseRepository.EditExpensesAsync(dExpense, id);
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
        public async Task<IActionResult> PartiallyUpdateExpense([FromBody] JsonPatchDocument<Models.ExpenseForUpdate> dExpenseUpdatePatch, [FromRoute] int id)
        {
            try
            {
                if (dExpenseUpdatePatch == null) return BadRequest();

                //Get the expense from the database
                var dbExp = await _expenseRepository.GetExpensesAsync(id);
                if (dbExp.Id < 1) return NotFound();

                //Create patch Expense from the database expense
                ////* Mapping the Domain object to Model object  >> Business logic may apply here *//
                //var expToPatch = new Models.ExpenseForUpdate();
                //expToPatch.ExpenseTitle = dbExp.ExpenseTitle;
                //expToPatch.ExpensesAmount = dbExp.ExpensesAmount;
                //expToPatch.ExpenseDate = dbExp.ExpenseDate;
                //expToPatch.CurrencyId = dbExp.CurrencyId;
                //expToPatch.CategoryId = dbExp.ExpenseCategoryId;
                //if (Enum.TryParse(dbExp.PaymentMethod.ToString(), out Models.PaymentMethod paymentMethod))
                //    expToPatch.PaymentMethod = paymentMethod;
                //if (Enum.TryParse(dbExp.PaymentType.ToString(), out Models.PaymentType paymentType))
                //    expToPatch.PaymentType = paymentType;
                //expToPatch.ExpenseDetail = dbExp.ExpenseDetail == null ? null: dbExp.ExpenseDetail.Detail;
                //expToPatch.Signature = dbExp.Signature;


                //DK: Problem in auto mapper need to fix
                var expToPatch = _mapper.Map<Models.ExpenseForUpdate>(dbExp);

                // Apply Expense to ModelState
                dExpenseUpdatePatch.ApplyTo(expToPatch, ModelState);

                //Add custom validation rules if needed as below
                if (expToPatch.ExpenseTitle == expToPatch.ExpenseDetail)
                    ModelState.AddModelError("Description", "Expense Title and details can't be same");
                
                //Validate the model state
                TryValidateModel(expToPatch);
                if (!ModelState.IsValid) return BadRequest(ModelState);

                //Create an expense object to update the database
                //* Mapping the Model object to Domain object >> Business logic may apply here *//
                var dExpense = new Domain.Expense();
                dExpense.ExpenseTitle = expToPatch.ExpenseTitle;
                dExpense.ExpensesAmount = expToPatch.ExpensesAmount;
                dExpense.ExpenseDate = expToPatch.ExpenseDate;
                dExpense.CurrencyId = expToPatch.CurrencyId;
                dExpense.ExpenseCategoryId = expToPatch.CategoryId;
                if (Enum.TryParse(expToPatch.PaymentMethod.ToString(), out Domain.PaymentMethod dPaymentMethod))
                    dExpense.PaymentMethod = dPaymentMethod;
                if (Enum.TryParse(expToPatch.PaymentType.ToString(), out Domain.PaymentType dPaymentType))
                    dExpense.PaymentType = dPaymentType;
                if (expToPatch.ExpenseDetail != null)
                    dExpense.ExpenseDetail = new Domain.ExpenseDetail { Detail = expToPatch.ExpenseDetail };
                dExpense.Signature = expToPatch.Signature;

                var row_affected = await _expenseRepository.EditExpensesAsync(dExpense, id);
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