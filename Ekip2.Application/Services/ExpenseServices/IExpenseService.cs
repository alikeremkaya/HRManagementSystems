using Ekip2.Application.DTOs.ExpenseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.ExpenseServices
{
    public interface IExpenseService
    {

        Task<IDataResult<List<ExpenseListDTO>>> GetAllAsync();
        Task<IDataResult<ExpenseDTO>> CreateAsync(ExpenseCreateDTO expenseCreateDTO);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<ExpenseDTO>> GetByIdAsync(Guid id);
        Task<IDataResult<ExpenseDTO>> UpdateAsync(ExpenseUpdateDTO expenseUpdateDTO);
      
    }
}
