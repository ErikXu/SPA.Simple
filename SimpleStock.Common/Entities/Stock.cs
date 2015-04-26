using System;

namespace SimpleStock.Common.Entities
{
    public class Stock : Entity
    {
        public string SecuritiesCode { get; set; }
        public string SecuritiesName { get; set; }
        public string SecuritiesNameEng { get; set; }
        public string Exchange { get; set; }
        public string IndustryCode { get; set; }
        public string IndustryName { get; set; }
        public string IndustryNameEng { get; set; }
        public string FullIndustryCode { get; set; }
        public string FullIndustryName { get; set; }
        public string FullIndustryNameEng { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
