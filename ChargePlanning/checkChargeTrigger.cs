using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

namespace ChargePlanning
{
    public static class checkChargeTrigger
    {
        [FunctionName("checkChargeTrigger")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "date", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **date** parameter")]
        [OpenApiParameter(name: "hour", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "The **hour** parameter")]
        [OpenApiParameter(name: "json", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **json** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Charge Plan JSON object")]


        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string json = req.Query["json"];
            string date = req.Query["date"];
            string hour = req.Query["hour"];
            
            string jsonStr = string.IsNullOrEmpty(json)
                ? "[{\"id\":\"20220604_0\",\"date\":\"2022-06-04\",\"hour\":0,\"est_consumption\":281,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":0.0,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_1\",\"date\":\"2022-06-04\",\"hour\":1,\"est_consumption\":258,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":0.0,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_2\",\"date\":\"2022-06-04\",\"hour\":2,\"est_consumption\":226,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1241.89,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_3\",\"date\":\"2022-06-04\",\"hour\":3,\"est_consumption\":270,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1168.45,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_4\",\"date\":\"2022-06-04\",\"hour\":4,\"est_consumption\":278,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1156.92,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_5\",\"date\":\"2022-06-04\",\"hour\":5,\"est_consumption\":204,\"est_production\":134,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1160.72,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_6\",\"date\":\"2022-06-04\",\"hour\":6,\"est_consumption\":247,\"est_production\":521,\"surplus_production\":274,\"charge_potential\":0.036533333,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1188.91,\"surplus_sellable_production\":274,\"sale_potential_dkk\":0.32576135,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_7\",\"date\":\"2022-06-04\",\"hour\":7,\"est_consumption\":187,\"est_production\":23,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1263.76,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_8\",\"date\":\"2022-06-04\",\"hour\":8,\"est_consumption\":190,\"est_production\":41,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1339.2,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_9\",\"date\":\"2022-06-04\",\"hour\":9,\"est_consumption\":805,\"est_production\":1829,\"surplus_production\":1024,\"charge_potential\":0.13653333,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1328.04,\"surplus_sellable_production\":1024,\"sale_potential_dkk\":1.359913,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_10\",\"date\":\"2022-06-04\",\"hour\":10,\"est_consumption\":484,\"est_production\":4358,\"surplus_production\":3874,\"charge_potential\":0.44,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1215.18,\"surplus_sellable_production\":3680,\"sale_potential_dkk\":4.4718623,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_11\",\"date\":\"2022-06-04\",\"hour\":11,\"est_consumption\":910,\"est_production\":3529,\"surplus_production\":2619,\"charge_potential\":0.3492,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1041.45,\"surplus_sellable_production\":2619,\"sale_potential_dkk\":2.7275574,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_12\",\"date\":\"2022-06-04\",\"hour\":12,\"est_consumption\":495,\"est_production\":4216,\"surplus_production\":3721,\"charge_potential\":0.44,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1005.44,\"surplus_sellable_production\":3680,\"sale_potential_dkk\":3.7000194,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_13\",\"date\":\"2022-06-04\",\"hour\":13,\"est_consumption\":648,\"est_production\":4660,\"surplus_production\":4012,\"charge_potential\":0.44,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":850.24,\"surplus_sellable_production\":3680,\"sale_potential_dkk\":3.1288834,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_14\",\"date\":\"2022-06-04\",\"hour\":14,\"est_consumption\":589,\"est_production\":4767,\"surplus_production\":4178,\"charge_potential\":0.44,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":705.76,\"surplus_sellable_production\":3680,\"sale_potential_dkk\":2.5971968,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_15\",\"date\":\"2022-06-04\",\"hour\":15,\"est_consumption\":461,\"est_production\":4508,\"surplus_production\":4047,\"charge_potential\":0.44,\"charge_trigger\":true,\"charging\":true,\"accumulated_charge\":0.0,\"elspotprice_dkk\":885.29,\"surplus_sellable_production\":747,\"sale_potential_dkk\":0.6613116,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_16\",\"date\":\"2022-06-04\",\"hour\":16,\"est_consumption\":446,\"est_production\":3859,\"surplus_production\":3413,\"charge_potential\":0.44,\"charge_trigger\":false,\"charging\":true,\"accumulated_charge\":0.44,\"elspotprice_dkk\":1052.17,\"surplus_sellable_production\":113,\"sale_potential_dkk\":0.11889522,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_17\",\"date\":\"2022-06-04\",\"hour\":17,\"est_consumption\":601,\"est_production\":2911,\"surplus_production\":2310,\"charge_potential\":0.308,\"charge_trigger\":false,\"charging\":true,\"accumulated_charge\":0.88,\"elspotprice_dkk\":1182.37,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_18\",\"date\":\"2022-06-04\",\"hour\":18,\"est_consumption\":1220,\"est_production\":1837,\"surplus_production\":617,\"charge_potential\":0.08226667,\"charge_trigger\":false,\"charging\":true,\"accumulated_charge\":1.0,\"elspotprice_dkk\":1337.94,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_19\",\"date\":\"2022-06-04\",\"hour\":19,\"est_consumption\":631,\"est_production\":824,\"surplus_production\":193,\"charge_potential\":0.025733333,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":1.0,\"elspotprice_dkk\":1480.79,\"surplus_sellable_production\":193,\"sale_potential_dkk\":0.28579247,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_20\",\"date\":\"2022-06-04\",\"hour\":20,\"est_consumption\":1242,\"est_production\":328,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":1.0,\"elspotprice_dkk\":1556.75,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_21\",\"date\":\"2022-06-04\",\"hour\":21,\"est_consumption\":2091,\"est_production\":161,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":1.0,\"elspotprice_dkk\":1480.41,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_22\",\"date\":\"2022-06-04\",\"hour\":22,\"est_consumption\":817,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":1.0,\"elspotprice_dkk\":1481.01,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0},{\"id\":\"20220604_23\",\"date\":\"2022-06-04\",\"hour\":23,\"est_consumption\":438,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":1.0,\"elspotprice_dkk\":1250.96,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0,\"elspotprice_eur\":0.0,\"sale_potential_eur\":0.0}]"
                : json;
            string dateStr = string.IsNullOrEmpty(date)
                ? "2022-06-04"
                : date;

            //Load JSON in the defined chargeplan row class
            var planTblArr = JsonConvert.DeserializeObject<List<ChargePlanRow>>(await new StreamReader(req.Body).ReadToEndAsync()) ?? JsonConvert.DeserializeObject<List<ChargePlanRow>>(jsonStr);


            // if param provided, use this charge trigger hour, otherwise use the hour of now()
            DateTime now = DateTime.Now;
            int charge_trigger_hour = string.IsNullOrEmpty(hour) ? now.Hour : Int16.Parse(hour);
            //log.LogInformation("Hour: " + charge_trigger_hour);


            //check for the chargetrigger
            if (planTblArr[charge_trigger_hour].charge_trigger == true) 
            {
                // return JSON result: 1
                return new OkObjectResult(1);
            } 

            // otherwise, return JSON result: 0
            return new OkObjectResult(0);
        }
    }
}

