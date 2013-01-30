using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public class AgentRepository
    {
        private ProspectRealEstateDbContext db = new ProspectRealEstateDbContext();

        public AgentRepository() { }

        public Agent Find(int id)
        {
            var agent = db.Agents.SingleOrDefault(a => a.ID == id);
            
            if (agent == null) return null;

            return agent;
        }

        public IQueryable<Agent> Find(string name)
        {
            var lower_name = name.ToLowerInvariant();
            return from a in db.Agents
                   where a.name.ToLowerInvariant().Contains(lower_name)
                   select a;
        }

        public IQueryable<Agent> All
        {
            get
            {
                return from a in db.Agents
                       select a;
            }
        }
    }
}