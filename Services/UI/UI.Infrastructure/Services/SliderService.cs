﻿using FileActor.Abstract;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class SliderService : GenericActiveService<string,Slider, SliderDTO>, ISliderService
	{
		private readonly IFileServiceActor _fileServiceProvider;
		public SliderService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileServiceActor fileServiceProvider) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
		}
		public override void Add(SliderDTO entityDTO)
		{
			_fileServiceProvider.SaveAll(entityDTO);
			base.Add(entityDTO);
		}
		public override void Update(SliderDTO entityDTO)
		{
			var entity = _repo.Get(entityDTO.Id);
			_fileServiceProvider.ReplaceAll(entityDTO);
			_mapper.Map(entityDTO, entity);
			_repo.Update(entity);
		}
		public override void Delete(object id)
		{
			var obj = _repo.Get(id);
			if (obj != null)
			{
				_fileServiceProvider.DeleteAll(obj);
				_repo.Delete(id);
			}
		}
	}
}
