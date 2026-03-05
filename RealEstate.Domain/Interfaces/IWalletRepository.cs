using RealEstate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task CreateWallet(Wallet wallet);
        Task<Wallet> GetWalletById(int walletId);
        Task<Wallet> GetWalletByUserId(int userId);
        Task<IEnumerable<Wallet>> GetAllWallets();
        Task Deposit(int walletId, decimal amount);
        Task Withdraw(int walletId, decimal amount);

        // Will be used for pagination and filtering
        Task<IEnumerable<WalletTransaction>> GetAllTransactions();
        Task<IEnumerable<WalletTransaction>> GetTransactionsByWalletId(int walletId);
    }
}