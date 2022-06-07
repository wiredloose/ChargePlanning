namespace ChargePlanning
{
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
        public float elspotprice_eur { get; set; }
        public float sale_potential_eur { get; set; }

    }
}

