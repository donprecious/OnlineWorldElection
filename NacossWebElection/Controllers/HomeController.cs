using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NacossWebElection.Models;
using System.Drawing;
using System.IO;

namespace NacossWebElection.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new Models.DBModel.NacossVotingDBEntities();
            var m = db.Candidates.ToList();
            return View(m);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult addVoters()
        {

            return View();
        }

        [HttpPost]
        public ActionResult addVoters(Models.ViewModels.voters m)
        {
            var add = new Infastructure.ClassModels.Voters().AddVoter(m.MatNo, m.FirstName, m.LastName, m.CurrentLevel, m.Phone, m.Sex);
            if (add)
            {
                string voteID = Infastructure.ClassModels.Voters.VoteID;
                Session["voteID"] = voteID;
                return Json(new { value = 200, message = "Successfully, Your VoteID is" + voteID+ "you will be redirected shortly", url = Url.Action("SubmitProof","home",new {id=m.MatNo }) });
               // return RedirectToAction("SubmitProof","home",new {id = m.MatNo });
            }
            else
            {
                return Json(new { value = 0, message = Infastructure.ClassModels.Voters.msg });
            }
      //      return View();
        }
        //public ActionResult SubmitProof()
        //{
        //    return View();
        //}

        public ActionResult SubmitProof(string id)
        {
            var db = new Models.DBModel.NacossVotingDBEntities();
            var user = db.Voters.Find(id);
            var view = new Models.ViewModels.UploadView();
            if (user != null)
            {
                view.ID = user.MatNo;
                view.photoUrl = user.VoterCredential;
                return View(view);
            }
            view.ID = "123";
            view.photoUrl = "Empty";
            return View(view);
        }

        [HttpPost]
        public ActionResult SubmitProof(string id, string imgData)
        {
            string ReturnMessage;
            if (string.IsNullOrEmpty(imgData))
            {
                
                ReturnMessage = "Credential upload failed. No data from image!";
                return Json(new { value = 0, message = ReturnMessage }, JsonRequestBehavior.AllowGet);
            }
            string matID = id;
            //id = id.Replace("/", "_");
            //string filename = Server.MapPath($"~/Credentials/{id}.jpg");
            //string filePath = $"~/Credentials/{id}.jpg";
            //if (Base64ImageToFile(imgData, filename, System.Drawing.Imaging.ImageFormat.Jpeg))
            //{
            //    if (new Infastructure.ClassModels.Voters().EditPhoto(matID, filePath))
            //    {

            //        ReturnMessage = "Credential uploaded successfully!";
            //        return Json(new { value = 200, message = ReturnMessage }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            if (Infastructure.FileUpload.uploadToNet(imgData, "Credentials"))
            {
                string filePath = Infastructure.FileUpload.filePath;
                if (new Infastructure.ClassModels.Voters().EditPhoto(matID, filePath))
                {

                    ReturnMessage = "Credential uploaded successfully!";
                    return Json(new { value = 200, message = ReturnMessage }, JsonRequestBehavior.AllowGet);
                }
            }
            ReturnMessage = "Failure uploading your Credential photo!";
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
               string ReturnMessage = ex.Message;
                return false;
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Candidate ()
        {
            using (var db = new Models.DBModel.NacossVotingDBEntities()) {

            }
                return View();
        }
        public ActionResult Verify()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult Verify(string voteID)
        {
          
            using (var db = new Models.DBModel.NacossVotingDBEntities())
            {

                var list = db.Voters.Where(a => a.VoteID == voteID).SingleOrDefault();
                var m = new Models.ViewModels.VerifyDetails();
                if(list != null)
                {
  m.FirstName = list.FirstName;
                m.LastName = list.LastName;
                m.matno = list.MatNo;
                m.PhoneNo = list.Phone;
                }
              
                return View(m);
            }

        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Verify(string id, Models.ViewModels.VerifyDetails m)
        {
           if(new Infastructure.ClassModels.Voters().VerifyVoter(m.matno, m.FirstName, m.LastName, m.PhoneNo)){
                Session["matNo"] = m.matno;
                Session["VERIFY"] = "SUCCESS";
                Session["FirstName"] = m.FirstName;
                Session["LastName"] = m.LastName;

                return Json(new { value = 200, message="Your Verification Was Successful, You can now vote" });
            }
            else if(Infastructure.ClassModels.Voters.value ==1)
            {
                return Json(new { value = 0, message = "It seems that your matriculation number has not been registered please contact your Executives to register you." });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Vote(string id, string candidateID, int positionID)
        {
            //check vote time 
           


                if (Session["matNo"] != null)
                {
                    var db = new Models.DBModel.NacossVotingDBEntities();
                DateTime currentDate = DateTime.UtcNow;
                DateTime startTime = db.VoteTimes.FirstOrDefault().VoteTimeStart.Value;
                DateTime EndTime = db.VoteTimes.FirstOrDefault().VoteTimeEnd.Value;
                int cmp = DateTime.Compare(currentDate, startTime);
          
                if (cmp < 0)
                {
                    //too early to vote 
                    return Json(new { value = 0, message = "You are too Early to vote wait till " + startTime.ToString() }, JsonRequestBehavior.AllowGet);

                }
                else if(cmp>= 0)
                {
                    //It Ok vote can proceed
                    int cmp2 = DateTime.Compare(currentDate, EndTime);
                    if (cmp2 >= 0)
                    {
                        // Now you can vote
                        string matno = Convert.ToString(Session["matNo"]);
                        var search = db.Voters.Find(matno);
                        if (search != null)
                        {
                            //vote 
                            var vote = new Models.VoteModel().voteCandidate(id, candidateID, positionID);
                            if (vote)
                            {
                                return Json(new { value = 200, message = "Your vote was successful" }, JsonRequestBehavior.AllowGet);
                            }
                            else if (Models.VoteModel.value == 1)
                            {
                                return Json(new { value = 0, message = "It seems you have already vote for this candidate, or this position" }, JsonRequestBehavior.AllowGet);

                            }
                            return Json(new { value = 1, message = "It seems Your Session has timed out, Try to Re Verify your Matno Before Proceeding" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //time for voting as passed
                        return Json(new { value = 0, message = "Time for voting as passed" }, JsonRequestBehavior.AllowGet);

                    }
                }          
            }
                else
                 {
                TempData["value"] = 1;
                string url = Url.Action("verify", "home");
                return Json(new { value = 1, message = "It seems Your Session has timed out, Try to Re Verify your Matno Before Proceeding", url=url }, JsonRequestBehavior.AllowGet);

               
                 }
            return Json(new { value = 0, message = "It seems Your Session has timed out, Try to Re Verify your Matno Before Proceeding" }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult VoteResult() {
            var db = new Models.DBModel.NacossVotingDBEntities();
            var m = db.CandidateVoteCounts.OrderByDescending(a => a.NoOfVotes).ToList();

            return PartialView("_VoteResult", m);
        }
    }
}