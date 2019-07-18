using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NacossWebElection.Models.DBModel;
namespace NacossWebElection.Models
{
    public class VoteModel
    {
        public static string returnMessage;
        public static int value;
        public bool checkPositionVote(string matno, int positionID)
        {
            if (string.IsNullOrEmpty(matno)) { return false; }

            using (var db = new NacossVotingDBEntities())
            {
                var voter = db.Voters.Find(matno);
                if (voter != null)
                {
                    //var checkPosition = db.CandidateVotes.Where
                    //    (a => a.positionID == positionID && a.VoterMatNo == matno);
                    var checkPosition = (from a in db.CandidateVotes where a.positionID == positionID && a.VoterMatNo == matno select a).SingleOrDefault();
                    if (checkPosition == null)
                    {
                        /// voter have not voted for that position
                        //
                        return true;
                    }
                    else
                    {
                        returnMessage = "You have already voted a candidate for that position";
                        return false;
                    }

                }
            }
            return false;
        }

        public bool checkCandidateVote(string voteMatno, string candidateMatno)
        {
            if (string.IsNullOrEmpty(voteMatno) || string.IsNullOrEmpty(candidateMatno)) { return false; }

            using (var db = new NacossVotingDBEntities())
            {
                var voter = db.Voters.Find(voteMatno);
                var candi = db.Candidates.Find(candidateMatno);
                if (voter != null && candi != null)
                {
                    var checkCandiate = (db.CandidateVotes.Where
                        (a => a.CandidateMatNo == candidateMatno && a.VoterMatNo == voteMatno)).SingleOrDefault();
                   
                    if (checkCandiate == null)
                    {
                        /// voter have not voted for that candidate
                        ///  
                        return true;
                    }
                    else
                    {
                        returnMessage = "You have already vote for that candidate";
                        return false;
                    }
                }
            }
            return false;
        }

        public bool voteCandidate(string voteMatno, string candidateMatno, int positionID)
        {
            if (checkPositionVote(voteMatno, positionID) && checkCandidateVote(voteMatno, candidateMatno))
            {
                // add vote count
                using (var db = new NacossVotingDBEntities())
                {
                    db.CandidateVotes.Add(new CandidateVote()
                    {
                        VoterMatNo = voteMatno,
                        CandidateMatNo = candidateMatno,
                        positionID = positionID,

                    });
                    //check if candidate is in candidateVote
                    var voteCandidate = db.CandidateVoteCounts.Find(candidateMatno);
                    if(voteCandidate != null)
                    {
                        //UPDATE RECORD [VOTE COUNT]
                        int count = voteCandidate.NoOfVotes;
                        voteCandidate.NoOfVotes = count + 1;

                        db.Entry(voteCandidate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        //add new candidate
                        db.CandidateVoteCounts.Add(new CandidateVoteCount {
                            CandidateMatNo = candidateMatno,
                            NoOfVotes = 1
                        });
                        db.SaveChanges();
                        return true;
                    }
                }   
             //   return true;
            }
            else
            {
                returnMessage = "Postion or Candidate has already been voted for";
                value = 1;

                return false;
            }
         
        }
    }

}