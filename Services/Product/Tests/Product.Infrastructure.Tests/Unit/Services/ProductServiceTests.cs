using AutoMapper;
using FileActor.Abstract;
using NUnit.Framework;
using Product.Application.Configurations;
using Product.Application.Exceptions;
using Product.Application.Repositories;
using Product.Application.UnitOfWork;
using Product.Domain.Entities;
using Product.Domain.ValueObjects;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Services;
using Product.Infrastructure.Tests.Utils.HelperTypes;
using Services.Shared.AppUtils;
using System;
using System.Linq;
using static Product.Infrastructure.Tests.Utils.TestUtilities;
namespace Product.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class ProductServiceTests
	{
		//PersistentShared
		private ProductService _service;
		private IProductRepo _repo;
		private IFileServiceActor _fileService;
		private IUnitOfWork _unitOfWork;
		private ApplicationDbContext _db;

		[SetUp]
		public void Setup()
		{
			_db = CreateDbContext("test-db");
			var mapper = CreateMapper(new TestMapperProfile(), new ServiceMapper(), new PersistMapperProfile(CreateCdnResolver()));
			_unitOfWork = new UnitOfWork(_db, mapper);
			_repo = _unitOfWork.ProductRepo;
			_fileService = CreateFileServiceActor();
			_service = new ProductService(_unitOfWork, mapper, _fileService);
		}
		[TearDown]
		public void TearDown()
		{
			_db.RemoveRange(_db.Products.ToList());
			_db.RemoveRange(_db.ProductCategories.ToList());
			_db.SaveChanges();
			_db.Dispose();
		}

		#region CommandMethodsTests

		[Test]
		public void Add_PassNull_ThrowException()
		{
			//Arrange
			//Act & Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				_service.Add(null);
			});
		}

		[Test]
		public void Add_PassEmptyObject_ThrowException()
		{
			//Arrange
			var productDTO = new HelperProductDTO();
			//Act & Assert
			Assert.Throws<InsertOperationException>(() =>
			{
				_service.Add(productDTO);
			});
		}
		[Test]
		public void Add_PassInvalidObject_ThrowException()
		{
			//Arrange
			var category = CreateProductCategory();
			var productDTO = new HelperProductDTO()
			{
				CategoryId = category.Id,
			};
			//Act & Assert
			Assert.Throws<InsertOperationException>(() =>
			{
				_service.Add(productDTO);
			});

			Assert.IsFalse(_repo.Exists(i => i.CategoryId == productDTO.CategoryId));
		}

		[Test]
		public void Add_PassValidObject_InsertTheObject()
		{
			//Arrange
			var category = CreateProductCategory();
			var productDTO = new HelperProductDTO()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImagePath = "/testDirectory\\testfolder",
				Name = "test product",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};

			//Act
			_service.Add(productDTO);

			//Assert
			var insertedObj = _repo.Get(productDTO.Id);
			Assert.IsNotNull(insertedObj);
			Assert.AreEqual(insertedObj.Name, productDTO.Name);
			Assert.AreEqual(insertedObj.MainImage.ToString(), new Blob("", productDTO.MainImagePath, "").ToString());
			Assert.AreEqual(insertedObj.CategoryId, productDTO.CategoryId);
			Assert.AreEqual(insertedObj.CreatedDateTime, productDTO.CreatedDateTime);
			Assert.AreEqual(insertedObj.Description, productDTO.Description);
			Assert.AreEqual(insertedObj.ShortDesc, productDTO.ShortDesc);
		}

		[Test]
		public void Update_PassNull_ThrowException()
		{
			//Act & Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				_service.Update(null);
			});
		}

		[Test]
		public void Update_PassEmptyObject_ThrowException()
		{
			//Arrange
			var productDTO = new HelperProductDTO();
			//Act & Assert
			Assert.Throws<UpdateOperationException>(() =>
			{
				_service.Update(productDTO);
			});
		}

		[Test]
		public void Update_PassInvalidObject_ThrowException()
		{
			//Arrange
			var category = CreateProductCategory();
			var product = new Domain.Entities.Product()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImage = new Blob("", "/testDirectory\\testfolder", ""),
				Name = $"product1",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};
			_repo.Add(product);
			_db.SaveChanges();
			//Act
			Assert.Throws<UpdateOperationException>(() =>
			{
				_service.Update(new HelperProductDTO()
				{
					Id = product.Id,
					Name = "product2"
				});
			});
			//Assert
			var actualProduct = _repo.Get(product.Id);
			Assert.AreEqual(actualProduct.Name, product.Name);
		}

		[Test]
		public void Update_PassValidObject_UpdateEntity()
		{
			//Arrange
			var category = CreateProductCategory();
			var product = new Domain.Entities.Product()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImage = new Blob("", "/testDirectory\\testfolder", ""),
				Name = $"product1",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};
			_repo.Add(product);

			//Act
			var productDTO = new HelperProductDTO()
			{
				Id = product.Id,
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				Name = $"product1",
				ShortDesc = "no short description",
				UnitPrice = 349485,
				MainImagePath = product.MainImage.ToString()
			};
			_service.Update(productDTO);
			//Assert
			var actualProduct = _repo.Get(product.Id);

			Assert.IsNotNull(actualProduct);
			Assert.AreEqual(actualProduct.Name, productDTO.Name);
			Assert.AreEqual(actualProduct.MainImage.ToString(), new Blob("", productDTO.MainImagePath, "").ToString());
			Assert.AreEqual(actualProduct.CategoryId, productDTO.CategoryId);
			Assert.AreEqual(actualProduct.CreatedDateTime, productDTO.CreatedDateTime);
			Assert.AreEqual(actualProduct.Description, productDTO.Description);
			Assert.AreEqual(actualProduct.ShortDesc, productDTO.ShortDesc);
		}
		#endregion

		#region QueryMethodsTests

		[Test]
		public void GetAll_PassNull_ThrowException()
		{
			//Act & Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				_service.GetAll(null);
			});
		}

		[Test]
		public void GetAll_PassNoMatchingExpression_ReturnEmptyList()
		{
			//Arrange
			var category = CreateProductCategory();
			foreach (var item in Enumerable.Range(1, 3))
			{
				var product = new Domain.Entities.Product()
				{
					CategoryId = category.Id,
					CreatedDateTime = DateTime.Now,
					Description = "no description",
					MainImage = new Blob("", "/testDirectory\\testfolder", ""),
					Name = $"product{item}",
					ShortDesc = "no short description",
					UnitPrice = 349485
				};
				_repo.Add(product);
			}
			//Act
			var result = _service.GetAll(i => i.Name == "no matching name");
			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(result.Count(), 0);
		}

		[Test]
		public void GetAll_PassMatchingExpression_ReturnMatchingEntities()
		{
			//Arrange
			var category = CreateProductCategory();
			foreach (var item in Enumerable.Range(1, 4))
			{
				var product = new Domain.Entities.Product()
				{
					CategoryId = category.Id,
					CreatedDateTime = DateTime.Now,
					Description = "no description",
					MainImage = new Blob("", "/testDirectory\\testfolder", ""),
					Name = $"test product{item}",
					ShortDesc = "no short description",
					UnitPrice = item * 10
				};
				_repo.Add(product);
			}
			//Act
			var result = _service.GetAll(i => i.UnitPrice < 40 && i.UnitPrice >= 20);
			//Assert
			var actualResult = _repo.Get(new QueryParams<Domain.Entities.Product>()
			{
				Expression = i => i.UnitPrice < 40 && i.UnitPrice >= 20
			});
			CollectionAssert.IsNotEmpty(result);
			CollectionAssert.AreEquivalent(result.Select(i => i.Id), actualResult.Select(i => i.Id));
		}

		[Test]
		public void GetAll_ReturnAllAvailableEntities()
		{
			//Arrange
			var category = CreateProductCategory();
			var product = new Domain.Entities.Product()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImage = new Blob("", "/testDirectory\\testfolder", ""),
				Name = "test product",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};
			_repo.Add(product);
			//Act
			var result = _service.GetAll();
			//Assert
			Assert.AreEqual(result.Count(), 1);
			var singleRes = result.Single();
			Assert.AreEqual(singleRes.Name, product.Name);
			Assert.AreEqual(singleRes.MainImage.ToString(), product.MainImage.ToString());
			Assert.AreEqual(singleRes.CategoryId, product.CategoryId);
			Assert.AreEqual(singleRes.CreatedDateTime, product.CreatedDateTime);
			Assert.AreEqual(singleRes.Description, product.Description);
			Assert.AreEqual(singleRes.ShortDesc, product.ShortDesc);
		}

		[Test]
		public void GetById_PassNull_ThrowException()
		{
			//Act & Assert
			Assert.Throws<ArgumentNullException>(() =>
				{
					_service.GetById(null);
				});
		}

		[Test]
		public void GetById_PassFakeIdWithValidType_ReturnNull()
		{
			//Arrange
			var fakeId = Guid.NewGuid().ToString();
			//Act
			var result = _service.GetById(fakeId);
			// Assert
			Assert.AreEqual(null, result);
		}

		[TestCase(837320)]
		[TestCase(true)]
		[TestCase(3.452)]
		public void GetById_PassFakeIdWithInvalidType_ThrowException(object fakeId)
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_service.GetById(fakeId);
			});
		}

		[Test]
		public void GetById_PassValidId_ReturnEntity()
		{
			//Arrange
			var category = CreateProductCategory();
			var product = new Domain.Entities.Product()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImage = new Blob("", "/testDirectory\\testfolder", ""),
				Name = "test product",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};
			_repo.Add(product);
			//Act
			var result = _service.GetById(product.Id);
			//Assert
			Assert.AreEqual(result.Name, product.Name);
			Assert.AreEqual(result.MainImage.ToString(), product.MainImage.ToString());
			Assert.AreEqual(result.CategoryId, product.CategoryId);
			Assert.AreEqual(result.CreatedDateTime, product.CreatedDateTime);
			Assert.AreEqual(result.Description, product.Description);
			Assert.AreEqual(result.ShortDesc, product.ShortDesc);
		}

		[Test]
		public void Exists_PassNull_ThrowException()
		{
			//Act & Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				_service.Exists(null);
			});
		}

		[Test]
		public void Exists_PassNoMatchingExpression_ReturnFalse()
		{
			//Arrange
			var category = CreateProductCategory();
			var product = new Domain.Entities.Product()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImage = new Blob("", "/testDirectory\\testfolder", ""),
				Name = "test product",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};
			_repo.Add(product);
			//Act
			var result = _service.Exists(i => i.Name == "no matching name");
			//Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void Exists_PassMatchingExpression_ReturnTrue()
		{
			//Arrange
			var category = CreateProductCategory();
			var product = new Domain.Entities.Product()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImage = new Blob("", "/testDirectory\\testfolder", ""),
				Name = "test product",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};
			_repo.Add(product);
			//Act
			var result = _service.Exists(i => i.Name == "test product");
			//Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void FirstOrDefault_EmptyDataBase_ReturnNull()
		{
			//Act
			var res = _service.FirstOrDefault();
			Assert.IsNull(res);
		}

		[Test]
		public void FirstOrDefault_DataBaseWithOneRecord_ReturnEntity()
		{
			//Arrange
			var category = CreateProductCategory();
			var product = new Domain.Entities.Product()
			{
				CategoryId = category.Id,
				CreatedDateTime = DateTime.Now,
				Description = "no description",
				MainImage = new Blob("", "/testDirectory\\testfolder", ""),
				Name = "test product",
				ShortDesc = "no short description",
				UnitPrice = 349485
			};
			_repo.Add(product);

			//Act
			var result = _service.FirstOrDefault();

			//Assert
			Assert.AreEqual(result.Name, product.Name);
			Assert.AreEqual(result.MainImage.ToString(), product.MainImage.ToString());
			Assert.AreEqual(result.CategoryId, product.CategoryId);
			Assert.AreEqual(result.CreatedDateTime, product.CreatedDateTime);
			Assert.AreEqual(result.Description, product.Description);
			Assert.AreEqual(result.ShortDesc, product.ShortDesc);
		}

		[Test]
		public void FirstOrDefault_DataBaseWithMoreThanOneRecord_ReturnFirstEntity()
		{
			//Arrange
			var category = CreateProductCategory();
			foreach (var item in Enumerable.Range(1, 3))
			{
				var product = new Domain.Entities.Product()
				{
					CategoryId = category.Id,
					CreatedDateTime = DateTime.Now,
					Description = "no description",
					MainImage = new Blob("", "/testDirectory\\testfolder", ""),
					Name = $"test product{item}",
					ShortDesc = "no short description",
					UnitPrice = 349485
				};
				_repo.Add(product);
			}

			//Act
			var result = _service.FirstOrDefault();

			//Assert
			var firstRecord = _repo.Get().First();
			Assert.AreEqual(result.Name, firstRecord.Name);
			Assert.AreEqual(result.MainImage.ToString(), firstRecord.MainImage.ToString());
			Assert.AreEqual(result.CategoryId, firstRecord.CategoryId);
			Assert.AreEqual(result.CreatedDateTime, firstRecord.CreatedDateTime);
			Assert.AreEqual(result.Description, firstRecord.Description);
			Assert.AreEqual(result.ShortDesc, firstRecord.ShortDesc);
		}

		#endregion

		#region HelperMethods
		private ProductCategory CreateProductCategory()
		{
			var category = new ProductCategory()
			{
				Name = "testCategory",
				IsActive = true,
			};
			_unitOfWork.ProductCategoryRepo.Add(category);
			return category;
		}
		#endregion
	}

	public class TestMapperProfile : Profile
	{
		public TestMapperProfile()
		{
			CreateMap<HelperProductDTO, Domain.Entities.Product>().ForMember(d => d.MainImage, opt => opt.MapFrom(s => new Blob("", s.MainImagePath, "")))
				.ReverseMap();
		}
	}
}
