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

namespace ChargePlanning
{
    public static class generatePlan
    {
        [FunctionName("generatePlan")]
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
            string jpath = req.Query["jpath"]; // JPath expressions, see https://www.newtonsoft.com/json/help/html/SelectToken.htm 
            string date = req.Query["date"];
            string hour = req.Query["hour"];

            string jsonStr = string.IsNullOrEmpty(json)
                ? "[{\"id\":\"20220604_0\",\"date\":\"2022-06-04\",\"hour\":0,\"est_consumption\":281,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":0.0,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_1\",\"date\":\"2022-06-04\",\"hour\":1,\"est_consumption\":258,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":0.0,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_2\",\"date\":\"2022-06-04\",\"hour\":2,\"est_consumption\":226,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1241.89,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_3\",\"date\":\"2022-06-04\",\"hour\":3,\"est_consumption\":270,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1168.45,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_4\",\"date\":\"2022-06-04\",\"hour\":4,\"est_consumption\":278,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1156.92,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_5\",\"date\":\"2022-06-04\",\"hour\":5,\"est_consumption\":204,\"est_production\":134,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1160.72,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_6\",\"date\":\"2022-06-04\",\"hour\":6,\"est_consumption\":247,\"est_production\":521,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1188.91,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_7\",\"date\":\"2022-06-04\",\"hour\":7,\"est_consumption\":187,\"est_production\":23,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1263.76,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_8\",\"date\":\"2022-06-04\",\"hour\":8,\"est_consumption\":190,\"est_production\":41,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1339.2,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_9\",\"date\":\"2022-06-04\",\"hour\":9,\"est_consumption\":805,\"est_production\":1829,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1328.04,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_10\",\"date\":\"2022-06-04\",\"hour\":10,\"est_consumption\":484,\"est_production\":4358,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1215.18,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_11\",\"date\":\"2022-06-04\",\"hour\":11,\"est_consumption\":910,\"est_production\":3529,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1041.45,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_12\",\"date\":\"2022-06-04\",\"hour\":12,\"est_consumption\":495,\"est_production\":4216,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1005.44,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_13\",\"date\":\"2022-06-04\",\"hour\":13,\"est_consumption\":648,\"est_production\":4660,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":850.24,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_14\",\"date\":\"2022-06-04\",\"hour\":14,\"est_consumption\":589,\"est_production\":4767,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":705.76,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_15\",\"date\":\"2022-06-04\",\"hour\":15,\"est_consumption\":461,\"est_production\":4508,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":885.29,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_16\",\"date\":\"2022-06-04\",\"hour\":16,\"est_consumption\":446,\"est_production\":3859,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1052.17,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_17\",\"date\":\"2022-06-04\",\"hour\":17,\"est_consumption\":601,\"est_production\":2911,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1182.37,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_18\",\"date\":\"2022-06-04\",\"hour\":18,\"est_consumption\":1220,\"est_production\":1837,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1337.94,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_19\",\"date\":\"2022-06-04\",\"hour\":19,\"est_consumption\":631,\"est_production\":824,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1480.79,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_20\",\"date\":\"2022-06-04\",\"hour\":20,\"est_consumption\":1242,\"est_production\":328,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1556.75,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_21\",\"date\":\"2022-06-04\",\"hour\":21,\"est_consumption\":2091,\"est_production\":161,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1480.41,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_22\",\"date\":\"2022-06-04\",\"hour\":22,\"est_consumption\":817,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1481.01,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_23\",\"date\":\"2022-06-04\",\"hour\":23,\"est_consumption\":438,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1250.96,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0}]"
                : json;
            string dateStr = string.IsNullOrEmpty(date)
                ? "2022-06-04"
                : date;

            //Load JSON in the defined chargeplan row class
            var planTblArr = JsonConvert.DeserializeObject<List<ChargePlanRow>>(await new StreamReader(req.Body).ReadToEndAsync()) ?? JsonConvert.DeserializeObject<List<ChargePlanRow>>(jsonStr);
            

            //return new OkObjectResult(jObject);
            return new OkObjectResult(JsonConvert.SerializeObject(planTblArr));
        }
    }


    public class ChargePlanTable
    {
        public ChargePlanRow[] Property1 { get; set; }
    }

    public class ChargePlanRow
    {
        public string id { get; set; }
        public string date { get; set; }
        public int hour { get; set; }
        public int est_consumption { get; set; }
        public int est_production { get; set; }
        public int surplus_production { get; set; }
        public float charge_potential { get; set; }
        public bool charge_trigger { get; set; }
        public bool charging { get; set; }
        public float accumulated_charge { get; set; }
        public float elspotprice_dkk { get; set; }
        public int surplus_sellable_production { get; set; }
        public float sale_potential_dkk { get; set; }
    }

    
}

