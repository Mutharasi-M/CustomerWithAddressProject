using Test.Data;

namespace Test.ViewModel
{
    public class CustomerVM
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DOB { get; set; }

        public string gender { get; set; }

        public IList<AddressTable> Addresses { get; set; }
    }
}
