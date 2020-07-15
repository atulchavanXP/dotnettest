using AutoMapper;
using TestApp_Domain.Utilities;
using domain = TestApp_Domain.Models;
using repo = TestApp_Repository.Entities;

namespace TestApp_Domain.Mappings
{
    public class StudentMapping : Profile
    {
        public StudentMapping()
        {
            CreateMap<repo.Student, domain.Student>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()); //Ignore password property from repo

            CreateMap<domain.Student, repo.Student>()
                .ForMember(
                dest => dest.Password,
                opt => opt.MapFrom(src => EncryptionandDecryption.Encrypt(src.Password))); //encrypt password property while saving to database
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

    public static class StudentMappingExtensions
    {
        /// <summary>
        /// map repo model to domain
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static domain.Student ToDomain(this repo.Student source)
        {
            return Mapper.Map<domain.Student>(source);
        }

        /// <summary>
        /// map domain model to repo
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static repo.Student ToRepo(this domain.Student source)
        {
            return Mapper.Map<repo.Student>(source);
        }
    }
}
