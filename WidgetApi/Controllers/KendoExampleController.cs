using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
using WidgetApi.Models;

namespace WidgetApi.Controllers
{
    /// <summary>
    /// Telerik Kendo Grid Design - Use of EFCore as ORM for Dynamic Linq Queries
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KendoExampleController : ControllerBase
    {
        private readonly ILogger<KendoExampleController> _logger;
        private readonly WidgetContext _context;

        public KendoExampleController(ILogger<KendoExampleController> logger, WidgetContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get([DataSourceRequest] DataSourceRequest request)
        {
            var result = _context.Widgets.ToDataSourceResult(request);
            return new JsonResult(result);
        }
    }
}