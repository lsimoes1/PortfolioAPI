using Portfolio.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.API.DAO.Interface
{
    public interface IAcademyDAO
    {
        public List<MAcademy> FindAllAcademyProjects();
    }
}
