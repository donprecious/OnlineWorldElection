//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NacossWebElection.Models.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Voter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voter()
        {
            this.CandidateVotes = new HashSet<CandidateVote>();
        }
    
        public string MatNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string CurrentLevel { get; set; }
        public string email { get; set; }
        public string VoteID { get; set; }
        public string VoterCredential { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateVote> CandidateVotes { get; set; }
    }
}
