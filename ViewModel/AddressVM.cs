using Test.Data;

namespace Test.ViewModel
{
    public class AddressVM
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string country { get; set; }


        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
