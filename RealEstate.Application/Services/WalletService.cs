using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace RealEstate.Application.Services
{
    public class WalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public List<Wallet> GetAll()
        {
            return _walletRepository.GetAll();
        }

        public Wallet GetById(int id)
        {
            return _walletRepository.GetById(id);
        }

        public void CreateWallet(int userId)
        {
            var wallet = new Wallet
            {
                UserId = userId,
                Balance = 0,
                CreatedAt = DateTime.UtcNow
            };
            _walletRepository.Add(wallet);
        }

        public Wallet GetWalletByUserId(int userId)
        {
            return _walletRepository.GetWalletByUserId(userId);
        }
        public void Deposit(int walletId, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Deposit amount must be greater than zero");
            _walletRepository.Deposit(walletId, amount);
        }
        public void Withdraw(int walletId, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Withdrawal amount must be greater than zero");
            _walletRepository.Withdraw(walletId, amount);
        }
        public List<WalletTransaction> GetAllTransactions()
        {
            return _walletRepository.GetAllTransactions();
        }
        public List<WalletTransaction> GetTransactionsByWalletId(int walletId)
        {
            return _walletRepository.GetTransactionsByWalletId(walletId);
        }
    }
}