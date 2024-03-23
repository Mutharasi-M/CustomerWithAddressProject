using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Test.Data;
using Test.ViewModel;

namespace Test.Controllers
{
    public class CustomerAddressController : Controller
    {
        private readonly TestDbContext TestDbContext;

        public CustomerAddressController(TestDbContext testDbContext)
        {
            TestDbContext = testDbContext;
        }



        [HttpPost]
        [ActionName("AddCustomer")]
        public IActionResult AddCustomer(CustomerVM customer)
        {
            var customerDetails = new Customer
            {
                Name = customer.Name,
                DOB = customer.DOB,
                Email = customer.Email,
                gender = customer.gender,
                Addresses = new List<AddressTable>()
            };

            TestDbContext.Customer.Add(customerDetails);
            TestDbContext.SaveChanges();

            return RedirectToAction("AddressForm", "Home", new { customerId = customerDetails.Id });
        }


        [HttpPost]
        public IActionResult AddAddress(AddressVM addressDetails, int customerId)
        {
            var customerAddressDetails = new AddressTable
            {
                Address = addressDetails.Address,
                state = addressDetails.state,
                city = addressDetails.city,
                country = addressDetails.country,
                CustomerId = customerId
            };

            TestDbContext.Address.Add(customerAddressDetails);
            TestDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [ActionName("CustomerList")]
        public IActionResult CustomerList()
        {
            var CustomerDetails = TestDbContext.Customer.Include(c => c.Addresses).ToList();
            return View(CustomerDetails);
        }


        public IActionResult EditCustomer(int Id)
        {
            var customerEdit = TestDbContext.Customer.FirstOrDefault(x => x.Id == Id);

            if (customerEdit == null)
            {
                ViewBag.Message = "Not Found";
                return View("/", ViewBag);
            }
            var customerDetails = new CustomerVM
            {
                Id = Id,
                Name = customerEdit.Name,
                DOB = customerEdit.DOB,
                Email = customerEdit.Email,
                gender = customerEdit.gender
            };

            return View("EditCustomer", customerEdit);
        }

        [HttpPost]
        public IActionResult EditCustDetails(CustomerVM customer)
        {
            var customerEdit = TestDbContext.Customer.Find(customer.Id);

            if (customerEdit == null)
            {
                TempData["Message"] = "Not Found";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                customerEdit.Name = customer.Name;
                customerEdit.DOB = customer.DOB;
                customerEdit.Email = customer.Email;
                customerEdit.gender = customer.gender;

                TestDbContext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult EditAddress(int Id)
        {
            var AddressEdit = TestDbContext.Address.FirstOrDefault(x => x.Id == Id);

            if (AddressEdit == null)
            {
                ViewBag.Message = "Not Found";
                return View("/", ViewBag);
            }
            var AddressDetails = new AddressVM
            {
                Id = Id,
                Address = AddressEdit.Address,
                city = AddressEdit.city,
                state = AddressEdit.state,
                country = AddressEdit.country
            };

            return View("EditAddress", AddressEdit);
        }

        [HttpPost]
        public IActionResult EditAddressDetails(AddressVM addressDetail)
        {
            var AddressEdit = TestDbContext.Address.Find(addressDetail.Id);

            if (AddressEdit == null)
            {
                TempData["Message"] = "Not Found";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                AddressEdit.Address = addressDetail.Address;
                AddressEdit.city = addressDetail.city;
                AddressEdit.state = addressDetail.state;
                AddressEdit.country = addressDetail.country;

                TestDbContext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult DeleteCustomer(int Id)
        {
            var customerEdit = TestDbContext.Customer.FirstOrDefault(x => x.Id == Id);
            if (customerEdit != null)
            {
                TestDbContext.Customer.Remove(customerEdit);
                TestDbContext.SaveChanges(true);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteAddress(int Id)
        {
            var AddressEdit = TestDbContext.Address.FirstOrDefault(x => x.Id == Id);
            if (AddressEdit != null)
            {
                TestDbContext.Address.Remove(AddressEdit);
                TestDbContext.SaveChanges(true);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
