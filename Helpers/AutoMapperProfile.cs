using AutoMapper;
using ViewModelTableFormation.Models;
using ViewModelTableFormation.ViewModel;

namespace ViewModelTableFormation.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Corrected the usage of ReverseMap() by adding parentheses
            CreateMap<Student, StudentDetailsModel>().ReverseMap();
            CreateMap<Student, CreateStudentModel>().ReverseMap();
            CreateMap<Student, EditStudentModel>().ReverseMap();
            CreateMap<Student, DeleteStudentModel>().ReverseMap(); 
        }
    }
}
