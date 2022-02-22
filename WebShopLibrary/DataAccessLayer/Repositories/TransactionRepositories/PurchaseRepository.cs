using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Transactions;

namespace WebShopLibrary.DataAccessLayer.Repositories.TransactionRepositories
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> SelectAllPurchases();
        Task<Purchase> SelectPurchaseById(int purchaseId);
        Task<Purchase> InsertNewPurchase(Purchase purchase);
        Task<Purchase> DeletePurchaseById(int purchaseId);
        Task<Purchase> UpdateExistingPurchaseById(int purchaseId, Purchase purchaseUpdate);
    }

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly WebShopDBContext _dbContext;

        public PurchaseRepository(WebShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Purchase> DeletePurchaseById(int purchaseId)
        {
            Purchase purchaseToDelete = await _dbContext.Purchase.FirstOrDefaultAsync(x => x.Id == purchaseId);

            if (purchaseToDelete != null)
            {
                _dbContext.Remove(purchaseToDelete);

                await _dbContext.SaveChangesAsync();
            }

            return purchaseToDelete;
        }

        public async Task<Purchase> InsertNewPurchase(Purchase purchase)
        {
            await _dbContext.Purchase.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();

            return purchase;
        }

        public async Task<List<Purchase>> SelectAllPurchases()
        {
            return await _dbContext.Purchase
                .Include(p => p.Product)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task<Purchase> SelectPurchaseById(int purchaseId)
        {
            return await _dbContext.Purchase
                .Include(p => p.Product)
                .Include(u => u.User)
                .FirstOrDefaultAsync(x => x.Id == purchaseId);
        }

        public async Task<Purchase> UpdateExistingPurchaseById(int purchaseId, Purchase purchaseUpdate)
        {
            Purchase purchaseToUpdate = await _dbContext.Purchase
                .FirstOrDefaultAsync(x => x.Id == purchaseId);

            if (purchaseToUpdate != null)
            {
                purchaseToUpdate.ProductId = purchaseUpdate.ProductId;
                purchaseToUpdate.PurchaseDate = purchaseUpdate.PurchaseDate;
                purchaseToUpdate.UserId = purchaseUpdate.UserId;

                await _dbContext.SaveChangesAsync();
            }

            return purchaseToUpdate;
        }
    }
}
