using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext _context;

        public WalletRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateWallet(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<Wallet> GetWalletById(int walletId)
        {
            return await _context.Wallets.FindAsync(walletId);
        }

        public async Task<Wallet> GetWalletByUserId(int userId)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<IEnumerable<Wallet>> GetAllWallets()
        {
            return await _context.Wallets.ToListAsync();
        }

        public async Task Deposit(int walletId, decimal amount)
        {
            var wallet = await _context.Wallets.FindAsync(walletId);
            wallet.Balance += amount;

            var transaction = new WalletTransaction
            {
                WalletId = walletId,
                Amount = amount,
                Type = "Deposit",
                Timestamp = DateTime.UtcNow
            };

            _context.Wallets.Update(wallet);
            await _context.WalletTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task Withdraw(int walletId, decimal amount)
        {
            var wallet = await _context.Wallets.FindAsync(walletId);

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

            _context.Wallets.Update(wallet);
            await _context.WalletTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WalletTransaction>> GetAllTransactions()
        {
            return await _context.WalletTransactions.ToListAsync();
        }

        public async Task<IEnumerable<WalletTransaction>> GetTransactionsByWalletId(int walletId)
        {
            return await _context.WalletTransactions
                .Where(w => w.WalletId == walletId)
                .ToListAsync();
        }
    }
}