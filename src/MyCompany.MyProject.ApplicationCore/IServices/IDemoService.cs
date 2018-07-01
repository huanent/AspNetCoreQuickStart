using MyCompany.MyProject.ApplicationCore.Dtos.Demo;

namespace MyCompany.MyProject.ApplicationCore.IServices
{
    public interface IDemoService
    {
        System.Threading.Tasks.Task CreateDemoAsync(AddDemoDto dto);
        void UpdateDemo(EditDemoDto dto);
    }
}
