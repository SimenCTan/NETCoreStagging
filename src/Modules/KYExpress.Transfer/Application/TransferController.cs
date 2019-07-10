using KYExpress.Core.Domain;
using KYExpress.Transfer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KYExpress.Core.Domain.Uow;
using KYExpress.Transfer.Application.Dto;
using System;
using KYExpress.Core;
using System.Threading.Tasks;

namespace KYExpress.Transfer.Application
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IRepository<TA_PDAHWLoadingScan> _loadScanRepository;
        private readonly IUnitOfWork _unitOfWork;
        public TransferController(IRepository<TA_PDAHWLoadingScan> loadSanRepository,IUnitOfWork unitOfWork)
        {
            _loadScanRepository = loadSanRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task TransferScan(PDAHWLoadingScanModel loadingScanModel)
        {
            var existScan = _loadScanRepository.FirstOrDefault(m => m.YD_No == loadingScanModel.YD_No
                                 && m.KndType == loadingScanModel.KndType && m.Department == loadingScanModel.Department
                                 && m.UploadBy == loadingScanModel.UploadBy);
            if (existScan != null)
            {
                _unitOfWork.Begin();
                var newTransferScan = new TA_PDAHWLoadingScan(loadingScanModel.YD_No, loadingScanModel.CHK_No
                    , loadingScanModel.ReturnDpt, loadingScanModel.UploadBy, DateTime.Now, loadingScanModel.Department, loadingScanModel.DeviceNo
                    , loadingScanModel.ErrorPrompt, loadingScanModel.Pullover, loadingScanModel.KndType, loadingScanModel.KndType
                    , loadingScanModel.PointName, loadingScanModel.ReturnDpt, loadingScanModel.PointName
                    , loadingScanModel.PointName,new Guid().ToString());
                await _loadScanRepository.InsertAsync(newTransferScan);
                _unitOfWork.Complete();
            }
            else
            {
                throw new CustomException("该运单号已经做过中转到件扫描",1401);
            }
        }
    }
}
