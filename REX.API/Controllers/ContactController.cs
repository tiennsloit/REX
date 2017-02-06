﻿using REX.Core.Services;
using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REX.API.Controllers
{
    public class ContactController : ApiController
    {
        readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        // GET api/<controller>
        public IEnumerable<Contact> Get()
        {
            return _contactService.GetContacts();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        /// <summary>
        /// Can test with postman using http://tyuiop.com/api/contact/postcontact
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string PostContact(Contact contact)
        {
            return "True";
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}