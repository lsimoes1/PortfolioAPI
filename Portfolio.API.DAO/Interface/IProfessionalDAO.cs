using Portfolio.API.Model;
using System.Collections.Generic;

namespace Portfolio.API.DAO.Interface
{
    public interface IProfessionalDAO
    {
        public List<MProfessional> FindProfessionalInfo();
    }
}
