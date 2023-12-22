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
    /// Stored Procedure Design - Extract From Kendo Grid Request
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SprocExampleController : ControllerBase
    {
        private readonly ILogger<SprocExampleController> _logger;
        private readonly WidgetContext _context;

        public SprocExampleController(ILogger<SprocExampleController> logger, WidgetContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get([DataSourceRequest] DataSourceRequest request)
        {
            KendoGridBinder binder = new(request);

            KendoParameters parameters = new()
            {
                PageNumber = binder.PageNumber,
                PageSize = binder.PageSize,
                SortColumn = binder.SortColumn,
                SortDirection = binder.SortDirection
            };

            const string procedureName = "spGetWidgets";
            using var cn = new SqlConnection(_context.Database.GetConnectionString());
            var cm = new SqlCommand(procedureName, cn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cm.Parameters.Add("@PageNumber", SqlDbType.BigInt).Value = parameters.PageNumber;
            cm.Parameters.Add("@PageSize", SqlDbType.BigInt).Value = parameters.PageSize;
            cm.Parameters.Add("@SortColumn", SqlDbType.NVarChar, -1).Value = parameters.SortColumn;
            cm.Parameters.Add("@SortDirection", SqlDbType.NVarChar, -1).Value = parameters.SortDirection;
            cm.Parameters.Add("@ReturnJSON", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();

            string payload = cm.Parameters["@ReturnJSON"].Value.ToString() ?? string.Empty;
            var result = JsonSerializer.Deserialize<IEnumerable<Widget>>(payload) ?? new List<Widget>();

            // Kendo Grid expects results wrapped in a "data" array
            WidgetList list = new()
            {
                data = new List<Widget>()
            };

            foreach (var item in result)
            {
                list.data.Add(item);
            }

            return new JsonResult(list);
        }
    }
}