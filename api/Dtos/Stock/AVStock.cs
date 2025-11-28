namespace api.Dtos.Stock
{
    public class AVStock
    {
        public string? Symbol { get; set; }
        public string? AssetType { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CIK { get; set; }
        public string? Exchange { get; set; }
        public string? Currency { get; set; }
        public string? Country { get; set; }
        public string? Sector { get; set; }
        public string? Industry { get; set; }
        public string? Address { get; set; }
        public string? OfficialSite { get; set; }
        public string? FiscalYearEnd { get; set; }
        public string? LatestQuarter { get; set; }
        public long MarketCapitalization { get; set; }
        public double EBITDA { get; set; }
        public double PERatio { get; set; }
        public double PEGRatio { get; set; }
        public double BookValue { get; set; }
        public double DividendPerShare { get; set; }
        public double DividendYield { get; set; }
        public double EPS { get; set; }
        public double RevenuePerShareTTM { get; set; }
        public double ProfitMargin { get; set; }
        public double OperatingMarginTTM { get; set; }
        public double ReturnOnAssetsTTM { get; set; }
        public double ReturnOnEquityTTM { get; set; }
        public double RevenueTTM { get; set; }
        public double GrossProfitTTM { get; set; }
        public double DilutedEPSTTM { get; set; }
        public double QuarterlyEarningsGrowthYOY { get; set; }
        public double QuarterlyRevenueGrowthYOY { get; set; }
        public double AnalystTargetPrice { get; set; }
        public double AnalystRatingStrongBuy { get; set; }
        public double AnalystRatingBuy { get; set; }
        public double AnalystRatingHold { get; set; }
        public double AnalystRatingSell { get; set; }
        public double AnalystRatingStrongSell { get; set; }
        public double TrailingPE { get; set; }
        public double ForwardPE { get; set; }
        public double PriceToSalesRatioTTM { get; set; }
        public double PriceToBookRatio { get; set; }
        public double EVToRevenue { get; set; }
        public double EVToEBITDA { get; set; }
        public double Beta { get; set; }
        public double Week52High { get; set; }
        public double Week52Low { get; set; }
        public double Day50MovingAverage { get; set; }
        public double Day200MovingAverage { get; set; }
        public long SharesOutstanding { get; set; }
        public decimal SharesFloat { get; set; }
        public decimal PercentInsiders { get; set; }
        public decimal PercentInstitutions { get; set; }
        public DateOnly DividendDate { get; set; }
        public DateOnly ExDividendDate { get; set; }

    }
}