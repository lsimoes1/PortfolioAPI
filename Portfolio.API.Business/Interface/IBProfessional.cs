using Portfolio.API.Model;
using System.Collections.Generic;

namespace Portfolio.API.Business.Interface
{
    public interface IBProfessional
    {
        List<MProfessional> GetAllProfessionalInfo();
    }
}
