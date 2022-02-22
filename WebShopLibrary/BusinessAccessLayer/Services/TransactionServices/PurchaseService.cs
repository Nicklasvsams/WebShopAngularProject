using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.PurchaseDTO;
using WebShopLibrary.DataAccessLayer.Database.Entities.Transactions;
using WebShopLibrary.DataAccessLayer.Repositories.TransactionRepositories;

namespace WebShopLibrary.BusinessAccessLayer.Services.TransactionServices
{
    public interface IPurchaseService
    {
        Task<List<PurchaseResponse>> GetAllPurchases();
        Task<PurchaseResponse> GetPurchaseById(int purchaseId);
        Task<PurchaseResponse> DeletePurchase(int purchaseId);
        Task<PurchaseResponse> UpdatePurchase(int purchaseId, PurchaseRequest purchaseUpdate);
        Task<PurchaseResponse> CreatePurchase(PurchaseRequest purchase);
    }

    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<PurchaseResponse> CreatePurchase(PurchaseRequest purchase)
        {
            Purchase createdPurchase = await _purchaseRepository.InsertNewPurchase(MapPurchaseRequestToPurchase(purchase));

            if (createdPurchase != null)
            {
                return MapPurchaseToPurchaseResponse(createdPurchase);
            }

            return null;
        }

        public async Task<PurchaseResponse> DeletePurchase(int purchaseId)
        {
            Purchase purchase = await _purchaseRepository.DeletePurchaseById(purchaseId);

            if (purchase != null)
            {
                return MapPurchaseToPurchaseResponse(purchase);
            }

            return null;
        }

        public async Task<List<PurchaseResponse>> GetAllPurchases()
        {
            List<Purchase> purchases = await _purchaseRepository.SelectAllPurchases();

            return purchases.Select(x => MapPurchaseToPurchaseResponse(x)).ToList();
        }

        public async Task<PurchaseResponse> GetPurchaseById(int purchaseId)
        {
            Purchase purchase = await _purchaseRepository.SelectPurchaseById(purchaseId);

            if (purchase != null)
            {
                return MapPurchaseToPurchaseResponse(purchase);
            }

            return null;
        }

        public async Task<PurchaseResponse> UpdatePurchase(int purchaseId, PurchaseRequest purchaseUpdate)
        {
            Purchase purchaseToUpdate = await _purchaseRepository.UpdateExistingPurchaseById(purchaseId, MapPurchaseRequestToPurchase(purchaseUpdate));

            if (purchaseToUpdate != null)
            {
                return MapPurchaseToPurchaseResponse(purchaseToUpdate);
            }

            return null;
        }

        private Purchase MapPurchaseRequestToPurchase(PurchaseRequest purReq)
        {
            return new Purchase
            {
                ProductId = purReq.ProductId,
                PurchaseDate = purReq.PurchaseDate,
                UserId = purReq.UserId
            };
        }

        private PurchaseResponse MapPurchaseToPurchaseResponse(Purchase purchase)
        {
            PurchaseResponse purRes =  new PurchaseResponse
            {
                Id = purchase.Id,
                PurchaseDate = purchase.PurchaseDate,
                UserId = purchase.UserId,
                ProductId = purchase.ProductId
            };

            if (purchase.Product != null && purchase.User != null)
            {
                purRes.Product = new PurchaseProductResponse
                {
                    Id = purchase.Product.Id,
                    Name = purchase.Product.Name,
                    Description = purchase.Product.Description
                };

                purRes.User = new PurchaseUserResponse
                {
                    Id = purchase.User.Id,
                    Username = purchase.User.Username,
                    Password = purchase.User.Password,
                    Email = purchase.User.Email,
                    UserType = purchase.User.UserType
                };
            }

            return purRes;
        }
    }
}
