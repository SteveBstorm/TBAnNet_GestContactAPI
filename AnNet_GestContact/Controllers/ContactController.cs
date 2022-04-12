using AnNet_GestContact.Dal;
using AnNet_GestContact.Dal.Entities;
using AnNet_GestContact.Dal.Services;
using AnNet_GestContact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.Tools.Database;
using System.Data;

namespace AnNet_GestContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;
        private readonly ILogger _logger;

        public ContactController(ILogger<ContactController> logger, ContactService contactService)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get All Contact is call...");
            return Ok(_contactService.Get());
        }

        [Authorize("auth")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Contact? contact = _contactService.Get(id);
            if (contact is not null)
                return Ok(contact);

            return NotFound();
        } 
        
        [Authorize("userPolicy")]
        [HttpPost]
        public IActionResult Post(PostContactForm form)
        {
            Contact contact = new Contact() { LastName = form.LastName, FirstName = form.FirstName, Email = form.Email, BirthDate = form.BirthDate };
            contact.Id = _contactService.Insert(contact);
            return Ok(contact);            
        }

        [Authorize("adminPolicy")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, PutContactForm form)
        {
            if(id != form.Id)
                return BadRequest();

            if (_contactService.Update(new Contact() { Id = form.Id, LastName = form.LastName, FirstName = form.FirstName, Email = form.Email, BirthDate = form.BirthDate }))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize("adminPolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Contact? contact = _contactService.Delete(id);
            if(contact is null)
            {
                return NotFound();
            }
            
            return Ok(contact);
        }
    }
}
