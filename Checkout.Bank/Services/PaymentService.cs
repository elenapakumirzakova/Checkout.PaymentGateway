using Checkout.Bank.Data;
using Checkout.Bank.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Bank
{
    public class PaymentService : IPaymentService
    {
        private readonly MockDbContext _context;

        public PaymentService(MockDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> MakePayment(PaymentRequestDto dto)
        {
            var result = new ResponseDto();

            //validation of request data
            var paymentProvider = _context.PaymentProviders
                .SingleOrDefault(x => x.PaymentProviderUniqueToken == dto.PaymentProviderUniqueToken);
            if (paymentProvider is null)
            {
                result.Status = PaymentStatus.Failed;
                result.ResultCode = ResultCode.PaymentProviderError;
                return result;
            }

            var card = _context.Cards
                .SingleOrDefault(x => 
                    string.Format("{0} {1}", x.Client.FirstName, x.Client.LastName) == dto.CardHolderName
                    && x.CardNumber == x.CardNumber
                    && x.ExpirationDate == x.ExpirationDate
                    && x.Cvc == x.Cvc);

            if (card is null)
            {
                result.Status = PaymentStatus.Failed;
                result.ResultCode = ResultCode.CardError;
                return result;
            }
            else if (card.Balance < dto.Amount)
            {
                result.Status = PaymentStatus.Failed;
                result.ResultCode = ResultCode.CardBalanceError;
                return result;
            }
            card.Balance -= dto.Amount;

            //create and save new transaction in db
            var transaction = new Transaction
            {
                PaymentProvider = dto.PaymentProviderUniqueToken,
                TimeStamp = dto.TimeStamp,
                Amount = dto.Amount,
                PaymentProviderToken = dto.PaymentProviderUniqueToken,
                CardId = card.Id,
                Status = result.Status,
                ResultCode = result.ResultCode
            };

            _context.Transactions.Add(transaction);
            //update card data and save all changes in db
            await _context.MockSaveChangesAsync();

            //if all request data is valid, take paymeny amount from card's balance 
            result.Status = PaymentStatus.Paid;
            result.ResultCode = ResultCode.Success;
            result.OperationId = Guid.NewGuid();//Mock transactionId

            return result;
        }
    }
}
