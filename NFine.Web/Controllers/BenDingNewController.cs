using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;

namespace NFine.Web.Controllers
{    /// <summary>
/// 本鼎插件模式新接口
/// </summary>
    public class BenDingNewController : ApiController
    {
        private readonly IWebBasicRepository _webServiceBasicRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IWebServiceBasicService _webServiceBasicService;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IResidentMedicalInsuranceService _residentMedicalInsuranceService;
        private readonly IOutpatientDepartmentService _outpatientDepartmentService;
        private readonly IOutpatientDepartmentRepository _outpatientDepartmentRepository;
        private readonly IWorkerMedicalInsuranceService _workerMedicalInsuranceService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insuranceRepository"></param>
        /// <param name="webServiceBasicService"></param>
        /// <param name="medicalInsuranceSqlRepository"></param>
        /// <param name="hisSqlRepository"></param>
        /// <param name="manageRepository"></param>
        /// <param name="residentMedicalInsuranceService"></param>
        /// <param name="webServiceBasicRepository"></param>
        /// <param name="outpatientDepartmentService"></param>
        /// <param name="outpatientDepartmentRepository"></param>
        /// <param name="workerMedicalInsuranceService"></param>
        public BenDingNewController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService webServiceBasicService,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            IHisSqlRepository hisSqlRepository,
            ISystemManageRepository manageRepository,
            IResidentMedicalInsuranceService residentMedicalInsuranceService,
            IWebBasicRepository webServiceBasicRepository,
            IOutpatientDepartmentService outpatientDepartmentService,
            IOutpatientDepartmentRepository outpatientDepartmentRepository,
            IWorkerMedicalInsuranceService workerMedicalInsuranceService
            )
        {
            _webServiceBasicService = webServiceBasicService;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _residentMedicalInsuranceRepository = insuranceRepository;
            _hisSqlRepository = hisSqlRepository;
            _systemManageRepository = manageRepository;
            _residentMedicalInsuranceService = residentMedicalInsuranceService;
            _webServiceBasicRepository = webServiceBasicRepository;
            _outpatientDepartmentService = outpatientDepartmentService;
            _outpatientDepartmentRepository = outpatientDepartmentRepository;
            _workerMedicalInsuranceService = workerMedicalInsuranceService;
        }


    }
}
