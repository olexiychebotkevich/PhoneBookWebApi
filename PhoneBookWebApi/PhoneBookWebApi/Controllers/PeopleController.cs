using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using PhoneBookWebApi;

namespace PhoneBookWebApi.Controllers
{
    /// <summary>
    /// API CRUD controller for Worker
    /// </summary>
    /// 
    [RoutePrefix("Api/People")]
    [EnableCors(origins: "http://localhost:4201", headers: "*", methods: "*")]
    public class PeopleController : ApiController
    {
        private PhoneBookContext db = new PhoneBookContext();

        // GET: api/People
        /// <summary>
        /// Read all people from DB
        /// </summary>
        /// <returns>All people info</returns>
        /// 
        [HttpGet]
        [Route("ReadAllPeopleInfo")]
        public IQueryable<Person> GetPeople()
        {
           
            return db.People;
        }

        // GET: api/People/5
        /// <summary>
        /// Get worker by known id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Person))]
        [HttpGet]
        [Route("ReadPersonById")]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        /// <summary>
        /// Update Person by known Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("UpdatePerson/{id}")]
        public IHttpActionResult PutPerson([FromUri]int id, [FromBody]Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/People
        /// <summary>
        /// Create person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [ResponseType(typeof(Person))]
        [HttpPost]
        [Route("CreatePerson")]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        /// <summary>
        /// Delete person by known id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Person))]
        [HttpDelete]
        [Route("DeletePersonById/{id}")]
        public IHttpActionResult DeletePerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }

        /// <summary>
        /// DataBase Dispose
        /// </summary>
        /// <param name="disposing"></param>

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.People.Count(e => e.Id == id) > 0;
        }
    }
}