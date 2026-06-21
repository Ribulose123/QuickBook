
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Interface;
namespace QuickBook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("trial-balance")]
        public async Task<IActionResult> GetTrialBalance()
        {
            var response = await _reportService.GetTrialBalanceAsync();
            return Ok(response);
        }
        [HttpGet("income-statement")]
        public async Task<IActionResult> GetIncomeStatement()
        {
            var response = await _reportService.GetIncomeStatementAsync();
            return Ok(response);
        }

        [HttpGet("balance-sheet")]
        public async Task<IActionResult> GetBalanceSheet()
        {
            var response = await _reportService.GetBalanceSheetAsync();
            return Ok(response);
        }
    }
}
