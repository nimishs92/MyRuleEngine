using System;

namespace BusinessEntities
{
    public class Payment
    {
        public PaymentType PaymentType { get; set; }

        public string ObjectID { get; set; }

        public string PrdName { get; set; }
    }

    public enum PaymentType
    {
        PHY_PROD, 
        BOOK, 
        MEMSHIP,
        UPGRD,
        VID
    } 
}
