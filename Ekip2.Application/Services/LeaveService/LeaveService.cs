using Ekip2.Application.DTOs.AdvanceDTOs;
using Ekip2.Application.DTOs.LeaveDTOs;
using Ekip2.Application.Services.AdvanceServices;
using Ekip2.Domain.Entities;
using Ekip2.Infrastructure.Repositories.LeaveRepositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.LeaveService
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly ILogger<LeaveService> _logger;
        private readonly IMailService _mailService;
         private readonly UserManager<IdentityUser> _userManager;

        private readonly IManagerService _managerService;

        public LeaveService(ILeaveRepository leaveRepository, ILogger<LeaveService> logger, IMailService mailService, IManagerService managerService, UserManager<IdentityUser> userManager)
        {
            _leaveRepository = leaveRepository;
            _logger = logger;
            _mailService = mailService;
            _managerService = managerService;
            _userManager = userManager;
        }

        public async Task<IDataResult<List<LeaveListDTO>>> GetAllAsync()
        {
            var leaves = await _leaveRepository.GetAllAsync();
            if (leaves == null)
            {
                return new ErrorDataResult<List<LeaveListDTO>>("Listeleme başarısız");
            }

            var leaveListDTOs = new List<LeaveListDTO>();

            foreach (var leave in leaves)
            {
                var leaveListDTO = leave.Adapt<LeaveListDTO>();

                // Manager rol bilgisi ve diğer gerekli bilgileri DTO'ya ekleyin
                leaveListDTO.ManagerFirstName = leave.Manager.FirstName;
                leaveListDTO.ManagerLastName = leave.Manager.LastName;
                leaveListDTO.Roles = leave.Manager.Roles; // `Roles` doğru atanmalı
                leaveListDTO.CompanyId = leave.Manager.CompanyId;
                leaveListDTO.LeaveTypeType = leave.LeaveType.Type;

                leaveListDTOs.Add(leaveListDTO);
            }

            return new SuccessDataResult<List<LeaveListDTO>>(leaveListDTOs, "Listeleme başarılı.");
        }


        public async Task<IDataResult<LeaveDTO>> CreateAsync(LeaveCreateDTO leaveCreateDTO)
        {
            var newLeave = leaveCreateDTO.Adapt<Leave>();
            newLeave.ManagerId = leaveCreateDTO.ManagerId;
            newLeave.LeaveTypeId = leaveCreateDTO.LeaveTypeId;
            newLeave.LeaveStatus = LeaveStatus.Pending;

            try
            {
                await _leaveRepository.AddAsync(newLeave);
                await _leaveRepository.SaveChangesAsync();

                var manager = await _managerService.GetByIdAsync(newLeave.ManagerId);

                if (manager != null && !string.IsNullOrEmpty(manager.Data.Email))
                {
                    var companyManagers = await _managerService.GetManagersByCompanyIdAsync(manager.Data.CompanyId);
                    var managerEmails = companyManagers
                        .Select(m => m.IdentityId)
                        .Distinct()
                        .ToList();

                    foreach (var identityId in managerEmails)
                    {
                        var user = await _userManager.FindByIdAsync(identityId);
                        if (user != null && !string.IsNullOrEmpty(user.Email))
                        {
                            var mailDTO = new MailDTO
                            {
                                Email = user.Email,
                                Subject = "New Leave Request Created",
                                Message = "A new leave request has been created and is pending your approval."
                            };

                            await _mailService.SendMailAsync(mailDTO);
                        }
                    }
                }

                return new SuccessDataResult<LeaveDTO>(newLeave.Adapt<LeaveDTO>(), "İzin başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İzin eklenirken bir hata oluştu.");
                return new ErrorDataResult<LeaveDTO>(newLeave.Adapt<LeaveDTO>(), "İzin ekleme başarısız: " + ex.Message);
            }
        }


        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingLeave = await _leaveRepository.GetByIdAsync(id);
            if (deletingLeave == null)
            {
                return new ErrorResult("Silinecek izin bulunamadı.");
            }

            await _leaveRepository.DeleteAsync(deletingLeave);
            await _leaveRepository.SaveChangesAsync();

            
            if (deletingLeave.Manager != null && !string.IsNullOrEmpty(deletingLeave.Manager.Email) && deletingLeave.Manager.Roles == Roles.Employee)
            {
                var mailDTO = new MailDTO
                {
                    Email = deletingLeave.Manager.Email,
                    Subject = "Leave Deleted",
                    Message = "A leave request for one of your employees has been deleted."
                };
                await _mailService.SendMailAsync(mailDTO);
            }

            return new SuccessResult("İzin başarıyla silindi.");
        }

        public async Task<IDataResult<LeaveDTO>> GetByIdAsync(Guid id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
            {
                return new ErrorDataResult<LeaveDTO>("Veri bulunamadı.");
            }

            // Mapster ile LeaveDTO'ya mapleme
            var leaveDTO = leave.Adapt<LeaveDTO>();

            return new SuccessDataResult<LeaveDTO>(leaveDTO, "Veri başarıyla bulundu");
        }

        public async Task<IDataResult<LeaveDTO>> UpdateAsync(LeaveUpdateDTO leaveUpdateDTO)
        {
            var leave = await _leaveRepository.GetByIdAsync(leaveUpdateDTO.Id);
            if (leave == null)
            {
                return new ErrorDataResult<LeaveDTO>("Güncellenecek izin bulunamadı.");
            }

            leave.StartDate = leaveUpdateDTO.StartDate;
            leave.EndDate = leaveUpdateDTO.EndDate;
            leave.LeaveTypeId = leaveUpdateDTO.LeaveTypeId;
            leave.ManagerId = leaveUpdateDTO.ManagerId;
            leave.LeaveStatus = leaveUpdateDTO.LeaveStatus;

            try
            {
                await _leaveRepository.UpdateAsync(leave);
                await _leaveRepository.SaveChangesAsync();
                return new SuccessDataResult<LeaveDTO>(leave.Adapt<LeaveDTO>(), "İzin güncelleme başarılı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İzin reddedilirken bir hata oluştu.");
                return new ErrorDataResult<LeaveDTO>("İzin güncelleme sırasında bir hata oluştu.");
            }
        }

        public async Task<IResult> ApproveLeaveAsync(Guid id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
            {
                return new ErrorResult("Onaylanacak izin bulunamadı");
            }

            leave.LeaveStatus = LeaveStatus.Approved;
            try
            {
                await _leaveRepository.UpdateAsync(leave);
                await _leaveRepository.SaveChangesAsync();

                if (leave.Manager != null && !string.IsNullOrEmpty(leave.Manager.Email))
                {
                    var mailDTO = new MailDTO
                    {
                        Email = leave.Manager.Email,
                        Subject = "Leave Approved",
                        Message = "Your leave request has been approved."
                    };
                    await _mailService.SendMailAsync(mailDTO);
                }

                return new SuccessDataResult<LeaveDTO>("İzin Onaylama Başarılı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İzin onaylanırken bir hata oluştu.");
                return new ErrorResult("İzin onaylama sırasında bir hata oluştu.");
            }


        }

        public async Task<IResult> RejectLeaveAsync(Guid id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
            {
                return new ErrorResult("İzin bulunamadı.");
            }

            leave.LeaveStatus = LeaveStatus.Rejected;
            try
            {
                await _leaveRepository.UpdateAsync(leave);
                await _leaveRepository.SaveChangesAsync();

                if (leave.Manager != null && !string.IsNullOrEmpty(leave.Manager.Email))
                {
                    var mailDTO = new MailDTO
                    {
                        Email = leave.Manager.Email,
                        Subject = "Leave Rejected",
                        Message = "Your leave request has been rejected."
                    };
                    await _mailService.SendMailAsync(mailDTO);
                }

                return new SuccessDataResult<LeaveDTO>("İzin reddetme başarılı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İzin reddedilirken bir hata oluştu.");
                return new ErrorResult("İzin reddetme sırasında bir hata oluştu.");
            }

        }
    }
}
