using CMS.BUSINESS.Business.Addmission;
using CMS.BUSINESS.Business.Event;
using CMS.BUSINESS.Business.Library;
using CMS.BUSINESS.Business.Master;
using CMS.BUSINESS.Business.Master.AdminPortal;
using CMS.BUSINESS.Business.Master.ContactUs;
using CMS.BUSINESS.Business.Master.Course;
using CMS.BUSINESS.Business.Master.Exam;
using CMS.BUSINESS.Business.Master.HomePage;
using CMS.BUSINESS.Business.Notice;
using CMS.BUSINESS.Business.Teachers;
using CMS.BUSINESS.Business.User;
using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.BUSINESS.IBusiness.IEvent;
using CMS.BUSINESS.IBusiness.ILibrary;
using CMS.BUSINESS.IBusiness.IMaster;
using CMS.BUSINESS.IBusiness.IMaster.IAdminPortal;
using CMS.BUSINESS.IBusiness.IMaster.IContactUs;
using CMS.BUSINESS.IBusiness.IMaster.ICourse;
using CMS.BUSINESS.IBusiness.IMaster.IExam;
using CMS.BUSINESS.IBusiness.IMaster.IHomePage;
using CMS.BUSINESS.IBusiness.INotice;
using CMS.BUSINESS.IBusiness.ITeachers;
using CMS.REPOSITORY.IRepository.IAddmission;
using CMS.REPOSITORY.IRepository.IEvent;
using CMS.REPOSITORY.IRepository.ILibrary;
using CMS.REPOSITORY.IRepository.IMaster;
using CMS.REPOSITORY.IRepository.IMaster.AdminPortal;
using CMS.REPOSITORY.IRepository.IMaster.IContactUs;
using CMS.REPOSITORY.IRepository.IMaster.ICourse;
using CMS.REPOSITORY.IRepository.IMaster.IExam;
using CMS.REPOSITORY.IRepository.IMaster.IHomePage;
using CMS.REPOSITORY.IRepository.INotice;
using CMS.REPOSITORY.IRepository.ITeachers;
using CMS.REPOSITORY.IRepository.IUser;
using CMS.REPOSITORY.Repository.Addmission;
using CMS.REPOSITORY.Repository.Event;
using CMS.REPOSITORY.Repository.Library;
using CMS.REPOSITORY.Repository.Master;
using CMS.REPOSITORY.Repository.Master.AdminPortal;
using CMS.REPOSITORY.Repository.Master.ContactUs;
using CMS.REPOSITORY.Repository.Master.Course;
using CMS.REPOSITORY.Repository.Master.Exam;
using CMS.REPOSITORY.Repository.Master.HomePage;
using CMS.REPOSITORY.Repository.Notice;
using CMS.REPOSITORY.Repository.Teachers;
using CMS.REPOSITORY.Repository.User;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IEventBusiness, EventBusiness>();
            container.RegisterType<IEventRepository, EventRepository>();

            container.RegisterType<IAccountBusiness, AccountBusiness>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<IUserStudentsBusiness, UserStudentsBusiness>();
            container.RegisterType<IUserStudentsRepository, UserStudentsRepository>();

            container.RegisterType<INewStudentAddmissionBusiness, NewStudentAddmissionBusiness>();
            container.RegisterType<INewStudentAddmissionRepository, NewStudentAddmissionRepository>();

            container.RegisterType<IStudentFessBusiness, StudentFessBusiness>();
            container.RegisterType<IStudentFessRepository, StudentFessRepository>();

            container.RegisterType<ILibraryBusiness, LibraryBusiness>();
            container.RegisterType<ILibraryRepository, LibraryRepository>();

            container.RegisterType<IBooksControlBusiness, BooksControlBusiness>();
            container.RegisterType<IBooksControlRepository, BooksControlRepository>();

            container.RegisterType<ICourseBusiness, CourseBusiness>();
            container.RegisterType<ICourseRepository, CourseRepository>();

            container.RegisterType<INoticeBusiness, NoticeBusiness>();
            container.RegisterType<INoticeRepository, NoticeRepository>();

            container.RegisterType<ITeachersRepository, TeachersRepository>();
            container.RegisterType<ITeachersBusiness, TeachersBusiness>();

            container.RegisterType<IAdminPortalBusiness, AdminPortalBusiness>();
            container.RegisterType<IAdminPortalRepository, AdminPortalRepository>();

            container.RegisterType<IContactUsBusiness, ContactUsBusiness>();
            container.RegisterType<IContactUsRepository, ContactUsRepository>();

            container.RegisterType<IExamBusiness, ExamBusiness>();
            container.RegisterType<IExamRepository, ExamRepository>();

            container.RegisterType<IHomePageBusiness, HomePageBusiness>();
            container.RegisterType<IHomePageRepository, HomePageRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}