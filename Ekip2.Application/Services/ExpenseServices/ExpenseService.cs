using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ekip2.Application.DTOs.ExpenseDTOs;

using Ekip2.Domain.Entities;
using Ekip2.Domain.Enums;
using Ekip2.Infrastructure.Repositories.ExpenseRepositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Ekip2.Application.Services.ExpenseServices
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ILogger<ExpenseService> _logger;
        private readonly IMailService _mailService;
        private readonly IManagerService _managerService;
        private readonly UserManager<IdentityUser> _userManager;

        public ExpenseService(IExpenseRepository expenseRepository, ILogger<ExpenseService> logger, IMailService mailService, IManagerService managerService, UserManager<IdentityUser> userManager)
        {
            _expenseRepository = expenseRepository;
            _logger = logger;
            _mailService = mailService;
            _managerService = managerService;
            _userManager = userManager;
        }

        public async Task<IDataResult<ExpenseDTO>> CreateAsync(ExpenseCreateDTO expenseCreateDTO)
        {
            var newExpense = expenseCreateDTO.Adapt<Expense>();
            newExpense.ManagerId = newExpense.ManagerId;

            try
            {
                await _expenseRepository.AddAsync(newExpense);
                await _expenseRepository.SaveChangesAsync();

                var manager = await _managerService.GetByIdAsync(newExpense.ManagerId);

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
                                Subject = "New Expense Request Created",
                                Message = "A new expense request has been created and is pending your approval."
                            };

                            await _mailService.SendMailAsync(mailDTO);
                        }
                    }
                }

                return new SuccessDataResult<ExpenseDTO>(newExpense.Adapt<ExpenseDTO>(), "Harcama başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Harcama eklenirken bir hata oluştu.");
                return new ErrorDataResult<ExpenseDTO>(newExpense.Adapt<ExpenseDTO>(), "Harcama ekleme başarısız: " + ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingExpense = await _expenseRepository.GetByIdAsync(id);

            if (deletingExpense is null)
            {
                return new ErrorResult("Silinecek harcama bulunamadı.");
            }

            await _expenseRepository.DeleteAsync(deletingExpense);
            await _expenseRepository.SaveChangesAsync();

            // Eğer silinen harcama bir çalışana aitse ve manager bilgisi varsa
            if (deletingExpense.Manager != null && !string.IsNullOrEmpty(deletingExpense.Manager.Email) && deletingExpense.Manager.Roles == Roles.Employee)
            {
                var mailDTO = new MailDTO
                {
                    Email = deletingExpense.Manager.Email,
                    Subject = "Expense Deleted",
                    Message = "An expense request for one of your employees has been deleted."
                };
                await _mailService.SendMailAsync(mailDTO);
            }

            return new SuccessResult("Harcama başarıyla silindi.");
        }

        public async Task<IDataResult<List<ExpenseListDTO>>> GetAllAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync(x => x.CreatedDate, true);

            if (expenses.Count() <= 0)
            {
                return new ErrorDataResult<List<ExpenseListDTO>>(expenses.Adapt<List<ExpenseListDTO>>(), "Listelenecek harcama bulunamadı.");
            }

            var expenseListDTOs = new List<ExpenseListDTO>();

            foreach (var expense in expenses)
            {
                var expenseListDTO = expense.Adapt<ExpenseListDTO>();

                // Manager rol bilgisi ve diğer gerekli bilgileri DTO'ya ekleyin
                expenseListDTO.ManagerFirstName = expense.Manager.FirstName;
                expenseListDTO.ManagerLastName = expense.Manager.LastName;
                expenseListDTO.Roles = expense.Manager.Roles; // assuming `Manager` entity has a `Roles` property
                expenseListDTO.CompanyId = expense.Manager.CompanyId;
                expenseListDTOs.Add(expenseListDTO);
            }

            return new SuccessDataResult<List<ExpenseListDTO>>(expenseListDTOs, "Harcama listeleme başarılı.");
        }

        public async Task<IDataResult<ExpenseDTO>> GetByIdAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense is null)
            {
                return new ErrorDataResult<ExpenseDTO>("Gösterilecek harcama bulunamadı.");
            }
            var expenseDto = expense.Adapt<ExpenseDTO>();

            return new SuccessDataResult<ExpenseDTO>(expenseDto, "Harcama gösterme işlemi başarılı.");
        }

        public async Task<IDataResult<ExpenseDTO>> UpdateAsync(ExpenseUpdateDTO expenseUpdateDTO)
        {
            var expense = await _expenseRepository.GetByIdAsync(expenseUpdateDTO.Id);
            if (expense == null)
            {
                return new ErrorDataResult<ExpenseDTO>("Güncellenecek harcama bulunamadı.");
            }

            expense.Amount = expenseUpdateDTO.Amount;
            expense.ExpenseDate = expenseUpdateDTO.ExpenseDate;
            expense.ManagerId = expenseUpdateDTO.ManagerId;
            expense.Description = expenseUpdateDTO.Description;

            if (expenseUpdateDTO.Image != null && expenseUpdateDTO.Image.Length > 0)
            {
                expense.Image = expenseUpdateDTO.Image;
            }

           

            try
            {
                await _expenseRepository.UpdateAsync(expense);
                await _expenseRepository.SaveChangesAsync();
                return new SuccessDataResult<ExpenseDTO>(expense.Adapt<ExpenseDTO>(), "Harcama güncelleme başarılı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Harcama güncellenirken bir hata oluştu.");
                return new ErrorDataResult<ExpenseDTO>("Harcama güncelleme sırasında bir hata oluştu.");
            }
        }

      

        
    }
}
