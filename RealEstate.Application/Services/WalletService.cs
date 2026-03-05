using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Application.Services
{
    public class WalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task CreateWallet(int userId)
        {
            var wallet = new Wallet
            {
                UserId = userId,
                Balance = 0,
                CreatedAt = DateTime.UtcNow
            };

            await _walletRepository.CreateWallet(wallet);
        }

        public async Task<Wallet> GetWalletByUserId(int userId)
        {
            return await _walletRepository.GetWalletByUserId(userId);
        }

        public async Task<Wallet> GetWalletById(int walletId)
        {
            return await _walletRepository.GetWalletById(walletId);
        }

        public async Task<IEnumerable<Wallet>> GetAllWallets()
        {
            return await _walletRepository.GetAllWallets();
        }

        public async Task Deposit(int walletId, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Deposit amount must be greater than zero");

            await _walletRepository.Deposit(walletId, amount);
        }

        public async Task Withdraw(int walletId, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Withdrawal amount must be greater than zero");

            await _walletRepository.Withdraw(walletId, amount);
        }

        public async Task<IEnumerable<WalletTransaction>> GetTransactionsByWalletId(int walletId)
        {
            return await _walletRepository.GetTransactionsByWalletId(walletId);
        }

        public async Task<IEnumerable<WalletTransaction>> GetAllTransactions()
        {
            return await _walletRepository.GetAllTransactions();
        }
    }
}
