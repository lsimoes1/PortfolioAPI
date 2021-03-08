using Newtonsoft.Json;
using Portfolio.API.Business.Interface;
using Portfolio.API.DAO.Interface;
using Portfolio.API.Model;
using System;
using System.Collections.Generic;

namespace Portfolio.API.Business
{
    public class BProfessional : IBProfessional
    {
        private IProfessionalDAO _professionalDao;

        public BProfessional(IProfessionalDAO professionalDao)
        {
            _professionalDao = professionalDao;
        }

        public List<MProfessional> GetAllProfessionalInfo()
        {
            try
            {
                List<MProfessional> professionalInfo = null;
                try
                {
                    professionalInfo = _professionalDao.FindProfessionalInfo();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                if (professionalInfo == null || professionalInfo.Count.Equals(0))
                {
                    return null;
                }
                else
                {
                    return professionalInfo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
