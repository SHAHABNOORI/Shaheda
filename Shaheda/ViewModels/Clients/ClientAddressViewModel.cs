using Shaheda.Enums;

namespace Shaheda.ViewModels.Clients
{
    public class ClientAddressViewModel
    {
        public int ClientAddressId { get; set; }

        public string Address { get; set; }

        public string BussinesAddress { get; set; }

        public Town Town { get; set; }

        public City City { get; set; }

        public string PostalCode { get; set; }
    }
}