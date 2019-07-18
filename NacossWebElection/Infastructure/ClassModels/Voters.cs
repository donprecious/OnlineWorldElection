using NacossWebElection.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infastructure.ClassModels
{
    public class Voters
    {

       public static int value;
      public  static string msg;
        public static string VoteID;
        public static Voter voter;
        public bool AddVoter(string matNo, string firstName, string lastName, string currentLevel, string phoneNo, string sex)
        {
            try
            {
                var db = new NacossVotingDBEntities();
                var query = db.Voters.Find(matNo);
                if (query == null)
                {
                    int voterID = new Reusable.Generators().WordGen1(6);
                    var rec = new Voter
                    {
                        CurrentLevel = currentLevel,
                        FirstName = firstName,
                        LastName =lastName,
                        MatNo = matNo,
                        Phone = phoneNo,
                        Sex = sex,
                        VoteID = voterID.ToString(),
                        VoterCredential = "No Item",
                    };   
                        db.Voters.Add(rec);
                        db.SaveChanges();
                        voter = rec;
                        VoteID = voterID.ToString();
                        return true;
                        //delete Candidate
                }
                else
                {
                    msg = "This Matriculation number is already registered";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

          //  return false;
        }

        public bool DeleteVoter(string matNo)
        {
            try
            {
                var db = new NacossVotingDBEntities();
                var query = db.Voters.Find(matNo);
                if (query != null)
                {
                    db.Voters.Remove(query);
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

        public bool EditVoters(string matNo,string firstName, string lastName, string sex, string phone, string currentLevel, string email) {
            try
            {
                var db = new NacossVotingDBEntities();
                var query = db.Voters.Find(matNo);
                if (query != null)
                {
                    query.email = email;
                    query.FirstName = firstName;
                    query.LastName = lastName;
                    query.Phone = phone;
                    query.Sex = sex;
                    query.CurrentLevel = currentLevel;

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
        public bool EditPhoto(string matNo, string fileUrl)
        {
            try
            {
                var db = new NacossVotingDBEntities();
                var query = db.Voters.Find(matNo);
                if (query != null)
                {
                    query.VoterCredential = fileUrl;
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
        public bool VerifyVoter(string matNo, string firstName, string lastName, string phone)
        {
            try
            {
                var db = new NacossVotingDBEntities();
                var query = db.Voters.Find(matNo);
                if (query != null)
                {
                    if (query.FirstName.ToUpper() == firstName.ToUpper() && query.LastName.ToUpper() == lastName.ToUpper() && query.Phone == phone) {
                        return true;
                    }
                    return false;
                    //delete Candidate
                }
                else
                {
                    value = 1;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            return false;
        }

        public bool checkVoteID(string voteID)
        {
            try
            {
                var db = new NacossVotingDBEntities();
                var q = db.Voters.Where(a => a.VoteID == voteID).SingleOrDefault();
                if(q== null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {

            }
                return false;
        }
    }
}