using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using Newtonsoft.Json;

namespace Bigschool_TH_11.Controllers.Api
{
    public class CBNVController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CBNV
        /*public IQueryable<CBNV> GetCBNVs()
        {
            return db.CBNVs;
        }*/
        // GET: api/CBNV
        public IEnumerable<CBNVViewModel> GetCBNVs()
        {
            /*return db.CBNVs.Include(c => c.CBNVChuyenNganhs.Select(x => x.ChuyenNganh)).Include(c => c.BankingInfo).Select(c => new CBNVViewModel
            {
                CBNV = c,
                BankingInfo = c.BankingInfo,
                ChuyenNganhs = c.CBNVChuyenNganhs.Select(x => x.ChuyenNganh).ToList()
            });*/
            using (var context = new ApplicationDbContext())
            {
                context.Configuration.ProxyCreationEnabled = false;

                return context.CBNVs
                    .Include(c => c.CBNVChuyenNganhs.Select(x => x.ChuyenNganh))
                    .Include(c => c.BankingInfo)
                    .Select(c => new CBNVViewModel
                    {
                        CBNV = c,
                        BankingInfo = c.BankingInfo,
                        ChuyenNganhs = c.CBNVChuyenNganhs.Select(x => x.ChuyenNganh).ToList()
                    }).ToList();
                    
            }
        }

        // GET: api/CBNV/5
        [ResponseType(typeof(CBNVViewModel))]
        public async Task<IHttpActionResult> GetCBNV(string id)
        {
            CBNV cbnv = db.CBNVs.Include(c => c.CBNVChuyenNganhs.Select(x => x.ChuyenNganh)).SingleOrDefault(c => c.MaCBNV == id);
            if (cbnv == null)
            {
                return NotFound();
            }

            BankingInfo bankingInfo = await db.BankingInfos.FirstOrDefaultAsync(b => b.CBNVId == cbnv.MaCBNV);

            CBNVViewModel cbnvViewModel = new CBNVViewModel
            {
                CBNV = cbnv,
                BankingInfo = bankingInfo,
                ChuyenNganhs = cbnv.CBNVChuyenNganhs.Select(x => x.ChuyenNganh).ToList(),
                PhongBan = cbnv.PhongBan // Add the related PhongBan to the ViewModel
            };

            return Ok(cbnvViewModel);
        }

        // PUT: api/CBNV/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCBNV(string id, [FromBody] CBNVViewModel cbnvViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cbnvViewModel.CBNV == null)
            {
                return BadRequest("CBNV object is null.");
            }

            if (id != cbnvViewModel.CBNV.MaCBNV)
            {
                return BadRequest();
            }

            // Load the existing CBNV entity from the database
            var existingCBNV = await db.CBNVs.Include(x => x.BankingInfo).SingleOrDefaultAsync(x => x.MaCBNV == id);

            if (existingCBNV == null)
            {
                return NotFound();
            }

            // Remove existing relationships
            var existingCBNVChuyenNganhs = db.CBNVChuyenNganhs.Where(x => x.MaCBNV == id).ToList();
            foreach (var existingCBNVChuyenNganh in existingCBNVChuyenNganhs)
            {
                db.CBNVChuyenNganhs.Remove(existingCBNVChuyenNganh);
            }

            // Add new relationships
            foreach (var chuyenNganh in cbnvViewModel.ChuyenNganhs)
            {
                db.CBNVChuyenNganhs.Add(new CBNVChuyenNganh { MaCBNV = id, MaChuyenNganh = chuyenNganh.MaChuyenNganh });
            }

            // Update the CBNV entity
            db.Entry(existingCBNV).CurrentValues.SetValues(cbnvViewModel.CBNV);

            // Update the PhongBan relationship
            if (cbnvViewModel.PhongBan != null)
            {
                existingCBNV.MaPhongBan = cbnvViewModel.PhongBan.MaPhongBan;
            }
            else
            {
                existingCBNV.MaPhongBan = null;
            }

            if (cbnvViewModel.BankingInfo != null)
            {
                if (existingCBNV.BankingInfo != null)
                {
                    // Update the existing banking info without changing the key property 'Id'
                    var updatedBankingInfo = new BankingInfo
                    {
                        //Id = existingCBNV.BankingInfo.Id,
                        BankName = cbnvViewModel.BankingInfo.BankName,
                        AccountNumber = cbnvViewModel.BankingInfo.AccountNumber,
                        AccountHolderName = cbnvViewModel.BankingInfo.AccountHolderName,
                        Branch = cbnvViewModel.BankingInfo.Branch,
                        SwiftCode = cbnvViewModel.BankingInfo.SwiftCode,
                        CBNVId = cbnvViewModel.CBNV.MaCBNV
                    };
                    //System.Diagnostics.Debug.WriteLine("AccountHolderName: " + updatedBankingInfo.AccountHolderName);
                    db.Entry(existingCBNV.BankingInfo).CurrentValues.SetValues(updatedBankingInfo);
                }
                else
                {
                    // Add new banking info
                    cbnvViewModel.BankingInfo.CBNVId = cbnvViewModel.CBNV.MaCBNV;
                    db.BankingInfos.Add(cbnvViewModel.BankingInfo);
                    // Set the relationship between the CBNV and BankingInfo entities
                    existingCBNV.BankingInfo = cbnvViewModel.BankingInfo;
                }
            }
            else if (existingCBNV.BankingInfo != null)
            {
                // Remove the existing banking info
                db.BankingInfos.Remove(existingCBNV.BankingInfo);
            }
            //System.Diagnostics.Debug.WriteLine($"Before Update - CBNV: {JsonConvert.SerializeObject(existingCBNV)}");
            //System.Diagnostics.Debug.WriteLine($"Before Update - ViewModel: {JsonConvert.SerializeObject(cbnvViewModel)}");
            try
            {
                /*int changedEntities =*/ await db.SaveChangesAsync();
                //System.Diagnostics.Debug.WriteLine("Number of entities changed: " + changedEntities);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CBNVExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //System.Diagnostics.Debug.WriteLine($"After Update - CBNV: {JsonConvert.SerializeObject(existingCBNV)}");
            //System.Diagnostics.Debug.WriteLine($"After Update - ViewModel: {JsonConvert.SerializeObject(cbnvViewModel)}");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CBNV
        [ResponseType(typeof(CBNV))]
        public async Task<IHttpActionResult> PostCBNV(CBNVViewModel cbnvViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generate the MaCBNV value
            cbnvViewModel.CBNV.MaCBNV = GenerateMaCBNV();

            // Set the PhongBan relationship
            if (cbnvViewModel.PhongBan != null)
            {
                cbnvViewModel.CBNV.MaPhongBan = cbnvViewModel.PhongBan.MaPhongBan;
            }

            if (cbnvViewModel.BankingInfo != null)
            {
                cbnvViewModel.BankingInfo.CBNVId = cbnvViewModel.CBNV.MaCBNV;
                cbnvViewModel.CBNV.BankingInfo = cbnvViewModel.BankingInfo;
            }

            db.CBNVs.Add(cbnvViewModel.CBNV);

            // Add ChuyenNganhs relationships
            foreach (var chuyenNganh in cbnvViewModel.ChuyenNganhs)
            {
                db.CBNVChuyenNganhs.Add(new CBNVChuyenNganh { MaCBNV = cbnvViewModel.CBNV.MaCBNV, MaChuyenNganh = chuyenNganh.MaChuyenNganh });
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Exception currentEx = ex;
                while (currentEx != null)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + currentEx.Message);
                    currentEx = currentEx.InnerException;
                }

                if (CBNVExists(cbnvViewModel.CBNV.MaCBNV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cbnvViewModel.CBNV.MaCBNV },  cbnvViewModel );
        }

        // DELETE: api/CBNV/5
        [ResponseType(typeof(CBNV))]
        public async Task<IHttpActionResult> DeleteCBNV(string id)
        {
            CBNV cbnv = await db.CBNVs.Include(c => c.BankingInfo).SingleOrDefaultAsync(c => c.MaCBNV == id);

            if (cbnv == null)
            {
                return NotFound();
            }

            if (cbnv.BankingInfo != null)
            {
                db.BankingInfos.Remove(cbnv.BankingInfo);
            }

            db.CBNVs.Remove(cbnv);
            await db.SaveChangesAsync();

            return Ok(cbnv);
        }

        private string GenerateMaCBNV()
        {
            var prefix = "CBNV";
            var lastCBNV = db.CBNVs.OrderByDescending(c => c.MaCBNV).FirstOrDefault();

            if (lastCBNV == null)
            {
                return $"{prefix}00001";
            }

            int currentNumber = int.Parse(lastCBNV.MaCBNV.Substring(prefix.Length));
            string newNumber = (currentNumber + 1).ToString("D5"); // Pad the number with leading zeros to 5 digits

            return $"{prefix}{newNumber}";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CBNVExists(string id)
        {
            return db.CBNVs.Count(e => e.MaCBNV == id) > 0;
        }
    }
}