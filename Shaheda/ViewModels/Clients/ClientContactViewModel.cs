using Shaheda.Enums;

namespace Shaheda.ViewModels.Clients
{
    public class ClientContactViewModel
    {
        public int ClientContactId { get; set; }

        public string HomeNumber { get; set; }

        public ClientContactType ContactType { get; set; }

        public string EmailAddress { get; set; }

        public string Website { get; set; }

        public string MobileNumber { get; set; }

        public string PhoneNumber { get; set; }

        public OkToContact OkToContact { get; set; }
    }
}