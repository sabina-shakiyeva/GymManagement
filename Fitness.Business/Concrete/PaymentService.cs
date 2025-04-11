using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using FitnessManagement.Dtos;
using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;
        private readonly IUserDal _userDal;
        private readonly IPackageDal _packageDal;

        public PaymentService(IPaymentDal paymentDal, IUserDal userDal, IPackageDal packageDal)
        {
            _paymentDal = paymentDal;
            _userDal = userDal;
            _packageDal = packageDal;
        }
        public async Task<string> PurchasePackageAsync(int userId, int packageId, PaymentDto paymentDto)
        {
            var user = await _userDal.Get(u => u.Id == userId);
            if (user == null)
                throw new Exception("İstifadəçi tapılmadı.");

            // Mövcud paket müddəti bitibsə, aktivliyi false et
            if (user.IsActive && user.PackageEndDate.HasValue && user.PackageEndDate.Value < DateTime.Now)
            {
                user.IsActive = false;
                user.PackageId = null;
                user.PackageStartDate = null;
                user.PackageEndDate = null;
                await _userDal.Update(user);
            }

            // Hələ də aktivdirsə, yeni paket ala bilməz
            if (user.IsActive)
                return "İstifadəçinin artıq aktiv paketi var.";

            var package = await _packageDal.Get(p => p.Id == packageId);
            if (package == null)
                throw new Exception("Paket tapılmadı.");

            // Fake ödəniş yoxlaması (simulyasiya)
            if (paymentDto.CardNumber.StartsWith("1111"))
                throw new Exception("Ödəniş kartı etibarsızdır.");

            // Ödəniş qeydi
            var payment = new Payment
            {
                UserId = userId,
                PackageId = packageId,
                Amount = package.Price,
                PaymentDate = DateTime.Now
            };
            await _paymentDal.Add(payment);

            // Yeni paket təyin və aktiv et
            user.PackageId = packageId;
            user.IsActive = true;
            user.PackageStartDate = DateTime.Now;
            user.PackageEndDate = DateTime.Now.AddMonths(package.DurationInMonths); // Ayla hesabladıq!
            await _userDal.Update(user);

            return "Paket uğurla alındı və aktivləşdirildi.";
        }




    }
}

