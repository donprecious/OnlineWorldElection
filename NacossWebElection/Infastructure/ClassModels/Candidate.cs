using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NacossWebElection.Models.DBModel;
namespace Infastructure.ClassModels
{
    public class Candidate
    {
       static int value;
        static string msg;
      public  static NacossWebElection.Models.DBModel.Candidate candidate;
        public bool addCandidate(string matNo, string firstName, string lastName, int positionID, string currentLevel, string sex, decimal gpa, string manifestor, string fileUrl) {
            var db = new NacossVotingDBEntities();
            using (db)
            {
                var rec = new NacossWebElection.Models.DBModel.Candidate()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MatNo = matNo,
                    Position = positionID,
                    CurrentLevel = currentLevel,
                    Sex = sex,
                    Gpa = gpa,
                    Manifestor = manifestor,
                    PhotoUrl = fileUrl
                };
                db.Candidates.Add(rec);
               db.SaveChanges();
                candidate = rec;
                return true;
            }
             return false;
           
        }

        public bool DeleteCandidate(string matNo)
        {
            try {
                var db = new NacossVotingDBEntities();
                var query = db.Candidates.Find(matNo);
                if (query != null)
                {
                    db.Candidates.Remove(query);
                    db.SaveChanges();
                    return true;
                    //delete Candidate

                }
            } catch (Exception ex) {
                msg = ex.Message;
                return false;
            }
           
               return false;
        }
        public bool EditCandidate(string matNo, string firstName, string lastName, int position, string phone, string sex, string currentLevel, decimal Gpa, string manifestor,string email)
        {
            try
            {
                var db = new NacossVotingDBEntities();
                var query = db.Candidates.Find(matNo);
                if (query != null)
                {
                    query.FirstName = firstName;
                    query.LastName = lastName;
                    query.phoneNo = phone;
                    query.Position = position;
                    query.Sex = sex;
                    query.Manifestor = manifestor;
                    query.Gpa = Gpa;
                    query.CurrentLevel = currentLevel;
                    query.email = email;
                    db.SaveChanges();
                    return true;
                    //delete Candidate

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            return false;
        }

        public bool EditPhoto(string matNo, string fileUrl)
        {
            try
            {
                var db = new NacossVotingDBEntities();
                var query = db.Candidates.Find(matNo);
                if (query != null)
                {
                    query.PhotoUrl = fileUrl;
                    db.Entry(query).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                    //delete Candidate
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            return false;
        }

        public bool DeletePhoto(string fileUrl)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            return false;
        }


    }
}