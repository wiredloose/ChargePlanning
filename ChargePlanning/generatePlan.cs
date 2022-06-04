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
            string date = req.Query["date"];
            string hour = req.Query["hour"];
            int max_batt_input_w = 3300;
            int max_grid_feedin_w = 3680;
            int batt_capacity_kwh = 7500;

            string jsonStr = string.IsNullOrEmpty(json)
                ? "[{\"id\":\"20220604_0\",\"date\":\"2022-06-04\",\"hour\":0,\"est_consumption\":281,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":0.0,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_1\",\"date\":\"2022-06-04\",\"hour\":1,\"est_consumption\":258,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":0.0,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_2\",\"date\":\"2022-06-04\",\"hour\":2,\"est_consumption\":226,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1241.89,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_3\",\"date\":\"2022-06-04\",\"hour\":3,\"est_consumption\":270,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1168.45,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_4\",\"date\":\"2022-06-04\",\"hour\":4,\"est_consumption\":278,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1156.92,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_5\",\"date\":\"2022-06-04\",\"hour\":5,\"est_consumption\":204,\"est_production\":134,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1160.72,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_6\",\"date\":\"2022-06-04\",\"hour\":6,\"est_consumption\":247,\"est_production\":521,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1188.91,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_7\",\"date\":\"2022-06-04\",\"hour\":7,\"est_consumption\":187,\"est_production\":23,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1263.76,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_8\",\"date\":\"2022-06-04\",\"hour\":8,\"est_consumption\":190,\"est_production\":41,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1339.2,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_9\",\"date\":\"2022-06-04\",\"hour\":9,\"est_consumption\":805,\"est_production\":1829,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1328.04,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_10\",\"date\":\"2022-06-04\",\"hour\":10,\"est_consumption\":484,\"est_production\":4358,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1215.18,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_11\",\"date\":\"2022-06-04\",\"hour\":11,\"est_consumption\":910,\"est_production\":3529,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1041.45,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_12\",\"date\":\"2022-06-04\",\"hour\":12,\"est_consumption\":495,\"est_production\":4216,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1005.44,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_13\",\"date\":\"2022-06-04\",\"hour\":13,\"est_consumption\":648,\"est_production\":4660,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":850.24,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_14\",\"date\":\"2022-06-04\",\"hour\":14,\"est_consumption\":589,\"est_production\":4767,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":705.76,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_15\",\"date\":\"2022-06-04\",\"hour\":15,\"est_consumption\":461,\"est_production\":4508,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":885.29,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_16\",\"date\":\"2022-06-04\",\"hour\":16,\"est_consumption\":446,\"est_production\":3859,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1052.17,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_17\",\"date\":\"2022-06-04\",\"hour\":17,\"est_consumption\":601,\"est_production\":2911,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1182.37,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_18\",\"date\":\"2022-06-04\",\"hour\":18,\"est_consumption\":1220,\"est_production\":1837,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1337.94,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_19\",\"date\":\"2022-06-04\",\"hour\":19,\"est_consumption\":631,\"est_production\":824,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1480.79,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_20\",\"date\":\"2022-06-04\",\"hour\":20,\"est_consumption\":1242,\"est_production\":328,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1556.75,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_21\",\"date\":\"2022-06-04\",\"hour\":21,\"est_consumption\":2091,\"est_production\":161,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1480.41,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_22\",\"date\":\"2022-06-04\",\"hour\":22,\"est_consumption\":817,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1481.01,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0},{\"id\":\"20220604_23\",\"date\":\"2022-06-04\",\"hour\":23,\"est_consumption\":438,\"est_production\":0,\"surplus_production\":0,\"charge_potential\":0.0,\"charge_trigger\":false,\"charging\":false,\"accumulated_charge\":0.0,\"elspotprice_dkk\":1250.96,\"surplus_sellable_production\":0,\"sale_potential_dkk\":0.0}]"
                : json;
            string dateStr = string.IsNullOrEmpty(date)
                ? "2022-06-04"
                : date;

            //Load JSON in the defined chargeplan row class
            var planTblArr = JsonConvert.DeserializeObject<List<ChargePlanRow>>(await new StreamReader(req.Body).ReadToEndAsync()) ?? JsonConvert.DeserializeObject<List<ChargePlanRow>>(jsonStr);



            //insert charge trigger hour
            int charge_trigger_hour = string.IsNullOrEmpty(hour) ? 0 : Int16.Parse(hour);
            if (charge_trigger_hour>0)
            {
                planTblArr[charge_trigger_hour].charge_trigger = true;
            } 
            
            // Do table calculations
            for (int i=0; i < planTblArr.Count; i++)
            {
                CalculateRow(planTblArr, i, max_batt_input_w, batt_capacity_kwh, max_grid_feedin_w, log);
            }

            // Store resulting estimated yield from this table run 
            List<PlanTableRun> planTblRuns = new List<PlanTableRun>();
            float total_yield = (float)0;
            foreach (ChargePlanRow row in planTblArr)
            {
                total_yield += (float)row.sale_potential_dkk;
                log.LogInformation("yield_dkk: " + total_yield + " from row: " + row.hour);
            }
            planTblRuns.Add(new PlanTableRun { charge_trigger_hour = charge_trigger_hour, total_yield_dkk = total_yield });
            log.LogInformation("total_yield_dkk: " + total_yield + " from charge_trigger_hour value: " + charge_trigger_hour);
            log.LogInformation("planTblRuns[0]: total_yield_dkk" + planTblRuns[0].total_yield_dkk + ", planTblRuns[0]: charge_trigger_hour" + planTblRuns[0].charge_trigger_hour);

            // return JSON result
            return new OkObjectResult(JsonConvert.SerializeObject(planTblArr));
        }



        private static void CalculateRow(List<ChargePlanRow> planTblArr, int rowNo, int max_batt_input_w, int batt_capacity_kwh, int max_grid_feedin_w, ILogger log)
        {
            log.LogInformation("row no: " + rowNo + " has charge trigger value: " + planTblArr[rowNo].charge_trigger);
            // surplus production
            int surplus_prod = planTblArr[rowNo].est_production - planTblArr[rowNo].est_consumption;
            surplus_prod = surplus_prod < 0 ? 0 : surplus_prod;
            planTblArr[rowNo].surplus_production = surplus_prod;

            // charge potential: =IF(surplus_prod>0;MIN(surplus_prod;MAX_BATTERY_INPUT)*0,44/MAX_BATTERY_INPUT;0)
            float charge_pot = surplus_prod > 0 ? (float)Math.Min(surplus_prod, max_batt_input_w) * ((float)max_batt_input_w/batt_capacity_kwh) / (float)max_batt_input_w : 0;
            planTblArr[rowNo].charge_potential = charge_pot;
            
            // accumulated charge: from rowno 1 onwards, it is
            // =IF(previousRow charging=true;
            //      MIN( previousRow accumulated_charge + previousRow charge_potential ; 1 );
            // ELSE
            //      previousRow accumulated_charge)
            if (rowNo > 0)
            {
                float acc_charge = planTblArr[rowNo-1].charging
                    ? (float)Math.Min( (float)planTblArr[rowNo - 1].accumulated_charge + (float)planTblArr[rowNo - 1].charge_potential, (float)1)
                    : planTblArr[rowNo - 1].accumulated_charge;
                planTblArr[rowNo].accumulated_charge = acc_charge;
            }

            // charging: from rowno 1 onwards, it is
            // =IF( charge_trigger=true;
            //      IF(previousRow charging=false;true;false);
            //          IF( previousRow acculumated_charge=1 && previousRow charging=true) ; false; previousRow charging))
            if (rowNo > 0)
            {
                bool boolCharging = false;
                if (planTblArr[rowNo].charge_trigger==true)
                {
                    // IF(previousRow charging=false;true;false);
                    if (planTblArr[rowNo-1].charging==false)
                    {
                        boolCharging = true;
                    } else
                    {
                        boolCharging = false;
                    }
                }
                else
                {
                    // IF( previousRow acculumated_charge=1 && previousRow charging=true) ; false; previousRow charging)
                    if (planTblArr[rowNo - 1].accumulated_charge == (float)1 && planTblArr[rowNo - 1].charging==true)
                    {
                        boolCharging = false;
                    }
                    else
                    {
                        boolCharging = planTblArr[rowNo - 1].charging;
                    }
                }

                planTblArr[rowNo].charging = boolCharging;
                
            }

            // sellable production: =IF charging==true
            //                          MIN(  MAX(surplus_prod - MAX_BATTERY_INPUT,  0), MAX_GRID_FEEDIN)
            //                      ELSE
            //                          MIN(  surplus_prod, MAX_GRID_FEEDIN)
            int surplus_sellable_prod;
            if (planTblArr[rowNo].charging == true)
            {
                surplus_sellable_prod = Math.Min( Math.Max(planTblArr[rowNo].surplus_production-max_batt_input_w, 0), max_grid_feedin_w);
            }
            else
            {
                surplus_sellable_prod = Math.Min(planTblArr[rowNo].surplus_production, max_grid_feedin_w);
            }
            planTblArr[rowNo].surplus_sellable_production = surplus_sellable_prod;
            
            // sale potential: =IF((elspotprice_dkk_mwh*surplus_sellable_production/1000000) > 0 THEN elspotprice_dkk_mwh*surplus_sellable_production/1000000 ELSE 0)
            float sale_pot = (planTblArr[rowNo].elspotprice_dkk * (float)planTblArr[rowNo].surplus_sellable_production / (float)1000000) > 0
                ? planTblArr[rowNo].elspotprice_dkk * (float)planTblArr[rowNo].surplus_sellable_production / (float)1000000
                : 0;
            planTblArr[rowNo].sale_potential_dkk = sale_pot;
        }
    }


    public class PlanTableRun
    {
        public int charge_trigger_hour { get; set; }
        public float total_yield_dkk { get; set; }
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

