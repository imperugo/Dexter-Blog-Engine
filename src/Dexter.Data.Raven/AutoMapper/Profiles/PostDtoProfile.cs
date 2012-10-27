namespace Dexter.Data.Raven.AutoMapper.Profiles
{
	using Dexter.Data.DataTransferObjects;

	using global::AutoMapper;

	using Dexter.Domain;

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