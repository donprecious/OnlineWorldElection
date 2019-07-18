using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NacossWebElection.Models;
using NacossWebElection.Models.DBModel;
using System.Drawing;
using System.IO;


using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NacossWebElection.Controllers
{
    [Authorize(Roles  = "Admin,SuperAdmin")]
    public class AdminController : Controller
    {
        // GET: Admin
        static string ReturnMessage;
    
        public ActionResult Index()
        {
            var db = new NacossVotingDBEntities();
            var list = db.CandidateVoteCounts.ToList();

            return View(list);
        }
        public ActionResult Candidates() {
         
        var db = new NacossVotingDBEntities();
            var list = (from p in db.Candidates select p).ToList();
            return View(list);
        }
        public ActionResult AddCandidate()
        {
            var db = new NacossVotingDBEntities();
         //   Models.ViewModels.Candidates m = new Models.ViewModels.Candidates();
            var list = (from p in db.Candidates select p).ToList();
            var pos = db.Positions.ToList();
         var    list1 = new SelectList(list, "PositionID", "Position1");
      
            return View();
        }

        [HttpPost]
        public ActionResult AddCandidate(Models.ViewModels.Candidates m)
        {
         
            var db = new NacossVotingDBEntities();
            if (ModelState.IsValid)
            {
                //Upload The Image
         //       string userID = m.MatNo.Replace("/", "_");
                //var UploadImage = new Infastructure.FileUpload().UploadAvatar(userID, imgData);

                // Get String ImagePath
                var AddNewCandidate = new Infastructure.ClassModels.Candidate().addCandidate(m.MatNo,m.FirstName,m.LastName,m.PositionID,m.CurrentLevel,m.Sex,m.Gpa,m.Manifestor,"No File Added");
                var rec = Infastructure.ClassModels.Candidate.candidate; 
                //get the File Path
                if(AddNewCandidate)
                {
                    string position = db.Positions.Where(p => p.PositionID == rec.Position).Select(a => a.Position1).SingleOrDefault().ToString();
                    return Json(new { post = rec, value = 200, message = "Candidate Added Successfully", position = position, url = Url.Action("UploadAvatar","admin", new {id =m.MatNo }) }); 
                }
            }
            else if(db.Candidates.Find(m.MatNo) != null)
            {
                return Json(new { message = "Matriculation Number Already Exits", value = 0 });
            }
          
            return View(m);
        }
        public ActionResult UploadAvater()
        {

            return View();
        }
       
        public ActionResult UploadAvatar(string id)
        {
            var db = new NacossVotingDBEntities();
            var user = db.Candidates.Find(id);
            if(user != null)
            {
                var view = new Models.ViewModels.UploadView();

                view.ID = user.MatNo;
                view.photoUrl = user.PhotoUrl;
                return View(view);
            }
            return View();
        }
        
        [HttpPost]
            public ActionResult UploadAvatar(string id, string imgData)
            {
            if (string.IsNullOrEmpty(imgData))
            {
                ReturnMessage = "Profile picture upload failed. No data from photo!";
                return Json(new { value = 0 , message = ReturnMessage}, JsonRequestBehavior.AllowGet);
            }
           string matID = id;
            // id = id.Replace("/", "_");
            // string filename = Server.MapPath($"~/Avatars/{id}.jpg");
            //string filePath = $"~/Avatars/{id}.jpg";
            // if (Base64ImageToFile(imgData, filename, System.Drawing.Imaging.ImageFormat.Jpeg))
            // {


            // }
            if (Infastructure.FileUpload.uploadToNet(imgData, "Avatars"))
            {
                string filePath = Infastructure.FileUpload.filePath;
                if (new Infastructure.ClassModels.Candidate().EditPhoto(matID, filePath))
                {

                    ReturnMessage = "Profile picture uploaded successfully!";
                    return Json(new { value = 200, message = ReturnMessage }, JsonRequestBehavior.AllowGet);
                }
            }
          
            ReturnMessage = "Failure uploading your profile picture!";
            return Json(new { value = 0, message = ReturnMessage }, JsonRequestBehavior.AllowGet);
        }




        public bool Base64ImageToFile(string Base64String, string SaveAs, System.Drawing.Imaging.ImageFormat Format)
        {
            try
            {
                // /9j/
                //Remove this part "data:image/jpeg;base64,"
                Base64String = Base64String.Split(',')[1];            
             //  Base64String = Base64String.Remove(0,4);
               byte[] bytes = Convert.FromBase64String(Base64String);
              //  byte[] bytes = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");
                 Image image;
                using (var ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                image.Save(SaveAs, Format);
                return true;
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                return false;
            }
        }
    
        [HttpGet]
        public ActionResult DeleteCandidate(string id) {
            if (new Infastructure.ClassModels.Candidate().DeleteCandidate(id))
            {
                return Json(new { message = "Candidate Deleted", status = 200 }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = "Operation Failed", status = "0" }, JsonRequestBehavior.AllowGet);
            
        }

    
        
        public ActionResult EditCandidate(string id)
    {
            var db = new NacossVotingDBEntities();
            var m = db.Candidates.Where(a => a.MatNo == id).SingleOrDefault();
            return View(m);
        }

        
        [HttpPost]
        public ActionResult EditCandidate(string id, Candidate m)
        {
            if (new Infastructure.ClassModels.Candidate().EditCandidate(id, m.FirstName, m.LastName, m.Position, m.phoneNo, m.Sex, m.CurrentLevel, m.Gpa, m.Manifestor, m.email)) {
                TempData["view"] = 1;
            }
            return View(m);
        }
        public ActionResult Notify() {
            ViewBag.Message = "Successful";
            return View();
        }

        public ActionResult voters()
        {
            var db = new NacossVotingDBEntities();
            var list = (from p in db.Voters select p).ToList();
            return View(list);
        }
        public ActionResult addvoters()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addvoters(Models.ViewModels.voters m)
        {
            if(ModelState.IsValid)
            {
                var reg = new Infastructure.ClassModels.Voters().AddVoter(m.MatNo, m.FirstName, m.LastName, m.CurrentLevel, m.Phone, m.Sex);
                var rec = Infastructure.ClassModels.Voters.voter;
               if(reg)
                {
                    return Json(new { message = "Voter Added", post = rec,  value = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "This Matno already exist, voter's ID is "+ rec.VoteID , value = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
                return View(m);
        }

        [HttpGet]
        public ActionResult DeleteVoter(string id)
        {
            if (new Infastructure.ClassModels.Voters().DeleteVoter(id))
            {
                return Json(new { message = "Voter Deleted", status = 200 }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = "Operation Failed", status = "0" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult position()
        {
            return View();
        }
        public ActionResult EditPosition(int id) {
            var db = new NacossVotingDBEntities();
            var s = db.Positions.Find(id);
            return View(s);
        }
        [HttpPost]
        public ActionResult EditPosition(int id, Models.DBModel.Position m)
        {
            var db = new NacossVotingDBEntities();
            var s = db.Positions.Find(id);
            if(s!= null)
            {
                s.Position1 = m.Position1;
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["view"] = 1;
                TempData["message"] = "Changes Was Recorded";
                //save changes
            }
            else
            {
                TempData["message"] = "Sorry Something Went Wrong";
                TempData["view"] = 0;
            }
            return View();
        }

        [ChildActionOnly]
        public ActionResult Positions()
        {
            var db = new NacossVotingDBEntities();
            var list = db.Positions.ToList();
            return PartialView("_Positions", list);
        }


        [HttpPost]
        public ActionResult position(Models.ViewModels.positions m)
        {
          
            if (ModelState.IsValid) {
                var db = new NacossVotingDBEntities();
                var post = new Position
                {
                    Position1 = m.position
                };
                db.Positions.Add(post);

                db.SaveChanges();
                return Json(new { post = post, value = 200, message = "Position Added Successfully!" },JsonRequestBehavior.AllowGet);
              
            }
           
            return View();
        }

        [HttpGet]
        public ActionResult DeletePosition(int id)
        {

            if (ModelState.IsValid)
            {
                var db = new NacossVotingDBEntities();
                var q = db.Positions.Find(id);
                if(q != null)
                {
                    db.Positions.Remove(q);
                    db.SaveChanges();
                    return Json(new { value=200, message = "Position Removed Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new {value=1, message = "Position Removed Successfully!" }, JsonRequestBehavior.AllowGet);

            }

            return View();
        }
        [ChildActionOnly]
        public ActionResult votersList()
        {
            var db = new NacossVotingDBEntities();

            var list = db.Voters.ToList();
            return PartialView("_VoterList", list);
        }

        public ActionResult EditVoters( string id)
        {
            var db = new NacossVotingDBEntities();
            var m = db.Voters.Where(a => a.MatNo == id).SingleOrDefault();
            return View(m);
        }

        [HttpPost]
        public ActionResult EditVoters(Voter m,string id)
        {
            if (new Infastructure.ClassModels.Voters().EditVoters(m.MatNo, m.FirstName, m.LastName, m.Sex, m.Phone, m.CurrentLevel, m.email)) {
                TempData["view"] = 1;
            }
            return View(m);
        }


        [ChildActionOnly]
        public ActionResult candidateList() {
            var db = new NacossVotingDBEntities();

            var list = db.Candidates.ToList();
            return PartialView("_CandidateList", list);
        }

        public ActionResult Users()
        {

            var db = new NacossVotingDBEntities();
            var list = db.Users.ToList();
            return View(list);
        }
        //ApplicationDbContext context = new ApplicationDbContext();
        //UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            var db = new NacossVotingDBEntities();
            //find Users
        
            var findUser = db.Users.Find(id);
             if(findUser != null)
                {
                //Delete User
               
                db.Users.Remove(findUser);
                db.SaveChanges();
                return Json(new { value = 200, message="User Deleted"}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { value = 0, message = "Something Went wrong" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MakeAdmin(string id)
        {
            Reusable.RoleCreator  roleHandle= new Reusable.RoleCreator();

            if (roleHandle.MakeUserAdmin(id))
            {
                return Json(new { value = 200, message = "User have been added to admin role" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { value = 0, message = "Something went wrong" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RemoveUserFromAdmin(string id)
        {
            Reusable.RoleCreator roleHandle = new Reusable.RoleCreator();

            if (roleHandle.RemoveUserFromAdmin(id))
            {
                return Json(new { value = 200, message = "User have been removed from admin role" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { value = 0, message = "Something went wrong" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddDateTime()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddDateTime(Models.ViewModels.VoteDateTimeView m)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    string StartdateTimeStr =  m.month + "/" + m.day + "/" + m.year + " " + m.Hour + ":" + m.minute + ":" + "00" + " " + m.period;
                    string StopdateTimeStr = m.smonth + "/" +  m.sday + "/" + m.syear + " " + m.sHour + ":" + m.sminute + ":" + "00" + " " + m.speriod;

                    DateTime StartdateTime = DateTime.Parse(StartdateTimeStr, System.Globalization.CultureInfo.InvariantCulture);
                    DateTime StopdateTime = DateTime.Parse(StopdateTimeStr, System.Globalization.CultureInfo.InvariantCulture);

                    using(var db = new Models.DBModel.NacossVotingDBEntities())
                    {
                        var q = db.VoteTimes.FirstOrDefault();
                        if(q == null)
                        {
                            //add new 
                            db.VoteTimes.Add(new VoteTime
                            {
                                VoteTimeStart = StartdateTime,
                                VoteTimeEnd = StopdateTime
                            });
                        }
                        else
                        {
                            //modify
                            q.VoteTimeEnd = StopdateTime;
                            q.VoteTimeStart = StartdateTime;
                            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                        }
                        db.SaveChanges();
                        //string newDate1="", newDate2="";
                        string[] date1 = StartdateTime.GetDateTimeFormats('f'), date2 = StopdateTime.GetDateTimeFormats('f');
                        //foreach (string x in date1) {
                        //    newDate1 += x;
                        //}
                        //foreach (string x in date2)
                        //{
                        //    newDate2 += x;
                        //}

                        return Json(new { value = 200, message = "Date set successful, new date is " + date1[0] + " to  " + date2[0] });
                    }
                }
                return Json(new { value = 0, message = "Something went wrong" });
            } 
            catch (Exception ex) {
                return Json(new { value = 0, message = "Something went wrong" });
            }
           // return View();
        }
    }

}