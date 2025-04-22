using AutoMapper;
using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.DataAccess.Concrete.EfEntityFramework;
using Fitness.Entities.Concrete;
using Fitness.Entities.Models.Product;
using FitnessManagement.Dtos;
using FitnessManagement.Entities;
using FitnessManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
	public class ProductService : IProductService
	{
		private readonly IProductDal _productDal;
		private readonly IFileService _fileService;
		private readonly IMapper _mapper;

		public ProductService(IProductDal productDal, IFileService fileService)
		{
			_productDal = productDal;
			_fileService = fileService;
		}

		public Task AddAsync(ProductCreateDto product)
		{
			throw new NotImplementedException();
		}
		

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Product>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Product> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(ProductUpdateDto product)
		{
			throw new NotImplementedException();
		}
	}
}
