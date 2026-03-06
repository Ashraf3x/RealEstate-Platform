using RealEstate.Domain.Entities;
using System.Collections.Generic;

namespace RealEstate.Domain.Interfaces
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        Wallet GetWalletByUserId(int userId);
        void Deposit(int walletId, decimal amount);
        void Withdraw(int walletId, decimal amount);

        // Will be used for pagination and filtering
        List<WalletTransaction> GetAllTransactions();
        List<WalletTransaction> GetTransactionsByWalletId(int walletId);
    }
}