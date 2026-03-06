using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstate.Infrastructure.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        AppDbContext context;

        public WalletRepository(AppDbContext con) : base(con)
        {
            context = con;
        }

        public Wallet GetWalletByUserId(int userId)
        {
            return context.Wallets
                .FirstOrDefault(w => w.UserId == userId);
        }

        public void Deposit(int walletId, decimal amount)
        {
            var wallet = context.Wallets.Find(walletId);
            if (wallet == null)
                throw new Exception("Wallet not found");
            wallet.Balance += amount;
            var transaction = new WalletTransaction
            {
                WalletId = walletId,
                Amount = amount,
                Type = "Deposit",
                Timestamp = DateTime.UtcNow
            };
            context.Wallets.Update(wallet);
            context.WalletTransactions.Add(transaction);
            context.SaveChanges();
        }

        public void Withdraw(int walletId, decimal amount)
        {
            var wallet = context.Wallets.Find(walletId);
            if (wallet == null)
                throw new Exception("Wallet not found");
            if (wallet.Balance < amount)
                throw new Exception("Insufficient Balance");
            wallet.Balance -= amount;
            var transaction = new WalletTransaction
            {
                WalletId = walletId,
                Amount = amount,
                Type = "Withdrawal",
                Timestamp = DateTime.UtcNow
            };
            context.Wallets.Update(wallet);
            context.WalletTransactions.Add(transaction);
            context.SaveChanges();
        }

        public List<WalletTransaction> GetAllTransactions()
        {
            return context.WalletTransactions.ToList();
        }

        public List<WalletTransaction> GetTransactionsByWalletId(int walletId)
        {
            return context.WalletTransactions
                .Where(w => w.WalletId == walletId)
                .ToList();
        }
    }
}