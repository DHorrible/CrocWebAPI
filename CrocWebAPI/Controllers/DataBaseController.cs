using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using CrocWebAPI.Data.Helper;
using CrocWebAPI.Npgsql.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrocWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        #region Fields

        private readonly ILogger<DataBaseController> logger;

        #endregion

        #region Constructors

        public DataBaseController(ILogger<DataBaseController> logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Controllers

        [HttpGet("{stringID}")]
        public ActionResult<string> Get(string stringID)
        {
            this.logger.LogInformation($"GET request has handled from {this.Request.Host}");
            
            try
            {
                var ids = stringID.Split('.').Select(s => new Guid(s)).ToArray();

                var instance = NpgsHelper.GetInstance(ids);
                
                if (instance == null)
                {
                    this.logger.LogError($"Instance with IDs = '{ids[0]}', '{ids[1]}', '{ids[2]}'  not found");
                    this.Response.StatusCode = 404;
                    
                    return $@"{{ ""Error"": ""Instance with ID s= '{ids}' not found"" }}";
                }

                return DataHelper.ToJson(instance);
            } 
            catch (FormatException e)
            {
                this.logger.LogError(e.Message);
                
                this.Response.StatusCode = 400;

                return @$"{{ ""Error"": ""{e.Message}"" }}";
            }
        }

        [HttpGet("image/{stringID}")]
        public ActionResult<string> GetImage(string stringID)
        {
            this.logger.LogInformation($"GET image request has handled from {this.Request.Host}");

            try
            {
                var ids = stringID.Split('.').Select(s => new Guid(s)).ToArray();

                var instance = NpgsHelper.GetInstance(ids);

                if (instance == null)
                {
                    this.logger.LogError($"Instance with IDs = '{ids[0]}', '{ids[1]}', '{ids[2]}'  not found");
                    this.Response.StatusCode = 404;

                    return $@"{{ ""Error"": ""Instance with ID s= '{ids}' not found"" }}";
                }

                var content = DataHelper.GetImageContent(instance.TypeID0);

                return content == null
                    ? string.Empty
                    : Convert.ToBase64String(content);
            }
            catch (FormatException e)
            {
                this.logger.LogError(e.Message);

                this.Response.StatusCode = 400;

                return @$"{{ ""Error"": ""{e.Message}"" }}";
            }
        }

        #endregion
    }
}
