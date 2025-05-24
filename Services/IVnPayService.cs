
using JDshop.Models.ViewModel.ViewModel;

namespace JDshop.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModels PaymentExecute(IQueryCollection collections);
    }
}
