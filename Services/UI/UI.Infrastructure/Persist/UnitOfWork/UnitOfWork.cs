using MongoDB.Driver;
using UI.Application.Repositories;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Infrastructure.Repositories;

namespace UI.Infrastructure.Persist
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		private readonly ICustomMapper _mapper;

		public UnitOfWork(ApplicationDbContext db, ICustomMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}
		private ISliderRepo _sliderRepo;
		public ISliderRepo SliderRepo
		{
			get
			{
				_sliderRepo ??= new SliderRepo(_db, _mapper);
				return _sliderRepo;
			}
		}

		private IFaqRepo _faqRepo;
		public IFaqRepo FaqRepo
		{
			get
			{
				_faqRepo ??= new FaqRepo(_db, _mapper);
				return _faqRepo;
			}
		}

		private IFaqCategoryRepo _faqCategoryRepo;
		public IFaqCategoryRepo FaqCategoryRepo
		{
			get
			{
				_faqCategoryRepo ??= new FaqCategoryRepo(_db, _mapper);
				return _faqCategoryRepo;
			}
		}

		private ISocialMediaRepo _socialMediaRepo;
		public ISocialMediaRepo SocialMediaRepo
		{
			get
			{
				_socialMediaRepo ??= new SocialMediaRepo(_db, _mapper);
				return _socialMediaRepo;
			}
		}

		private IAboutUsRepo _aboutUsRepo;
		public IAboutUsRepo AboutUsRepo
		{
			get
			{
				_aboutUsRepo ??= new AboutUsRepo(_db, _mapper);
				return _aboutUsRepo;
			}
		}

		private IContactUsRepo _contactUsRepo;
		public IContactUsRepo ContactUsRepo
		{
			get
			{
				_contactUsRepo ??= new ContactUsRepo(_db, _mapper);
				return _contactUsRepo;
			}
		}

		public void Dispose()
		{
			_db.Dispose();
		}

		public ValueTask DisposeAsync()
		{
			return _db.DisposeAsync();
		}

		public IRepository<T> GetRepo<T>()
			where T : class
		{
			var props = GetType().GetProperties();
			var res = props.FirstOrDefault(i => i.PropertyType.GetInterfaces().Contains(typeof(IRepository<T>)))?.GetValue(this, null);
			return res as IRepository<T>;
		}
	}
}
