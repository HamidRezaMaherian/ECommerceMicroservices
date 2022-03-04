﻿using AutoMapper;
using Discount.Application.Repositories;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.DAOs;

namespace Discount.Infrastructure.Repositories
{
	public class PriceDiscountRepo : Repository<PriceDiscount, PriceDiscountDAO>, IPriceDiscountRepo
	{
		public PriceDiscountRepo(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
		{
		}
	}
}