using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ExamResult.Models;
using System.Web.Http.Cors;

namespace ExamResult.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ExamResult.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Result>("Results");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class ResultsController : ODataController
    {
        private ExamResultContext db = new ExamResultContext();

        // GET: odata/Results
        [EnableQuery]
        public IQueryable<Result> GetResults()
        {
            return db.Results;
        }

        // GET: odata/Results(5)
        [EnableQuery]
        public SingleResult<Result> GetResult([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Results.Where(result => result.ResultId == key));
        }

        // PUT: odata/Results(5)
        public IHttpActionResult Put([FromODataUri] Guid key, Delta<Result> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Result result = db.Results.Find(key);
            if (result == null)
            {
                return NotFound();
            }

            patch.Put(result);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(result);
        }

        // POST: odata/Results
        public IHttpActionResult Post(Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Results.Add(result);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResultExists(result.ResultId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(result);
        }

        // PATCH: odata/Results(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<Result> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Result result = db.Results.Find(key);
            if (result == null)
            {
                return NotFound();
            }

            patch.Patch(result);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(result);
        }

        // DELETE: odata/Results(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            Result result = db.Results.Find(key);
            if (result == null)
            {
                return NotFound();
            }

            db.Results.Remove(result);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultExists(Guid key)
        {
            return db.Results.Count(e => e.ResultId == key) > 0;
        }
    }
}
