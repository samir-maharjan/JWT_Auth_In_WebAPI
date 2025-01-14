using System.ComponentModel;

namespace JWT_token_auth_Demo
{
    public class Enum
    {

        public enum EnumPropertyStatus
        {
            [Description("Sale")]
            Sale = 0,
            [Description("Sold")]
            Sold = 1,
            [Description("On Lease")]
            OnLease = 2,
            [Description("Leased")]
            Leased = 3,
            [Description("On Rent")]
            OnRent = 4,
            [Description("Buy")]
            Buy = 5
        }
    }
}
