namespace Dexter.Data.Raven.AutoMapper.Profiles
{
	using Dexter.Data.Raven.Domain;

	using global::AutoMapper;

	using Dexter.Data.DataTransferObjects;

	public class PostDtoProfile : Profile
	{
		#region Methods

		protected override void Configure()
		{
			Mapper.CreateMap<Post, PostDto>();
		}

		#endregion
	}
}