using System.Threading;
using Microsoft.AspNetCore.Mvc;
using PhonebookApp.Data;
using PhonebookApp.Models;
using PhonebookApp.Service;

namespace PhonebookApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUserService _userService;

        public ContactController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddContact(string submitButton)
        {
            Contact contact = new Contact();
            bool isContactExist = bool.Parse(Request.Form["isContactExist"].ToString());

            if (isContactExist)
            {
                contact.ID = int.Parse(Request.Form["id"]);
            }
            contact.FirstName = Request.Form["FirstName"].ToString();
            contact.LastName = Request.Form["LastName"].ToString();
            contact.PhoneNumber = Request.Form["PhoneNumber"];
            contact.Address = Request.Form["Address"].ToString();
            contact.Email = Request.Form["Email"].ToString();
            contact.UserName = _userService.GetUserName();

            ContactModel model = new ContactModel();

            if (submitButton == "Delete")
            {
                model.DeleteContact(contact);
            }
            else
            {
                model.AddContacts(contact);
            }

            return View("../Home/IndexLogged");
        }

        public IActionResult DeleteContact(Contact contact)
        {
            contact.UserName = _userService.GetUserName();
            ContactModel model = new ContactModel();
            model.DeleteContact(contact);

            return View();
        }

        [HttpPost]
        public IActionResult RecoverContact(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ContactModel model = new ContactModel();
                model.RecoverContact(int.Parse(id));
            }
            //Waiting 1 second because the db needs refresh and sometimes the recovered rows are missing from the contact list
            Thread.Sleep(1000);
            return View("../Home/IndexLogged");
        }

        [HttpPost]
        public IActionResult SearchContacts()
        {
            string userName = _userService.GetUserName();
            string filter = Request.Form["Filter"].ToString();
            ContactModel model = new ContactModel();
            model.SearchContacts(userName, filter);
            return View("../Home/IndexLogged", model);
        }
    }
}
